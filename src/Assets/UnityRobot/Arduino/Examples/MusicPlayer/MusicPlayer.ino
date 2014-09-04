#include <UnityRobot.h>
#include "ToneModule.h"

#define NOTE_B0  31
#define NOTE_C1  33
#define NOTE_CS1 35
#define NOTE_D1  37
#define NOTE_DS1 39
#define NOTE_E1  41
#define NOTE_F1  44
#define NOTE_FS1 46
#define NOTE_G1  49
#define NOTE_GS1 52
#define NOTE_A1  55
#define NOTE_AS1 58
#define NOTE_B1  62
#define NOTE_C2  65
#define NOTE_CS2 69
#define NOTE_D2  73
#define NOTE_DS2 78
#define NOTE_E2  82
#define NOTE_F2  87
#define NOTE_FS2 93
#define NOTE_G2  98
#define NOTE_GS2 104
#define NOTE_A2  110
#define NOTE_AS2 117
#define NOTE_B2  123
#define NOTE_C3  131
#define NOTE_CS3 139
#define NOTE_D3  147
#define NOTE_DS3 156
#define NOTE_E3  165
#define NOTE_F3  175
#define NOTE_FS3 185
#define NOTE_G3  196
#define NOTE_GS3 208
#define NOTE_A3  220
#define NOTE_AS3 233
#define NOTE_B3  247
#define NOTE_C4  262
#define NOTE_CS4 277
#define NOTE_D4  294
#define NOTE_DS4 311
#define NOTE_E4  330
#define NOTE_F4  349
#define NOTE_FS4 370
#define NOTE_G4  392
#define NOTE_GS4 415
#define NOTE_A4  440
#define NOTE_AS4 466
#define NOTE_B4  494
#define NOTE_C5  523
#define NOTE_CS5 554
#define NOTE_D5  587
#define NOTE_DS5 622
#define NOTE_E5  659
#define NOTE_F5  698
#define NOTE_FS5 740
#define NOTE_G5  784
#define NOTE_GS5 831
#define NOTE_A5  880
#define NOTE_AS5 932
#define NOTE_B5  988
#define NOTE_C6  1047
#define NOTE_CS6 1109
#define NOTE_D6  1175
#define NOTE_DS6 1245
#define NOTE_E6  1319
#define NOTE_F6  1397
#define NOTE_FS6 1480
#define NOTE_G6  1568
#define NOTE_GS6 1661
#define NOTE_A6  1760
#define NOTE_AS6 1865
#define NOTE_B6  1976
#define NOTE_C7  2093
#define NOTE_CS7 2217
#define NOTE_D7  2349
#define NOTE_DS7 2489
#define NOTE_E7  2637
#define NOTE_F7  2794
#define NOTE_FS7 2960
#define NOTE_G7  3136
#define NOTE_GS7 3322
#define NOTE_A7  3520
#define NOTE_AS7 3729
#define NOTE_B7  3951
#define NOTE_C8  4186
#define NOTE_CS8 4435
#define NOTE_D8  4699
#define NOTE_DS8 4978


ToneModule toneModule(0);

void OnToneChanged(byte id, word note)
{
  if(id == 0)
  {
    digitalWrite(8, LOW);
    digitalWrite(9, LOW);
    digitalWrite(10, LOW);
    digitalWrite(11, LOW);
    digitalWrite(12, LOW);
    digitalWrite(13, LOW);
    digitalWrite(14, LOW);    
    
    if(note == NOTE_MUTE)
    {      
      noTone(7);
    }
    else
    {      
      if(note == NOTE_C1 || note == NOTE_C2
        || note == NOTE_C3 || note == NOTE_C4
        || note == NOTE_C5 || note == NOTE_C6
        || note == NOTE_C7)
      {
        digitalWrite(8, HIGH);
      }
      else if(note == NOTE_D1 || note == NOTE_D2
        || note == NOTE_D3 || note == NOTE_D4
        || note == NOTE_D5 || note == NOTE_D6
        || note == NOTE_D7)
      {
        digitalWrite(9, HIGH);
      }
      else if(note == NOTE_E1 || note == NOTE_E2
        || note == NOTE_E3 || note == NOTE_E4
        || note == NOTE_E5 || note == NOTE_E6
        || note == NOTE_E7)
      {
        digitalWrite(10, HIGH);
      }
      else if(note == NOTE_F1 || note == NOTE_F2
        || note == NOTE_F3 || note == NOTE_F4
        || note == NOTE_F5 || note == NOTE_F6
        || note == NOTE_F7)
      {
        digitalWrite(11, HIGH);
      }
      else if(note == NOTE_G1 || note == NOTE_G2
        || note == NOTE_G3 || note == NOTE_G4
        || note == NOTE_G5 || note == NOTE_G6
        || note == NOTE_G7)
      {
        digitalWrite(12, HIGH);
      }
      else if(note == NOTE_A1 || note == NOTE_A2
        || note == NOTE_A3 || note == NOTE_A4
        || note == NOTE_A5 || note == NOTE_A6
        || note == NOTE_A7)
      {
        digitalWrite(13, HIGH);
      }
      else if(note == NOTE_B1 || note == NOTE_B2
        || note == NOTE_B3 || note == NOTE_B4
        || note == NOTE_B5 || note == NOTE_B6
        || note == NOTE_B7)
      {
        digitalWrite(14, HIGH);
      }
      
      tone(7, note);
    }
  }  
}

void OnUpdate(byte id)
{
  toneModule.update(id);
}

// When recieved end of update
void OnAction(void)
{
  //TODO: Synchronizing module's action
  toneModule.action();
}

// When recieved start of connection
void OnStart(void)
{
  //TODO: Initialize argument of module
  toneModule.reset();
}

// When recieved exit of connection
void OnExit(void)
{
  //TODO: Initialize argument of module
  toneModule.reset();
}

void OnReady(void)
{
}

void setup()
{
  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);
  pinMode(10, OUTPUT);
  pinMode(11, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(13, OUTPUT);
  pinMode(14, OUTPUT);
  
  UnityRobot.attach(CMD_UPDATE, OnUpdate);
  UnityRobot.attach(CMD_ACTION, OnAction);
  UnityRobot.attach(CMD_START, OnStart);
  UnityRobot.attach(CMD_EXIT, OnExit);
  UnityRobot.attach(CMD_READY, OnReady);
  UnityRobot.begin(57600);
  
  toneModule.attach(OnToneChanged);
}

void loop()
{
  UnityRobot.process();
}
