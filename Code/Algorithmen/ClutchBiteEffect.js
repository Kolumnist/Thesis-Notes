if(root["shiftShockTimer"] == null) root["shiftShockTimer"] = 0;
if(root["lastSlip"] == null) root["lastSlip"] = 0;

// CAR CALIBRATION
var BITEPOINT_MAX = $prop('SimTelemetryPlugin.CarCalibration.BITEPOINT_MAX') || 0.875;
var BITEPOINT_MIN = $prop('SimTelemetryPlugin.CarCalibration.BITEPOINT_MIN') || 0.5;
var TORQUE_MAX = $prop('SimTelemetryPlugin.CarCalibration.TORQUE_MAX') || 250;
var ENGINE_RPM_MAX = $prop('SimTelemetryPlugin.CarCalibration.ENGINE_RPM_MAX') || 5000;
var SLIP_RPM_MAX = $prop('SimTelemetryPlugin.CarCalibration.SLIP_RPM_MAX') || 2000;
var GEAR_N = $prop('SimTelemetryPlugin.CarCalibration.GEAR_N') || 1;

// MOTOR & MORE CALIBRATION
var FREQUENCY_MIN = $prop('SimTelemetryPlugin.Settings.FreqBase') || 8;
var FREQUENCY_MAX = $prop('SimTelemetryPlugin.Settings.FreqMod') || 30;
var AMPLITUDE_MAX = $prop('SimTelemetryPlugin.Settings.AMPLITUDE_MAX') || 1;
var torqueCalibration = $prop('SimTelemetryPlugin.Settings.TorqueCalibration') || 2;
var slipCalibration = $prop('SimTelemetryPlugin.Settings.SlipCalibration') || 2;

// INPUTS
var speed = $prop('SimTelemetryPlugin.SpeedKmh');
var engineRPM = $prop('SimTelemetryPlugin.EngineRPM');
var transmissionRPM = $prop('SimTelemetryPlugin.TransmissionRPM');
var transmissionGear = $prop('SimTelemetryPlugin.TransmissionGear');
var torque = $prop('SimTelemetryPlugin.EngineTorque');
var clutchPedalPosition = $prop('SimTelemetryPlugin.ClutchPedalPosition'); // 0.0 bis 1.0
var throttlePedalPosition = $prop('SimTelemetryPlugin.ThrottlePedalPosition'); // 0.0 bis 1.0
var engineRunning = $prop('SimTelemetryPlugin.EngineRunning');

var brakeAddition = 0;

// ---
switch(transmissionGear){
    case 0:
        transmissionRPM *= 7;
        break;
    case 1:
        break;
    case 2:
        transmissionRPM *= 7;
        break;
    case 3:
        transmissionRPM *= 4;
        break;
    case 4:
        transmissionRPM *= 2;
        break;
    case 5:
        transmissionRPM *= 1.6;
        break;
    case 6:
        transmissionRPM *= 1.2;
        break;
    case 7:
        transmissionRPM *= 0.9;
        break;
}

let slipRPM = engineRPM - transmissionRPM; // => Höherer Gang = transmissionRPM höher;

let engineRPM_n = Math.min(engineRPM / ENGINE_RPM_MAX, 1);
let slipRPM_n = Math.min(Math.abs(slipRPM) / (SLIP_RPM_MAX), 1); //+speed
let torque_n = Math.min(Math.abs(torque) / TORQUE_MAX, 1);

let slip_f = Math.pow(slipRPM_n, 1);
let torque_f = Math.pow(torque_n, 1);
let bitePoint_f = (clutchPedalPosition >= BITEPOINT_MIN) * (clutchPedalPosition <= BITEPOINT_MAX);
let gear_f = transmissionGear == GEAR_N ? 0 : 1;


let amplitudePlus = Math.min((slip_f + torque_f) * bitePoint_f * gear_f, 1) * MAX_AMPLITUDE;

let amplitude = Math.min(slip_f * torque_f * bitePoint_f * gear_f, 1) * MAX_AMPLITUDE;

let frequency = FREQUENCY_MIN + (FREQUENCY_MAX - FREQUENCY_MIN) * rpmNormalized;

// Note: Add Brake stuff Engine RPM Drop
// Note: Add Shift Early:
let lugging = transmissionGear >= GEAR_N+2 && speed < 20 && throttlePedalPosition > 0.5 && clutchPedalPosition < BITEPOINT_MIN;
let motorBrake = slipRPM < -10 && clutchPedalPosition < BITEPOINT_MAX;

if (lugging) 
{
    // Shifting too early
    amplitude = Math.min(Math.abs(slip_f-1) * torque_f * gear_f, 1) * MAX_AMPLITUDE;
    frequency = FREQUENCY_MIN;
} else if (slipRPM < -10 && clutchPedalPosition < BITEPOINT_MAX) {
    // Engine Braking
    amplitude *= Math.min(slip_f * torque_f * gear_f, 1);
    frequency = FREQUENCY_MIN;
}

// Shifting and Stall:

let slipDelta = Math.abs(slipRPM - root["lastSlip"]);
root["lastSlip"] = slipRPM;
if (slipDelta > 1500) 
    {
    root["shockTimer"] = 5;
} else if (motorBrake)
{
    root["shockTimer"] = slipRPM_n*5
}


if(root["shockTimer"] > 0) {
    // Shift Shock OR Stall Shock
    amplitude = Math.min(Math.abs(slip_f-1) * torque_f * gear_f, 1) * MAX_AMPLITUDE;
    frequency = FREQUENCY_MIN;
    root["shockTimer"]--;
}

return Math.round(frequency) + ";" + Math.round(amplitude) + "\n";
