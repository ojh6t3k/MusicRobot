#include <UnityRobot.h>
#include <PulseModule.h>
#include <Herkulex.h>

#define ID_ELBOW_R 1
#define ID_HAND_R 3
#define ID_ELBOW_L 2
#define ID_HAND_L 4
#define ID_NECK 5

#define PIN_BASE 8
#define PIN_SNARE 17
#define PIN_DRUM1 9
#define PIN_DRUM2 10
#define PIN_DRUM3 A1
#define PIN_HIHAT_R 16
#define PIN_HIHAT_L A0
#define PIN_CYMBALS_R 18
#define PIN_CYMBALS_L A2

#define POS_ELBOW_R_DRUM1 0
#define POS_ELBOW_R_SNARE -20
#define POS_ELBOW_R_CYMBALS -20
#define POS_ELBOW_R_HIHAT -45

#define POS_ELBOW_L_DRUM2 0
#define POS_ELBOW_L_DRUM3 20
#define POS_ELBOW_L_CYMBALS 20
#define POS_ELBOW_L_HIHAT 45

#define POS_HAND_R_START 80
#define POS_HAND_R_END 45
#define POS_HAND_L_START -80
#define POS_HAND_L_END -45

#define POS_NECK_START 0
#define POS_NECK_DELTA 6

PulseModule pulseHandR(1);
PulseModule pulseHandL(2);
PulseModule pulseBase(3);
PulseModule pulseSnare(4);
PulseModule pulseDrum1(5);
PulseModule pulseDrum2(6);
PulseModule pulseDrum3(7);
PulseModule pulseHihatR(8);
PulseModule pulseHihatL(9);
PulseModule pulseCymbalsR(10);
PulseModule pulseCymbalsL(11);


void OnUpdate(byte id)
{
  pulseHandR.update(id);
  pulseHandL.update(id);
  pulseBase.update(id);
  pulseSnare.update(id);
  pulseDrum1.update(id);
  pulseDrum2.update(id);
  pulseDrum3.update(id);
  pulseHihatR.update(id);
  pulseHihatL.update(id);
  pulseCymbalsR.update(id);
  pulseCymbalsL.update(id);
}

// When recieved end of update
void OnAction(void)
{
  //TODO: Synchronizing module's action
  pulseHandR.action();
  pulseHandL.action();
  pulseBase.action();
  pulseSnare.action();
  pulseDrum1.action();
  pulseDrum2.action();
  pulseDrum3.action();
  pulseHihatR.action();
  pulseHihatL.action();
  pulseCymbalsR.action();
  pulseCymbalsL.action();
}

// When recieved start of connection
void OnStart(void)
{
  //TODO: Initialize argument of module
  pulseHandR.reset();
  pulseHandL.reset();
  pulseBase.reset();
  pulseSnare.reset();
  pulseDrum1.reset();
  pulseDrum2.reset();
  pulseDrum3.reset();
  pulseHihatR.reset();
  pulseHihatL.reset();
  pulseCymbalsR.reset();
  pulseCymbalsL.reset();
}

// When recieved exit of connection
void OnExit(void)
{
  //TODO: Initialize argument of module
}

void OnReady(void)
{
  
}

void setup()
{
  Herkulex.beginSerial1(115200); //open serial
  Herkulex.reboot(ID_ELBOW_R); //reboot first motor
  Herkulex.reboot(ID_HAND_R); //reboot first motor
  Herkulex.reboot(ID_ELBOW_L); //reboot first motor
  Herkulex.reboot(ID_HAND_L); //reboot first motor
  Herkulex.reboot(ID_NECK); //reboot first motor
  delay(500); 
  Herkulex.initialize(); //initialize motors
  delay(200);  
  
  UnityRobot.attach(CMD_UPDATE, OnUpdate);
  UnityRobot.attach(CMD_ACTION, OnAction);
  UnityRobot.attach(CMD_START, OnStart);
  UnityRobot.attach(CMD_EXIT, OnExit);
  UnityRobot.attach(CMD_READY, OnReady);  
  UnityRobot.begin(57600);
  
  pinMode(PIN_BASE, OUTPUT);
  pinMode(PIN_SNARE, OUTPUT);
  pinMode(PIN_DRUM1, OUTPUT);
  pinMode(PIN_DRUM2, OUTPUT);
  pinMode(PIN_DRUM3, OUTPUT);
  pinMode(PIN_HIHAT_R, OUTPUT);
  pinMode(PIN_HIHAT_L, OUTPUT);
  pinMode(PIN_CYMBALS_R, OUTPUT);
  pinMode(PIN_CYMBALS_L, OUTPUT);
}

void loop()
{
  UnityRobot.process();
  
  float pulseValue = pulseHandR.process();
  if(pulseValue > 0)
  {
    Herkulex.moveOneAngle(ID_HAND_R, POS_HAND_R_START, 1, 0);
  }
  else
  {
    Herkulex.moveOneAngle(ID_HAND_R, POS_HAND_R_END, 1, 0);
  }
  
  pulseValue = pulseHandL.process();
  if(pulseValue > 0)
  {
    Herkulex.moveOneAngle(ID_HAND_L, POS_HAND_L_START, 1, 0);
  }
  else
  {
    Herkulex.moveOneAngle(ID_HAND_L, POS_HAND_L_END, 1, 0);
  }
  
  pulseValue = pulseBase.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_BASE, HIGH);    
  }
  else
  {
    digitalWrite(PIN_BASE, LOW);
  }
  
  pulseValue = pulseSnare.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_SNARE, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_R, POS_ELBOW_R_SNARE, 1, 0);
    Herkulex.moveOneAngle(ID_NECK, -30, 200, 0);
  }
  else
  {
    digitalWrite(PIN_SNARE, LOW);
    Herkulex.moveOneAngle(ID_NECK, 0, 200, 0);
  }
  
  pulseValue = pulseDrum1.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_DRUM1, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_R, POS_ELBOW_R_DRUM1, 1, 0);
  }
  else
  {
    digitalWrite(PIN_DRUM1, LOW);
  }
  
  pulseValue = pulseDrum2.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_DRUM2, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_L, POS_ELBOW_L_DRUM2, 1, 0);
  }
  else
  {
    digitalWrite(PIN_DRUM2, LOW);
  }
  
  pulseValue = pulseDrum3.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_DRUM3, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_L, POS_ELBOW_L_DRUM3, 1, 0);
  }
  else
  {
    digitalWrite(PIN_DRUM3, LOW);
  }
  
  pulseValue = pulseHihatR.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_HIHAT_R, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_R, POS_ELBOW_R_HIHAT, 1, 0);
  }
  else
  {
    digitalWrite(PIN_HIHAT_R, LOW);
  }
  
  pulseValue = pulseHihatL.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_HIHAT_L, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_L, POS_ELBOW_L_HIHAT, 1, 0);
  }
  else
  {
    digitalWrite(PIN_HIHAT_L, LOW);
  }
  
  pulseValue = pulseCymbalsR.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_CYMBALS_R, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_R, POS_ELBOW_R_CYMBALS, 1, 0);
  }
  else
  {
    digitalWrite(PIN_CYMBALS_R, LOW);
  }
  
  pulseValue = pulseCymbalsL.process();
  if(pulseValue > 0)
  {
    digitalWrite(PIN_CYMBALS_L, HIGH);
    Herkulex.moveOneAngle(ID_ELBOW_L, POS_ELBOW_L_CYMBALS, 1, 0);
  }
  else
  {
    digitalWrite(PIN_CYMBALS_L, LOW);
  }
}
