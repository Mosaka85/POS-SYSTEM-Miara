using Miara.Processor_Class;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public static class SettingsManager
    {
        private static readonly string settingsFilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Miara", "settings.xml");
        public static ApplicationSettings LoadSettings()
        {
            try
            {
                string directory = Path.GetDirectoryName(settingsFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                if (File.Exists(settingsFilePath))
                {
                    var serializer = new XmlSerializer(typeof(ApplicationSettings));
                    using (var stream = new FileStream(settingsFilePath, FileMode.Open))
                    {
                        return serializer.Deserialize(stream) as ApplicationSettings;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new ApplicationSettings();
        }

        public static void SaveSettings(ApplicationSettings settings)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ApplicationSettings));
                using (var stream = new FileStream(settingsFilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, settings);
                }
                MessageBox.Show("Settings saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}