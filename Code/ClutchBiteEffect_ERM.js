const BITEPOINT_CENTER = 0.6;
//const BITEPOINT_WIDTH = 0.25;
const BITEPOINT_MAX = 0.875;
const BITEPOINT_MIN = 0.3;

const DRIVETRAIN_FREQUENCY = 10; // between 5-30 hz

const MAX_TORQUE = 270;
const MAX_ENGINE_RPM = 5000;
const MAX_SLIP_RPM = 2000; // between 300 to 2500

const FREQUENCY_MODULATION = 20;
const BASE_AMPLITUDE = 1;

var clutch = $prop('SimTelemetryPlugin.ClutchPedalPosition'); // 0.0 bis 1.0
var speed = $prop('SimTelemetryPlugin.SpeedMs') * 3.6;
var engineRPM = $prop('SimTelemetryPlugin.EngineRPM');
var transRPM = $prop('SimTelemetryPlugin.TransmissionRPM');
var torque = $prop('SimTelemetryPlugin.EngineTorque');
var gear = $prop('SimTelemetryPlugin.GearInUse'); // we need to calibrate this value somehow or have it defined as currently I do not know what 1 or 2 is.

var MAX_SLIP_RPM_SHIFT = 5000;

// Clutch Bite Point
var clutchDistance = 0;
var noBitePoint = 0;

var brakeAddition = 0;

var slipRPM = 0;

// normalized values
var rpmNormalized = 0;
var slipNormalized = 0;
var torqueNormalized = 0;

// ---- Helpers ----
function clamp01(x) {
    return Math.max(0, Math.min(1, x));
}

// Add Brake stuff

function clutchBitePointIntensity() {
    slipRPM = Math.abs(engineRPM - transRPM);
    slipNormalized = clamp01(slipRPM / MAX_SLIP_RPM, 1);
    let slipFactor = Math.pow(slipNormalized * (1 - Math.abs(slipNormalized - 0.6)) + 0.1, 0.4);

    torqueNormalized = clamp01(Math.abs(torque) / MAX_TORQUE);
    let torqueFactor = 1 + 0.4 * torqueNormalized;

    let range = (clutch <= BITEPOINT_CENTER) ? (BITEPOINT_CENTER - BITEPOINT_MIN) : (BITEPOINT_MAX - BITEPOINT_CENTER);
    clutchDistance = Math.abs(clutch - BITEPOINT_CENTER); // [0 ; BITEPOINT_CENTER or 1]
    let clutchDistanceNormalized = Math.min(clutchDistance / range, 1);
    let bitePointFactor = Math.pow(1.0 - clutchDistanceNormalized, 0.8); // [1 ; 0]
    
    let bitePointIntensity = Math.max(slipFactor * bitePointFactor * torqueFactor, 1) * (gear != 1);

    return bitePointIntensity;
}

rpmNormalized = engineRPM / MAX_ENGINE_RPM;
let frequency = DRIVETRAIN_FREQUENCY + FREQUENCY_MODULATION * rpmNormalized; // [30; ~14]

let amplitude = clutchBitePointIntensity() * BASE_AMPLITUDE * 100;

if (amplitude != 0 && engineRPM <= 600) {
    //abwürgen
    amplitude = 100;
    frequency = 7;
}

return Math.round(frequency) + ";" + Math.round(amplitude) + "\n";


//Slip Shock Code
/*
var rpmDelta = engineRPM - prevEngineRPM;
var rpmDropRate = Math.abs(rpmDelta);
function shiftShock() {
    if (rpmDropRate > 800 && clutch > 0.6) {
        return clamp01(rpmDropRate / 3000);
    }
    return 0;
}

return Math.max(
    intensity(),
    shiftShock() * 1.5
);
function frequency1() {
    var base = DRIVETRAIN_FREQUENCY * (1 + frequencyModulation * slipNorm);
    return base + shiftShock() * 20;
}var sharpShift = Math.pow(shiftShock(), 0.15);
amplitude = Math.max(amplitude, sharpShift * 1.2);

// --- torque conditioning ---
var TORQUE_FLOOR = 0.35;
var TORQUE_DECAY = 0.92;

var rawTorqueNorm = clamp01(Math.abs(torque) / MAX_TORQUE);

torqueFiltered = Math.max(
    rawTorqueNorm,
    torqueFiltered * TORQUE_DECAY
);
You’re simulating inertia, not cheating.
torqueNorm = Math.max(torqueFiltered, TORQUE_FLOOR);

*/