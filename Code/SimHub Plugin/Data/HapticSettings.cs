using Newtonsoft.Json;

namespace User.SimPlugin
{
    public class HapticSettings
    {
        public int FREQUENCY_MIN { get; set; } = 12; //DRIVETRAIN_FREQUENCY
        public int FREQUENCY_MAX { get; set; } = 30;
        public float AMPLITUDE_MAX { get; set; } = 1;
        public float TorqueCalibration { get; set; } = 1;
        public float SlipCalibration { get; set; } = 1;
        public int ShockTimer { get; set; } = 5;
        public int ShockSlipDelta { get; set; } = 1000;
    }
}
