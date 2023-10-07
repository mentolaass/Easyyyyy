using Easyyyyy.Models;
using Easyyyyy.Views;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;

namespace Easyyyyy
{
    public partial class App : Application
    {
        public static Configuration configApplication { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            loadConfig(); var MainWindow = new MainWindow();

            MainWindow.Closed += (s, e_) =>
            {
                Application.Current.Shutdown();
            };

            MainWindow.Show();
        }

        private string dirLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easyyyyy";
        private static string configLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easyyyyy\\config.json";
        private void loadConfig()
        {
            if (!Directory.Exists(dirLocation))
            {
                Directory.CreateDirectory(dirLocation);
            }

            if (!File.Exists(configLocation))
            {
                File.Create(configLocation).Close();

                // add json
                JObject config = new JObject(
                    new JProperty("toggle_mode", false),
                    new JProperty("default_clicks", true),
                    new JProperty("count_cps", 7),
                    new JProperty("enabled_random", true),
                    new JProperty("bind_key", null),
                    new JProperty("is_left_click", true),
                    new JProperty("int_bind_key", 0));

                File.WriteAllText(configLocation, config.ToString());
            }

            using (var reader = new StreamReader(configLocation))
            {
                configApplication = Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(reader.ReadToEnd());

                reader.Dispose();
                reader.Close();
            }
        }

        public static void updateConfig()
        {
            if (!File.Exists(configLocation))
            {
                File.Create(configLocation).Close();
            }
            
            // add json
            JObject config = new JObject(
                new JProperty("toggle_mode", configApplication.isToggleMode),
                new JProperty("default_clicks", configApplication.isDefaultClicks),
                new JProperty("count_cps", configApplication.countCPS),
                new JProperty("enabled_random", configApplication.isEnabledRandom),
                new JProperty("bind_key", configApplication.bindKey),
                new JProperty("is_left_click", configApplication.isLeftClick),
                new JProperty("int_bind_key", configApplication.intBindKey));

            File.WriteAllText(configLocation, config.ToString());
        }
    }
}
