using UnityEngine;
using System.Collections;
using System;
using UnityRobot;


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
	public MidiPlayer _midiPlayer;
	public MusicBot[] _musicBots;
	public LightFactory _lightFactory;

	public GameObject _BGM;

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


	// Start -----------------------------------------------
	void Start () 
	{
		if(_lightFactory != null)
		{
			_lightFactory.OnSignalGo += OnSignalGo;
			_lightFactory.OnSignalStop += OnSignalStop;
		}

		foreach(MusicBot musicBot in _musicBots)
		{
			if(musicBot != null)
			{
				musicBot.toneTracks = _midiPlayer.FindTracks(musicBot.trackNames);
			}
		}
	}


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
		foreach(string port in _lstUsingPort)
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
		_midiPlayer.Play();
		_BGM.audio.Play();
	}


	// PauseMusicRobot ------------------------------------------------
	public void PauseMusicRobot()
	{
		if ((_CurState == EState.PLAY) || (_CurState == EState.RESUME))
		{
			_CurState = EState.PAUSE;
			_UILblPause.text = "Play";
			_midiPlayer.Pause();
			_BGM.audio.Pause();
		}
		else
		{
			_CurState = EState.RESUME;
			_UILblPause.text = "Pause";
			_midiPlayer.Resume();
			_BGM.audio.Play();
		}
	}


	// StopMusicRobot ------------------------------------------------
	public void StopMusicRobot()
	{
		_goPlayBtn.SetActive(true);
		_goPauseBtn.SetActive(false);
		_goStopBtn.SetActive(false);
		_goTimeBar.SetActive(false);
		_CurState = EState.STOP;
		_UILblPause.text = "Pause";
		_midiPlayer.Stop();
		_BGM.audio.Stop();
	}

	void OnSignalGo(object sender, EventArgs e)
	{
		PlayMusicRobot();
	}
	
	void OnSignalStop(object sender, EventArgs e)
	{
		StopMusicRobot();
	}









	// Update ----------------------------------------------
	void Update () 
	{
		UpdateDisplayTime();
	}




	// UpdateDisplayTime -----------------------------------------
	void UpdateDisplayTime()
	{
		if (_CurState == EState.READY)
			return;
		if (_CurState == EState.STOP)
			return;
		if (_CurState == EState.PAUSE)
			return;

		int nTotalMinut = (int)Mathf.Floor(_midiPlayer.totalTime / 60f);
		string strTotalMinute = string.Format("{0:D2}", nTotalMinut);

		int nTotalSecond = (int)Mathf.Floor(_midiPlayer.totalTime) - (nTotalMinut * 60);
		string strTotalSecond = string.Format("{0:D2}", nTotalSecond);

		int nCurMinut = (int)Mathf.Floor(_midiPlayer.currentTime / 60f);
		string strCurMinute = string.Format("{0:D2}", nCurMinut);
		
		int nCurSecond = (int)Mathf.Floor(_midiPlayer.currentTime) - (nCurMinut * 60);
		string strCurSecond = string.Format("{0:D2}", nCurSecond);

		_UILblMusicTimeLength.text = strTotalMinute + "." + strTotalSecond;
		_UILblMusicTimeBar.text = strCurMinute + "." + strCurSecond;

		_UISldTimeBar.value = _midiPlayer.currentTime / _midiPlayer.totalTime;

		if (!_midiPlayer.isPlaying)
			StopMusicRobot();
	}





















}

