using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public class PlayerModeSelectionForm : Form
    {
        private Button twoPlayerButton;
        private Button vsComputerButton;
        private Label titleLabel;
        public bool IsVsComputer { get; private set; }

        public PlayerModeSelectionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tic-Tac-Toe - Spēlētāju Skaits";
            this.Size = new Size(400, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label();
            titleLabel.Text = "Izvēlies Spēles Veidu";
            titleLabel.Size = new Size(350, 40);
            titleLabel.Location = new Point(25, 30);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            twoPlayerButton = new Button();
            twoPlayerButton.Text = "👥 Divi Spēlētāji\n(X vs O)";
            twoPlayerButton.Size = new Size(180, 70);
            twoPlayerButton.Location = new Point(110, 90);
            twoPlayerButton.Font = new Font("Arial", 11, FontStyle.Bold);
            twoPlayerButton.Click += TwoPlayerButton_Click;
            this.Controls.Add(twoPlayerButton);

            vsComputerButton = new Button();
            vsComputerButton.Text = "🤖 Pret Datoru\n(X vs Dators)";
            vsComputerButton.Size = new Size(180, 70);
            vsComputerButton.Location = new Point(110, 170);
            vsComputerButton.Font = new Font("Arial", 11, FontStyle.Bold);
            vsComputerButton.Click += VsComputerButton_Click;
            this.Controls.Add(vsComputerButton);
        }

        private void TwoPlayerButton_Click(object sender, EventArgs e)
        {
            IsVsComputer = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void VsComputerButton_Click(object sender, EventArgs e)
        {
            IsVsComputer = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}