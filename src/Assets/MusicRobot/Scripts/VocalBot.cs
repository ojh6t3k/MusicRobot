using UnityEngine;
using System.Collections;
using System;
using UnityRobot;

public class VocalBot : MusicBot
{
	public MusicDataModule musicData;
	public PulseModule pulse;

	protected override void OnAwake ()
	{
		base.OnAwake ();
	}

	protected override void OnUpdate ()
	{
		base.OnUpdate ();
	}

	protected override void OnReady ()
	{
		base.OnReady ();
	}

	protected override void OnPlay (ToneTrack track)
	{
		base.OnPlay (track);

		int index = 0;
		switch(track.currentToneNote)
		{
		case ToneNote.C1:
		case ToneNote.C2:
		case ToneNote.C3:
		case ToneNote.C4:
		case ToneNote.C5:
		case ToneNote.C6:
		case ToneNote.C7:
			index = 1;
			break;
			
		case ToneNote.CS1:
		case ToneNote.CS2:
		case ToneNote.CS3:
		case ToneNote.CS4:
		case ToneNote.CS5:
		case ToneNote.CS6:
		case ToneNote.CS7:
			index = 2;
			break;
			
		case ToneNote.D1:
		case ToneNote.D2:
		case ToneNote.D3:
		case ToneNote.D4:
		case ToneNote.D5:
		case ToneNote.D6:
		case ToneNote.D7:
			index = 3;
			break;
			
		case ToneNote.DS1:
		case ToneNote.DS2:
		case ToneNote.DS3:
		case ToneNote.DS4:
		case ToneNote.DS5:
		case ToneNote.DS6:
		case ToneNote.DS7:
			index = 4;
			break;
			
		case ToneNote.E1:
		case ToneNote.E2:
		case ToneNote.E3:
		case ToneNote.E4:
		case ToneNote.E5:
		case ToneNote.E6:
		case ToneNote.E7:
			index = 5;
			break;
			
		case ToneNote.F1:
		case ToneNote.F2:
		case ToneNote.F3:
		case ToneNote.F4:
		case ToneNote.F5:
		case ToneNote.F6:
		case ToneNote.F7:
			index = 6;
			break;
			
		case ToneNote.FS1:
		case ToneNote.FS2:
		case ToneNote.FS3:
		case ToneNote.FS4:
		case ToneNote.FS5:
		case ToneNote.FS6:
		case ToneNote.FS7:
			index = 7;
			break;
			
		case ToneNote.G1:
		case ToneNote.G2:
		case ToneNote.G3:
		case ToneNote.G4:
		case ToneNote.G5:
		case ToneNote.G6:
		case ToneNote.G7:
			index = 8;
			break;
			
		case ToneNote.GS1:
		case ToneNote.GS2:
		case ToneNote.GS3:
		case ToneNote.GS4:
		case ToneNote.GS5:
		case ToneNote.GS6:
		case ToneNote.GS7:
			index = 9;
			break;
			
		case ToneNote.A1:
		case ToneNote.A2:
		case ToneNote.A3:
		case ToneNote.A4:
		case ToneNote.A5:
		case ToneNote.A6:
		case ToneNote.A7:
			index = 10;
			break;
			
		case ToneNote.AS1:
		case ToneNote.AS2:
		case ToneNote.AS3:
		case ToneNote.AS4:
		case ToneNote.AS5:
		case ToneNote.AS6:
		case ToneNote.AS7:
			index = 11;
			break;
			
		case ToneNote.B1:
		case ToneNote.B2:
		case ToneNote.B3:
		case ToneNote.B4:
		case ToneNote.B5:
		case ToneNote.B6:
		case ToneNote.B7:
			index = 12;
			break;
		}
		
		if(track.currentToneNote != ToneNote.MUTE)
		{
			if(musicData != null)
				musicData.SetData(index, Color.red);
			if(pulse != null)
				pulse.DurationTime = 500;
		}
	}

	public override void Test ()
	{
		base.Test ();

		if(musicData != null)
			musicData.SetData(12, Color.red);
		if(pulse != null)
			pulse.DurationTime = 1000;
	}
}
