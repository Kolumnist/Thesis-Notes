# Refine & Iterate

Zyklus Vier hat den Fokus auf die Effekte gelegt und dabei konnte ich feststellen, dass die Hardware ausreichend ist. Einen LRA zu testen wäre im Interesse aber nicht notwendig. Das Gesamtsystem(mit Buttshaker und Haptiksitz) zusammen zu testen muss allerdings gemacht werden um sicherzustellen, dass mein System gut mit den anderen Zusammenspielt und nicht schlecht ist. Vibrationsmessungen abzuschließen wäre ebenfalls von Interesse aber nicht notwendig.

## Ziel

_Das Ziel ist es,_

das Schalten realistischer zu erweitern sodass, mindestens beim langsamen Schalten alle genannten Szenarien realistisch spürbar sind. ... Motor abwürgen und Starten anders... Schalter für Buttshaker vorne am Pedal.

Innerhalb von einer Woche soll die Logik optimiert und die Effekte für alle Szenarien definiert, erstellt und getestet werden.

Anschließend erfolgt eine Review mit fünf Testpersonen mittels des vorhandenen Fragenbogens.

## Recherche



## Konzepte

Die Szenarien haben alle spezifische Unterszenarien welche definiert werden müssen. Daraus ergeben sich Effekte die erstellt und in die Logik miteingebaut werden. Die Logik wird verbessert und dann die Review durchführt.

**Das Kupplungspedal vibriert bei allen Szenarien mit ihrem jeweilig relevanten Effekt über einen einzigen Aktuator am Pedal. Die Vibration soll auf eine Hilfe beim Erlernen der Schaltkompetenz abzielen. Dazu muss es realistisch sein.**

## Tickets

### Unterszenarien definieren

Die folgenden Szenarien:

- Anfahren
- Anfahren am Berg
- Schalten
- Hochschalten
- Runterschalten

haben jeweils Unterszenarien die identifiziert werden müssen. Zum Beispiel gibt es die Möglichkeit von Anfahren am Berg mit Bremse oder ohne. Man kann den Motor abwürgen und mehr. Alles dies führt zu verschiedenen Effekten die das Kupplungspedal darstellen können sollte.
Ich möchte als Entwickler, dass diese Szenarien definiert sind um daraus Effekte definieren zu können.

### Effekte definieren

Als Entwickler möchte ich das alle Effekte definiert sind, damit dann die Logik angepasst werden kann.

### Logik anpassen

Als Entwickler möchte ich das die Logik für alle Effekte angepasst wird um alle Szenarien gut abzudecken.

Es kann vorkommen, dass mehrere Effekte als neue Effekte erstellt werden müssen. In dem Fall ein neues Ticket pro Effekt anlegen.
