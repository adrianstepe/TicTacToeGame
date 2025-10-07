using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public class CustomDialog : Form
    {
        private Label titleLabel;
        private Label messageLabel;
        private Button button1;
        private Button button2;
        private Button button3;

        public CustomDialog(string title, string message, string btn1Text, string btn2Text, string btn3Text = null)
        {
            InitializeDialog(title, message, btn1Text, btn2Text, btn3Text);
        }

        private void InitializeDialog(string title, string message, string btn1Text, string btn2Text, string btn3Text)
        {
            this.Text = title;
            this.Size = new Size(480, btn3Text != null ? 280 : 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ThemeManager.GetBackground();

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.AutoSize = false;
            titleLabel.Size = new Size(420, 45);
            titleLabel.Location = new Point(30, 25);
            titleLabel.Font = ThemeManager.GetFont(16, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.ForeColor = ThemeManager.GetText();
            this.Controls.Add(titleLabel);

            // Message Label
            messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.AutoSize = false;
            messageLabel.Size = new Size(420, 60);
            messageLabel.Location = new Point(30, 75);
            messageLabel.Font = ThemeManager.GetFont(11, FontStyle.Regular);
            messageLabel.TextAlign = ContentAlignment.TopCenter;
            messageLabel.ForeColor = ThemeManager.GetTextSecondary();
            this.Controls.Add(messageLabel);

            int buttonY = 145;
            int buttonWidth = btn3Text != null ? 130 : 180;
            int spacing = btn3Text != null ? 10 : 20;

            // Button 1 (Yes/OK)
            button1 = new Button();
            button1.Text = btn1Text;
            button1.Size = new Size(buttonWidth, 50);
            button1.Location = new Point(btn3Text != null ? 30 : 60, buttonY);
            button1.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            button1.Click += (s, e) => { this.DialogResult = DialogResult.Yes; this.Close(); };
            ThemeManager.ApplyButtonStyle(button1, true);
            this.Controls.Add(button1);

            // Button 2 (No)
            button2 = new Button();
            button2.Text = btn2Text;
            button2.Size = new Size(buttonWidth, 50);
            button2.Location = new Point(button1.Location.X + buttonWidth + spacing, buttonY);
            button2.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            button2.Click += (s, e) => { this.DialogResult = DialogResult.No; this.Close(); };
            ThemeManager.ApplyButtonStyle(button2);
            this.Controls.Add(button2);

            // Button 3 (Cancel) - optional
            if (btn3Text != null)
            {
                button3 = new Button();
                button3.Text = btn3Text;
                button3.Size = new Size(buttonWidth, 50);
                button3.Location = new Point(button2.Location.X + buttonWidth + spacing, buttonY);
                button3.Font = ThemeManager.GetFont(11, FontStyle.Bold);
                button3.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
                ThemeManager.ApplyButtonStyle(button3);
                this.Controls.Add(button3);
            }

            // Hover animations
            button1.MouseEnter += async (s, e) => await AnimationManager.PulseAnimation(button1, 1);
            button2.MouseEnter += async (s, e) => await AnimationManager.PulseAnimation(button2, 1);
            if (button3 != null)
                button3.MouseEnter += async (s, e) => await AnimationManager.PulseAnimation(button3, 1);
        }
    }
}