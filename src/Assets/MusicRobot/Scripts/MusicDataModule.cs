using UnityEngine;
using System.Collections;

namespace UnityRobot
{
	public class MusicDataModule : ModuleProxy
	{
		private byte _index;
		private byte _red;
		private byte _green;
		private byte _blue;
		
		void Awake()
		{
		}
		
		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update ()
		{
		}
		
		public override void Reset ()
		{
			_index = 0;
			_red = 255;
			_green = 0;
			_blue = 0;
			canUpdate = true;
		}
		
		public override void Action ()
		{
		}
		
		public override void OnPop ()
		{
		}
		
		public override void OnPush ()
		{
			Push (_index);
			Push (_red);
			Push (_green);
			Push (_blue);
		}

		public void SetData(int index, Color color)
		{
			_index = (byte)index;
			int value = (int)(color.r * 255f);
			_red = (byte)value;
			value = (int)(color.g * 255f);
			_green = (byte)value;
			value = (int)(color.b * 255f);
			_blue = (byte)value;

			canUpdate = true;
		}
	}
}
