using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class GameModeSelectionForm : Form
    {
        private Button mode3x3Button;
        private Button mode4x4Button;
        private Button mode5x5Button;
        private Label titleLabel;
        private Label subtitleLabel;
        private Panel decorPanel;
        public int SelectedSize { get; private set; }

        public GameModeSelectionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tic-Tac-Toe - Laukuma Izmērs";
            this.Size = new Size(520, 600);
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
            titleLabel.Text = "🎯 Izvēlies Laukuma Izmēru";
            titleLabel.AutoSize = false;
            titleLabel.Size = new Size(460, 60);
            titleLabel.Location = new Point(30, 35);
            titleLabel.Font = ThemeManager.GetFont(20, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.ForeColor = ThemeManager.GetText();
            this.Controls.Add(titleLabel);

            // Subtitle
            subtitleLabel = new Label();
            subtitleLabel.Text = "";
            subtitleLabel.AutoSize = false;
            subtitleLabel.Size = new Size(460, 35);
            subtitleLabel.Location = new Point(30, 100);
            subtitleLabel.Font = ThemeManager.GetFont(11, FontStyle.Regular);
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            subtitleLabel.ForeColor = ThemeManager.GetTextSecondary();
            this.Controls.Add(subtitleLabel);

            // 3x3 Button
            mode3x3Button = new Button();
            mode3x3Button.Text = "3 × 3\n\n✓ 3 simboli pēc kārtas";
            mode3x3Button.Size = new Size(240, 120);
            mode3x3Button.Location = new Point(140, 160);
            mode3x3Button.Font = ThemeManager.GetFont(13, FontStyle.Bold);
            mode3x3Button.Click += Mode3x3Button_Click;
            ThemeManager.ApplyButtonStyle(mode3x3Button, true);
            mode3x3Button.FlatAppearance.BorderSize = 3;
            mode3x3Button.FlatAppearance.BorderColor = ThemeManager.GetPrimary();
            this.Controls.Add(mode3x3Button);

            // 4x4 Button
            mode4x4Button = new Button();
            mode4x4Button.Text = "4 × 4\n\n✓ 4 simboli pēc kārtas";
            mode4x4Button.Size = new Size(240, 120);
            mode4x4Button.Location = new Point(140, 295);
            mode4x4Button.Font = ThemeManager.GetFont(13, FontStyle.Bold);
            mode4x4Button.Click += Mode4x4Button_Click;
            ThemeManager.ApplyButtonStyle(mode4x4Button);
            mode4x4Button.FlatAppearance.BorderSize = 3;
            mode4x4Button.FlatAppearance.BorderColor = ThemeManager.GetButtonBorder();
            this.Controls.Add(mode4x4Button);

            // 5x5 Button
            mode5x5Button = new Button();
            mode5x5Button.Text = "5 × 5\n\n✓ 5 simboli pēc kārtas";
            mode5x5Button.Size = new Size(240, 120);
            mode5x5Button.Location = new Point(140, 430);
            mode5x5Button.Font = ThemeManager.GetFont(13, FontStyle.Bold);
            mode5x5Button.Click += Mode5x5Button_Click;
            ThemeManager.ApplyButtonStyle(mode5x5Button);
            mode5x5Button.FlatAppearance.BorderSize = 3;
            mode5x5Button.FlatAppearance.BorderColor = ThemeManager.GetButtonBorder();
            this.Controls.Add(mode5x5Button);

            // Hover effects ar labāku feedback
            mode3x3Button.MouseEnter += async (s, e) => {
                mode3x3Button.Font = ThemeManager.GetFont(14, FontStyle.Bold);
                mode3x3Button.FlatAppearance.BorderColor = ThemeManager.GetPrimaryHover();
                await AnimationManager.PulseAnimation(mode3x3Button, 1);
            };
            mode3x3Button.MouseLeave += (s, e) => {
                mode3x3Button.Font = ThemeManager.GetFont(13, FontStyle.Bold);
                mode3x3Button.FlatAppearance.BorderColor = ThemeManager.GetPrimary();
            };

            mode4x4Button.MouseEnter += async (s, e) => {
                mode4x4Button.FlatAppearance.BorderColor = ThemeManager.GetPrimary();
                mode4x4Button.Font = ThemeManager.GetFont(14, FontStyle.Bold);
                mode4x4Button.BackColor = ThemeManager.GetButtonHover();
                await AnimationManager.PulseAnimation(mode4x4Button, 1);
            };
            mode4x4Button.MouseLeave += (s, e) => {
                mode4x4Button.FlatAppearance.BorderColor = ThemeManager.GetButtonBorder();
                mode4x4Button.Font = ThemeManager.GetFont(13, FontStyle.Bold);
                mode4x4Button.BackColor = ThemeManager.GetCardBackground();
            };

            mode5x5Button.MouseEnter += async (s, e) => {
                mode5x5Button.FlatAppearance.BorderColor = ThemeManager.GetPrimary();
                mode5x5Button.Font = ThemeManager.GetFont(14, FontStyle.Bold);
                mode5x5Button.BackColor = ThemeManager.GetButtonHover();
                await AnimationManager.PulseAnimation(mode5x5Button, 1);
            };
            mode5x5Button.MouseLeave += (s, e) => {
                mode5x5Button.FlatAppearance.BorderColor = ThemeManager.GetButtonBorder();
                mode5x5Button.Font = ThemeManager.GetFont(13, FontStyle.Bold);
                mode5x5Button.BackColor = ThemeManager.GetCardBackground();
            };
        }

        private async void Mode3x3Button_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(mode3x3Button, "");
            SelectedSize = 3;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void Mode4x4Button_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(mode4x4Button, "");
            SelectedSize = 4;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void Mode5x5Button_Click(object sender, EventArgs e)
        {
            await AnimationManager.PopAnimation(mode5x5Button, "");
            SelectedSize = 5;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}