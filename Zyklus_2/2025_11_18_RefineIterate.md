# Refine & Iterate

## Ziel

_Das Ziel ist es,_

die aktuelle Testumgebung mit der SYNPLI Fahrschulsimulation zu ersetzen.

Innerhalb von zwei Wochen sollen, die für das aktuelle Vibrationsskript benötigten Telemetriedaten, von der Fahrschulsimulation über UDP an Simhub gesendet werden. Dazu wird ein Simhub Plugin programmiert.

Anschließend erfolgt eine Review mit vier Testpersonen und demselben Fragebogen des ersten Tests, jetzt jedoch in der Fahrschulsimulation.

## Recherche

Simhub Möglichkeiten zur Einbindung einer/s eigenen Simulation/Spiels. Daten von Unreal Engine zu Simhub schicken.

## Konzepte

Genauere Recherche zu Simhub Plugins und Custom Game findet statt. Die UDP Verbindung zwischen Simhub und Simulation aufbauen. Identifizieren welche Daten benötigt werden. Das Plugin erstellen und die Daten empfangen. Den Custom Effect für die neuen Daten abändern. Das Gesamtsystem testen.

Die Review durchführen. Zuerst den Fragebogen überarbeiten. Es werden firmen-interne Mitarbeiter, mit ausreichend Erfahrung, herangezogen und spielen die Simulation in einem vorgegebenen Level und füllen den Fragebogen aus.

### Simhub Verbindung

Simhub kann Daten über UDP empfangen. Es gibt Simhub Plugins mit denen man die UDP Verbindung nutzen kann und Simhubs Funktionen erweitern kann. Da das Bass Shaker Plugin nur funktioniert während ein Spiel aktiv von Simhub wahrgenommen wird, kann das Custom Game Feature von Simhub verwendet werden. Es bedarf weiterer Recherche da Plugins und Custom Game Feature schlecht dokumentiert sind.

### Fahrschulsimulation

Die Fahrschulsimulation wird von der Firma bereitgestellt und kann nur in VR korrekt gestartet werden. Es gibt daher keine Option außerhalb von VR die Review durchzuführen. Außerdem sind die Level mit manueller Schaltung auf Wenige beschränkt. Es gibt daher keine freie Fahrt als Testumgebung.

## Tickets

### Recherche Plugins und Custom Game

Als Entwickler möchte ich eine Übersicht über warum und wie ich Simhub Plugins erstelle und das Custom Game Feature nutze damit ich ein Plugin erstellen kann und den Nutzen des Features verstehe.

### Telemetriedaten sammeln

Als Entwickler möchte ich eine Liste der notwendigen Telemetriedaten, um alle notwendigen Daten zu schicken und herausfinde ob ich noch mehr brauche.

### Simhub Plugin

Als Entwickler möchte ich ein Simhub Plugin welches eine UDP Verbindung startet und die Daten entgegennehmen kann damit die Daten in Simhub für die Effekte verwendet werden können.

### UDP Verbindung in Fahrschulsimulation erstellen

Als Entwickler möchte ich, dass die Fahrschulsimulation die Daten zum Simhub Plugin über UDP sendet.

### System testen und Custom Effect überarbeiten

Als Review-Leiter möchte ich, dass das System vor der Review getestet wird und wenn Änderungen zum Effekt gemacht werden müssen diese gemacht sind, sodass die Fehler nicht bei der Review auftreten.
