using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
        private Button themeToggleButton;
        private Panel mainPanel;
        private Panel gamePanel;
        private bool gameEnded;
        private int currentBoardSize;
        private bool isVsComputer;
        private DifficultyLevel difficulty;
        private ComputerAI computerAI;
        private List<Point> winningCells;
        private bool isAnimating;

        public MainForm()
        {
            InitializeUI();
            ShowInitialSelections();
        }

        private void InitializeUI()
        {
            this.Text = "Tic-Tac-Toe Modern";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            playerX = new Player("X");
            playerO = new Player("O");
            winningCells = new List<Point>();
            isAnimating = false;
        }

        private void ShowInitialSelections()
        {
            PlayerModeSelectionForm playerModeForm = new PlayerModeSelectionForm();
            if (playerModeForm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            isVsComputer = playerModeForm.IsVsComputer;

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

            GameModeSelectionForm modeForm = new GameModeSelectionForm();
            if (modeForm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            currentBoardSize = modeForm.SelectedSize;

            InitializeGame();
        }

        private async void InitializeGame()
        {
            this.Controls.Clear();

            gameBoard = new GameBoard(currentBoardSize);
            gameLogic = new GameLogic(gameBoard);
            currentPlayer = playerX;
            gameEnded = false;
            winningCells.Clear();

            if (isVsComputer)
            {
                computerAI = new ComputerAI(gameBoard, gameLogic, difficulty, playerO.Symbol, playerX.Symbol);
            }

            int buttonSize = CalculateButtonSize();
            int formWidth = Math.Max(500, currentBoardSize * buttonSize + 140);
            int formHeight = currentBoardSize * buttonSize + 340;
            this.Size = new Size(formWidth, formHeight);
            this.BackColor = ThemeManager.GetBackground();

            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = ThemeManager.GetBackground();
            this.Controls.Add(mainPanel);

            CreateUIElements(buttonSize);
            ResetBoard();
            UpdateStatus();
            UpdateScore();

            // Fade-in animācija formai
            await AnimationManager.FadeIn(mainPanel, 200);
        }

        private int CalculateButtonSize()
        {
            switch (currentBoardSize)
            {
                case 3: return 110;
                case 4: return 90;
                case 5: return 75;
                default: return 90;
            }
        }

        private void CreateUIElements(int buttonSize)
        {
            int totalGameWidth = currentBoardSize * buttonSize + 10;
            int startX = Math.Max(50, (this.ClientSize.Width - totalGameWidth) / 2);
            int startY = 150;

            // Theme Toggle Button
            themeToggleButton = new Button();
            themeToggleButton.Text = ThemeManager.CurrentTheme == ThemeMode.Light ? "🌙" : "☀️";
            themeToggleButton.Size = new Size(55, 45);
            themeToggleButton.Location = new Point(this.ClientSize.Width - 75, 15);
            themeToggleButton.Font = ThemeManager.GetFont(18);
            ThemeManager.ApplyButtonStyle(themeToggleButton);
            themeToggleButton.Click += ThemeToggleButton_Click;
            mainPanel.Controls.Add(themeToggleButton);

            // Mode Label
            modeLabel = new Label();
            modeLabel.AutoSize = false;
            modeLabel.Size = new Size(this.ClientSize.Width - 150, 30);
            modeLabel.Location = new Point(20, 22);
            modeLabel.Font = ThemeManager.GetFont(10, FontStyle.Regular);
            modeLabel.TextAlign = ContentAlignment.MiddleLeft;
            modeLabel.ForeColor = ThemeManager.GetTextSecondary();
            string gameMode = isVsComputer ? $"🤖 Pret Datoru ({GetDifficultyText()})" : "👥 Divi Spēlētāji";
            modeLabel.Text = $"{currentBoardSize}×{currentBoardSize} | {gameMode}";
            mainPanel.Controls.Add(modeLabel);

            // Status Label
            statusLabel = new Label();
            statusLabel.AutoSize = false;
            statusLabel.Size = new Size(this.ClientSize.Width - 40, 40);
            statusLabel.Location = new Point(20, 60);
            statusLabel.Font = ThemeManager.GetFont(15, FontStyle.Bold);
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            statusLabel.ForeColor = ThemeManager.GetText();
            mainPanel.Controls.Add(statusLabel);

            // Score Label
            scoreLabel = new Label();
            scoreLabel.AutoSize = false;
            scoreLabel.Size = new Size(this.ClientSize.Width - 40, 30);
            scoreLabel.Location = new Point(20, 110);
            scoreLabel.Font = ThemeManager.GetFont(11, FontStyle.Regular);
            scoreLabel.TextAlign = ContentAlignment.MiddleCenter;
            scoreLabel.ForeColor = ThemeManager.GetTextSecondary();
            mainPanel.Controls.Add(scoreLabel);

            // Game Panel
            gamePanel = new Panel();
            gamePanel.Size = new Size(totalGameWidth, currentBoardSize * buttonSize + 10);
            gamePanel.Location = new Point(startX, startY);
            gamePanel.BackColor = ThemeManager.GetButtonBorder();
            mainPanel.Controls.Add(gamePanel);

            // Game Buttons
            buttons = new Button[currentBoardSize, currentBoardSize];
            int fontSize = currentBoardSize == 3 ? 48 : (currentBoardSize == 4 ? 38 : 32);

            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(buttonSize - 2, buttonSize - 2);
                    buttons[i, j].Location = new Point(5 + j * buttonSize, 5 + i * buttonSize);
                    buttons[i, j].Font = ThemeManager.GetFont(fontSize, FontStyle.Bold);
                    buttons[i, j].Tag = new Point(i, j);
                    buttons[i, j].Click += CellButton_Click;
                    buttons[i, j].Text = "";
                    ThemeManager.ApplyGameButtonStyle(buttons[i, j]);
                    gamePanel.Controls.Add(buttons[i, j]);
                }
            }

            int bottomY = startY + gamePanel.Height + 25;
            int buttonY = Math.Min(bottomY, this.ClientSize.Height - 70);
            int totalButtonWidth = 300;
            int buttonStartX = (this.ClientSize.Width - totalButtonWidth) / 2;

            // New Game Button
            newGameButton = new Button();
            newGameButton.Text = "🎮 New Game";
            newGameButton.Size = new Size(145, 48);
            newGameButton.Location = new Point(buttonStartX, buttonY);
            newGameButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            newGameButton.Click += NewGameButton_Click;
            ThemeManager.ApplyButtonStyle(newGameButton, true);
            mainPanel.Controls.Add(newGameButton);

            // Exit Button
            exitButton = new Button();
            exitButton.Text = "❌ Exit";
            exitButton.Size = new Size(145, 48);
            exitButton.Location = new Point(buttonStartX + 155, buttonY);
            exitButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            exitButton.Click += ExitButton_Click;
            ThemeManager.ApplyButtonStyle(exitButton);
            mainPanel.Controls.Add(exitButton);
        }

        private async void ThemeToggleButton_Click(object sender, EventArgs e)
        {
            ThemeManager.CurrentTheme = ThemeManager.CurrentTheme == ThemeMode.Light ? ThemeMode.Dark : ThemeMode.Light;
            themeToggleButton.Text = ThemeManager.CurrentTheme == ThemeMode.Light ? "🌙" : "☀️";

            await AnimationManager.PulseAnimation(themeToggleButton, 1);
            ApplyThemeToAll();
        }

        private void ApplyThemeToAll()
        {
            this.BackColor = ThemeManager.GetBackground();
            mainPanel.BackColor = ThemeManager.GetBackground();
            gamePanel.BackColor = ThemeManager.GetButtonBorder();

            modeLabel.ForeColor = ThemeManager.GetTextSecondary();
            statusLabel.ForeColor = ThemeManager.GetText();
            scoreLabel.ForeColor = ThemeManager.GetTextSecondary();

            ThemeManager.ApplyButtonStyle(themeToggleButton);
            ThemeManager.ApplyButtonStyle(newGameButton, true);
            ThemeManager.ApplyButtonStyle(exitButton);

            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    if (winningCells.Contains(new Point(i, j)))
                    {
                        buttons[i, j].BackColor = ThemeManager.GetWinningCell();
                    }
                    else
                    {
                        ThemeManager.ApplyGameButtonStyle(buttons[i, j]);
                    }

                    if (buttons[i, j].Text == "X")
                        buttons[i, j].ForeColor = ThemeManager.GetXColor();
                    else if (buttons[i, j].Text == "O")
                        buttons[i, j].ForeColor = ThemeManager.GetOColor();
                }
            }
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
            winningCells.Clear();

            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    buttons[i, j].Text = "";
                    buttons[i, j].Enabled = true;
                    ThemeManager.ApplyGameButtonStyle(buttons[i, j]);
                }
            }

            UpdateStatus();
        }

        private async void CellButton_Click(object sender, EventArgs e)
        {
            if (gameEnded || currentPlayer.IsComputer || isAnimating)
                return;

            Button clickedButton = (Button)sender;
            Point position = (Point)clickedButton.Tag;
            int row = position.X;
            int col = position.Y;

            bool moveMade = await MakeMove(row, col);

            if (moveMade && isVsComputer && !gameEnded && currentPlayer.IsComputer)
            {
                await ComputerMakeMove();
            }
        }

        private async Task<bool> MakeMove(int row, int col)
        {
            if (gameBoard.MakeMove(row, col, currentPlayer.Symbol))
            {
                isAnimating = true;

                buttons[row, col].Text = currentPlayer.Symbol;
                buttons[row, col].Enabled = false;
                buttons[row, col].ForeColor = currentPlayer.Symbol == "X" ? ThemeManager.GetXColor() : ThemeManager.GetOColor();

                // Pop animācija
                await AnimationManager.PopAnimation(buttons[row, col], currentPlayer.Symbol);

                isAnimating = false;

                if (gameLogic.CheckWinner(currentPlayer.Symbol))
                {
                    await HandleWin();
                    return false;
                }
                else if (gameLogic.IsDraw())
                {
                    await HandleDraw();
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
            // Atspējo visas pogas
            for (int i = 0; i < currentBoardSize; i++)
            {
                for (int j = 0; j < currentBoardSize; j++)
                {
                    buttons[i, j].Enabled = false;
                }
            }

            statusLabel.Text = "🤖 Dators domā...";
            statusLabel.ForeColor = ThemeManager.GetTextSecondary();

            // Pulse animācija statusam
            await AnimationManager.PulseAnimation(statusLabel, 1);

            await Task.Delay(600);

            Point move = computerAI.GetBestMove();
            if (move.X != -1 && move.Y != -1)
            {
                await MakeMove(move.X, move.Y);
            }

            if (!gameEnded)
            {
                EnableAllButtons();
            }
        }

        private async Task HandleWin()
        {
            gameEnded = true;
            currentPlayer.IncrementWins();

            winningCells = FindWinningCells(currentPlayer.Symbol);

            // Izveidojam masīvu ar uzvarošajām pogām
            Button[] winningButtons = new Button[winningCells.Count];
            for (int i = 0; i < winningCells.Count; i++)
            {
                winningButtons[i] = buttons[winningCells[i].X, winningCells[i].Y];
            }

            // Glow animācija uzvarošajām šūnām
            await AnimationManager.GlowAnimation(winningButtons);

            // Blink animācija
            await AnimationManager.BlinkAnimation(winningButtons, 2);

            string winnerText = isVsComputer && currentPlayer.IsComputer ? "🤖 Dators uzvar!" : $"🎉 Spēlētājs {currentPlayer.Symbol} uzvar!";

            statusLabel.Text = winnerText;
            statusLabel.ForeColor = ThemeManager.GetWinningCell();

            await AnimationManager.PulseAnimation(statusLabel, 2);

            UpdateScore();
            DisableAllButtons();
        }

        private List<Point> FindWinningCells(string symbol)
        {
            List<Point> cells = new List<Point>();

            for (int i = 0; i < gameBoard.Size; i++)
            {
                bool rowWin = true;
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    if (gameBoard.GetCell(i, j) != symbol)
                    {
                        rowWin = false;
                        break;
                    }
                }
                if (rowWin)
                {
                    for (int j = 0; j < gameBoard.Size; j++)
                        cells.Add(new Point(i, j));
                    return cells;
                }
            }

            for (int j = 0; j < gameBoard.Size; j++)
            {
                bool colWin = true;
                for (int i = 0; i < gameBoard.Size; i++)
                {
                    if (gameBoard.GetCell(i, j) != symbol)
                    {
                        colWin = false;
                        break;
                    }
                }
                if (colWin)
                {
                    for (int i = 0; i < gameBoard.Size; i++)
                        cells.Add(new Point(i, j));
                    return cells;
                }
            }

            bool mainDiag = true;
            for (int i = 0; i < gameBoard.Size; i++)
            {
                if (gameBoard.GetCell(i, i) != symbol)
                {
                    mainDiag = false;
                    break;
                }
            }
            if (mainDiag)
            {
                for (int i = 0; i < gameBoard.Size; i++)
                    cells.Add(new Point(i, i));
                return cells;
            }

            bool antiDiag = true;
            for (int i = 0; i < gameBoard.Size; i++)
            {
                if (gameBoard.GetCell(i, gameBoard.Size - 1 - i) != symbol)
                {
                    antiDiag = false;
                    break;
                }
            }
            if (antiDiag)
            {
                for (int i = 0; i < gameBoard.Size; i++)
                    cells.Add(new Point(i, gameBoard.Size - 1 - i));
                return cells;
            }

            return cells;
        }

        private async Task HandleDraw()
        {
            gameEnded = true;
            statusLabel.Text = "⚖️ Neizšķirts!";
            statusLabel.ForeColor = Color.Orange;

            await AnimationManager.ShakeAnimation(statusLabel, 8);

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
                statusLabel.ForeColor = ThemeManager.GetText();
            }
        }

        private void UpdateScore()
        {
            string xLabel = "X";
            string oLabel = isVsComputer ? "🤖 Dators" : "O";
            scoreLabel.Text = $"📊 Rezultāti: {xLabel} {playerX.Wins} - {playerO.Wins} {oLabel}";
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

        private async void NewGameButton_Click(object sender, EventArgs e)
        {
            // Izveidojam custom dialogu
            CustomDialog dialog = new CustomDialog(
                "Jauna Spēle",
                "Vai vēlaties izvēlēties jaunu režīmu?",
                "Jā - Jauns režīms",
                "Nē - Pašreizējais režīms",
                "Atcelt"
            );

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.Yes)
            {
                // Fade-out animācija
                await AnimationManager.ZoomTransition(gamePanel, gamePanel.Size, new Size(0, 0), 300);
                ShowInitialSelections();
            }
            else if (result == DialogResult.No)
            {
                ResetBoard();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            // Izveidojam custom exit dialogu
            CustomDialog dialog = new CustomDialog(
                "Apstiprināt Iziešanu",
                "Vai tiešām vēlaties iziet no spēles?",
                "Jā, iziet",
                "Nē, turpināt"
            );

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}