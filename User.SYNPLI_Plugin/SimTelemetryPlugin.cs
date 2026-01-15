using GameReaderCommon;
using Newtonsoft.Json;
using SimHub.Plugins;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using User.SYNPLISimTelemetryPlugin;

[PluginDescription("...TODO")]
[PluginAuthor("Collin Hoss")]
[PluginName("SINPLY Sim Telemetry Plugin")]
public class SimTelemetryPlugin : IPlugin, IDataPlugin, IWPFSettingsV2
{
    public PluginManager PluginManager { get; set; }

    public CalibrationSettingsData Settings;

    public ImageSource PictureIcon => this.ToIcon(User.SYNPLISimTelemetryPlugin.Properties.Resources.sdkmenuicon);

    public string LeftMenuTitle => "Sim Haptic Telemetry";

    public const int LISTENING_PORT = 20777;

    private EndPoint _endPoint;
    private UdpClient _udpClient;
    private byte[] _receivedBytes = new byte[2048];
    private Type _pluginType;
    private TelemetryData _telemetryData;

    private string jsonTelemetry = "";

    /// <summary>
    /// Called once after plugins startup
    /// </summary>
    /// <param name="pluginManager"></param>
    public void Init(PluginManager pluginManager)
    {
        SimHub.Logging.Current.Info("Starting plugin");
        Settings = this.ReadCommonSettings<CalibrationSettingsData>("HapticSettings", () => new CalibrationSettingsData());

        _endPoint = new IPEndPoint(IPAddress.Any, LISTENING_PORT);
        _udpClient = new UdpClient(LISTENING_PORT);

        _telemetryData = new TelemetryData
        {
            SpeedMs = 0,
            EngineRPM = 0,
            TransmissionRPM = 0,
            GearInUse = 0,
            EngineTorque = 0,
            ClutchPedalPosition = 0
        };
        _pluginType = this.GetType();

        // Input Values
        pluginManager.AttachDelegate("SpeedMs", _pluginType,
            valueProvider: () => _telemetryData.SpeedMs);
        pluginManager.AttachDelegate("EngineRPM", _pluginType,
            valueProvider: () => _telemetryData.EngineRPM);
        pluginManager.AttachDelegate("TransmissionRPM", _pluginType,
            valueProvider: () => _telemetryData.TransmissionRPM);
        pluginManager.AttachDelegate("GearInUse", _pluginType,
            valueProvider: () => _telemetryData.GearInUse);
        pluginManager.AttachDelegate("EngineTorque", _pluginType,
            valueProvider: () => _telemetryData.EngineTorque);
        pluginManager.AttachDelegate("ClutchPedalPosition", _pluginType,
            valueProvider: () => _telemetryData.ClutchPedalPosition);

        // Settings Calibration Values
        pluginManager.AttachDelegate("Settings.BITEPOINT_MAX", _pluginType, 
            valueProvider: () => Settings.BITEPOINT_MAX);
        pluginManager.AttachDelegate("Settings.BITEPOINT_MIN", _pluginType, 
            valueProvider: () => Settings.BITEPOINT_MIN);
        pluginManager.AttachDelegate("Settings.DRIVETRAIN_FREQUENCY", _pluginType, 
            valueProvider: () => Settings.DRIVETRAIN_FREQUENCY);
        pluginManager.AttachDelegate("Settings.FREQUENCY_MODULATION", _pluginType, 
            valueProvider: () => Settings.FREQUENCY_MODULATION);
        pluginManager.AttachDelegate("Settings.MAX_TORQUE", _pluginType, 
            valueProvider: () => Settings.MAX_TORQUE);
        pluginManager.AttachDelegate("Settings.MAX_ENGINE_RPM", _pluginType, 
            valueProvider: () => Settings.MAX_ENGINE_RPM);
        pluginManager.AttachDelegate("Settings.MAX_SLIP_RPM", _pluginType, 
            valueProvider: () => Settings.MAX_SLIP_RPM);
        pluginManager.AttachDelegate("Settings.AmplitudeCalibration", _pluginType, 
            valueProvider: () => Settings.AmplitudeCalibration);
        pluginManager.AttachDelegate("Settings.TorqueCalibration", _pluginType, 
            valueProvider: () => Settings.TorqueCalibration);

        // Test Values
        pluginManager.AttachDelegate("Settings.TestModeEnabled", _pluginType,
            valueProvider: () => Settings.TestModeEnabled);
        pluginManager.AttachDelegate("Settings.TestClutch", _pluginType,
            valueProvider: () => Settings.TestClutch);
        pluginManager.AttachDelegate("Settings.TestEngineRPM", _pluginType,
            valueProvider: () => Settings.TestEngineRPM);
        pluginManager.AttachDelegate("Settings.TestTransmissionRPM", _pluginType,
            valueProvider: () => Settings.TestTransmissionRPM);
        pluginManager.AttachDelegate("Settings.TestTorque", _pluginType,
            valueProvider: () => Settings.TestTorque);
        pluginManager.AttachDelegate("Settings.TestGear", _pluginType,
            valueProvider: () => Settings.TestGear);
    }

    /// <summary>
    /// Called one time per game data update, contains all normalized game data,
    /// </summary>
    /// <param name="pluginManager"></param>
    /// <param name="data">Current game data, including current and previous data frame.</param>
    public void DataUpdate(PluginManager pluginManager, ref GameData data)
    {
        int packetLength = 0;

        _udpClient.Client.Blocking = true;
        while (_udpClient.Available > 0)
        {
            packetLength = _udpClient.Client.ReceiveFrom(_receivedBytes, ref _endPoint);
        }
        if (packetLength == 0) return;

        try
        {
            _ = _udpClient.Client.ReceiveFrom(_receivedBytes, ref _endPoint);
            _udpClient.Client.Blocking = false;
            jsonTelemetry = Encoding.UTF8.GetString(_receivedBytes);
            JsonConvert.PopulateObject(jsonTelemetry, _telemetryData);
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"ERROR: Socket receiver not blocked. Details: {ex.Message}");
        }
        catch (JsonReaderException ex)
        {
            Console.WriteLine($"ERROR: Malformed JSON data. Details: {ex.Message}");
        }
        catch (JsonSerializationException ex)
        {
            Console.WriteLine($"ERROR: JSON data structure mismatch. Details: {ex.Message}");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"ERROR: Null argument provided. Details: {ex.Message}");
        }
    }

    /// <summary>
    /// Called at plugin manager stop, close/dispose anything needed here !
    /// </summary>
    /// <param name="pluginManager"></param>
    public void End(PluginManager pluginManager)
    {
        this.SaveCommonSettings("HapticSettings", Settings);
        _udpClient.Close();
    }

    public Control GetWPFSettingsControl(PluginManager pluginManager)
    {
        return new SettingsCalibration(this);
    }

    //XCOPY /Y /R "C:\Program Files (x86)\SimHub\PluginSdk\User.SYNPLI_Plugin\bin\Debug\User.SYNPLISimTelemetryPlugin*" "C:\Program Files (x86)\SimHub"
}