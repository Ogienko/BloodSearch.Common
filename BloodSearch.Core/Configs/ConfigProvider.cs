using System.Configuration;

namespace BloodSearch.Core.Configs {

    public static class ConfigProvider {

        public static string Get(string key) {
            return ConfigurationManager.AppSettings[key];
        }
    }
}