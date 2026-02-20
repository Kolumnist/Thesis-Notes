if(root["shiftShockTimer"] == null) root["shiftShockTimer"] = 0;
if(root["lastSlip"] == null) root["lastSlip"] = 0;
if(root["previousGear"] == null) root["previousGear"] = 0;

// CAR CALIBRATION
var BITEPOINT_MAX = $prop('SimTelemetryPlugin.CarCalibration.BITEPOINT_MAX') || 0.875;
var BITEPOINT_MIN = $prop('SimTelemetryPlugin.CarCalibration.BITEPOINT_MIN') || 0.5;
var MAX_TORQUE = $prop('SimTelemetryPlugin.CarCalibration.MAX_TORQUE') || 250;
var MAX_ENGINE_RPM = $prop('SimTelemetryPlugin.CarCalibration.MAX_ENGINE_RPM') || 5000;
var MAX_SLIP_RPM = $prop('SimTelemetryPlugin.CarCalibration.MAX_SLIP_RPM') || 2000;
var GEAR_N = $prop('SimTelemetryPlugin.CarCalibration.GEAR_N') || 1;

// ACTUATOR & MORE CALIBRATION
var DRIVETRAIN_FREQUENCY = $prop('SimTelemetryPlugin.Settings.FreqBase') || 8;
var FREQUENCY_MODULATION = $prop('SimTelemetryPlugin.Settings.FreqMod') || 30;
var amplitudeCalibration = $prop('SimTelemetryPlugin.Settings.AmplitudeCalibration') || 1;
var torqueCalibration = $prop('SimTelemetryPlugin.Settings.TorqueCalibration') || 2;

// INPUTS
var speed = $prop('SimTelemetryPlugin.SpeedMs') * 3.6;
var engineRPM = $prop('SimTelemetryPlugin.EngineRPM');
var transmissionRPM = $prop('SimTelemetryPlugin.TransmissionRPM');
var gearInUse = $prop('SimTelemetryPlugin.GearInUse');
var torque = $prop('SimTelemetryPlugin.EngineTorque');
var clutch = $prop('SimTelemetryPlugin.ClutchPedalPosition'); // 0.0 bis 1.0
var throttle = $prop('SimTelemetryPlugin.ThrottlePedalPosition'); // 0.0 bis 1.0
var brake = $prop('SimTelemetryPlugin.BrakePedalPosition'); // 0.0 bis 1.0
var engineRunning = $prop('SimTelemetryPlugin.EngineRunning');

var brakeAddition = 0;

// ---
let slipRPM = engineRPM - transmissionRPM;

let engineRPM_n = Math.min(engineRPM / MAX_ENGINE_RPM, 1);
let slipRPM_n = Math.min(Math.abs(slipRPM) / (MAX_SLIP_RPM), 1); //+speed
let torque_n = Math.min(Math.abs(torque) / MAX_TORQUE, 1);

//let torqueFactor = 2 - (1 / (1 + torqueCalibration * torqueNormalized)); //hyperbl 1 - 1.66

let slip_f = Math.pow(slipRPM_n, 0.8);
let torque_f = Math.pow(torque_n, 0.8);
let bitePoint_f = (clutch >= BITEPOINT_MIN) * (clutch <= BITEPOINT_MAX);
let gear_f = gearInUse == GEAR_N ? 0 : 1;

let amplitude = Math.min(slip_f * torque_f * bitePoint_f * gear_f, 1) * MAX_AMPLITUDE;

// Note: Add Brake stuff Engine RPM Drop
// Note: Add Shift Early:
/* else if (gear > 2 && engineRPM < 1200 && throttle > 0.8 && clutch < 0.2) {
    // Shifting too early
    amplitude = 100;
    frequency = 7;
} */


let frequency = DRIVETRAIN_FREQUENCY + FREQUENCY_MODULATION * rpmNormalized;


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
