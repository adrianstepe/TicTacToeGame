using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public class DifficultySelectionForm : Form
    {
        private Button easyButton;
        private Button mediumButton;
        private Button hardButton;
        private Label titleLabel;
        private Label descriptionLabel;
        public DifficultyLevel SelectedDifficulty { get; private set; }

        public DifficultySelectionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tic-Tac-Toe - Grūtības Līmenis";
            this.Size = new Size(450, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label();
            titleLabel.Text = "Izvēlies Grūtības Līmeni";
            titleLabel.Size = new Size(400, 40);
            titleLabel.Location = new Point(25, 20);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            descriptionLabel = new Label();
            descriptionLabel.Text = "Dators spēlē kā 'O'";
            descriptionLabel.Size = new Size(400, 25);
            descriptionLabel.Location = new Point(25, 60);
            descriptionLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            descriptionLabel.TextAlign = ContentAlignment.MiddleCenter;
            descriptionLabel.ForeColor = Color.Gray;
            this.Controls.Add(descriptionLabel);

            easyButton = new Button();
            easyButton.Text = "😊 Viegls\nDators dara nejaušus gājienus";
            easyButton.Size = new Size(200, 70);
            easyButton.Location = new Point(125, 100);
            easyButton.Font = new Font("Arial", 11, FontStyle.Bold);
            easyButton.BackColor = Color.LightGreen;
            easyButton.Click += EasyButton_Click;
            this.Controls.Add(easyButton);

            mediumButton = new Button();
            mediumButton.Text = "😐 Vidējs\nDators reizēm kļūdās";
            mediumButton.Size = new Size(200, 70);
            mediumButton.Location = new Point(125, 180);
            mediumButton.Font = new Font("Arial", 11, FontStyle.Bold);
            mediumButton.BackColor = Color.LightYellow;
            mediumButton.Click += MediumButton_Click;
            this.Controls.Add(mediumButton);

            hardButton = new Button();
            hardButton.Text = "😈 Grūts\nDators spēlē perfekti!";
            hardButton.Size = new Size(200, 70);
            hardButton.Location = new Point(125, 260);
            hardButton.Font = new Font("Arial", 11, FontStyle.Bold);
            hardButton.BackColor = Color.LightCoral;
            hardButton.Click += HardButton_Click;
            this.Controls.Add(hardButton);
        }

        private void EasyButton_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = DifficultyLevel.Easy;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MediumButton_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = DifficultyLevel.Medium;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void HardButton_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = DifficultyLevel.Hard;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}