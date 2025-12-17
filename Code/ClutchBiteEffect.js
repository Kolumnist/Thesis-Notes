var clutch = $prop('SimTelemetryPlugin.ClutchPedalPosition'); // 0.0 bis 1.0
var engineRPM = $prop('SimTelemetryPlugin.EngineRPM'); 
var transRPM = $prop('SimTelemetryPlugin.TransmissionRPM');
var torque = $prop('SimTelemetryPlugin.Torque');
var gear = $prop('SimTelemetryPlugin.GearInUse'); // we need to calibrate this value somehow or have it defined as currently I do not know what 1 or 2 is.

const CLUTCH_POINT_CENTER = 0.75;
const CLUTCH_POINT_WIDTH = 0.1;
const MAX_TORQUE = 2000;

// Get calculated each time
var bitePointDistanceFromCenter = 0;
var slipRPM = 0;
var slipNorm = 0;
var slipFactor = 0;
var torqueNorm = 0;

// Unknowns. Need to be determined. Mostly by measuring or feel.
var frequencyModulation = 0.3; // 0.2 to 0.8 maybe even more or less
var DRIVETRAIN_FREQUENCY = 15; // between 5-30 hz
var MAX_SLIP_RPM = 2000; // between 300 to 4000
var optimalSlipCenter = 0.5 // between 0.3 to 0.7
var optimalSlipWidth = 0.2 // 0.1-0.4
var BASE_AMPLITUDE = 1 // can be anything I want. Its basically like my Audio Volume Regler lol xd

if (gear == 1 || slipRPM <= 100 || engineRPM <= 100) {

}

// ---- Helpers ----
function clamp01(x) { return Math.max(0, Math.min(1, x)); }

slipRPM = Math.abs(engineRPM - transRPM);
slipNorm = clamp01(slipRPM / MAX_SLIP_RPM);

function intensity() {

    bitePointDistanceFromCenter = Math.abs(clutch - CLUTCH_POINT_CENTER) / CLUTCH_POINT_WIDTH;
    slipFactor = Math.abs(slipNorm - 0.5) / 0.4;
    

    if (bitePointDistanceFromCenter > 1 || slipFactor > 1) {
        return 0;
    }
    let intensity = Math.cos(bitePointDistanceFromCenter * Math.PI / 2) * Math.pow(slipFactor, 0.6);
    return intensity;
}

function frequency() {
    // ---- Frequency (RPM → Hz) ----
    var freqHz = rpm / 60.0;
    var freq = Math.round(freqHz);

    // ###

    return DRIVETRAIN_FREQUENCY * (1 * frequencyModulation * slipNorm);
}

function amplitude() {
    
    torqueNorm = clamp01(Math.abs(torque) / MAX_TORQUE); // Yes you could add gain to make it "always rumble a bit" depends

    // ---- Amplitude ----
    var slipShaped = Math.pow(slipNorm, 0.8);    // slight curve
    var torqueGain = 0.3 + 0.7 * torqueNorm;    // 0.3..1.0

    var ampNorm = clamp01(slipShaped * torqueGain * intensity());
    var amp = Math.round(ampNorm * 255.0);

    // ##
    console.log(gear + "---- \n")
    console.log(intensity() + "; " + torqueNorm + "; " + slipFactor + "; ");
    return intensity() * torqueNorm * slipFactor * BASE_AMPLITUDE;
}

return frequency() + ";" + amplitude() + "\n";

// ---- Shape (based on harshness) ----
var harshness = slipNorm * torqueNorm;
var shape = 0;
if (ampNorm > 0.02) {
    if (harshness < 0.4)      shape = 1; // sine
    else                      shape = 2; // square
}

// ---- Build serial string ----
var ampStr  = format(amp, "000");
var freqStr = format(freq, "000");
return ampStr + ";" + freqStr + ";" + shape + "\n";
