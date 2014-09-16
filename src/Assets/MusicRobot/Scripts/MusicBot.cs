using UnityEngine;
using System.Collections;
using System;


namespace UnityRobot
{
	public class MusicBot : MonoBehaviour
	{
		public RobotProxy robot;
		public string[] trackNames;
		public ToneTrack[] toneTracks;

		public virtual void Test() {}

		protected virtual void OnReady() {}
		protected virtual void OnPlay(ToneTrack playTrack) {}
		protected virtual void OnAwake() {}
		protected virtual void OnUpdate() {}


		void Awake()
		{
			OnAwake();
		}

		// Use this for initialization
		void Start ()
		{
		}
		
		// Update is called once per frame
		void Update ()
		{
			OnUpdate();		
		}

		public void Ready()
		{
			foreach(ToneTrack tr in toneTracks)
				tr.OnNoteChanged += OnNoteChanged;

			OnReady();
		}

		void OnNoteChanged(object sender, EventArgs e)
		{
			OnPlay((ToneTrack)sender);
		}
	}
}
