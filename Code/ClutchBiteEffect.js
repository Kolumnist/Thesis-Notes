var clutch = $prop('GameRawData.Clutch'); // 0-1
var rpm = $prop('GameRawData.EngineRPM');
var transRpm = $prop('GameRawData.TransmissionRPM');
var gear = $prop('GameRawData.GearInUse');

// 1. Definiere den Schleifpunkt (Diesen Wert müssen wir durch Testen finden!)
// Beim Opel 8: Wo fängt er an zu rollen? Sagen wir mal bei 40% Pedalweg.
var bitePointStart = 0.3; 
var bitePointEnd = 0.6;

var maxEngineRPM = 5600;

// 3. Berechne theoretischen Schlupf (Drehzahl-Differenz)
// Wenn Gang = 0 (Neutral), kein Schlupf am Antriebsstrang (aber vllt leichtes Rütteln)
var slipIntensity = 0;

if (pedal > bitePointStart && pedal < bitePointEnd) {
    return slipIntensity;
}

if (gear > 0 && inBiteZone == 1) {
   slipIntensity = (rpm / maxEngineRPM) * 100; 
   
   // Hier später: Vergleich mit TransmissionRPM für Runterschalt-Effekt
}

return slipIntensity;