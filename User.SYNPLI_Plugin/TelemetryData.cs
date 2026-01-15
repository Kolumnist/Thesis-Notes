using Newtonsoft.Json;

namespace User.SYNPLISimTelemetryPlugin
{
    public class TelemetryData
    {

        [JsonProperty("Game")]
        public string GameName { get; set; } = "SYNPLI";

        [JsonProperty("speedKmh")]
        public double SpeedMs { get; set; }

        [JsonProperty("engineRpm")]
        public float EngineRPM { get; set; }

        [JsonProperty("transmissionRpm")]
        public int TransmissionRPM { get; set; }

        [JsonProperty("gearInUse")]
        public int GearInUse { get; set; }

        [JsonProperty("engineTorque")]
        public float EngineTorque { get; set; }

        [JsonProperty("clutchPedalPosition")]
        public float ClutchPedalPosition { get; set; }
    }
}
