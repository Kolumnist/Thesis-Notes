if(root["shiftShockTimer"] == null) root["shiftShockTimer"] = 0;
if(root["lastSlip"] == null) root["lastSlip"] = 0;

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
switch(gearInUse){
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

let engineRPM_n = Math.min(engineRPM / MAX_ENGINE_RPM, 1);
let slipRPM_n = Math.min(Math.abs(slipRPM) / (MAX_SLIP_RPM), 1); //+speed
let torque_n = Math.min(Math.abs(torque) / MAX_TORQUE, 1);

let slip_f = Math.pow(slipRPM_n, 1);
let torque_f = Math.pow(torque_n, 1);
let bitePoint_f = (clutch >= BITEPOINT_MIN) * (clutch <= BITEPOINT_MAX);
let gear_f = gearInUse == GEAR_N ? 0 : 1;

let amplitude = Math.min(slip_f * torque_f * bitePoint_f * gear_f, 1) * MAX_AMPLITUDE;

let frequency = DRIVETRAIN_FREQUENCY + FREQUENCY_MODULATION * rpmNormalized;

// Note: Add Brake stuff Engine RPM Drop
// Note: Add Shift Early:
let lugging = gearInUse >= GEAR_N+2 && speed < 20 && throttle > 0.5 && clutch < BITEPOINT_MIN;
let motorBrake = slipRPM < -10 && clutch < BITEPOINT_MAX;

if (lugging) 
{
    // Shifting too early
    amplitude = Math.min(Math.abs(slip_f-1) * torque_f * gear_f, 1) * MAX_AMPLITUDE;
    frequency = DRIVETRAIN_FREQUENCY;
} else if (slipRPM < -10 && clutch < BITEPOINT_MAX) {
    // Engine Braking
    amplitude *= Math.min(slip_f * torque_f * gear_f, 1);
    frequency = DRIVETRAIN_FREQUENCY;
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
    frequency = DRIVETRAIN_FREQUENCY;
    root["shockTimer"]--;
}

return Math.round(frequency) + ";" + Math.round(amplitude) + "\n";
