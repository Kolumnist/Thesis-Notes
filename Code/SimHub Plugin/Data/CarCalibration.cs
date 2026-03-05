using Newtonsoft.Json;

namespace User.SimPlugin
{
    public class CarCalibration
    {
        [JsonProperty("")]
        public double BITEPOINT_MAX { get; set; } = 0.8;

        [JsonProperty("")]
        public double BITEPOINT_MIN { get; set; } = 0.45;

        [JsonProperty("engineTorqueMax")]
        public int TORQUE_MAX { get; set; } = 250;
        
        [JsonProperty("engineRpmMax")]
        public int ENGINE_RPM_MAX { get; set; } = 5000;

        [JsonProperty("")]
        public int SLIP_RPM_MAX { get; set; } = 2000;
        
        [JsonProperty("")]
        public int GEAR_N { get; set; } = 1;
    }
}
