
# Anforderungsanalyse

Rahmen bezieht sich auf die Probleme, Lösungen und "Wie"-Aufgaben sowie eine Festhaltung der Software und Hardware mitsamt ihrer Anforderungen und dem Stand der Technik.

## Verfügbare Hard- und Software

### Hardware

- Standard ERM Motor (Fanatec v3i Pedals)
- DFRobot Mini Vibration Motor 10x2.7mm, 11000rpm,
3V Nennspannung, 50mA Nennstrom, 1.5-4.2V
- Fanatec v3i Pedals
- Arduino Uno und Nano

Fehlt: Motor Driver

### Mindestanforderungen

- Frequenz

### ERM Motoren

Latenz: ~100-200ms (https://makeabilitylab.github.io/physcomp/advancedio/vibromotor.html)

Frequenzen: ~47-283Hz

### LRA Motoren

Latenz: ~20-50ms (https://blog.ineedmotors.com/lra-vibration-motors-haptic-feedback/)

Frequenzen: ~60-320Hz

### TACHAMMER DRAKE (LF/MF/LFi Modell)

## Rahmenbedingungen

Frequenz Minimum 33

## Auswahl der Hard- und Software sowie Kombinationen

## Fragebogen des Reviews

## AI
To perform a requirements analysis for simulating the a new clutch's bite point vibration with haptic modules (ERMs), you need to objectively characterize the physical sensation in a real vehicle and then match those parameters with available haptic actuator specifications.
The process involves three main steps: characterizing the real-world vibration, defining the haptic system requirements, and selecting the appropriate ERM motor.
1. Characterize the Real-World Vibration
The first step is to measure the physical characteristics of the vibration in the actual Golf 8 clutch pedal at the bite point. A "new" clutch will still have some minimal, inherent vibration due to engine harmonics and the dual-mass flywheel system. 
Measure the Frequency (Hz): Use an accelerometer or vibration sensor attached to the clutch pedal to capture the frequency range of the vibration when the clutch is engaging (slipping to static friction transition). Clutch vibrations typically fall within a range, possibly between 100 Hz and 300 Hz, related to engine RPM harmonics.
Measure the Amplitude (G-force or m/s²): Determine the intensity of the vibration in terms of acceleration (G-force or meters per second squared). This quantifies how "strong" the vibration feels.
Identify the Duration and Location: Note the exact pedal position (displacement) where the vibration starts and stops, and how long it lasts during engagement. 
2. Define Haptic System Requirements
Translate the measured physical data into specific technical requirements for your haptic simulation system.
Target Frequency Range: The ERM motor must be able to operate effectively within the measured frequency range of the clutch vibration (e.g., 150 Hz to 200 Hz).
Target Amplitude: The system must be capable of generating at least the measured G-force or acceleration amplitude to feel realistic.
Response Time: ERM motors typically have a slow response time (50 to 100 ms rise/fall times), which might be acceptable for the relatively sustained feeling of a clutch bite point, but it's a key consideration. Linear Resonant Actuators (LRAs) offer faster response times if needed.
Control/Integration: Define how the haptic feedback will be triggered. This typically requires a control system (e.g., a microcontroller or PC software like SimHub) that reads the clutch pedal position sensor and activates the ERM motor driver only within the defined "bite point" travel range.
Mounting and Integration: Determine where the haptic module will be placed (e.g., directly on the pedal arm or the pedal plate) and ensure the form factor of the module is suitable. 
3. Select the Appropriate ERM Module
Using the requirements from the previous steps, choose a suitable ERM motor.
Check Datasheets: Review manufacturer datasheets for ERM motors, paying attention to:
Rated Voltage (typically 3V DC).
Operating Frequency Range.
Vibration Amplitude/G-force at rated voltage.
Physical Size/Form Factor.
Select a Driver Circuit: ERM motors are typically voltage-controlled, but require a dedicated haptic driver integrated circuit to manage the voltage, use overdrive to start quickly, and provide active braking for quick stops.
Prototype and Test: Build a prototype system and conduct user testing to subjectively evaluate if the chosen module and parameters realistically simulate the "feel" of the clutch bite point. 