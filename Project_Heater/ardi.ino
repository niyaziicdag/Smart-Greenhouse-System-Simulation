#include <PWM.h>

int pwm = 9;                
int32_t frequency = 10;
int bozucu= 3;
int onoff=5;
//int value=0;
//int analogPin =A3;
void setup()
{
  
 // pinMode(A3,INPUT);
  pinMode(5,OUTPUT);
  pinMode(3,OUTPUT);
  digitalWrite(bozucu,HIGH);
  digitalWrite(onoff,HIGH);
  InitTimersSafe(); 
 SetPinFrequencySafe(pwm, frequency);
 Serial.begin(9600);
  }

void loop()
{
  
  pwmWrite(pwm, 27);

    int value=analogRead(A0);
    value=map(value,0,2800,0,1023); 
    Serial.println(value);
    delay(1000);
    if (value==373)
     digitalWrite(onoff,LOW);
     delay(3000);
     digitalWrite(onoff,HIGH);
     delay(10000);
   
     
     
}
