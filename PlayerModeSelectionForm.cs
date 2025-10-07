using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class PlayerModeSelectionForm : Form
    {
        private Button twoPlayerButton;
        private Button vsComputerButton;
        private Label titleLabel;
        private Label subtitleLabel;
        private Panel decorPanel;
        public bool IsVsComputer { get; private set; }

        public PlayerModeSelectionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tic-Tac-Toe - Spēles Veids";
            this.Size = new Size(520, 460);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ThemeManager.GetBackground();

            // Decorative Panel at top
            decorPanel = new Panel();
            decorPanel.Size = new Size(520, 5);
            decorPanel.Location = new Point(0, 0);
            decorPanel.BackColor = ThemeManager.GetPrimary();
            this.Controls.Add(decorPanel);

            // Title
            titleLabel = new Label();
            titleLabel.Text = "🎮 Izvēlies Spēles Veidu";
            titleLabel.AutoSize = false;
            titleLabel.Size = new Size(460, 60);
            titleLabel.Location = new Point(30, 35);
            titleLabel.Font = ThemeManager.GetFont(20, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.ForeColor = ThemeManager.GetText();
            this.Controls.Add(titleLabel);

            // Subtitle
            subtitleLabel = new Label();
            subtitleLabel.Text = "Izvēlies, kā vēlies spēlēt";
            subtitleLabel.AutoSize = false;
            subtitleLabel.Size = new Size(460, 35);
            subtitleLabel.Location = new Point(30, 100);
            subtitleLabel.Font = ThemeManager.GetFont(11, FontStyle.Regular);
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            subtitleLabel.ForeColor = ThemeManager.GetTextSecondary();
            this.Controls.Add(subtitleLabel);

            // Two Player Button
            twoPlayerButton = new Button();
            twoPlayerButton.Text = "👥 Divi Spēlētāji\n X pret O";
            twoPlayerButton.Size = new Size(260, 120);
            twoPlayerButton.Location = new Point(130, 160);
            twoPlayerButton.Font = ThemeManager.GetFont(12, FontStyle.Bold);
            twoPlayerButton.Click += TwoPlayerButton_Click;
            ThemeManager.ApplyButtonStyle(twoPlayerButton);
            twoPlayerButton.FlatAppearance.BorderSize = 3;
            twoPlayerButton.FlatAppearance.BorderColor = ThemeManager.GetButtonBorder();
            this.Controls.Add(twoPlayerButton);

            // VS Computer Button
            vsComputerButton = new Button();
            vsComputerButton.Text = "🤖 Pret Datoru\n Tu esi X";
            vsComputerButton.Size = new Size(260, 120);
            vsComputerButton.Location = new Point(130, 295);
            vsComputerButton.Font = ThemeManager.GetFont(12, FontStyle.Bold);
            vsComputerButton.Click += VsComputerButton_Click;
            ThemeManager.ApplyButtonStyle(vsComputerButton, true);
            vsComputerButton.FlatAppearance.BorderSize = 3;
            vsComputerButton.FlatAppearance.BorderColor = ThemeManager.GetPrimary();
            this.Controls.Add(vsComputerButton);

            // Hover effects ar labāku feedback
            twoPlayerButton.MouseEnter += async (s, e) => {
                twoPlayerButton.FlatAppearance.BorderColor = ThemeManager.GetPrimary();
                twoPlayerButton.Font = ThemeManager.GetFont(13, FontStyle.Bold);
                twoPlayerButton.BackColor = ThemeManager.GetButtonHover();
                await AnimationManager.PulseAnimation(twoPlayerButton, 1);
            };
            twoPlayerButton.MouseLeave += (s, e) => {
                twoPlayerButton.FlatAppearance.BorderColor = ThemeManager.GetButtonBorder();
                twoPlayerButton.Font = ThemeManager.GetFont(12, FontStyle.Bold);
                twoPlayerButton.BackColor = ThemeManager.GetCardBackground();
            };

            vsComputerButton.MouseEnter += async (s, e) => {
                vsComputerButton.Font = ThemeManager.GetFont(13, FontStyle.Bold);
                vsComputerButton.FlatAppearance.BorderColor = ThemeManager.GetPrimaryHover();
                await AnimationManager.PulseAnimation(vsComputerButton, 1);
            };
            vsComputerButton.MouseLeave += (s, e) => {
                vsComputerButton.Font = ThemeManager.GetFont(12, FontStyle.Bold);
                vsComputerButton.FlatAppearance.BorderColor = ThemeManager.GetPrimary();
            };
        }

        private async void TwoPlayerButton_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(twoPlayerButton, "");
            IsVsComputer = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void VsComputerButton_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(vsComputerButton, "");
            IsVsComputer = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}