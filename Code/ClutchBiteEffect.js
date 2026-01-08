// Constants - Adjust these to your car's profile
const BITE_POINT = 0.7;       // Position of the bite point (0-1)
const BITE_WIDTH = 0.15;      // How "thick" the bite zone is
const MAX_SLIP_RPM = 2500;    // RPM diff where vibration reaches max intensity
const BASE_FREQ = 15;         // Minimum vibration frequency in Hz
const FREQ_SCALAR = 0.04;     // How much RPM increases frequency (RPM * 0.04)

function calculateProfessionalHaptics(data) {
    // 1. EXTRACT DATA (From UDP)
    const { engineRPM, transRPM, clutchPos, throttle, brake, torque } = data;

    // 2. CLUTCH ENGAGEMENT CURVE (Gaussian/Bell Curve)
    // This makes the center of the bite point strongest and tapers off smoothly.
    const distFromBite = Math.abs(clutchPos - BITE_POINT);
    const biteFactor = Math.exp(-Math.pow(distFromBite / BITE_WIDTH, 2));

    if (biteFactor < 0.01) return { amplitude: 0, frequency: 0 };

    // 3. SLIP CALCULATION
    const slipRPM = Math.abs(engineRPM - transRPM);
    // Use a power function (0.7) to make the buildup feel more "mechanical" and less linear
    const slipIntensity = Math.pow(Math.min(slipRPM / MAX_SLIP_RPM, 1.0), 0.7);

    // 4. LOAD MULTIPLIER (Torque & Brakes)
    // Torque makes the vibration "thicker". Brakes add "tension".
    const loadMultiplier = 1.0 + (throttle * 0.8) + (brake * 0.4);

    // 5. STALL SHUDDER (The "Chug")
    // If RPM is very low (e.g., < 800), add a low-frequency surge to simulate stalling.
    let stallShudder = 0;
    if (engineRPM < 900) {
        stallShudder = (1.0 - (engineRPM / 900)) * 0.5;
    }

    // --- FINAL OUTPUTS ---
    
    // Total Amplitude: How much the pedal shakes
    const amplitude = biteFactor * (slipIntensity + stallShudder) * loadMultiplier;

    // Frequency: The "texture" of the vibration
    // High RPM = High frequency buzz; Low RPM = Low frequency thumping
    const frequency = BASE_FREQ + (engineRPM * FREQ_SCALAR);

    return {
        amplitude: clamp01(amplitude), // 0.0 to 1.0
        frequency: frequency           // Result in Hz (e.g., 20Hz to 150Hz)
    };
}

function clamp01(val) { return Math.min(Math.max(val, 0), 1); }