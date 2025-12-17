const CLUTCH_POINT_CENTER = 0.6;
const CLUTCH_POINT_WIDTH = 0.24;
const MAX_TORQUE = 270;
var MAX_SLIP_RPM = 800; // between 300 to 2500

var clutch = $prop('SimTelemetryPlugin.ClutchPedalPosition'); // 0.0 bis 1.0
var speed = $prop('SimTelemetryPlugin.SpeedMs') * 3.6;
var engineRPM = $prop('SimTelemetryPlugin.EngineRPM'); 
var transRPM = $prop('SimTelemetryPlugin.TransmissionRPM');
var torque = $prop('SimTelemetryPlugin.EngineTorque')
var gear = $prop('SimTelemetryPlugin.GearInUse'); // we need to calibrate this value somehow or have it defined as currently I do not know what 1 or 2 is.
var MAX_SLIP_RPM_LAUNCH = 2500;
var MAX_SLIP_RPM_SHIFT  = 5000;

// Get calculated each time
var bitePointDistance = 0;
var slipRPM = 0;
var slipNorm = 0;
var torqueNorm = 0;

// Unknowns. Need to be determined. Mostly by measuring or feel.
var frequencyModulation = 0.3; // 0.2 to 0.8 maybe even more or less
var DRIVETRAIN_FREQUENCY = 15; // between 5-30 hz
var optimalSlipCenter = 0.5 // between 0.3 to 0.7
var optimalSlipWidth = 0.2 // 0.1-0.4
var BASE_AMPLITUDE = 1 // can be anything I want. Its basically like my Audio Volume Regler 

// ---- Helpers ----
function clamp01(x) { return Math.max(0, Math.min(1, x)); }

slipRPM = Math.abs(engineRPM - transRPM);
slipNorm = clamp01(slipRPM / MAX_SLIP_RPM);
torqueNorm = clamp01(Math.abs(torque) / MAX_TORQUE);

function intensity() {	
    
    bitePointDistance = Math.abs(clutch - CLUTCH_POINT_CENTER) / CLUTCH_POINT_WIDTH;
    if (bitePointDistance > 1 || gear == 1) {
        return 0;
    }
    
    let bitePointFactor = Math.cos(bitePointDistance * Math.PI / 2);
    
    let slipDistanceFromCenter = Math.abs(slipNorm - 0.5) * 2;
    let slipFactor = Math.cos(slipDistanceFromCenter * Math.PI / 2) + 0.125;
    
    return Math.pow(bitePointFactor * slipFactor,0.45);
}

let frequency = DRIVETRAIN_FREQUENCY * (1 + frequencyModulation * slipNorm);

let amplitude = intensity() * BASE_AMPLITUDE * (0.5 + 0.5*torqueNorm);
return amplitude * 100;
//Math.abs(Math.sin(frequency*2*Math.PI))

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

*/