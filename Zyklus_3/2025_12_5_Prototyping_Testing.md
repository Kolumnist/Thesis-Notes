# Prototyping

Pedale einbauen

Buttkicker einbauen

Simulation nutzen

## Benötigte Telemetriedaten

- Clutch Pedal Position
- Engine RPM
- Clutch Slip Speed (Ground Speed, Wheel Angular Velocity)
- Engine Torque
- Gear in Use

Optimisieren der Verbindung zum Vermeiden von "packet losses" oder jittering.

Programmieren der Schnittstelle zwischen Simhub und den Pedalen, vorläufig mit UDP. In Zukunft evtl. über Memory Share. UDP wurde aufgrund der Einfachheit genutzt.
