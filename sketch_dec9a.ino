#include <Wire.h>
#include "Adafruit_DRV2605.h"

Adafruit_DRV2605 drv; // 120 MAX DRIVE to 240 MAX BRAKE

void setup() {
  Serial.begin(9600);
  Serial.println("DRV test");
  drv.begin();

  // I2C trigger by sending 'go' command
  drv.setMode(DRV2605_MODE_REALTIME); // default, internal trigger when sending GO command

  drv.selectLibrary(1);
  drv.setWaveform(0, 84);  // ramp up medium 1, see datasheet part 11.2
  drv.setWaveform(1, 1);  // strong click 100%, see datasheet part 11.2
  drv.setWaveform(2, 0);  // end of waveforms
}

float x = 0;
float a = 20;
float f = 0;
float offset = 70;

void sinewave()
{
  x += 1 / (2 * PI);

  float y = min(a * sin(x) + offset, 120);

  Serial.print((int)y);
  Serial.print("\n");

  if (y >= offset - 1 + a || y <= offset + 1 - a)
  {
    a = random(0, 20);
    //Serial.print(a);
    //Serial.print("\n");
    x += 1 / (2 * PI);
  }

  //drv.setRealtimeValue((int)y);
  delay(100 / 15);
}

void triangularwave()
{
  x++;

  float y = a * x + offset;

  if (y >= offset + a || y <= offset - a)
  {
    a = random(0, 20);
    Serial.print(a);
    Serial.print("\n");
  }

  drv.setRealtimeValue(y);
}

void squarewave()
{
  float i = sin(x);
  float y = offset * (int)((i > 0) - (i < 0));
  Serial.print((int)y);
  Serial.print("\n");
  x += 0.001 / (2 * PI);
  if (millis() % 1000 == 0)
  {
    offset = random(0, 70);
  }

  drv.setRealtimeValue((int)y);
}


String inputString = "";
bool stringComplete = false;
unsigned long lastCommandTime = 0;

float amplitude = 0;
int frequency = 0;
int motorValue = 0;

boolean pulseOff = false;
unsigned long lastPulseTime = 0;

void loop()
{
  //drv.go();
  //delay(1000);

  motorValue = map(amplitude, 0, 100, 0, 120);

  int period = 1000 / frequency;
  int activeTimeMs = period / 1.25;

  unsigned long currentTime = millis();
  unsigned long currentPeriodTime = currentTime - lastPulseTime;

  if (pulseOff && currentPeriodTime >= activeTimeMs && frequency < 25)
  {
    pulseOff = false;
    drv.setRealtimeValue(240);
  }
  else if (currentPeriodTime >= period) {
    lastPulseTime = millis();
    pulseOff = true;
    drv.setRealtimeValue(motorValue);
    Serial.println(motorValue);
  }

  // Failsafe
  if (millis() - lastCommandTime > 2000) {
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
      lastCommandTime = millis();
    }
    else
    {
      inputString += inChar;
    }
  }
}
