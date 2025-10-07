using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class DifficultySelectionForm : Form
    {
        private Button easyButton;
        private Button mediumButton;
        private Button hardButton;
        private Label titleLabel;
        private Label descriptionLabel;
        private Panel decorPanel;
        public DifficultyLevel SelectedDifficulty { get; private set; }

        public DifficultySelectionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tic-Tac-Toe - Grūtības Līmenis";
            this.Size = new Size(540, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ThemeManager.GetBackground();

            // Decorative Panel at top
            decorPanel = new Panel();
            decorPanel.Size = new Size(540, 5);
            decorPanel.Location = new Point(0, 0);
            decorPanel.BackColor = ThemeManager.GetPrimary();
            this.Controls.Add(decorPanel);

            // Title
            titleLabel = new Label();
            titleLabel.Text = "🎯 Izvēlies Grūtības Līmeni";
            titleLabel.AutoSize = false;
            titleLabel.Size = new Size(480, 60);
            titleLabel.Location = new Point(30, 30);
            titleLabel.Font = ThemeManager.GetFont(20, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.ForeColor = ThemeManager.GetText();
            this.Controls.Add(titleLabel);

            // Description
            descriptionLabel = new Label();
            descriptionLabel.Text = "Tu spēlēsi kā 'X' (sarkanā), dators būs 'O' (zaļā)";
            descriptionLabel.AutoSize = false;
            descriptionLabel.Size = new Size(480, 35);
            descriptionLabel.Location = new Point(30, 95);
            descriptionLabel.Font = ThemeManager.GetFont(11, FontStyle.Regular);
            descriptionLabel.TextAlign = ContentAlignment.MiddleCenter;
            descriptionLabel.ForeColor = ThemeManager.GetTextSecondary();
            this.Controls.Add(descriptionLabel);

            // Easy Button
            easyButton = new Button();
            easyButton.Text = "😊 Viegls\n\n✓ Dators dara nejaušus gājienus";
            easyButton.Size = new Size(280, 115);
            easyButton.Location = new Point(130, 155);
            easyButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            easyButton.Click += EasyButton_Click;
            ThemeManager.ApplyButtonStyle(easyButton);
            easyButton.FlatAppearance.BorderSize = 3;
            easyButton.FlatAppearance.BorderColor = Color.FromArgb(134, 239, 172);
            this.Controls.Add(easyButton);

            // Medium Button
            mediumButton = new Button();
            mediumButton.Text = "😐 Vidējs\n\n Dators reizēm kļūdās";
            mediumButton.Size = new Size(280, 115);
            mediumButton.Location = new Point(130, 285);
            mediumButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            mediumButton.Click += MediumButton_Click;
            ThemeManager.ApplyButtonStyle(mediumButton);
            mediumButton.FlatAppearance.BorderSize = 3;
            mediumButton.FlatAppearance.BorderColor = Color.FromArgb(251, 191, 36);
            this.Controls.Add(mediumButton);

            // Hard Button
            hardButton = new Button();
            hardButton.Text = "😈 Grūts\n\n Dators spēlē gandrīz perfekti!";
            hardButton.Size = new Size(280, 115);
            hardButton.Location = new Point(130, 415);
            hardButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            hardButton.Click += HardButton_Click;
            ThemeManager.ApplyButtonStyle(hardButton, true);
            hardButton.FlatAppearance.BorderSize = 3;
            hardButton.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
            this.Controls.Add(hardButton);

            // Hover effects ar labāku vizuālo feedback
            easyButton.MouseEnter += async (s, e) => {
                easyButton.BackColor = Color.FromArgb(220, 252, 231);
                easyButton.Font = ThemeManager.GetFont(12, FontStyle.Bold);
                easyButton.FlatAppearance.BorderColor = Color.FromArgb(34, 197, 94);
                await AnimationManager.PulseAnimation(easyButton, 1);
            };
            easyButton.MouseLeave += (s, e) => {
                easyButton.BackColor = ThemeManager.GetCardBackground();
                easyButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
                easyButton.FlatAppearance.BorderColor = Color.FromArgb(134, 239, 172);
            };

            mediumButton.MouseEnter += async (s, e) => {
                mediumButton.BackColor = Color.FromArgb(254, 243, 199);
                mediumButton.Font = ThemeManager.GetFont(12, FontStyle.Bold);
                mediumButton.FlatAppearance.BorderColor = Color.FromArgb(245, 158, 11);
                await AnimationManager.PulseAnimation(mediumButton, 1);
            };
            mediumButton.MouseLeave += (s, e) => {
                mediumButton.BackColor = ThemeManager.GetCardBackground();
                mediumButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
                mediumButton.FlatAppearance.BorderColor = Color.FromArgb(251, 191, 36);
            };

            hardButton.MouseEnter += async (s, e) => {
                hardButton.Font = ThemeManager.GetFont(12, FontStyle.Bold);
                hardButton.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
                await AnimationManager.PulseAnimation(hardButton, 1);
            };
            hardButton.MouseLeave += (s, e) => {
                hardButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
                hardButton.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
            };
        }

        private async void EasyButton_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(easyButton, "");
            SelectedDifficulty = DifficultyLevel.Easy;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void MediumButton_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(mediumButton, "");
            SelectedDifficulty = DifficultyLevel.Medium;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void HardButton_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(hardButton, "");
            SelectedDifficulty = DifficultyLevel.Hard;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}