using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public class SettingsForm : Form
    {
        private Label titleLabel;
        private Panel settingsPanel;

        // Sound settings
        private Label soundLabel;
        private CheckBox soundEnabledCheckBox;
        private Label volumeLabel;
        private TrackBar volumeTrackBar;
        private Label volumeValueLabel;
        private Button testSoundButton;

        // Theme settings
        private Label themeLabel;
        private RadioButton lightThemeRadio;
        private RadioButton darkThemeRadio;

        // Buttons
        private Button saveButton;
        private Button cancelButton;

        public SettingsForm()
        {
            InitializeComponents();
            LoadCurrentSettings();
        }

        private void InitializeComponents()
        {
            this.Text = "⚙️ Iestatījumi";
            this.Size = new Size(500, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ThemeManager.GetBackground();

            // Title
            titleLabel = new Label();
            titleLabel.Text = "⚙️ Spēles Iestatījumi";
            titleLabel.AutoSize = false;
            titleLabel.Size = new Size(440, 50);
            titleLabel.Location = new Point(30, 20);
            titleLabel.Font = ThemeManager.GetFont(18, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.ForeColor = ThemeManager.GetText();
            this.Controls.Add(titleLabel);

            // Settings Panel
            settingsPanel = new Panel();
            settingsPanel.Size = new Size(440, 320);
            settingsPanel.Location = new Point(30, 80);
            settingsPanel.BackColor = ThemeManager.GetCardBackground();
            this.Controls.Add(settingsPanel);

            int yPos = 20;

            // === SOUND SECTION ===
            soundLabel = new Label();
            soundLabel.Text = "🔊 Skaņas Iestatījumi";
            soundLabel.AutoSize = false;
            soundLabel.Size = new Size(400, 30);
            soundLabel.Location = new Point(20, yPos);
            soundLabel.Font = ThemeManager.GetFont(13, FontStyle.Bold);
            soundLabel.ForeColor = ThemeManager.GetPrimary();
            settingsPanel.Controls.Add(soundLabel);
            yPos += 40;

            soundEnabledCheckBox = new CheckBox();
            soundEnabledCheckBox.Text = "Ieslēgt skaņas";
            soundEnabledCheckBox.AutoSize = false;
            soundEnabledCheckBox.Size = new Size(200, 30);
            soundEnabledCheckBox.Location = new Point(40, yPos);
            soundEnabledCheckBox.Font = ThemeManager.GetFont(11);
            soundEnabledCheckBox.ForeColor = ThemeManager.GetText();
            soundEnabledCheckBox.CheckedChanged += SoundEnabledCheckBox_CheckedChanged;
            settingsPanel.Controls.Add(soundEnabledCheckBox);
            yPos += 40;

            volumeLabel = new Label();
            volumeLabel.Text = "Skaļums:";
            volumeLabel.AutoSize = false;
            volumeLabel.Size = new Size(100, 30);
            volumeLabel.Location = new Point(40, yPos);
            volumeLabel.Font = ThemeManager.GetFont(11);
            volumeLabel.ForeColor = ThemeManager.GetText();
            settingsPanel.Controls.Add(volumeLabel);

            volumeValueLabel = new Label();
            volumeValueLabel.Text = "50%";
            volumeValueLabel.AutoSize = false;
            volumeValueLabel.Size = new Size(60, 30);
            volumeValueLabel.Location = new Point(350, yPos);
            volumeValueLabel.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            volumeValueLabel.ForeColor = ThemeManager.GetPrimary();
            volumeValueLabel.TextAlign = ContentAlignment.MiddleRight;
            settingsPanel.Controls.Add(volumeValueLabel);
            yPos += 35;

            volumeTrackBar = new TrackBar();
            volumeTrackBar.Minimum = 0;
            volumeTrackBar.Maximum = 100;
            volumeTrackBar.TickFrequency = 10;
            volumeTrackBar.Value = 50;
            volumeTrackBar.Size = new Size(370, 45);
            volumeTrackBar.Location = new Point(40, yPos);
            volumeTrackBar.ValueChanged += VolumeTrackBar_ValueChanged;
            settingsPanel.Controls.Add(volumeTrackBar);
            yPos += 50;

            testSoundButton = new Button();
            testSoundButton.Text = "🔔 Testēt Skaņu";
            testSoundButton.Size = new Size(150, 40);
            testSoundButton.Location = new Point(40, yPos);
            testSoundButton.Font = ThemeManager.GetFont(10, FontStyle.Bold);
            testSoundButton.Click += TestSoundButton_Click;
            ThemeManager.ApplyButtonStyle(testSoundButton);
            settingsPanel.Controls.Add(testSoundButton);
            yPos += 60;

            // === THEME SECTION ===
            themeLabel = new Label();
            themeLabel.Text = "🎨 Tēmas Iestatījumi";
            themeLabel.AutoSize = false;
            themeLabel.Size = new Size(400, 30);
            themeLabel.Location = new Point(20, yPos);
            themeLabel.Font = ThemeManager.GetFont(13, FontStyle.Bold);
            themeLabel.ForeColor = ThemeManager.GetPrimary();
            settingsPanel.Controls.Add(themeLabel);
            yPos += 40;

            lightThemeRadio = new RadioButton();
            lightThemeRadio.Text = "☀️ Gaišā Tēma";
            lightThemeRadio.AutoSize = false;
            lightThemeRadio.Size = new Size(150, 30);
            lightThemeRadio.Location = new Point(40, yPos);
            lightThemeRadio.Font = ThemeManager.GetFont(11);
            lightThemeRadio.ForeColor = ThemeManager.GetText();
            settingsPanel.Controls.Add(lightThemeRadio);

            darkThemeRadio = new RadioButton();
            darkThemeRadio.Text = "🌙 Tumšā Tēma";
            darkThemeRadio.AutoSize = false;
            darkThemeRadio.Size = new Size(150, 30);
            darkThemeRadio.Location = new Point(210, yPos);
            darkThemeRadio.Font = ThemeManager.GetFont(11);
            darkThemeRadio.ForeColor = ThemeManager.GetText();
            settingsPanel.Controls.Add(darkThemeRadio);

            // === BUTTONS ===
            cancelButton = new Button();
            cancelButton.Text = "❌ Atcelt";
            cancelButton.Size = new Size(200, 45);
            cancelButton.Location = new Point(40, 415);
            cancelButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            cancelButton.Click += (s, e) => this.Close();
            ThemeManager.ApplyButtonStyle(cancelButton);
            this.Controls.Add(cancelButton);

            saveButton = new Button();
            saveButton.Text = "✓ Saglabāt";
            saveButton.Size = new Size(200, 45);
            saveButton.Location = new Point(260, 415);
            saveButton.Font = ThemeManager.GetFont(11, FontStyle.Bold);
            saveButton.Click += SaveButton_Click;
            ThemeManager.ApplyButtonStyle(saveButton, true);
            this.Controls.Add(saveButton);
        }

        private void LoadCurrentSettings()
        {
            GameSettings settings = SettingsManager.CurrentSettings;

            soundEnabledCheckBox.Checked = settings.SoundEnabled;
            volumeTrackBar.Value = settings.SoundVolume;
            volumeValueLabel.Text = $"{settings.SoundVolume}%";

            if (settings.Theme == ThemeMode.Light)
                lightThemeRadio.Checked = true;
            else
                darkThemeRadio.Checked = true;

            UpdateVolumeControlsState();
        }

        private void SoundEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVolumeControlsState();
        }

        private void UpdateVolumeControlsState()
        {
            bool enabled = soundEnabledCheckBox.Checked;
            volumeTrackBar.Enabled = enabled;
            volumeLabel.Enabled = enabled;
            volumeValueLabel.Enabled = enabled;
            testSoundButton.Enabled = enabled;
        }

        private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            volumeValueLabel.Text = $"{volumeTrackBar.Value}%";
        }

        private void TestSoundButton_Click(object sender, EventArgs e)
        {
            // Temporarily apply settings for test
            bool oldEnabled = SoundManager.IsSoundEnabled;
            int oldVolume = SoundManager.Volume;

            SoundManager.IsSoundEnabled = soundEnabledCheckBox.Checked;
            SoundManager.Volume = volumeTrackBar.Value;

            SoundManager.TestSound();

            // Restore old settings
            SoundManager.IsSoundEnabled = oldEnabled;
            SoundManager.Volume = oldVolume;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Save settings
            SettingsManager.CurrentSettings.SoundEnabled = soundEnabledCheckBox.Checked;
            SettingsManager.CurrentSettings.SoundVolume = volumeTrackBar.Value;
            SettingsManager.CurrentSettings.Theme = lightThemeRadio.Checked ? ThemeMode.Light : ThemeMode.Dark;

            SettingsManager.SaveSettings();
            SettingsManager.ApplySettings();

            MessageBox.Show(
                "Iestatījumi veiksmīgi saglabāti!",
                "Pabeigts",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}