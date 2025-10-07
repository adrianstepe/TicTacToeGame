using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public class GameModeSelectionForm : Form
    {
        private Button mode3x3Button;
        private Button mode4x4Button;
        private Button mode5x5Button;
        private Label titleLabel;
        public int SelectedSize { get; private set; }

        public GameModeSelectionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tic-Tac-Toe - Izvēlies Režīmu";
            this.Size = new Size(400, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label();
            titleLabel.Text = "Izvēlies Spēles Laukuma Izmēru";
            titleLabel.Size = new Size(350, 40);
            titleLabel.Location = new Point(25, 30);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            mode3x3Button = new Button();
            mode3x3Button.Text = "3 x 3\n(3 pēc kārtas)";
            mode3x3Button.Size = new Size(140, 80);
            mode3x3Button.Location = new Point(130, 100);
            mode3x3Button.Font = new Font("Arial", 12, FontStyle.Bold);
            mode3x3Button.Click += Mode3x3Button_Click;
            this.Controls.Add(mode3x3Button);

            mode4x4Button = new Button();
            mode4x4Button.Text = "4 x 4\n(4 pēc kārtas)";
            mode4x4Button.Size = new Size(140, 80);
            mode4x4Button.Location = new Point(130, 190);
            mode4x4Button.Font = new Font("Arial", 12, FontStyle.Bold);
            mode4x4Button.Click += Mode4x4Button_Click;
            this.Controls.Add(mode4x4Button);

            mode5x5Button = new Button();
            mode5x5Button.Text = "5 x 5\n(5 pēc kārtas)";
            mode5x5Button.Size = new Size(140, 80);
            mode5x5Button.Location = new Point(130, 280);
            mode5x5Button.Font = new Font("Arial", 10, FontStyle.Bold);
            mode5x5Button.Click += Mode5x5Button_Click;
            this.Controls.Add(mode5x5Button);
        }

        private void Mode3x3Button_Click(object sender, EventArgs e)
        {
            SelectedSize = 3;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Mode4x4Button_Click(object sender, EventArgs e)
        {
            SelectedSize = 4;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Mode5x5Button_Click(object sender, EventArgs e)
        {
            SelectedSize = 5;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}