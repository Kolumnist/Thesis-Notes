using GameReaderCommon;
using Newtonsoft.Json;
using SimHub.Plugins;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using User.SimPlugin;

[PluginDescription("...TODO")]
[PluginAuthor("Collin Hoss")]
[PluginName("Sim Telemetry Plugin")]
public class SimTelemetryPlugin : IPlugin, IDataPlugin, IWPFSettingsV2
{
    public PluginManager PluginManager { get; set; }

    public ImageSource PictureIcon => this.ToIcon(User.SimPlugin.Properties.Resources.sdkmenuicon);
    public string LeftMenuTitle => "Sim Haptic Telemetry";

    private const int LISTENING_PORT = 29373;
    private EndPoint _endPoint;
    private UdpClient _udpClient;
    private byte[] _receivedBytes = new byte[2048];

    public CarCalibration carCalibration;
    public HapticSettings hapticSettings;
    public TestTelemetry testTelemetry;
    private Telemetry _telemetry;

    private string jsonTelemetry = "";

    /// <summary>
    /// Called once after plugins startup
    /// </summary>
    /// <param name="pluginManager"></param>
    public void Init(PluginManager pluginManager)
    {
        SimHub.Logging.Current.Info("Starting plugin");
        Type _pluginType = this.GetType();

        _endPoint = new IPEndPoint(IPAddress.Any, LISTENING_PORT);
        _udpClient = new UdpClient(LISTENING_PORT);

        carCalibration = this.ReadCommonSettings<CarCalibration>("CarCalibration", () => new CarCalibration());
        hapticSettings = this.ReadCommonSettings<HapticSettings>("CarCalibration", () => new HapticSettings());
        testTelemetry = new TestTelemetry();

        _telemetry = new Telemetry
        {
            SpeedMs = 0,
            EngineRPM = 0,
            TransmissionRPM = 0,
            GearInUse = 0,
            EngineTorque = 0,
            ClutchPedalPosition = 0,
            ThrottlePedalPosition = 0,
            BrakePedalPosition = 0,
            EngineRunning = false
        };

        // Input and Telemetry
        pluginManager.AttachDelegate(nameof(_telemetry.SpeedMs), _pluginType,
            valueProvider: () => _telemetry.SpeedMs);
        pluginManager.AttachDelegate(nameof(_telemetry.EngineRPM), _pluginType,
            valueProvider: () => _telemetry.EngineRPM);
        pluginManager.AttachDelegate(nameof(_telemetry.TransmissionRPM), _pluginType,
            valueProvider: () => _telemetry.TransmissionRPM);
        pluginManager.AttachDelegate(nameof(_telemetry.GearInUse), _pluginType,
            valueProvider: () => _telemetry.GearInUse);
        pluginManager.AttachDelegate(nameof(_telemetry.EngineTorque), _pluginType,
            valueProvider: () => _telemetry.EngineTorque);
        pluginManager.AttachDelegate(nameof(_telemetry.ClutchPedalPosition), _pluginType,
            valueProvider: () => _telemetry.ClutchPedalPosition);
        pluginManager.AttachDelegate(nameof(_telemetry.ThrottlePedalPosition), _pluginType,
            valueProvider: () => _telemetry.ThrottlePedalPosition);
        pluginManager.AttachDelegate(nameof(_telemetry.BrakePedalPosition), _pluginType,
            valueProvider: () => _telemetry.BrakePedalPosition);
        pluginManager.AttachDelegate(nameof(_telemetry.EngineRunning), _pluginType,
            valueProvider: () => _telemetry.EngineRunning);

        // CAR CALIBRATION
        pluginManager.AttachDelegate("CarCalibration."+nameof(carCalibration.BITEPOINT_MAX), _pluginType, 
            valueProvider: () => carCalibration.BITEPOINT_MAX);
        pluginManager.AttachDelegate("CarCalibration." + nameof(carCalibration.BITEPOINT_MIN), _pluginType, 
            valueProvider: () => carCalibration.BITEPOINT_MIN);
        pluginManager.AttachDelegate("CarCalibration." + nameof(carCalibration.MAX_TORQUE), _pluginType, 
            valueProvider: () => carCalibration.MAX_TORQUE);
        pluginManager.AttachDelegate("CarCalibration." + nameof(carCalibration.MAX_ENGINE_RPM), _pluginType, 
            valueProvider: () => carCalibration.MAX_ENGINE_RPM);
        pluginManager.AttachDelegate("CarCalibration." + nameof(carCalibration.MAX_SLIP_RPM), _pluginType, 
            valueProvider: () => carCalibration.MAX_SLIP_RPM);
        pluginManager.AttachDelegate("CarCalibration." + nameof(carCalibration.GEAR_N), _pluginType,
            valueProvider: () => carCalibration.GEAR_N);

        // HAPTIC CALIBRATION
        pluginManager.AttachDelegate("HapticSettings." + nameof(hapticSettings.BaseFrequency), _pluginType,
            valueProvider: () => hapticSettings.BaseFrequency);
        pluginManager.AttachDelegate("HapticSettings." + nameof(hapticSettings.MaxFrequency), _pluginType,
            valueProvider: () => hapticSettings.MaxFrequency);
        pluginManager.AttachDelegate("HapticSettings." + nameof(hapticSettings.MaxAmplitude), _pluginType,
            valueProvider: () => hapticSettings.MaxAmplitude);
        pluginManager.AttachDelegate("HapticSettings." + nameof(hapticSettings.TorqueCalibration), _pluginType,
            valueProvider: () => hapticSettings.TorqueCalibration);
        pluginManager.AttachDelegate("HapticSettings." + nameof(hapticSettings.SlipCalibration), _pluginType,
            valueProvider: () => hapticSettings.SlipCalibration);
        pluginManager.AttachDelegate("HapticSettings." + nameof(hapticSettings.ShockTimer), _pluginType,
            valueProvider: () => hapticSettings.ShockTimer);
        pluginManager.AttachDelegate("HapticSettings." + nameof(hapticSettings.ShockSlipDelta), _pluginType,
            valueProvider: () => hapticSettings.ShockSlipDelta);
    }

