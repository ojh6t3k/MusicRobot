﻿using UnityEngine;
using System.Collections;



public enum EState
{
	READY,
	STOP,
	PLAY,
	PAUSE,
	RESUME,
}




public class Main : MonoBehaviour 
{
	public string[] _lstUsingPort = new string[4];
	public string[] _lstConnectingPort = new string[4];

	public GameObject _goPopPort;
	public PopUp _scrPopPort;

	public GameObject _goPopStart;
	public PopUp _scrPopStart;

	public GameObject _goPlayBtn;
	public GameObject _goPauseBtn;
	public GameObject _goStopBtn;
	public UILabel _UILblPause;

	public GameObject _goTimeBar;
	public UISlider _UISldTimeBar;

	public UILabel _UILblMusicTimeLength;
	public UILabel _UILblMusicTimeBar;

	EState _CurState = EState.READY;


//	// Start -----------------------------------------------
//	void Start () 
//	{
//	
//	}


	// CheckUsePort ---------------------------------------------
	public bool CheckUsePort(string p_Port)
	{
		bool _bPort = true;

		foreach(string port in _lstUsingPort)
		{
			if (port == p_Port)
				_bPort = false;
		}

		foreach(string port in _lstConnectingPort)
		{
			if (port == p_Port)
				_bPort = false;
		}

		return _bPort;
	}


	// SetUsingPort ---------------------------------------------------
	public void SetUsingPort(int p_ID, string p_Port)
	{
		_lstUsingPort[p_ID-1] = p_Port;
	}

	// SetConnectingPort ---------------------------------------------
	public void SetConnectingPort(int p_ID, string p_Port)
	{
		_lstConnectingPort[p_ID-1] = p_Port;
	}




	public void CheckMusicRobot()
	{
		bool AllConnect = true;
		foreach(string port in _lstConnectingPort)
		{
			if (port == "")
				AllConnect = false;
		}
		
		if (AllConnect)
		{
			PlayMusicRobot();
		}
		else
		{
			_scrPopStart.SetTextAllRobot();
			_goPopStart.SetActive(true);
		}
	}



	// PlayMusicRobot ------------------------------------------------
	public void PlayMusicRobot()
	{
		_goPlayBtn.SetActive(false);
		_goPauseBtn.SetActive(true);
		_goStopBtn.SetActive(true);
		_goTimeBar.SetActive(true);
		_CurState = EState.PLAY;
	}


	// PauseMusicRobot ------------------------------------------------
	public void PauseMusicRobot()
	{
		if ((_CurState == EState.PLAY) || (_CurState == EState.RESUME))
		{
			_CurState = EState.PAUSE;
			_UILblPause.text = "Play";
		}
		else
		{
			_CurState = EState.RESUME;
			_UILblPause.text = "Pause";
		}
	}


	// StopMusicRobot ------------------------------------------------
	public void StopMusicRobot()
	{
		_goPlayBtn.SetActive(true);
		_goPauseBtn.SetActive(false);
		_goStopBtn.SetActive(false);
		_goTimeBar.SetActive(false);
	}











//	// Update ----------------------------------------------
//	void Update () 
//	{
//	
//	}


}
