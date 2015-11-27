using System.Configuration;

namespace FileImporter.Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static int GetCheckTimeout()
        {
            var configValue = ConfigurationManager.AppSettings["CheckInterval"];
            return configValue == null ? 5000 : int.Parse(configValue);
        }

        public static string GetWorkingDirectory()
        {
            return ConfigurationManager.AppSettings["WorkingDirectory"];
        }

        public static string GetLandingDirectory()
        {
            return ConfigurationManager.AppSettings["LandingDirectory"];
        }

        public static string GetPluginsPath()
        {
            return ConfigurationManager.AppSettings["PluginsPath"];
        }
    }
}
