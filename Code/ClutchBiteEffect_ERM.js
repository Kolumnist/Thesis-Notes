if(root["shiftShockTimer"] == null) root["shiftShockTimer"] = 0;
if(root["lastSlip"] == null) root["lastSlip"] = 0;
if(root["previousGear"] == null) root["previousGear"] = 0;

const BITEPOINT_MAX = 0.875;
const BITEPOINT_MIN = 0.5;

const DRIVETRAIN_FREQUENCY = 8; // between 5-30 hz
const FREQUENCY_MODULATION = 30;

const MAX_TORQUE = 250;
const MAX_ENGINE_RPM = 5000;
const MAX_SLIP_RPM = 2000; // between 300 to 2500

// calibration values
var amplitudeCalibration = 1;
var torqueCalibration = 2;

// INPUTS
var clutch = $prop('SimTelemetryPlugin.ClutchPedalPosition'); // 0.0 bis 1.0
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
