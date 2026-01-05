using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Miara.Models;

namespace Miara.Services
{
    public class ConfigService
    {
        private readonly string _configFile;

        public ConfigService(string configFile)
        {
            _configFile = configFile;
        }

        public bool ConfigExists => File.Exists(_configFile);

        public async Task<LoginInfo> LoadLoginInfoAsync(CancellationToken token)
        {
            if (!File.Exists(_configFile))
                throw new FileNotFoundException("Configuration file not found.", _configFile);

            // Read the entire XML file into a string (NO LOCKING)
            string xmlContent;
            using (var stream = new FileStream(_configFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream))
            {
                xmlContent = await reader.ReadToEndAsync();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));

            using (StringReader stringReader = new StringReader(xmlContent))
            {
                return (LoginInfo)serializer.Deserialize(stringReader);
            }
        }

        public string BuildConnectionString(LoginInfo info)
        {
            return $"Data Source={info.DataSource};" +
                   $"Initial Catalog={info.SelectedDatabase};" +
                   $"User ID={info.Username};" +
                   $"Password={info.Password};" +
                   $"TrustServerCertificate=True";
        }
    }
}
