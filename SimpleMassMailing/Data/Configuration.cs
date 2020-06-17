using System;
using System.Collections.Generic;

namespace SimpleMassMailing.Data
{
    public class Configuration
    {
        public const string CONFIG_FILE = "config.ini";
        private readonly IDictionary<string, string> AllConfiguration;

        public Configuration(string[] args)
        {
            AllConfiguration = ReadConfiguration();

            // Override by command line arguments.
            // Ex. -login=... -password=...
            foreach (var item in args)
            {
                var keyValue = item.Split("=");
                if (keyValue.Length == 2)
                {
                    var key = keyValue[0].Replace("-", "");
                    var value = keyValue[1].Trim();
                    if (AllConfiguration.ContainsKey(key))
                        AllConfiguration[key] = value;
                }
            }
        }

        public string Smtp => GetConfigurationValue("smtp");
        public string Port => GetConfigurationValue("port");
        public bool Ssl => Convert.ToBoolean(GetConfigurationValue("ssl"));
        public string From => GetConfigurationValue("from");
        public string FromDisplayName => GetConfigurationValue("fromdisplayname");
        public string Cc => GetConfigurationValue("cc");
        public string Login => GetConfigurationValue("login");
        public string Password => GetConfigurationValue("password");
        public string ContentFile => GetConfigurationValue("contentfile");
        public string Attachment => GetConfigurationValue("Attachment");
        public string DataFile => GetConfigurationValue("datafile");
        public bool PromptBeforeSend => Convert.ToBoolean(GetConfigurationValue("PromptBeforeSend"));

        private string GetConfigurationValue(string key)
        {
            return AllConfiguration.ContainsKey(key.ToLower()) ? AllConfiguration[key.ToLower()] : String.Empty;
        }

        private IDictionary<string, string> ReadConfiguration()
        {
            var lines = System.IO.File.ReadAllLines(CONFIG_FILE);
            var config = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                int separatorIndex = line.IndexOf('=');
                if (separatorIndex > 0)
                {
                    config.Add(line.Substring(0, separatorIndex).Trim().ToLower(), line.Substring(separatorIndex + 1).Trim());
                }
                else
                {
                    throw new ArgumentException("Invalid file format of config.ini file. Use <key>=<value> only.");
                }
            }

            if (!config.ContainsKey("smtp") ||
                !config.ContainsKey("port") ||
                !config.ContainsKey("ssl") ||
                !config.ContainsKey("password") ||
                !config.ContainsKey("from") ||
                !config.ContainsKey("contentfile") ||
                !config.ContainsKey("datafile"))
            {
                throw new ArgumentException("The config.ini file must contains SMTP, PORT, SSL, LOGIN, PASSWORD, FROM, CONTENTFILE, DATAFILE.");
            }

            return config;
        }
    }
}
