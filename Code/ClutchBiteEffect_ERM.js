if(root["shiftShockTimer"] == null) root["shiftShockTimer"] = 0;
if(root["lastSlip"] == null) root["lastSlip"] = 0;
if(root["previousGear"] == null) root["previousGear"] = 0;

// Calibration Values
var BITEPOINT_MAX = $prop('SimTelemetryPlugin.Settings.BiteMax') || 0.875;
var BITEPOINT_MIN = $prop('SimTelemetryPlugin.Settings.BiteMin') || 0.5;
var DRIVETRAIN_FREQUENCY = $prop('SimTelemetryPlugin.Settings.FreqBase') || 8;
var FREQUENCY_MODULATION = $prop('SimTelemetryPlugin.Settings.FreqMod') || 30;
var MAX_TORQUE = $prop('SimTelemetryPlugin.Settings.MaxTorque') || 250;
var MAX_ENGINE_RPM = $prop('SimTelemetryPlugin.Settings.MaxRPM') || 5000;
var MAX_SLIP_RPM = $prop('SimTelemetryPlugin.Settings.MaxSlip') || 2000;
var amplitudeCalibration = $prop('SimTelemetryPlugin.Settings.AmplitudeCalibration') || 1;
var torqueCalibration = $prop('SimTelemetryPlugin.Settings.TorqueCalibration') || 2;

// INPUTS
var clutch = $prop('') ? $prop('SimTelemetryPlugin.Settings.TestClutch') : $prop('SimTelemetryPlugin.ClutchPedalPosition'); // 0.0 bis 1.0
var speed = $prop('SimTelemetryPlugin.SpeedMs') * 3.6;
var engineRPM = $prop('SimTelemetryPlugin.EngineRPM');
var transRPM = $prop('SimTelemetryPlugin.TransmissionRPM');
var torque = $prop('SimTelemetryPlugin.EngineTorque');
var gear = $prop('SimTelemetryPlugin.GearInUse'); // we need to calibrate this value somehow or have it defined as currently I do not know what 1 or 2 is.

var brakeAddition = 0;

// ---
var rpmNormalized = Math.min(engineRPM / MAX_ENGINE_RPM, 1);

// Note: Add Brake stuff
// Note: Add Shift Early:
/* else if (gear > 2 && engineRPM < 1200 && throttle > 0.8 && clutch < 0.2) {
    // Shifting too early
    amplitude = 100;
    frequency = 7;
} */

let bitePointFactor = (clutch >= BITEPOINT_MIN) * (clutch <= BITEPOINT_MAX);

let slipRPM = engineRPM - transRPM;
let slipFactor = Math.pow(Math.min(Math.abs(slipRPM) / (MAX_SLIP_RPM+speed)), 0.45);

let torqueNormalized = Math.min(Math.abs(torque) / MAX_TORQUE);
let torqueFactor = 2 - (1 / (1 + torqueCalibration * torqueNormalized)); //hyperbl 1 - 1.66

let bitePointIntensity = Math.min(bitePointFactor * slipFactor * torqueFactor, 1) * (gear != 1);

let frequency = DRIVETRAIN_FREQUENCY + FREQUENCY_MODULATION * rpmNormalized;

let amplitude = bitePointIntensity * amplitudeCalibration * 100;

// Shifting and Stall:

let slipDelta = Math.abs(slipRPM - root["lastSlip"]);
if (slipDelta > 700 || (engineRPM <= 600 && slipDelta > 100)) {
    root["shockTimer"] = 5;
}

if(root["shockTimer"] > 0) {
    // Shift Shock OR Stall Shock
    amplitude = 100;
    frequency = 5;
    root["shockTimer"]--;
} else if (slipRPM < -100 && clutch < BITEPOINT_MAX) {
    // Engine Braking
    amplitude = slipFactor*100;
    frequency = 30 + (rpmNormalized * FREQUENCY_MODULATION);
}

root["lastSlip"] = slipRPM;

return Math.round(frequency) + ";" + Math.round(amplitude) + "\n";
