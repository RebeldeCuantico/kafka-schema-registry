using Newtonsoft.Json;

namespace JsonConsole
{
    public class Vehicle
    {
        [JsonRequired] 
        [JsonProperty("registration")]
        public string Registration { get; set; }

        [JsonRequired]
        [JsonProperty("speed")]
        public int Speed { get; set;}

        [JsonRequired]
        [JsonProperty("coordinates")]
        public string Coordinates { get; set; }
    }
}
