namespace User.SYNPLISimTelemetryPlugin
{
    public class CalibrationSettingsData
    {
        public float BITEPOINT_MAX { get; set; } = 0.875f;
        public float BITEPOINT_MIN { get; set; } = 0.5f;

        public int DRIVETRAIN_FREQUENCY { get; set; } = 8;
        public int FREQUENCY_MODULATION { get; set; } = 30;

        public int MAX_TORQUE { get; set; } = 250;
        public int MAX_ENGINE_RPM { get; set; } = 5000;
        public int MAX_SLIP_RPM { get; set; } = 2000;

        public float AmplitudeCalibration { get; set; } = 1;
        public float TorqueCalibration { get; set; } = 2;

        // Testmode
        public bool TestModeEnabled { get; set; } = false;
        public double TestClutch { get; set; } = 0.5;
        public double TestEngineRPM { get; set; } = 1000;
        public double TestTransmissionRPM { get; set; } = 1000;
        public double TestTorque { get; set; } = 50;
        public double TestGear { get; set; } = 50;
    }
}
