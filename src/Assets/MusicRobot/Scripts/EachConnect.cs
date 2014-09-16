using UnityEngine;
using System.Collections;
using System;
using UnityRobot;




public class EachConnect : MonoBehaviour 
{
	public Main _main;
	public string _Name = "Robot";
	public int _RobotID = 0;
	public RobotProxy _RobotProxy;
	public MusicBot _musicBot;

	public UIPopupList _UIPortList;

	bool IsConnect_Robot = false;

	public GameObject _goBtnConnect;
	public GameObject _goConnectingDeco;
	public GameObject _goPortList;
	public GameObject _goBtnDisconnect;
	public GameObject _goBtnTest;
	public GameObject _goRobotConnect;

	public UILabel _UIlblName;


	// Start ---------------------------------------------
	void Start () 
	{
		_UIlblName.text = _Name + " : " + PlayerPrefs.GetString("PP_Port" + _RobotID.ToString());

		_RobotProxy.OnConnected += OnConnected;
		_RobotProxy.OnConnectionFailed += OnConnectionFailed;
		_RobotProxy.OnDisconnected += OnDisconnected;
		_RobotProxy.OnSearchCompleted += OnSearchCompleted;
		
		_RobotProxy.PortSearch();

		_UIPortList.enabled = true; // NGUI
		_goConnectingDeco.SetActive(false); // NGUI
		_goRobotConnect.SetActive(false); // NGUI
		_goBtnDisconnect.SetActive(false); // NGUI
		_goBtnTest.SetActive(false); // NGUI
	}



	// Search_Ports -----------------------------------------------
	public void Search_Ports()
	{
		_goBtnConnect.SetActive(false);		// NGUI
		_RobotProxy.PortSearch();
	}
	

	// Connect_Port --------------------------------------------------
	public void Connect_Port()
	{
		if (_main == null)
		{
			Debug.LogWarning("No Main.cs");
			return;
		}

		if (!_main.CheckUsePort(_UIPortList.value))
		{
			_main._scrPopPort.SetTextPort(_UIPortList.value);
			_main._goPopPort.SetActive(true);
			return;
		}


		_UIPortList.enabled = false; // NGUI
		_goConnectingDeco.SetActive(true); // NGUI
		_goBtnConnect.SetActive(false);	// NGUI
		_goRobotConnect.SetActive(false); // NGUI

		_main.SetConnectingPort(_RobotID, _UIPortList.value);
		_main.SetUsingPort(_RobotID, "");

		_RobotProxy.portName = _UIPortList.value;
		_RobotProxy.Connect();
	}
	
	

	// Disconnect ------------------------------------------
	public void Disconnect()
	{
		_RobotProxy.Disconnect();

		IsConnect_Robot = false; // NGUI
		_UIPortList.enabled = true; // NGUI
		_goConnectingDeco.SetActive(false); // NGUI
		_goBtnConnect.SetActive(true);	// NGUI
		_goPortList.SetActive(true);	// NGUI
		_goRobotConnect.SetActive(false); // NGUI
		_goBtnDisconnect.SetActive(false); // NGUI
		_goBtnTest.SetActive(false); // NGUI
		Invoke("Search_Ports", 0.2f);
		
		_main.SetConnectingPort(_RobotID, "");
		_main.SetUsingPort(_RobotID, "");
	}



	// RobotTest ------------------------------------------
	public void RobotTest()
	{
		if(_musicBot != null)
			_musicBot.Test();
	}




//	// Update ---------------------------------------------
//	void Update () 
//	{
//	
//	}


	void OnConnected(object sender, EventArgs e)
	{
		IsConnect_Robot = true;
		_goConnectingDeco.SetActive(false); // NGUI
		_goBtnConnect.SetActive(false);	// NGUI
		_goPortList.SetActive(false);	// NGUI
		_goRobotConnect.SetActive(true); // NGUI
		_goBtnDisconnect.SetActive(true); // NGUI
		_goBtnTest.SetActive(true); // NGUI

		_main.SetConnectingPort(_RobotID, "");
		_main.SetUsingPort(_RobotID, _RobotProxy.portName);

		PlayerPrefs.SetString("PP_Port" + _RobotID.ToString(), _RobotProxy.portName);
		_UIlblName.text = _Name + " : " + PlayerPrefs.GetString("PP_Port" + _RobotID.ToString());

		if(_musicBot != null)
			_musicBot.Ready();
	}
	
	void OnConnectionFailed(object sender, EventArgs e)
	{
		IsConnect_Robot = false; // NGUI
		_UIPortList.enabled = true; // NGUI
		_goConnectingDeco.SetActive(false); // NGUI
		_goBtnConnect.SetActive(true);	// NGUI
		_goPortList.SetActive(true);	// NGUI
		_goRobotConnect.SetActive(false); // NGUI
		_goBtnDisconnect.SetActive(false); // NGUI
		_goBtnTest.SetActive(false); // NGUI

		_main.SetConnectingPort(_RobotID, "");
		_main.SetUsingPort(_RobotID, "");
	}
	
	void OnDisconnected(object sender, EventArgs e)
	{
		IsConnect_Robot = false; // NGUI
		_UIPortList.enabled = true; // NGUI
		_goConnectingDeco.SetActive(false); // NGUI
		_goBtnConnect.SetActive(true);	// NGUI
		_goPortList.SetActive(true);	// NGUI
		_goRobotConnect.SetActive(false); // NGUI
		_goBtnDisconnect.SetActive(false); // NGUI
		_goBtnTest.SetActive(false); // NGUI
		Invoke("Search_Ports", 0.2f);

		_main.SetConnectingPort(_RobotID, "");
		_main.SetUsingPort(_RobotID, "");
	}
	
	void OnSearchCompleted(object sender, EventArgs e)
	{
		_UIPortList.items.Clear();
		
		if(_RobotProxy.portNames.Count > 0)
		{
			int nSelectItem = 0;
			for(int i=0; i<_RobotProxy.portNames.Count; i++)
			{
				_UIPortList.items.Add(_RobotProxy.portNames[i]);
				if (_RobotProxy.portNames[i] == PlayerPrefs.GetString("PP_Port" + _RobotID.ToString()))
					nSelectItem = i;
			}
			_UIPortList.value = _UIPortList.items[nSelectItem];
			_goBtnConnect.SetActive(true);		// NGUI


		}
		else if(_RobotProxy.portNames.Count == 0)
		{
			_UIPortList.items.Add("None");
		}
	}


}
