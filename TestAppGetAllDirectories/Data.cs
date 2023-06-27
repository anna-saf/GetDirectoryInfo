using System.Text.Json;
using System.IO;

namespace TestAppGetAllDirectories
{
    internal class Data
    {
        private const string pathToConfig = @"Data\Config.json";
        private static Config config;

        public Config GetConfig()
        {
            CheckConfig();
            return config;
        }

        private void ReadConfig()
        {
            string configText = File.ReadAllText(pathToConfig);
            config = JsonSerializer.Deserialize<Config>(configText);
        }

        public string GetStyle()
        {
            CheckConfig();
            string stylePath = config.PATH_TO_STYLE;

            if (File.Exists(stylePath))
            {
                using (StreamReader streamReader = new StreamReader(stylePath))
                {
                    string style = streamReader.ReadToEnd();
                    return style;
                }
            }
            return null;
        } 

        private void CheckConfig()
        {
            if (config == null)
            {
                ReadConfig();
            }
        }
    }
}
