# Refine & Iterate

Zyklus drei war zu viel auf einmal und schlecht geplant. Plaung für Zyklus vier wird genauer und länger sein um wirklich am Ziel dranzubleiben.

## Ziel

_Das Ziel ist es,_

die Vibrationen näher an die Vibrationen eines Opel 8 anzupassen und zeitlich abgepasster zu machen.

Innerhalb von einer Woche soll eine Anforderungsanalyse zur Bestimmung des fortan genutzten Aktuatorentyps (VCA, ERM oder LRA), auf Basis von Messungen der Vibrationen an einem Opel Auto, erstellt werden. Die Messungen werden dem Internet sowie eigenen Messungen mittels einem Beschleunigungssensor entstammen. Der Aktuator wird mit dem bestehenden System verbunden und entsprechend der Messungen kalibriert.

Anschließend erfolgt eine Review mit fünf Testpersonen mittels des vorhandenen Fragenbogens.

**Anfängliches Ziel verworfen**. Es war nicht ausreichend. Nach der Autofahrt sollte ich es abändern.

_Das Ziel ist es,_

die Vibrationen an die Vibrationen beim Anfahren in einem Opel gefühlsmäßig und zeitlich anzupassen.

Innerhalb von einer Woche soll eine Anforderungsanalyse zur Bestimmung des fortan genutzten Aktuatorentyps gemacht werden. Dazu wird der verfügbare ERM und VCA auf ihre Intensität und ihr Gefühl verglichen. Der Aktuator wird mit dem bestehenden System verbunden. Es wird ein Arduino und Controller für den ERM besorgt.

Anschließend erfolgt eine Review mit vier Testpersonen mittels des vorhandenen Fragenbogens.

## Recherche

Autofahrt in einem Opel X auf Firmengelände um Schleifpunkt zu spüren. Einkaufsliste für Controller und Arduino erstellt und Einkauf getätigt. (Firma konnte mir da es Ende des Jahres war keine Versprechen für Einkäufe geben, eigene Käufe wurden getätigt).

## Konzepte

Kupplungsgefühl und vibrieren im Auto in Erfahrung bringen. (Spezifisch Opel wenn möglich) Genauere Recherche zu Schleifpunkt und Physik betreiben. Anforderungsanalyse erstellen. Daraus folgend Aktuatoren einbauen und testen. Review durchführen.

**Das Kupplungspedal vibriert, während der Review, am Stärksten am Schleifpunkt und fällt sehr schnell ab. Man spürt es beim Schaltvorgang sowie beim Anfahren. Das macht den Schleifpunkt realistisch identifizierbar und realistisch im Gefühl und Wahrnehmung.**

### Neue Pedale

Der Entwicklungssim sollte neue Pedale bekommen. Daher habe ich diese eingebaut. Die FanatecV3i Pedale. Ich brauchte für den VCA daher eine neue Befestigung und musste die neuen Pedale kalibrieren. Die Pedale hatten am Gas und Bremspedal jedoch einen ERM welchen ich ausbauen und nutzen durfte.

### Neuer Aktuator

Ein Standard ERM der auch in Controllern benutzt werden würde. Das Gefühl der Vibration ist ganz anders als beim VCA und Frequenz ist gleich der Drehzahl des ERMs umso höher die Drehzahl desto stärker auch die Vibration. Das könnte ein Problem sein.

### Haptic Controller

Zum schnellen Testen und aufgrund des Preises und Einfachheit habe ich den Adafruit DRV2605L Haptic Controller gekauft. Dieser kommt mit einer Library und kann ERM sowie LRA mit bis zu 5V ansteuern.

### Arduino Nano

Der Haptik Controller braucht nicht viel daher reicht ein Arduino Nano aus. Für künftige Stromversorgung sollte eine externe Stromversorgung in Betracht gezogen werden.

## Tickets

### Anforderungsanalyse

Intensität war für den VCA immer ein Problem. Evtl. kann eine neue Befestigung das ändern aber das ist jetzt noch nicht sicher. Der ERM ist stärker aber fühlt er sich besser an.

Als Entickler möchte ich eine Anforderungsanalyse und Entscheidung der Aktuatorik Wahl, damit ein Aktuator nicht ohne Grund gewählt wird.
