namespace User.SimPlugin
{
    public class TestTelemetry
    {
        public bool TestModeOn { get; set; } = false;

        public double SpeedKmh { get; set; } = 0;

        public float EngineRPM { get; set; } = 0;

        public int TransmissionRPM { get; set; } = 0;

        public int TransmissionGear { get; set; } = 0;

        public float EngineTorque { get; set; } = 0;

        public bool EngineRunning { get; set; } = false;
        
        public float ClutchPedalPosition { get; set; } = 0;

        public float ThrottlePedalPosition { get; set; } = 0;

        public float BrakePedalPosition { get; set; } = 0;

    }
}
