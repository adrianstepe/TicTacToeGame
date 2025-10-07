using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class MainForm : Form
    {
        private GameBoard gameBoard;
        private GameLogic gameLogic;
        private Player playerX;
        private Player playerO;
        private Player currentPlayer;
        private Button[,] buttons;
        private Label statusLabel;
        private Label scoreLabel;
        private Label modeLabel;
        private Button newGameButton;
        private Button exitButton;
        private bool gameEnded;
        private int currentBoardSize;
        private bool isVsComputer;
        private DifficultyLevel difficulty;
        private ComputerAI computerAI;

        public MainForm()
        {
            InitializeUI();
            ShowInitialSelections();
        }

        private void InitializeUI()
        {
            this.Text = "Tic-Tac-Toe";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            playerX = new Player("X");
            playerO = new Player("O");
        }

        private void ShowInitialSelections()
        {
            // 1. Izvēlies spēlētāju skaitu
            PlayerModeSelectionForm playerModeForm = new PlayerModeSelectionForm();
            if (playerModeForm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            isVsComputer = playerModeForm.IsVsComputer;

            // 2. Ja pret datoru, izvēlies grūtību
            if (isVsComputer)
            {
                DifficultySelectionForm difficultyForm = new DifficultySelectionForm();
                if (difficultyForm.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
                difficulty = difficultyForm.SelectedDifficulty;
                playerO = new Player("O", true);
            }

            // 3. Izvēlies laukuma izmēru
            GameModeSelectionForm modeForm = new GameModeSelectionForm();
            if (modeForm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            currentBoardSize = modeForm.SelectedSize;

            InitializeGame();
        }

        private void InitializeGame()
        {
            this.Controls.Clear();

            gameBoard = new GameBoard(currentBoardSize);
            gameLogic = new GameLogic(gameBoard);
            currentPlayer = playerX;
            gameEnded = false;

            if (isVsComputer)
            {
                computerAI = new ComputerAI(gameBoard, gameLogic, difficulty, playerO.Symbol, playerX.Symbol);
            }

            int buttonSize = CalculateButtonSize();
            int formWidth = currentBoardSize * buttonSize + 100;
            int formHeight = currentBoardSize * buttonSize + 250;
            this.Size = new Size(formWidth, formHeight);

            CreateUIElements(buttonSize);
            ResetBoard();
            UpdateStatus();
            UpdateScore();
        }

        private int CalculateButtonSize()
        {
            switch (currentBoardSize)
            {
                case 3: return 100;
                case 4: return 85;
                case 5: return 70;
                default: return 80;
            }
        }

        private void CreateUIElements(int buttonSize)
        {
            int startX = 50;
            int startY = 120;

            modeLabel = new Label();
            modeLabel.Size = new Size(350, 25);
            modeLabel.Location = new Point(startX, 20);
            modeLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            modeLabel.TextAlign = ContentAlignment.MiddleCenter;
            string gameMode = isVsComputer ? $"Pret Datoru ({GetDifficultyText()})" : "Divi Spēlētāji";
            modeLabel.Text = $"Režīms: {currentBoardSize}x{currentBoardSize} | {gameMode}";
            this.Controls.Add(modeLabel);

            statusLabel = new Label();
            statusLabel.Size = new Size(350, 30);
            statusLabel.Location = new Point(startX, 50);
            statusLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(statusLabel);

            scoreLabel = new Label();
            scoreLabel.Size = new Size(350, 30);
            scoreLabel.Location = new Point(startX, 90);
            scoreLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            scoreLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(scoreLabel);

            buttons = new Button[currentBoardSize, currentBoardSize];
            int fontSize = currentBoardSize == 3 ? 36 : (currentBoardSize == 4 ? 28 : 22);

            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(buttonSize, buttonSize);
                    buttons[i, j].Location = new Point(
                        startX + j * buttonSize,
                        startY + i * buttonSize
                    );
                    buttons[i, j].Font = new Font("Arial", fontSize, FontStyle.Bold);
                    buttons[i, j].Tag = new Point(i, j);
                    buttons[i, j].Click += CellButton_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }

            int bottomY = startY + (currentBoardSize * buttonSize) + 20;

            newGameButton = new Button();
            newGameButton.Text = "New Game";
            newGameButton.Size = new Size(120, 40);
            newGameButton.Location = new Point(startX + 30, bottomY);
            newGameButton.Font = new Font("Arial", 10, FontStyle.Bold);
            newGameButton.Click += NewGameButton_Click;
            this.Controls.Add(newGameButton);

            exitButton = new Button();
            exitButton.Text = "Exit";
            exitButton.Size = new Size(120, 40);
            exitButton.Location = new Point(startX + 180, bottomY);
            exitButton.Font = new Font("Arial", 10, FontStyle.Bold);
            exitButton.Click += ExitButton_Click;
            this.Controls.Add(exitButton);
        }

        private string GetDifficultyText()
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy: return "Viegls";
                case DifficultyLevel.Medium: return "Vidējs";
                case DifficultyLevel.Hard: return "Grūts";
                default: return "";
            }
        }

        private void ResetBoard()
        {
            gameBoard.Reset();
            gameEnded = false;
            currentPlayer = playerX;

            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    buttons[i, j].Text = "";
                    buttons[i, j].Enabled = true;
                    buttons[i, j].BackColor = SystemColors.Control;
                }
            }

            UpdateStatus();
        }

        private async void CellButton_Click(object sender, EventArgs e)
        {
            if (gameEnded || currentPlayer.IsComputer)
                return;

            Button clickedButton = (Button)sender;
            Point position = (Point)clickedButton.Tag;
            int row = position.X;
            int col = position.Y;

            bool moveMade = MakeMove(row, col);

            if (moveMade && isVsComputer && !gameEnded && currentPlayer.IsComputer)
            {
                await ComputerMakeMove();
            }
        }

        private bool MakeMove(int row, int col)
        {
            if (gameBoard.MakeMove(row, col, currentPlayer.Symbol))
            {
                buttons[row, col].Text = currentPlayer.Symbol;
                buttons[row, col].Enabled = false;

                if (currentPlayer.Symbol == "X")
                    buttons[row, col].ForeColor = Color.Blue;
                else
                    buttons[row, col].ForeColor = Color.Red;

                if (gameLogic.CheckWinner(currentPlayer.Symbol))
                {
                    HandleWin();
                    return false;
                }
                else if (gameLogic.IsDraw())
                {
                    HandleDraw();
                    return false;
                }
                else
                {
                    SwitchPlayer();
                    UpdateStatus();
                    return true;
                }
            }
            return false;
        }

        private async Task ComputerMakeMove()
        {
            // Atspējo pogas kamēr dators domā
            foreach (Button btn in buttons)
            {
                btn.Enabled = false;
            }

            statusLabel.Text = "🤖 Dators domā...";
            Application.DoEvents(); // Pārvelk UI

            await Task.Delay(800); // Pauze reālisma dēļ

            Point move = computerAI.GetBestMove();
            if (move.X != -1 && move.Y != -1)
            {
                MakeMove(move.X, move.Y);
            }

            // Iespējo tikai tukšas pogas, ja spēle nav beigusies
            if (!gameEnded)
            {
                EnableAllButtons();
            }
        }

        private void HandleWin()
        {
            gameEnded = true;
            currentPlayer.IncrementWins();

            string winnerText = isVsComputer && currentPlayer.IsComputer
                ? "🤖 Dators uzvar!"
                : $"🎉 Spēlētājs {currentPlayer.Symbol} uzvar!";

            statusLabel.Text = winnerText;
            statusLabel.ForeColor = Color.Green;
            UpdateScore();
            DisableAllButtons();
        }

        private void HandleDraw()
        {
            gameEnded = true;
            statusLabel.Text = "⚖️ Neizšķirts!";
            statusLabel.ForeColor = Color.Orange;
            DisableAllButtons();
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == playerX) ? playerO : playerX;
        }

        private void UpdateStatus()
        {
            if (!gameEnded)
            {
                if (currentPlayer.IsComputer)
                {
                    statusLabel.Text = "🤖 Datora kārta";
                }
                else
                {
                    statusLabel.Text = $"▶ Spēlētāja {currentPlayer.Symbol} kārta";
                }
                statusLabel.ForeColor = Color.Black;
            }
        }

        private void UpdateScore()
        {
            string xLabel = "X";
            string oLabel = isVsComputer ? "Dators" : "O";
            scoreLabel.Text = $"Rezultāti - {xLabel}: {playerX.Wins}  |  {oLabel}: {playerO.Wins}";
        }

        private void DisableAllButtons()
        {
            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    buttons[i, j].Enabled = false;
                }
            }
        }

        private void EnableAllButtons()
        {
            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    if (gameBoard.IsEmpty(i, j))
                    {
                        buttons[i, j].Enabled = true;
                    }
                }
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Vai vēlaties izvēlēties jaunu režīmu?\n\nJā = Izvēlēties jaunu režīmu\nNē = Turpināt ar pašreizējo režīmu",
                "Jauna Spēle",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ShowInitialSelections();
            }
            else if (result == DialogResult.No)
            {
                ResetBoard();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Vai tiešām vēlaties iziet no spēles?",
                "Apstiprināt iziešanu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
