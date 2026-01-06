#include <Wire.h>
#include "Adafruit_DRV2605.h"

Adafruit_DRV2605 drv;

String inputString = ""; 
bool stringComplete = false;
unsigned long lastCommandTime = 0;
float phase = 0;
int motorValue = 0;
int amplitude;
int frequency;

void setup() {
  Serial.begin(19200); 
  drv.begin();
  drv.selectLibrary(1); 
  drv.useERM();
  drv.setMode(DRV2605_MODE_REALTIME); 

  Serial.print("Setup complete \n");
}

void loop() {
  if (stringComplete) {
       
    motorValue = amplitude * 100;

    //phase += frequency * millis()-lastCommandTime*0.001;
    //float envelope = pow(abs(sin(phase * PI/2)) + 0.01, 0.25); // 
    //motorValue *= envelope;

    Serial.println(motorValue);
    drv.setRealtimeValue(motorValue);
    
    lastCommandTime = millis(); // Zeitstempel merken
    inputString = "";
    stringComplete = false;
  }
  
  // Failsafe
  // Wenn länger als T kein Befehl kam -> Motor aus!
  if (millis() - lastCommandTime > 1000) {
     drv.setRealtimeValue(0);
  }
}

void serialEvent() {
  while (Serial.available()) {
    
    char inChar = (char)Serial.read();

    if (inChar == ';') 
    {
      frequency = inputString.toInt();
      Serial.print("Frequency: ");  
      Serial.println(frequency);  
      
      inputString = "";
    } 
    else if (inChar == '\n') 
    {
      amplitude = inputString.toInt();
      Serial.print("Amplitude: ");
      Serial.println(amplitude);

      inputString = "";
      stringComplete = true;
    }
    else
    {
      inputString += inChar;
    }
  }
}