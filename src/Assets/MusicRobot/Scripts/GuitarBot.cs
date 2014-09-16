using UnityEngine;
using System.Collections;
using System;
using UnityRobot;


public class GuitarBot : MusicBot
{
	public Light light;
	public float trailTime = 1f;
	public MusicDataModule musicData;
	public PulseModule pulseLED;
	public PulseModule pulseHand;

	private float _time;

	protected override void OnReady ()
	{
		base.OnReady ();
//		LightTurnOff();
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
			if(light != null)
				light.color = new Color(1f, 0f, 0f); // red
			index = 1;
			break;

		case ToneNote.CS1:
		case ToneNote.CS2:
		case ToneNote.CS3:
		case ToneNote.CS4:
		case ToneNote.CS5:
		case ToneNote.CS6:
		case ToneNote.CS7:
			if(light != null)
				light.color = new Color(1f, 0.3f, 0f);
			index = 2;
			break;

		case ToneNote.D1:
		case ToneNote.D2:
		case ToneNote.D3:
		case ToneNote.D4:
		case ToneNote.D5:
		case ToneNote.D6:
		case ToneNote.D7:
			if(light != null)
				light.color = new Color(1f, 0.65f, 0f); // orange
			index = 3;
			break;

		case ToneNote.DS1:
		case ToneNote.DS2:
		case ToneNote.DS3:
		case ToneNote.DS4:
		case ToneNote.DS5:
		case ToneNote.DS6:
		case ToneNote.DS7:
			if(light != null)
				light.color = new Color(1f, 0.85f, 0f);
			index = 4;
			break;

		case ToneNote.E1:
		case ToneNote.E2:
		case ToneNote.E3:
		case ToneNote.E4:
		case ToneNote.E5:
		case ToneNote.E6:
		case ToneNote.E7:
			if(light != null)
				light.color = new Color(1f, 1f, 0f); // yellow
			index = 5;
			break;

		case ToneNote.F1:
		case ToneNote.F2:
		case ToneNote.F3:
		case ToneNote.F4:
		case ToneNote.F5:
		case ToneNote.F6:
		case ToneNote.F7:
			if(light != null)
				light.color = new Color(0f, 0.5f, 0f); // green
			index = 6;
			break;

		case ToneNote.FS1:
		case ToneNote.FS2:
		case ToneNote.FS3:
		case ToneNote.FS4:
		case ToneNote.FS5:
		case ToneNote.FS6:
		case ToneNote.FS7:
			if(light != null)
				light.color = new Color(0f, 0.25f, 0.5f);
			index = 7;
			break;

		case ToneNote.G1:
		case ToneNote.G2:
		case ToneNote.G3:
		case ToneNote.G4:
		case ToneNote.G5:
		case ToneNote.G6:
		case ToneNote.G7:
			if(light != null)
				light.color = new Color(0f, 0f, 1f); // blue
			index = 8;
			break;

		case ToneNote.GS1:
		case ToneNote.GS2:
		case ToneNote.GS3:
		case ToneNote.GS4:
		case ToneNote.GS5:
		case ToneNote.GS6:
		case ToneNote.GS7:
			if(light != null)
				light.color = new Color(0.15f, 0f, 0.75f);
			index = 9;
			break;

		case ToneNote.A1:
		case ToneNote.A2:
		case ToneNote.A3:
		case ToneNote.A4:
		case ToneNote.A5:
		case ToneNote.A6:
		case ToneNote.A7:
			if(light != null)
				light.color = new Color(0.3f, 0f, 0.5f); // indigo
			index = 10;
			break;

		case ToneNote.AS1:
		case ToneNote.AS2:
		case ToneNote.AS3:
		case ToneNote.AS4:
		case ToneNote.AS5:
		case ToneNote.AS6:
		case ToneNote.AS7:
			if(light != null)
				light.color = new Color(0.6f, 0.25f, 0.7f);
			index = 11;
			break;

		case ToneNote.B1:
		case ToneNote.B2:
		case ToneNote.B3:
		case ToneNote.B4:
		case ToneNote.B5:
		case ToneNote.B6:
		case ToneNote.B7:
			if(light != null)
				light.color = new Color(0.93f, 0.5f, 0.93f); // violet
			index = 12;
			break;
		}

		if(track.currentToneNote != ToneNote.MUTE)
		{
			_time = 0f;
			if(light != null)
				light.intensity = 1f;
			if(musicData != null)
				musicData.SetData(index, light.color);
			if(pulseLED != null)
				pulseLED.DurationTime = 1000;
			if(pulseHand != null)
				pulseHand.DurationTime = 100;
		}
	}

	protected override void OnAwake ()
	{
		base.OnAwake ();

	//	LightTurnOff();
	}

	protected override void OnUpdate ()
	{
		base.OnUpdate ();

		if(light != null)
		{
			if(light.intensity > 0f)
			{
				if(_time > trailTime)
					light.intensity = 0f;
				else
				{
					_time += Time.deltaTime;
					light.intensity = 1f - (_time / trailTime);
				}
			}
		}
	}

	public override void Test ()
	{
		base.Test ();
		if(pulseLED != null)
			pulseLED.DurationTime = 1000;
	}

	void LightTurnOff()
	{
		_time = trailTime;
		if(light != null)
			light.intensity = 0f;
	}
}
