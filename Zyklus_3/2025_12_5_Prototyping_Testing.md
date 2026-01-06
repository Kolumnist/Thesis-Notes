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

Nach ~11 Tagen wurde der Zyklus in die Review gezwungen. Es gab folgende zusätzliche Aufgaben:

- *Neue Pedale einbauen*
- *Buttkicker einbauen und mit System verbinden*
- *ERM mit Controller und Arduino einrichten*
- *ERM mit System verbinden*
- *Logik überarbeiten*

Review gezwungen.

### Anforderungsanalyse (unvollständig)

Weitere Recherche über Aktuatorik betrieben. Dadurch erfahren, dass die meisten Hersteller ERMs nutzen. Durch Tests herausgefunden, dass:

- VCA schwächer ist als ERM
- ERM Befestigung außerdem einfacher
- VCA hat mehr Konfigurationsmöglichkeiten
- VCA Hersteller gute Beziehung mit Firma
- ERM ist mit Kabel verbunden VCA via Bluetooth braucht aber extern Strom

Allerdings wurde Anforderungsanalyse nicht ausreichend getätigt um eine entgültige Entscheidung zu geben.

### +Neue Pedale einbauen

Neue Pedale eingebaut und neue Befestigung für VCA erstellen lassen. Einiges vom Design selber überlegt. Angebracht und ausprobiert und um ein weiteres Design gebeten für andere Richtung diese noch nicht getestet.

### +Buttkicker einbauen und mit System verbinden

Buttkicker eingebaut und herausgefunden das er stärker ist als VCA. Buttkicker fühlte sich aber richtig gut für das Vibrationsgefühl an. Daher behalten aber sehr schwach eingestellt um Pedal in Fokus zu rücken.

### +ERM mit Controller und Arduino einrichten

Bestellter Einkauf durchgetestet und konfiguriert. Code abgeändert. Fehler im Aufbau entdeckt. Spontanes Einkaufen von Testequipment. Fehler gefunden(es waren die Kabel) und erneut aufgebaut und durchgetestet.

### +ERM mit System verbinden

Löten etwas anstrengend und ERM Kabel sehr dünn und etwas kurz. Allgemein waren die Kabel für das Verbinden am PC bis zu den Pedalen etwas kurz aber hat funktioniert.

Recherche für Arduino Verbindung mit Simhub. Direktes Senden von Seriellen Nachrichten möglich oder über Motor Plugin. Entscheidung viel auf Serial Devices Feature, da dieses einfacher und genauer ist. Hat ebenfalls Möglichkeit für Javascript.

### +Logik überarbeiten (unvollständig)

Logik war bisher nur sehr simple. Umstellung auf komplexere Logik mit Berücksichtigung von Torque und Slip. Physikrecherche. Autorecherche. Logik in Simhub und im Arduino überarbeitet und ausgiebig getestet. Mehrmals abgeändert und immernoch unzufrieden aber Zeit hat nicht gereicht.
