using UnityEngine;
using System.Collections;
using System;
using UnityRobot;


public class BaseGuitarBot : MusicBot
{
	public Light light;
	public float trailTime = 1f;
	
	private float _time;
	
	protected override void OnReady ()
	{
		base.OnReady ();
		//		LightTurnOff();
	}
	
	protected override void OnPlay (ToneTrack track)
	{
		base.OnPlay (track);
		
		switch(track.currentToneNote)
		{
		case ToneNote.C1:
		case ToneNote.C2:
		case ToneNote.C3:
		case ToneNote.C4:
		case ToneNote.C5:
		case ToneNote.C6:
		case ToneNote.C7:		
			light.color = new Color(1f, 0f, 0f); // red
			break;
			
		case ToneNote.D1:
		case ToneNote.D2:
		case ToneNote.D3:
		case ToneNote.D4:
		case ToneNote.D5:
		case ToneNote.D6:
		case ToneNote.D7:		
			light.color = new Color(1f, 0.65f, 0f); // orange
			break;
			
		case ToneNote.E1:
		case ToneNote.E2:
		case ToneNote.E3:
		case ToneNote.E4:
		case ToneNote.E5:
		case ToneNote.E6:
		case ToneNote.E7:		
			light.color = new Color(1f, 1f, 0f); // yellow
			break;
			
		case ToneNote.F1:
		case ToneNote.F2:
		case ToneNote.F3:
		case ToneNote.F4:
		case ToneNote.F5:
		case ToneNote.F6:
		case ToneNote.F7:		
			light.color = new Color(0f, 0.5f, 0f); // green
			break;
			
		case ToneNote.G1:
		case ToneNote.G2:
		case ToneNote.G3:
		case ToneNote.G4:
		case ToneNote.G5:
		case ToneNote.G6:
		case ToneNote.G7:		
			light.color = new Color(0f, 0f, 1f); // blue
			break;
			
		case ToneNote.A1:
		case ToneNote.A2:
		case ToneNote.A3:
		case ToneNote.A4:
		case ToneNote.A5:
		case ToneNote.A6:
		case ToneNote.A7:		
			light.color = new Color(0.3f, 0f, 0.5f); // indigo
			break;
			
		case ToneNote.B1:
		case ToneNote.B2:
		case ToneNote.B3:
		case ToneNote.B4:
		case ToneNote.B5:
		case ToneNote.B6:
		case ToneNote.B7:		
			light.color = new Color(0.93f, 0.5f, 0.93f); // violet
			break;
		}
		
		if(track.currentToneNote != ToneNote.MUTE)
		{
			_time = 0f;
			light.intensity = 1f;
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
	
	void LightTurnOff()
	{
		_time = trailTime;
		light.intensity = 0f;
	}
}
