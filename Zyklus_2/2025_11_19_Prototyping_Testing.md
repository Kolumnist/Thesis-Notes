# Prototyping

## Anmerkungen

Pedale einbauen, Buttkicker einbauen

Vorläufig wurde auf UDP aufgrund von Einfachheit und Dokumentation gesetzt. In Zukunft evtl. über Memory Share, wenn noch weiter optimiert werden muss.

Es gibt kein Level zum Testen für Berg Anfahren aktuell.

Neuester Build der Simulation hat einen nicht gefixten Fehler welcher es unmöglich macht zu fahren wenn man die VR Brille absetzt und wieder aufsetzt. Das Spiel muss dann neugestartet werden.

## Tickets abarbeiten

Nach 12 Tagen waren alle Tickets abgearbeitet und es gab nur eine zusätzliche Aufgabe:

- *Custom Game hinzufügen und durchtesten*

Review startet früher.

### Recherche Plugins und Custom Game

Custom Game funktioniert nicht wie ich anfangs vermutete aber es erkennt eine .exe wenn sie gestartet wird. Dadurch wird Simhub aktiv und das Bass Shaker Plugin funktioniert.

Plugins sind sehr schlecht dokumentiert aber es gibt sehr viele Möglichkeiten. UI kann erstellt werden. Telemetriedaten gespeichert und auf einige verschiedene Wege geupdated werden. Allerdings musste vieles aus Community Projekten herausgelesen werden.

### Custom Game hinzufügen und durchtesten

Custom Game hat wie erwähnt nich so funktioniert wie ich dachte. Daher musste ich es mehrmals testen und herausfinden ob ich doch recht habe und ohne Plugin an die Telemetriedaten komme. Das tue ich nicht. Allerdings konnte ich herausfinden das dieses Feature sehr nutzlos für meinen Zweck ist außer um das Bass Shaker Plugin zu aktivieren.

### Telemetriedaten sammeln

- Kupplungspedalposition
- Geschwindigkeit
- Motordrehzahl
- Motordrehmoment
- Getriebedrehzahl
- Aktuell geschaltener Gang

Als Nicht-Fahrer war es nicht zu einfach diese Daten herauszufinden.

### Simhub Plugin

Da ich endlich Code anfassen konnte im gesamten Projekt wollte ich direkt das es perfekt ist. Alles so gut funktioniert aber ich habe mich verkünstelt. 3 verschiedene Versionen gemacht und gemerkt alle sind nicht "effizienter" sondern nur komplexer und bin dann auf die einfachste Art und Weise zurückgekehrt. Habe eine UDP Verbindung eingerichtet und die Daten als Properties in Simhub angelegt. Jedes mal wenn man Daten erhält werden sie geupdated.

Zum testen der UDP Verbindung habe ich mich dann auch verkünstelt, mehrmals aber habe es dann hinbekommen. Die UDP Verbindung auf Seiten von Simhub hatte ab und zu crashes weswegen eine künftige Optimisierung der Verbindung zum Vermeiden von "packet losses" oder jittering nützlich sein kann.

Leider habe ich zum Selben Zeitpunkt eine neue "Problematik" gefunden die ich aber auf Zyklus 3 geschoben habe.

### UDP Verbindung in Fahrschulsimulation erstellen

Die Fahrschulsimulation ist in Unreal Engine gebaut. Bedeutet eine Verbindung mit C++ zu erstellen wurde gemacht. Das einzige Problem ist, dass ich die Lizenz für das Versionierungssystem nicht bekomme und deshalb mein aktueller Build nur für mich ist. - Eventuell werde ich eine eigene Mini-Simulation bauen, um Abhängigkeiten von den Programmierern zu verhindern.

### System testen und Custom Effect überarbeiten

Das System wurde ausreichend getestet. Der Custom Effect musste wegen der Property Namen geändert werden, ansonsten blieb dieser gleich.
