using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;
using System.Xml;


public class LightFactory : MonoBehaviour
{
	public EventHandler OnSignalGo;
	public EventHandler OnSignalStop;
	
	private Socket _socket;
	private int _port = 11004;
	private Thread _thread = null;
	private bool _signalGo = false;
	private bool _signalStop = false;

	void Awake()
	{
		_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);		
		IPEndPoint ipep = new IPEndPoint(IPAddress.Any, _port);		
		_socket.Bind(ipep);

		_thread = new Thread(ThreadWork);		
		_thread.Start();
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_signalGo == true)
		{

			_signalGo = false;
			if(OnSignalGo != null)
				OnSignalGo(this, null);
		}

		if(_signalStop == true)
		{
			_signalStop = false;
			if(OnSignalStop != null)
				OnSignalStop(this, null);
		}
	}

	void OnApplicationQuit()
	{
		Debug.Log("end");
		_thread.Abort();
		_socket.Close();
	}

	private void ThreadWork()		
	{
		while(true)
		{
			_socket.Listen(1);
			Debug.Log("wait client....");

			Socket client = _socket.Accept();//client에서 수신을 요청하면 접속합니다.
			Debug.Log("connected client");

			IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
			NetworkStream recvStm = new NetworkStream(client);

			while (true)
			{
				byte[] receiveBuffer = new byte[1024 * 1024];
				int length = recvStm.Read(receiveBuffer, 0, receiveBuffer.Length);
				if(length > 0)
				{
					int id = MakeUSHORT(receiveBuffer[0], receiveBuffer[1]);
					if(id == 0xFFFF)
					{
						int size = MakeUSHORT(receiveBuffer[2], receiveBuffer[3]);
						if(size == length)
						{
							//Debug.Log(string.Format("Size:{0:d}", size));

							byte checksum1 = receiveBuffer[4];
							byte checksum2 = receiveBuffer[5];
							byte calchecksum1 = 0;
							for( int i = 0; i < size; i++ )
							{
								if(i != 4 && i != 5)
									calchecksum1 ^= receiveBuffer[i];
							}
							calchecksum1 &= 0xFE;
							byte calchecksum2 = (byte)((~calchecksum1) & 0xFE);
							if(checksum1 == calchecksum1 && checksum2 == calchecksum2)
							{
								string xmlText = Encoding.Unicode.GetString(receiveBuffer, 8, (size - 8));
								//Debug.Log(xmlText);

								XmlDocument xmlDoc = new XmlDocument();
								try
								{
									xmlDoc.LoadXml(xmlText);
									XmlNode requestMsgNode = xmlDoc.SelectSingleNode("requestMsg");
									if(requestMsgNode != null)
									{
										if(requestMsgNode.Attributes["name"].Value.Equals("motion.play") == true)
										{
											XmlNode paramsNode = requestMsgNode.SelectSingleNode("params");
											if(paramsNode != null)
											{
												XmlNodeList paramList = paramsNode.SelectNodes("param");
												foreach(XmlNode param in paramList)
												{
													if(param.InnerText.Equals("go") == true)
													{
														_signalGo = true;
													}
													else if(param.InnerText.Equals("stop") == true)
													{
														_signalStop = true;
													}
												}
											}
										}
									}
								}
								catch(Exception e)
								{
									Debug.Log(e);
								}
							}
							else
								Debug.Log("checksum error");
						}
						else
							Debug.Log("size error");
					}
					else
						Debug.Log("id error");
				}
				else
				{
					Debug.Log("disconnected client");
					client.Close();
					break;
				}
			}
		}
	}

	private int MakeUSHORT(byte high, byte low)
	{
		return (int)(low + (high << 8));
	}
}