    /// <summary>
    /// Called one time per game data update, contains all normalized game data,
    /// </summary>
    /// <param name="pluginManager"></param>
    /// <param name="data">Current game data, including current and previous data frame.</param>
    public void DataUpdate(PluginManager pluginManager, ref GameData data)
    {
        int packetLength = 0;

        if (testTelemetry.TestModeOn)
        {
            _telemetry.SpeedMs = testTelemetry.SpeedMs;
            _telemetry.EngineRPM = testTelemetry.EngineRPM;
            _telemetry.TransmissionRPM = testTelemetry.TransmissionRPM;
            _telemetry.GearInUse = testTelemetry.GearInUse;
            _telemetry.EngineTorque = testTelemetry.EngineTorque;
            _telemetry.ClutchPedalPosition = testTelemetry.ClutchPedalPosition;
            _telemetry.ThrottlePedalPosition = testTelemetry.ThrottlePedalPosition;
            _telemetry.BrakePedalPosition = testTelemetry.BrakePedalPosition;
            _telemetry.EngineRunning = true;
            return;
        }

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
            JsonConvert.PopulateObject(jsonTelemetry, _telemetry);
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
        this.SaveCommonSettings("CarCalibration", carCalibration);
        _udpClient.Close();
    }

    public Control GetWPFSettingsControl(PluginManager pluginManager)
    {
        return new SettingsCalibration(this);
    }

    //XCOPY /Y /R "C:\Program Files (x86)\SimHub\PluginSdk\User.SYNPLI_Plugin\bin\Debug\User.SYNPLISimTelemetryPlugin*" "C:\Program Files (x86)\SimHub"
}