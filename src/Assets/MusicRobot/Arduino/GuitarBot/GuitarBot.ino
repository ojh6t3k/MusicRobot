#include <UnityRobot.h>
#include <PulseModule.h>
#include <Herkulex.h>

#define ID_HAND 1
#define ID_NECK 2
#define PIN_RED 16
#define PIN_GREEN 18
#define PIN_BLUE 20
#define POS_HAND_START 10
#define POS_HAND_END -50
#define POS_NECK_START 0
#define POS_NECK_DELTA 6


class MusicDataModule
{
  int _id;
  boolean _updated;
  
  byte _index;
  byte _red;
  byte _green;
  byte _blue;
  
public:
  MusicDataModule(byte id);
  
  void update(byte id);
  void reset();
  int getIndex();
  int getRed();
  int getGreen();
  int getBlue();
};


MusicDataModule music(0);
PulseModule pulseHand(1);
PulseModule pulseLED(2);


void OnUpdate(byte id)
{
  music.update(id);
  pulseLED.update(id);
  pulseHand.update(id);
}

// When recieved end of update
void OnAction(void)
{
  //TODO: Synchronizing module's action
  pulseLED.action();
  pulseHand.action();
}

// When recieved start of connection
void OnStart(void)
{
  //TODO: Initialize argument of module
  music.reset();
  pulseLED.reset();
  pulseHand.reset();
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
  Herkulex.reboot(BROADCAST_ID);
  delay(500); 
  Herkulex.initialize(); //initialize motors
  delay(200);  
  
  UnityRobot.attach(CMD_UPDATE, OnUpdate);
  UnityRobot.attach(CMD_ACTION, OnAction);
  UnityRobot.attach(CMD_START, OnStart);
  UnityRobot.attach(CMD_EXIT, OnExit);
  UnityRobot.attach(CMD_READY, OnReady);  
  UnityRobot.begin(57600);
}

void loop()
{
  UnityRobot.process();
  
  float pulseValue = pulseLED.process();
  if(pulseValue > 0)
  {
    analogWrite(PIN_RED, (int)(pulseValue * music.getRed()));
    analogWrite(PIN_GREEN, (int)(pulseValue * music.getGreen()));
    analogWrite(PIN_BLUE, (int)(pulseValue * music.getBlue()));
    
    Herkulex.moveOneAngle(ID_NECK, POS_NECK_START + POS_NECK_DELTA * (music.getIndex() - 6), 600, 0);
  }
  else
  {
    analogWrite(PIN_RED, 0);
    analogWrite(PIN_GREEN, 0);
    analogWrite(PIN_BLUE, 0);
    
    Herkulex.moveOneAngle(ID_NECK, POS_NECK_START, 600, 0);
  }
  
  pulseValue = pulseHand.process();
  if(pulseValue > 0)
  {
    Herkulex.moveOneAngle(ID_HAND, POS_HAND_END, 0, 0);
  }
  else
  {
    Herkulex.moveOneAngle(ID_HAND, POS_HAND_START, 0, 0);
  }
}
