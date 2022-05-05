#include <PWM.h>

int pwm = 9;                
int32_t frequency = 10;
int bozucu= 3;
int onoff=7;
int latchPin = 5;
int clockPin = 6;
int dataPin = 4;
byte leds = 0;

void setup()
{
  
  pinMode(7,OUTPUT);
  pinMode(3,OUTPUT);
  pinMode(latchPin, OUTPUT);
  pinMode(dataPin, OUTPUT);  
  pinMode(clockPin, OUTPUT);
  digitalWrite(bozucu,HIGH);
  digitalWrite(onoff,HIGH);
  InitTimersSafe(); 
 SetPinFrequencySafe(pwm, frequency);
 registeraYaz();
 Serial.begin(9600);
 while (! Serial); 
 // Serial.print("1 ile sistemi acin ");
 // Serial.println("0 ile sistemi kapatin");
  }

void loop()
{
  
  pwmWrite(pwm, 27);

     double value=0;
    value=analogRead(A0);
    value=map(value,0,1024,0,28);
    //Serial.print("temperature value : ");

    Serial.println("*");
    Serial.println(value);
    Serial.println("/");
    delay(4000);
    if (value>=27){
     digitalWrite(onoff,LOW);
     delay(100);
     }
    else if (value<=22){
     digitalWrite(onoff,HIGH);
     delay(100);
     }

      if (Serial.available())
  {
    char ch = Serial.read();
    if (ch == '1')
    {
      int led = ch - '1';
      bitSet(leds, led);
      registeraYaz();
      Serial.print(led +1);
     // Serial.println(" sistem acik");
    }
    if (ch == '0')
    {
      leds = 0;
      registeraYaz();
     // Serial.println("sistem kapali");
    }
  }
}

void registeraYaz()
{
   digitalWrite(latchPin, LOW);
   shiftOut(dataPin, clockPin, LSBFIRST, leds);
   digitalWrite(latchPin, HIGH);
   
     
     
}
