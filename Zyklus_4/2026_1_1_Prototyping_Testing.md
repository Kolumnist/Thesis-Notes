# Prototyping

## Anmerkungen

Ticket vergessen zu erstellen:

- Neue Pedale einbauen

Außerdem sollte der Buttkicker eingebaut werden. Dieser wurde dann mit Simhub als Audioquelle verbunden und getestet. Da dieser gleichzeitig dazugeschaltet werden kann und deutlich stärker ist wie der VCA wurde alles sehr unsicher.

Custom Level zum Testen mit Berg Anfahren erhalten.

Fehler im neuesten Build besteht weiterhin.

Aufgrund von vielen Faktoren und Verwirrung ist Zyklus 3 gescheitert. Durch das ändern der Pedale und hinzufügen des Buttkickers mussten diese getestet werden... aber eigentlich auch nicht es wurde dennoch getan von mir. Das Ziel ist verloren gegangen während des Zyklus und anstatt den neuen Aktuator zu testen wurde maximal an der Logik gefeilt. Allerdings wurde alles gleichzeitig gemacht.

Simhub und der Arduino sind verbunden und es scheint gut und vor allem schnell zu funktionieren!

Logik anpassen so dass es sich anfühlt wie das was ich spürte UND die vorigen Reviews beachten.

## Tickets abarbeiten

Nach 7 Tagen wurde der Zyklus beendet. Es gab keine zusätzlichen Aufgaben

Zwei der Effekte wurden nicht umgesetzt aufgrund fehlender Daten.

Review musste allerdings verschoben werden. Fokus auf Präsentation.

### Szenarien definieren

Es ist auch immer das Gegenteil möglich.

Anfahren & Schalten sind die Hauptszenarien und folgendes sind Unterszenarien:

| Szenario | Vibrations-Art | Frequenz | Intensität |
| :--- | :--- | :--- | :--- |
| **Normaler Schleifpunkt** | Feines Kribbeln | Hoch | Niedrig |
| **Hohe Drehzahl (viel Gas)** | Raues, aggressives Summen | Sehr Hoch | Mittel bis Hoch |
| **Kurz vor dem Abwürgen** | Grobes Schlagen / Ruckeln | Niedrig | Sehr Hoch |
| **Bremse & Schleifpunkt** | Starkes, "angespanntes" Zittern | Hoch | Hoch |
| --- | --- | --- | --- |
| **Kupplung voll oder gar nicht getreten** | Fast keine (ruhig) | - | Minimal |
| **Gang N** | Ruhig | - | minimal |
| --- | --- | --- | --- |
| **Runterschalten (Motorbremse)** | Aggressives Sägen / Surren,  Motor wird vom Getriebe "hochgerissen" | Sehr Hoch | Hoch |
| **Zu frühes Hochschalten** | Dumpfes Wummern / Klopfen,  Motor läuft untertourig (nahe Abwürggrenze) | Niedrig | Mittel |
| **Schalten unter Last (Gas geben)** | Kurzer, harter Schlag, Drehmomentspitze trifft auf die Reibscheibe | - | Hoch |

### Effekte definieren

Einige Definitionen sind bereits in der Tabelle vorhanden aber um genauer für jeden "Effekt" oder jeden Einfluss zu sprechen nutze ich dieses Ticket:

#### Normaler Schleifpunkt

Ab 80% des Kupplungsweges fängt das Simulations-Auto zu rollen an.
Die Vibration muss daher kurz vorher spürbar sein.
Außerdem sollte die Vibration bei ungefähr 70/60% am Stärksten spürbar sein. (theoretisch)

Wenn wir davon ausgehen das bei 40/30% ungefähr die Kupplung gut sitzt dann sollte dort die Schleifpunktvibration enden.

Relevant für diese Vibration ist der Schlupf, Drehmoment und die Position des Kupplungspedals.

Idee: Schlupf und Drehmoment deutlich stärker spürbar. Schleifpunkt wird nun durch diese zwei bestimmt und selbst nicht wirklich.

#### Hohe Drehzahl Hohe Frequenz

Mit höherer Drehzahl wird die Frequenz höher. Da der Schlupf höher sein sollte wird die Vibration außerdem intensiver.

#### Kurz vor dem Abwürgen, Ruckeln und Grobes Schlagen & Schalten unter Last

Die Intensität wird auf das Maximum gebracht und die Frequenz auf ein Minimum. Eine weitere Möglichkeit ist es den DRV2605 auszunutzen aber dann ist es weniger modular nutzbar.

Für das Schalten wird auf die Drehzahländerung pro frame geachtet. Wenn die Drehzahländerung pro frame hoch genug ist wird der Effekt aktiv.

#### Zu frühes Hochschalten

Muss im nächsten Zyklus fertiggestellt werden, da ohne Sensordaten des Gaspedals, dass zu frohe Hochschalten physikalisch wenig Sinn macht.

#### Motorbremse

Wenn Schlupfdrehzahl negativ ist, also Motordrehzahl niedriger ist wie Transmission Drehzahl, dann sollte es eine starke kurze Vibration geben.

### Logik umschreiben

#### Arduino Logik Änderungen

...

#### Taktische Entscheidungen für die JS Logik

Remap Methode probiert um Vibration am Schleifpunkt vom Gefühl besser zu machen:

    bitePointFactor = Math.max(((1 + BITEPOINT_WIDTH) - ((1 + BITEPOINT_WIDTH) / (1 - BITEPOINT_WIDTH)) * clutchDistance) - BITEPOINT_WIDTH, 0); // [1 ; 0]

Wirklich arg viel besser war das nicht und nutzte den Bitepoint width. Ich möchte aber keine Gaussian Bell Curve also Idee verworfen.

Zweiter Versuch für Schleifpunkt ist schön aber ich brauche eigentlich den Wert nur als 0 oder 1. Deshalb wird diese Rechnung ebenfalls verworfen. Alles andere spürt man eh nicht.

    clutchDistance = Math.abs(clutch - BITEPOINT_CENTER); // [0 ; BITEPOINT_CENTER or 1]
    let range = (clutch <= BITEPOINT_CENTER) ? (BITEPOINT_CENTER - BITEPOINT_MIN) : (BITEPOINT_MAX - BITEPOINT_CENTER);
    let clutchDistanceNormalized = Math.min(clutchDistance / range, 1);
    let bitePointFactor = Math.pow(1.0 - clutchDistanceNormalized, 0.01); // [1 ; 0]

Modularität, Realismus, Gefühl und System
