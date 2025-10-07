using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TicTacToeGame
{
    public enum ThemeMode
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        public static ThemeMode CurrentTheme { get; set; } = ThemeMode.Light;

        // Light Theme Colors
        public static class LightTheme
        {
            public static Color Background = Color.FromArgb(240, 244, 248);
            public static Color CardBackground = Color.White;
            public static Color Primary = Color.FromArgb(59, 130, 246);
            public static Color PrimaryHover = Color.FromArgb(37, 99, 235);
            public static Color Text = Color.FromArgb(17, 24, 39);
            public static Color TextSecondary = Color.FromArgb(107, 114, 128);
            public static Color XColor = Color.FromArgb(239, 68, 68);
            public static Color OColor = Color.FromArgb(34, 197, 94);
            public static Color ButtonBorder = Color.FromArgb(229, 231, 235);
            public static Color WinningCell = Color.FromArgb(134, 239, 172);
            public static Color ButtonHover = Color.FromArgb(249, 250, 251);
        }

        // Dark Theme Colors
        public static class DarkTheme
        {
            public static Color Background = Color.FromArgb(17, 24, 39);
            public static Color CardBackground = Color.FromArgb(31, 41, 55);
            public static Color Primary = Color.FromArgb(96, 165, 250);
            public static Color PrimaryHover = Color.FromArgb(59, 130, 246);
            public static Color Text = Color.FromArgb(243, 244, 246);
            public static Color TextSecondary = Color.FromArgb(156, 163, 175);
            public static Color XColor = Color.FromArgb(248, 113, 113);
            public static Color OColor = Color.FromArgb(74, 222, 128);
            public static Color ButtonBorder = Color.FromArgb(55, 65, 81);
            public static Color WinningCell = Color.FromArgb(34, 197, 94);
            public static Color ButtonHover = Color.FromArgb(55, 65, 81);
        }

        public static Color GetBackground() => CurrentTheme == ThemeMode.Light ? LightTheme.Background : DarkTheme.Background;
        public static Color GetCardBackground() => CurrentTheme == ThemeMode.Light ? LightTheme.CardBackground : DarkTheme.CardBackground;
        public static Color GetPrimary() => CurrentTheme == ThemeMode.Light ? LightTheme.Primary : DarkTheme.Primary;
        public static Color GetPrimaryHover() => CurrentTheme == ThemeMode.Light ? LightTheme.PrimaryHover : DarkTheme.PrimaryHover;
        public static Color GetText() => CurrentTheme == ThemeMode.Light ? LightTheme.Text : DarkTheme.Text;
        public static Color GetTextSecondary() => CurrentTheme == ThemeMode.Light ? LightTheme.TextSecondary : DarkTheme.TextSecondary;
        public static Color GetXColor() => CurrentTheme == ThemeMode.Light ? LightTheme.XColor : DarkTheme.XColor;
        public static Color GetOColor() => CurrentTheme == ThemeMode.Light ? LightTheme.OColor : DarkTheme.OColor;
        public static Color GetButtonBorder() => CurrentTheme == ThemeMode.Light ? LightTheme.ButtonBorder : DarkTheme.ButtonBorder;
        public static Color GetWinningCell() => CurrentTheme == ThemeMode.Light ? LightTheme.WinningCell : DarkTheme.WinningCell;
        public static Color GetButtonHover() => CurrentTheme == ThemeMode.Light ? LightTheme.ButtonHover : DarkTheme.ButtonHover;

        public static Font GetFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI", size, style);
        }

        public static void ApplyButtonStyle(System.Windows.Forms.Button button, bool isPrimary = false)
        {
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 2;

            if (isPrimary)
            {
                button.BackColor = GetPrimary();
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderColor = GetPrimary();
            }
            else
            {
                button.BackColor = GetCardBackground();
                button.ForeColor = GetText();
                button.FlatAppearance.BorderColor = GetButtonBorder();
            }

            button.FlatAppearance.MouseOverBackColor = isPrimary ? GetPrimaryHover() : GetButtonHover();
            button.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        public static void ApplyGameButtonStyle(System.Windows.Forms.Button button)
        {
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 2;
            button.BackColor = GetCardBackground();
            button.ForeColor = GetText();
            button.FlatAppearance.BorderColor = GetButtonBorder();
            button.FlatAppearance.MouseOverBackColor = GetButtonHover();
            button.Cursor = System.Windows.Forms.Cursors.Hand;
        }
    }
}