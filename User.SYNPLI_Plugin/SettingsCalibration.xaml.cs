using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Navigation;

namespace User.SYNPLISimTelemetryPlugin
{
    /// <summary>
    /// Interaction logic for SettingsCalibration.xaml
    /// </summary>
    public partial class SettingsCalibration : UserControl
    {
        public SimTelemetryPlugin Plugin { get; }

        public SettingsCalibration(SimTelemetryPlugin plugin)
        {
            InitializeComponent();
            this.Plugin = plugin;
            this.DataContext = this;

            // Header
            CalibrationPanel.Children.Add(new Label
            {
                FontSize = 18,
                Foreground = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, 0, 0, 20),
                Content = "Clutch Haptic Calibration",
            });

            CalibrationPanel.Children.Add(CreateSliderDockPanel("BITEPOINT_MAX", 0, 1, 0.01, (double) Plugin.Settings.BITEPOINT_MAX));
            CalibrationPanel.Children.Add(CreateSliderDockPanel("BITEPOINT_MIN", 0, 1, 0.01, (double) Plugin.Settings.BITEPOINT_MIN));
            // Make Min not able to cross Max maybe do a double slider or smth.

            CalibrationPanel.Children.Add(CreateSliderDockPanel("DRIVETRAIN_FREQUENCY", 0, 30, 1, (double) Plugin.Settings.DRIVETRAIN_FREQUENCY));
            CalibrationPanel.Children.Add(CreateSliderDockPanel("FREQUENCY_MODULATION", 0, 100, 1, (double) Plugin.Settings.FREQUENCY_MODULATION));
            CalibrationPanel.Children.Add(CreateSliderDockPanel("MAX_TORQUE", 0, 500, 10, (double) Plugin.Settings.MAX_TORQUE));
            CalibrationPanel.Children.Add(CreateSliderDockPanel("MAX_ENGINE_RPM", 0, 8000, 100, (double) Plugin.Settings.MAX_ENGINE_RPM));
            CalibrationPanel.Children.Add(CreateSliderDockPanel("MAX_SLIP_RPM", 0, 5000, 100, (double) Plugin.Settings.MAX_SLIP_RPM));
            CalibrationPanel.Children.Add(CreateSliderDockPanel("AmplitudeCalibration", 0, 1, 0.01, (double) Plugin.Settings.AmplitudeCalibration));
            CalibrationPanel.Children.Add(CreateSliderDockPanel("TorqueCalibration", 0, 10, 1, (double) Plugin.Settings.TorqueCalibration));

            TestPanel.Children.Add(new CheckBox
            {
                IsChecked = Plugin.Settings.TestModeEnabled,
                Foreground = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, 0, 0, 20),
                Content = "Enable Test Mode (Ignore Game Data)"
            });

            TestPanel.Children.Add(CreateSliderDockPanel("TestClutch", 0, 1, 0.01, (double)Plugin.Settings.TestClutch));
            TestPanel.Children.Add(CreateSliderDockPanel("TestEngineRPM", 0, 8000, 100, (double)Plugin.Settings.TestEngineRPM));
            TestPanel.Children.Add(CreateSliderDockPanel("TestTransmissionRPM", 0, 8000, 100, (double)Plugin.Settings.TestTransmissionRPM));
            TestPanel.Children.Add(CreateSliderDockPanel("TestTorque", 0, 250, 10, (double)Plugin.Settings.TestTorque));
            TestPanel.Children.Add(CreateSliderDockPanel("TestGear", 0, 6, 1, (double)Plugin.Settings.TestGear));
        }

        private DockPanel CreateSliderDockPanel(string name, double min, double max, double frequency, double defaultValue)
        {
            DockPanel dock = new DockPanel { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10) };
            Label label = new Label { Content = name, FontWeight = FontWeights.Bold };
            DockPanel.SetDock(label, Dock.Top);

            Slider slider = new Slider
            {
                Maximum = max,
                Minimum = min,
                TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight,
                TickFrequency = frequency,
                IsSnapToTickEnabled = true,
                Name = name,
                Tag = name,
                Value = defaultValue
            };
            slider.ValueChanged += OnValueChanged;

            TextBox valueBox = new TextBox
            {
                Width = 40,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Right
            };
            DockPanel.SetDock(valueBox, Dock.Right);
            Binding binding = new Binding("Value") { Source = slider, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            valueBox.SetBinding(TextBox.TextProperty, binding);

            dock.Children.Add(label);
            dock.Children.Add(valueBox);
            dock.Children.Add(slider);

            return dock;
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider s = sender as Slider;
            string parameter = s.Tag.ToString(); // This gets "Brightness" or "Contrast"

            switch(parameter)
            {
                case "BITEPOINT_MAX":
                    this.Plugin.Settings.BITEPOINT_MAX = (int)e.NewValue;
                    break;
                case "BITEPOINT_MIN":
                    this.Plugin.Settings.BITEPOINT_MIN = (int)e.NewValue;
                    break;
                case "DRIVETRAIN_FREQUENCY":
                    this.Plugin.Settings.DRIVETRAIN_FREQUENCY = (int)e.NewValue;
                    break;
                case "FREQUENCY_MODULATION":
                    this.Plugin.Settings.FREQUENCY_MODULATION = (int)e.NewValue;
                    break;
                case "MAX_TORQUE":
                    this.Plugin.Settings.MAX_TORQUE = (int)e.NewValue;
                    break;
                case "MAX_ENGINE_RPM":
                    this.Plugin.Settings.MAX_ENGINE_RPM = (int)e.NewValue;
                    break;
                case "MAX_SLIP_RPM":
                    this.Plugin.Settings.MAX_SLIP_RPM = (int)e.NewValue;
                    break;
                case "AmplitudeCalibration":
                    this.Plugin.Settings.AmplitudeCalibration = (int)e.NewValue;
                    break;
                case "TorqueCalibration":
                    this.Plugin.Settings.TorqueCalibration = (int)e.NewValue;
                    break;
            }
        }
    }
}
