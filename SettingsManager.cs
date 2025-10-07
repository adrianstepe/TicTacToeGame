using System;
using System.IO;
using System.Xml.Serialization;

namespace TicTacToeGame
{
    [Serializable]
    public class GameSettings
    {
        public bool SoundEnabled { get; set; }
        public int SoundVolume { get; set; }
        public ThemeMode Theme { get; set; }
        public int LastBoardSize { get; set; }
        public bool LastWasVsComputer { get; set; }
        public DifficultyLevel LastDifficulty { get; set; }

        public GameSettings()
        {
            // Default vērtības
            SoundEnabled = true;
            SoundVolume = 50;
            Theme = ThemeMode.Dark;
            LastBoardSize = 3;
            LastWasVsComputer = true;
            LastDifficulty = DifficultyLevel.Medium;
        }
    }

    public static class SettingsManager
    {
        private static string settingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TicTacToeGame",
            "settings.xml"
        );

        public static GameSettings CurrentSettings { get; private set; }

        static SettingsManager()
        {
            LoadSettings();
        }

        public static void LoadSettings()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
                    using (FileStream fs = new FileStream(settingsFilePath, FileMode.Open))
                    {
                        CurrentSettings = (GameSettings)serializer.Deserialize(fs);
                    }
                }
                else
                {
                    CurrentSettings = new GameSettings();
                    SaveSettings();
                }

                // Piemēro iestatījumus
                ApplySettings();
            }
            catch
            {
                CurrentSettings = new GameSettings();
            }
        }

        public static void SaveSettings()
        {
            try
            {
                string directory = Path.GetDirectoryName(settingsFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
                using (FileStream fs = new FileStream(settingsFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, CurrentSettings);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Neizdevās saglabāt iestatījumus: {ex.Message}",
                    "Kļūda",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
            }
        }

        public static void ApplySettings()
        {
            // Piemēro skaņu iestatījumus
            SoundManager.IsSoundEnabled = CurrentSettings.SoundEnabled;
            SoundManager.Volume = CurrentSettings.SoundVolume;

            // Piemēro tēmu
            ThemeManager.CurrentTheme = CurrentSettings.Theme;
        }
    }
}