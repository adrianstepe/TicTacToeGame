using System;
using System.Media;
using System.IO;

namespace TicTacToeGame
{
    public static class SoundManager
    {
        private static SoundPlayer clickPlayer;
        private static SoundPlayer winPlayer;
        private static SoundPlayer losePlayer;
        private static SoundPlayer drawPlayer;

        public static bool IsSoundEnabled { get; set; } = true;
        public static int Volume { get; set; } = 50; // 0-100

        static SoundManager()
        {
            InitializeSounds();
        }

        private static void InitializeSounds()
        {
            try
            {
                // Izmantojam Windows sistēmas skaņas
                clickPlayer = new SoundPlayer();
                winPlayer = new SoundPlayer();
                losePlayer = new SoundPlayer();
                drawPlayer = new SoundPlayer();

                // Ģenerējam vienkāršas skaņas ar frekvencēm
                // (Alternative: var izmantot .wav failus)
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Sound initialization error: {ex.Message}");
            }
        }

        public static void PlayClick()
        {
            if (!IsSoundEnabled) return;

            try
            {
                // Vienkārša sistēmas skaņa
                System.Console.Beep(800, 50);
            }
            catch { }
        }

        public static void PlayWin()
        {
            if (!IsSoundEnabled) return;

            try
            {
                // Uzvara - pieaugoša melodija
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        System.Console.Beep(523, 150); // C
                        System.Threading.Thread.Sleep(50);
                        System.Console.Beep(659, 150); // E
                        System.Threading.Thread.Sleep(50);
                        System.Console.Beep(784, 300); // G
                    }
                    catch { }
                });
            }
            catch { }
        }

        public static void PlayLose()
        {
            if (!IsSoundEnabled) return;

            try
            {
                // Zaudējums - lejupejoša melodija
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        System.Console.Beep(659, 150); // E
                        System.Threading.Thread.Sleep(50);
                        System.Console.Beep(523, 150); // C
                        System.Threading.Thread.Sleep(50);
                        System.Console.Beep(392, 300); // G
                    }
                    catch { }
                });
            }
            catch { }
        }

        public static void PlayDraw()
        {
            if (!IsSoundEnabled) return;

            try
            {
                // Neizšķirts - neitrāla skaņa
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        System.Console.Beep(440, 200); // A
                        System.Threading.Thread.Sleep(100);
                        System.Console.Beep(440, 200); // A
                    }
                    catch { }
                });
            }
            catch { }
        }

        public static void TestSound()
        {
            if (!IsSoundEnabled) return;

            try
            {
                System.Console.Beep(1000, 200);
            }
            catch { }
        }
    }
}