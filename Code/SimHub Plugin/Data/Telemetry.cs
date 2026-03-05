using Newtonsoft.Json;

namespace User.SimPlugin
{
    public class Telemetry
    {
        [JsonProperty("speedKmh")]
        public double SpeedKmh { get; set; }

        [JsonProperty("engineRpm")]
        public float EngineRPM { get; set; }

        [JsonProperty("transmissionRpm")]
        public int TransmissionRPM { get; set; }

        [JsonProperty("transmissionGear")]
        public int TransmissionGear { get; set; }

        [JsonProperty("engineTorque")]
        public float EngineTorque { get; set; }

        [JsonProperty("engineRunning")]
        public bool EngineRunning { get; set; }

        [JsonProperty("clutchPedalPosition")]
        public float ClutchPedalPosition { get; set; }

        [JsonProperty("gasPedalPosition")]
        public float ThrottlePedalPosition { get; set; }

        [JsonProperty("brakePedalPosition")]
        public float BrakePedalPosition { get; set; }        
    }
}
