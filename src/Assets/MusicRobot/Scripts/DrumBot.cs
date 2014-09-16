using UnityEngine;
using System.Collections;
using System;
using UnityRobot;


public class DrumBot : MusicBot
{
	public Light baseDrumLight;
	public Light drum1Light;
	public Light drum2Light;
	public Light drum3Light;
	public Light drum4Light;
	public Light cymbals1Light;
	public Light cymbals2Light;

	public PulseModule pulseHandR;
	public PulseModule pulseHandL;
	public PulseModule pulseBase;
	public PulseModule pulseSnare;
	public PulseModule pulseDrum1;
	public PulseModule pulseDrum2;
	public PulseModule pulseDrum3;
	public PulseModule pulseHihatR;
	public PulseModule pulseHihatL;
	public PulseModule pulseCymbalsR;
	public PulseModule pulseCymbalsL;

	protected override void OnReady ()
	{
		base.OnReady ();

		LightTurnOff();
	}

	protected override void OnPlay (ToneTrack track)
	{
		base.OnPlay (track);

	//	Debug.Log(track.currentToneNote);

		LightTurnOff();

		switch(track.currentToneNote)
		{		
		case ToneNote.C3: // base drum
			baseDrumLight.intensity = 1f;
			pulseBase.DurationTime = 100;
			break;

		case ToneNote.C4: // drum2
			pulseDrum2.DurationTime = 100;
			pulseHandL.DurationTime = 100;
			break;

		case ToneNote.D3: // snare drum
			drum1Light.intensity = 1f;
			pulseSnare.DurationTime = 100;
			pulseHandR.DurationTime = 100;
			break;

		case ToneNote.D4: // drum1
			pulseDrum1.DurationTime = 100;
			pulseHandR.DurationTime = 100;
			break;

		case ToneNote.FS3: // hihat
			cymbals1Light.intensity = 1f;
			pulseHihatL.DurationTime = 100;
			pulseHandL.DurationTime = 100;
			break;

		case ToneNote.B3: // drum3
			pulseDrum3.DurationTime = 100;
			pulseHandL.DurationTime = 100;
			break;
		
		case ToneNote.CS4:
		case ToneNote.A4: // crash cymbals
			pulseCymbalsR.DurationTime = 100;
			pulseHandR.DurationTime = 100;
			break;

		case ToneNote.B4: // ride cymbals
			pulseCymbalsL.DurationTime = 100;
			pulseHandL.DurationTime = 100;
			break;

		case ToneNote.DS4: // ride cymbals
			pulseHihatR.DurationTime = 100;
			pulseHandR.DurationTime = 100;
			break;

		case ToneNote.MUTE:
			break;

		default:
			Debug.Log(track.currentToneNote);
			break;
		}
	}

	protected override void OnAwake ()
	{
		base.OnAwake ();
		
		LightTurnOff();
	}

	public override void Test ()
	{
		base.Test ();
	//	if(pulseBase != null)
	//		pulseBase.DurationTime = 500;

		pulseBase.DurationTime = 500;
	//	pulseSnare.DurationTime = 5000;
	//	pulseDrum1.DurationTime = 5000;
	//	pulseDrum2.DurationTime = 5000;
	//	pulseDrum3.DurationTime = 5000;
	//	pulseHihatR.DurationTime = 5000;
	//	pulseHihatL.DurationTime = 5000;
	//	pulseCymbalsR.DurationTime = 5000;
	//	pulseCymbalsL.DurationTime = 5000;
	}

	void LightTurnOff()
	{
		baseDrumLight.intensity = 0f;
		drum1Light.intensity = 0f;
		drum2Light.intensity = 0f;
		drum3Light.intensity = 0f;
		drum4Light.intensity = 0f;
		cymbals1Light.intensity = 0f;
		cymbals2Light.intensity = 0f;
	}
}
