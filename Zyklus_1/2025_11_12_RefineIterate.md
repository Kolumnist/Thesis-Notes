# Refine & Iterate

## Ziel

_Das Ziel ist es,_

den VCA erfolgreich über die SimHub-Telemetrie für den Schleifpunkt am Kupplungspedal anzusteuern und synchron zur Testumgebung analog zu konfigurieren.

Innerhalb von einer Woche soll das Vibrationsmuster, welches SimHub für den Schleifpunkt bereitstellt, für alle Kupplungssituation am Pedal erzeugt werden.

Anschließend erfolgt eine Review mit vier Testpersonen, die das echte Gefühl eines Schleifpunktes beim Schaltvorgang kennen und beurteilen können wie nahe, die Test-Simulation diesem kommt. Die Probanden bewerten die Intensität der wahrgenommenen Vibrationen auf einer Skala und geben eine schriftliche Einschätzung zum Realismus ab.

## Recherche

Lange Recherche betrieben, um Möglichkeiten sowie Gedankengut auf das richtige Ziel zu bringen.
Simhub, Aktuatorik, Vibrationen und mehr. Aber zu oberflächig. Weitere Recherchen im nächsten Zyklus notwendig.

## Konzepte

Der VCA wird mit einem Audioverstärker mit dem Computer als Audioquelle verbunden. Dann wird die Audioquelle innerhalb des Simhub Bass Shaker Plugins verbunden. Die vorgefertigten Effekte werden in Assetto Corsa und BeamNG getestet. Danach wird ein oder mehrere simple Custom Effects aus dem Internet oder eigenhändig erstellt und getestet. Dann wird eine Befestigung für den VCA am Pedal angefertigt. Befestigt am Pedal wird der VCA und das gesamte System nun getestet.

Schlussendlich findet die Review statt. Zuerst wird der Fragebogen erstellt. Dann werden firmen-interne Mitarbeiter, mit ausreichend Erfahrung, herangezogen und testen das System in BeamNG und füllen den Fragebogen aus.

**Das Kupplungspedal vibriert, während der Review, beim Schaltvorgang sowie beim Anfahren und macht den Schleifpunkt identifizierbar.**

### Simhub

Simhub ist eine Drittanbieter Applikation welche zur Ansteuerung von Hardware in der SimRacing Branche oft verwendet wird und ist für Prototyping der schnellste und einfachste Weg. Es bietet mit ihrem Bass Shaker Plugin, die Möglichkeit Audioquellen hinzuzufügen und Vibrationsignale(Frequenzen und Intensität) an jene zu senden. So soll der VCA angesprochen werden. Außerdem zieht sich Simhub viele Telemetriedaten von unterstützten Spielen. Unterstützte Spiele wie BeamNG oder Assetto Corsa welche als Testumgebung/Simulation für Zyklus eins genutzt werden. Innerhalb des Plugins gibt es vorgefertigte Effekte die nur mit den Telemetriedaten von unterstützten Spielen funktionieren. Es können auch "Custom Effects" erstellt werden. Dort ist die Frequenz sowie die Intensität konfigurierbar. Die Frequenz und Intensität lassen sich durch Javascript automatisieren. Simhub bietet die Möglichkeit in der App selbst Skripte anzufertigen und die Telemetriedaten dort einzubinden.

### VCA

Der VCA ist von Grewus und ist über einen Audioverstärker von renkforce, ein 4 Channel Car Amplifier, mit dem Computer verbunden. Mehrere dieser VCAs ist in Besitz der Firma und ein anderer Aktuator kostet und würde Lieferzeit beanspruchen. Dieser VCA wird in Haptik-Sitzen verwendet. In den nächsten Zyklen wird versucht auf einen ERM oder LRA umzusteigen, da diese intensivere Vibrationen erzeugen können.

### Pedal

Es wird das bereits verbaute MOZA SR-P Kupplungspedal verwendet. Dieses ist bereits verbaut.

### Befestigung

Für die Befestigung von dem VCA an dem Pedal werden 3D-Drucke angefertigt und getestet. Metall federt zu stark, es direkt am Pedal zu befestigen führt außerdem zu einer Menge Lärm.

## Tickets

### VCA als Audioquelle in Simhub Plugin

Als Entwickler möchte ich das der VCA als Audioquelle mit Simhub, mithilfe des Bass Shaker Plugins, verbunden ist, damit die Vibrationseffekte über Simhub getestet werden können.

### VCA Befestigung anfertigen

Als Entwickler möchte ich eine Befestigung für den VCA direkt am Pedal, damit die Vibrationen dort spürbar sind und der VCA fest bleibt.

### Spiele vorbereiten

Als Tester brauche ich die vorliegenden Spiele Assetto Corsa und BeamNG verbunden mit Simhub und dem gesamten Rig, damit die späteren Tests am Rig durchgeführt werden kann. (Evtl. mit VR)

### Vibrationstests Standardeffekte

Als Review-Leiter möchte ich, dass die Standardvibrationseffekte von Simhub in einem der Spiele getestet sind, damit beurteilt werden kann ob der VCA funktioniert und um ein erstes Gefühl für die Vibrationen zu bekommen.

### Custom Effect

Als Review-Leiter möchte ich, dass ein Custom Effect welches nur beim Schleifpunkt (jeweils beim Schalten und beim Anfahren) erstellt und getestet wird, damit dieses für die Review genutzt werden kann.
