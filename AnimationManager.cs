using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public static class AnimationManager
    {
        // Pop animācija pogai - VIENKĀRŠA VERSIJA
        public static async Task PopAnimation(Button button, string symbol)
        {
            if (button == null || button.IsDisposed) return;

            int originalSize = (int)button.Font.Size;
            Font originalFont = button.Font;

            // Palielināšanās
            for (int i = 0; i <= 8; i++)
            {
                if (button.IsDisposed) return;
                float scale = 1.0f + (i / 20.0f); // 1.0 -> 1.4
                int newSize = (int)(originalSize * scale);
                button.Font = new Font(button.Font.FontFamily, newSize, button.Font.Style);
                await Task.Delay(20);
                Application.DoEvents();
            }

            // Samazināšanās
            for (int i = 8; i >= 0; i--)
            {
                if (button.IsDisposed) return;
                float scale = 1.0f + (i / 20.0f);
                int newSize = (int)(originalSize * scale);
                button.Font = new Font(button.Font.FontFamily, newSize, button.Font.Style);
                await Task.Delay(20);
                Application.DoEvents();
            }

            if (!button.IsDisposed)
                button.Font = originalFont;
        }

        // Mirgošanas animācija - VIENKĀRŠA
        public static async Task BlinkAnimation(Button[] buttons, int times = 3)
        {
            if (buttons == null || buttons.Length == 0) return;

            Color highlightColor = ThemeManager.GetWinningCell();
            Color darkHighlight = ControlPaint.Dark(highlightColor, 0.1f);

            for (int i = 0; i < times; i++)
            {
                // Tumšāka
                foreach (Button btn in buttons)
                {
                    if (btn != null && !btn.IsDisposed)
                        btn.BackColor = darkHighlight;
                }
                Application.DoEvents();
                await Task.Delay(250);

                // Gaišāka
                foreach (Button btn in buttons)
                {
                    if (btn != null && !btn.IsDisposed)
                        btn.BackColor = highlightColor;
                }
                Application.DoEvents();
                await Task.Delay(250);
            }

            // Gala krāsa
            foreach (Button btn in buttons)
            {
                if (btn != null && !btn.IsDisposed)
                    btn.BackColor = highlightColor;
            }
            Application.DoEvents();
        }

        // Izgaismošanās - VIENKĀRŠA
        public static async Task GlowAnimation(Button[] buttons)
        {
            if (buttons == null || buttons.Length == 0) return;

            Color startColor = ThemeManager.GetCardBackground();
            Color endColor = ThemeManager.GetWinningCell();

            for (int step = 0; step <= 15; step++)
            {
                float ratio = step / 15.0f;
                int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
                int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
                int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);

                Color currentColor = Color.FromArgb(r, g, b);

                foreach (Button btn in buttons)
                {
                    if (btn != null && !btn.IsDisposed)
                        btn.BackColor = currentColor;
                }

                Application.DoEvents();
                await Task.Delay(40);
            }
        }

        // Fade-in - VIENKĀRŠA
        public static async Task FadeIn(Control control, int duration = 300)
        {
            if (control == null || control.IsDisposed) return;
            control.Visible = true;
            Application.DoEvents();
            await Task.Delay(duration);
        }

        // Shake animācija - VIENKĀRŠA
        public static async Task ShakeAnimation(Control control, int intensity = 8)
        {
            if (control == null || control.IsDisposed) return;

            Point originalLocation = control.Location;

            for (int i = 0; i < 3; i++)
            {
                if (control.IsDisposed) return;

                control.Location = new Point(originalLocation.X + intensity, originalLocation.Y);
                Application.DoEvents();
                await Task.Delay(50);

                control.Location = new Point(originalLocation.X - intensity, originalLocation.Y);
                Application.DoEvents();
                await Task.Delay(50);
            }

            if (!control.IsDisposed)
                control.Location = originalLocation;
            Application.DoEvents();
        }

        // Pulse animācija - VIENKĀRŠA
        public static async Task PulseAnimation(Control control, int pulses = 1)
        {
            if (control == null || control.IsDisposed) return;

            Size originalSize = control.Size;
            Point originalLocation = control.Location;

            for (int p = 0; p < pulses; p++)
            {
                // Palielināšanās
                for (int i = 0; i <= 3; i++)
                {
                    if (control.IsDisposed) return;

                    float scale = 1.0f + (i / 30.0f);
                    int newWidth = (int)(originalSize.Width * scale);
                    int newHeight = (int)(originalSize.Height * scale);

                    control.Size = new Size(newWidth, newHeight);
                    control.Location = new Point(
                        originalLocation.X - (newWidth - originalSize.Width) / 2,
                        originalLocation.Y - (newHeight - originalSize.Height) / 2
                    );

                    Application.DoEvents();
                    await Task.Delay(40);
                }

                // Samazināšanās
                for (int i = 3; i >= 0; i--)
                {
                    if (control.IsDisposed) return;

                    float scale = 1.0f + (i / 30.0f);
                    int newWidth = (int)(originalSize.Width * scale);
                    int newHeight = (int)(originalSize.Height * scale);

                    control.Size = new Size(newWidth, newHeight);
                    control.Location = new Point(
                        originalLocation.X - (newWidth - originalSize.Width) / 2,
                        originalLocation.Y - (newHeight - originalSize.Height) / 2
                    );

                    Application.DoEvents();
                    await Task.Delay(40);
                }
            }

            if (!control.IsDisposed)
            {
                control.Size = originalSize;
                control.Location = originalLocation;
            }
            Application.DoEvents();
        }

        // Zoom animācija - VIENKĀRŠA
        public static async Task ZoomTransition(Panel panel, Size startSize, Size endSize, int duration = 400)
        {
            if (panel == null || panel.IsDisposed) return;

            Point centerPoint = new Point(
                panel.Location.X + panel.Width / 2,
                panel.Location.Y + panel.Height / 2
            );

            int steps = 15;
            int delay = duration / steps;

            for (int i = 0; i <= steps; i++)
            {
                if (panel.IsDisposed) return;

                float progress = i / (float)steps;
                int currentWidth = startSize.Width + (int)((endSize.Width - startSize.Width) * progress);
                int currentHeight = startSize.Height + (int)((endSize.Height - startSize.Height) * progress);

                panel.Size = new Size(currentWidth, currentHeight);
                panel.Location = new Point(
                    centerPoint.X - panel.Width / 2,
                    centerPoint.Y - panel.Height / 2
                );

                Application.DoEvents();
                await Task.Delay(delay);
            }

            if (!panel.IsDisposed)
            {
                panel.Size = endSize;
                panel.Location = new Point(
                    centerPoint.X - panel.Width / 2,
                    centerPoint.Y - panel.Height / 2
                );
            }
            Application.DoEvents();
        }

        // Slide animācija - VIENKĀRŠA
        public static async Task SlideIn(Control control, int startX, int endX, int duration = 300)
        {
            if (control == null || control.IsDisposed) return;

            int steps = 15;
            int delay = duration / steps;
            int deltaX = endX - startX;

            for (int i = 0; i <= steps; i++)
            {
                if (control.IsDisposed) return;

                float progress = i / (float)steps;
                int currentX = startX + (int)(deltaX * progress);
                control.Location = new Point(currentX, control.Location.Y);

                Application.DoEvents();
                await Task.Delay(delay);
            }

            if (!control.IsDisposed)
                control.Location = new Point(endX, control.Location.Y);
            Application.DoEvents();
        }
    }
}