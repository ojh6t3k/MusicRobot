#include <UnityRobot.h>
#include <PulseModule.h>
#include <Herkulex.h>

#define ID_NECK 11
#define ID_R_SHOULDER_PITCH 1
#define ID_R_SHOULDER_ROLL 3
#define ID_R_SHOULDER_YAW 5
#define ID_R_ELBOW 7
#define ID_R_HAND 9
#define ID_L_SHOULDER_PITCH 2
#define ID_L_SHOULDER_ROLL 4
#define ID_L_SHOULDER_YAW 6
#define ID_L_ELBOW 8
#define ID_L_HAND 10

#define POS_NECK 0
#define POS_R_SHOULDER_PITCH 0
#define POS_R_SHOULDER_ROLL -45
#define POS_R_SHOULDER_YAW 0
#define POS_R_ELBOW -60
#define POS_R_HAND -30
#define POS_L_SHOULDER_PITCH 0
#define POS_L_SHOULDER_ROLL 45
#define POS_L_SHOULDER_YAW 0
#define POS_L_ELBOW 60
#define POS_L_HAND 30

#define POS_DELTA 8

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
PulseModule pulse(1);


void OnUpdate(byte id)
{
  music.update(id);
  pulse.update(id);
}

// When recieved end of update
void OnAction(void)
{
  pulse.action();
}

// When recieved start of connection
void OnStart(void)
{
  //TODO: Initialize argument of module
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
  
  float pulseValue = pulse.process();
  if(pulseValue > 0)
  {
    Herkulex.moveAllAngle(ID_NECK, POS_NECK + POS_DELTA * (music.getIndex() - 6), 0);
    Herkulex.moveAllAngle(ID_R_SHOULDER_PITCH, POS_R_SHOULDER_PITCH + POS_DELTA * music.getIndex(), 0);
 //   Herkulex.moveAllAngle(ID_R_ELBOW, POS_R_ELBOW + POS_DELTA * (music.getIndex() - 0), 0);
    Herkulex.moveAllAngle(ID_L_SHOULDER_PITCH, POS_L_SHOULDER_PITCH - POS_DELTA * music.getIndex(), 0);
 //   Herkulex.moveAllAngle(ID_L_ELBOW, POS_L_ELBOW - POS_DELTA * (music.getIndex() - 0), 0);
    Herkulex.actionAll(500);
  }
  else
  {
    Herkulex.moveAllAngle(ID_NECK, POS_NECK, 0);
    Herkulex.moveAllAngle(ID_R_SHOULDER_PITCH, POS_R_SHOULDER_PITCH, 0);
    Herkulex.moveAllAngle(ID_R_SHOULDER_ROLL, POS_R_SHOULDER_ROLL, 0);
    Herkulex.moveAllAngle(ID_R_SHOULDER_YAW, POS_R_SHOULDER_YAW, 0);
    Herkulex.moveAllAngle(ID_R_ELBOW, POS_R_ELBOW, 0);
    Herkulex.moveAllAngle(ID_R_HAND, POS_R_HAND, 0);
    Herkulex.moveAllAngle(ID_L_SHOULDER_PITCH, POS_L_SHOULDER_PITCH, 0);
    Herkulex.moveAllAngle(ID_L_SHOULDER_ROLL, POS_L_SHOULDER_ROLL, 0);
    Herkulex.moveAllAngle(ID_L_SHOULDER_YAW, POS_L_SHOULDER_YAW, 0);
    Herkulex.moveAllAngle(ID_L_ELBOW, POS_L_ELBOW, 0);
    Herkulex.moveAllAngle(ID_L_HAND, POS_L_HAND, 0);
    Herkulex.actionAll(500);
  }
}
