using SimHub.Plugins.DataPlugins.ShakeItWind.Settings;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;

namespace User.SimPlugin
{
    /// <summary>
    /// Interaction logic for SettingsCalibration.xaml
    /// </summary>
    public partial class SettingsCalibration : UserControl
    {
        private SimTelemetryPlugin _plugin { get; }

        private Brush foreground = Brushes.White;

        public SettingsCalibration(SimTelemetryPlugin plugin)
        {
            InitializeComponent();
            this._plugin = plugin;
            this.DataContext = this;

            // Header
            CalibrationPanel.Children.Add(new Label
            {
                FontSize = 18,
                Foreground = foreground,
                Margin = new Thickness(0, 0, 0, 20),
                Content = "Clutch Haptic Calibration",
            });

            //car calibration
            CalibrationPanel.Children.Add(new Label
            {
                FontSize = 18,
                Foreground = foreground,
                Margin = new Thickness(0, 20, 0, 20),
                Content = "Car Calibration",
            });

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.carCalibration.BITEPOINT_MAX),
                min: 0, max: 1, frequency: 0.01,
                valueGetter: () => (double)_plugin.carCalibration.BITEPOINT_MAX,
                onSliderChanged: (newValue) => 
                {
                    if (newValue >= _plugin.carCalibration.BITEPOINT_MIN)
                        _plugin.carCalibration.BITEPOINT_MAX = newValue;
                }
            ));

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.carCalibration.BITEPOINT_MIN),
                min: 0, max: 1, frequency: 0.01,
                valueGetter: () => (double)_plugin.carCalibration.BITEPOINT_MIN,
                onSliderChanged: (newValue) => 
                {
                    if (newValue <= _plugin.carCalibration.BITEPOINT_MAX)
                        _plugin.carCalibration.BITEPOINT_MIN = newValue;
                }
            ));
            // Make Min not able to cross Max maybe do a double slider or smth.

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.carCalibration.TORQUE_MAX),
                min: 0, max: 500, frequency: 10,
                valueGetter: () => _plugin.carCalibration.TORQUE_MAX,
                onSliderChanged: (newValue) => _plugin.carCalibration.TORQUE_MAX = (int) newValue
            ));

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.carCalibration.ENGINE_RPM_MAX),
                min: 0, max: 8000, frequency: 100,
                valueGetter: () => _plugin.carCalibration.ENGINE_RPM_MAX,
                onSliderChanged: (newValue) => _plugin.carCalibration.ENGINE_RPM_MAX = (int)newValue
            ));
            
            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.carCalibration.SLIP_RPM_MAX),
                min: 0, max: 5000, frequency: 100,
                valueGetter: () => _plugin.carCalibration.SLIP_RPM_MAX,
                onSliderChanged: (newValue) => _plugin.carCalibration.SLIP_RPM_MAX = (int)newValue
            ));

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.carCalibration.GEAR_N),
                min: -1, max: 7, frequency: 1,
                valueGetter: () => _plugin.carCalibration.GEAR_N,
                onSliderChanged: (newValue) => _plugin.carCalibration.GEAR_N = (int)newValue
            ));

            // Haptic Settings
            CalibrationPanel.Children.Add(new Label
            {
                FontSize = 18,
                Foreground = foreground,
                Margin = new Thickness(0, 20, 0, 20),
                Content = "Haptic Settings",
            });

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.hapticSettings.FREQUENCY_MIN),
                min: 5, max: 20, frequency: 1,
                valueGetter: () => (double)_plugin.hapticSettings.FREQUENCY_MIN,
                onSliderChanged: (newValue) => _plugin.hapticSettings.FREQUENCY_MIN = (int)newValue
            ));

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.hapticSettings.AMPLITUDE_MAX),
                min: 0, max: 120, frequency: 1,
                valueGetter: () => (double)_plugin.hapticSettings.AMPLITUDE_MAX,
                onSliderChanged: (newValue) => _plugin.hapticSettings.AMPLITUDE_MAX = (int)newValue
            ));
            // Make Min not able to cross Max maybe do a double slider or smth.

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.hapticSettings.TorqueCalibration),
                min: 0, max: 1, frequency: 0.01,
                valueGetter: () => _plugin.hapticSettings.TorqueCalibration,
                onSliderChanged: (newValue) => _plugin.hapticSettings.TorqueCalibration = (float)newValue
            ));

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.hapticSettings.SlipCalibration),
                min: 0, max: 1, frequency: 0.01,
                valueGetter: () => _plugin.hapticSettings.SlipCalibration,
                onSliderChanged: (newValue) => _plugin.hapticSettings.SlipCalibration = (float)newValue
            ));

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.hapticSettings.ShockTimer),
                min: 0, max: 30, frequency: 1,
                valueGetter: () => _plugin.hapticSettings.ShockTimer,
                onSliderChanged: (newValue) => _plugin.hapticSettings.ShockTimer = (int)newValue
            ));

            CalibrationPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.hapticSettings.ShockSlipDelta),
                min: 0, max: 5000, frequency: 100,
                valueGetter: () => _plugin.hapticSettings.ShockSlipDelta,
                onSliderChanged: (newValue) => _plugin.hapticSettings.ShockSlipDelta = (int)newValue
            ));

            // TestPanel

            var testModeToggle = new ToggleButton
            {
                IsChecked = _plugin.testTelemetry.TestModeOn,
                Foreground = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, 0, 0, 20),
                Content = "Enable Test Mode (Ignore Game Data)"
            };
            testModeToggle.Checked += (sender, e) => _plugin.testTelemetry.TestModeOn = true;
            testModeToggle.Unchecked += (sender, e) => _plugin.testTelemetry.TestModeOn = false;
            TestPanel.Children.Add(testModeToggle);

            TestPanel.Children.Add(new Label
            {
                FontSize = 18,
                Foreground = foreground,
                Margin = new Thickness(0, 0, 0, 20),
                Content = "Test Values",
            });

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.SpeedKmh),
                min: 0, max: 100, frequency: 5,
                valueGetter: () => (double)_plugin.testTelemetry.SpeedKmh,
                onSliderChanged: (newValue) => _plugin.testTelemetry.SpeedKmh = newValue
            ));

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.EngineRPM),
                min: 0, max: 8000, frequency: 100,
                valueGetter: () => (double)_plugin.testTelemetry.EngineRPM,
                onSliderChanged: (newValue) => _plugin.testTelemetry.EngineRPM = (float)newValue
            ));

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.TransmissionRPM),
                min: 0, max: 8000, frequency: 100,
                valueGetter: () => _plugin.testTelemetry.TransmissionRPM,
                onSliderChanged: (newValue) => _plugin.testTelemetry.TransmissionRPM = (int)newValue
            ));

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.TransmissionGear),
                min: 0, max: 7, frequency: 1,
                valueGetter: () => _plugin.testTelemetry.TransmissionGear,
                onSliderChanged: (newValue) => _plugin.testTelemetry.TransmissionGear = (int)newValue
            ));

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.EngineTorque),
                min: 0, max: 500, frequency: 10,
                valueGetter: () => _plugin.testTelemetry.EngineTorque,
                onSliderChanged: (newValue) => _plugin.testTelemetry.EngineTorque = (int)newValue
            ));

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.ClutchPedalPosition),
                min: 0, max: 1, frequency: 0.01,
                valueGetter: () => _plugin.testTelemetry.ClutchPedalPosition,
                onSliderChanged: (newValue) => _plugin.testTelemetry.ClutchPedalPosition = (float)newValue
            ));

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.ThrottlePedalPosition),
                min: 0, max: 1, frequency: 0.01,
                valueGetter: () => _plugin.testTelemetry.ThrottlePedalPosition,
                onSliderChanged: (newValue) => _plugin.testTelemetry.ThrottlePedalPosition = (float)newValue
            ));

            TestPanel.Children.Add(CreateSliderDockPanel
            (
                name: nameof(_plugin.testTelemetry.BrakePedalPosition),
                min: 0, max: 1, frequency: 0.01,
                valueGetter: () => _plugin.testTelemetry.BrakePedalPosition,
                onSliderChanged: (newValue) => _plugin.testTelemetry.BrakePedalPosition = (float)newValue
            ));
        }

        private DockPanel CreateSliderDockPanel(string name, double min, double max, double frequency, Func<double> valueGetter, Action<double> onSliderChanged)
        {
            DockPanel dock = new DockPanel { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10) };

            var label = CreateLabelForSlider(name);
            var slider = CreateSlider(name, min, max, frequency, valueGetter(), onSliderChanged);
            var textBox = CreateTextBoxForSlider(slider);

            dock.Children.Add(label);
            dock.Children.Add(textBox);
            dock.Children.Add(slider);

            return dock;
        }

        private Label CreateLabelForSlider(string name)
        {
            Label label = new Label { Content = name, Foreground = this.foreground, FontWeight = FontWeights.Bold };
            DockPanel.SetDock(label, Dock.Top);
            return label;
        }

        private Slider CreateSlider(string name, double min, double max, double frequency, double defaultValue, Action<double> onSliderChanged)
        {
            Slider slider = new Slider
            {
                Maximum = max,
                Minimum = min,
                TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight,
                TickFrequency = frequency,
                IsSnapToTickEnabled = true,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 8, 0),
                Name = name,
                Tag = name,
                Value = defaultValue
            };

            slider.ValueChanged += (object sender, RoutedPropertyChangedEventArgs<double> e) =>
            {
                onSliderChanged(e.NewValue);
            };

            return slider;
        }

        private TextBox CreateTextBoxForSlider(Slider slider)
        {
            TextBox textBox = new TextBox
            {
                Width = 45,
                Foreground = foreground,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0, 0, 0, 1),
                BorderBrush = Brushes.Gray,
                TextAlignment = TextAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center
            };
            DockPanel.SetDock(textBox, Dock.Right);
            Binding textBoxToSliderBinding = new Binding("Value") { Source = slider, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            textBox.SetBinding(TextBox.TextProperty, textBoxToSliderBinding);
            return textBox;
        }
    }
}
