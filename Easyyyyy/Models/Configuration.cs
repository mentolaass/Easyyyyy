using Newtonsoft.Json;

namespace Easyyyyy.Models
{
    public class Configuration
    {
        [JsonProperty("toggle_mode")]
        public bool isToggleMode { get; set; }

        [JsonProperty("default_clicks")]
        public bool isDefaultClicks { get; set; }

        [JsonProperty("count_cps")]
        public int countCPS { get; set; }

        [JsonProperty("enabled_random")]
        public bool isEnabledRandom { get; set; }

        [JsonProperty("int_bind_key")]
        public int intBindKey { get; set; }

        [JsonProperty("bind_key")]
        public string bindKey { get; set; }
    }
}
