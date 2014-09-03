using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour 
{
	public UILabel _UILblText;

//	// Start --------------------------------------------
//	void Start () 
//	{
//	
//	}

	public void SetTextPort(string p_Text)
	{
		_UILblText.text = p_Text + " port already in use";
	}


	public void SetTextAllRobot()
	{
		_UILblText.text = "All robots are still not connected";
	}


	public void ClosePopUp()
	{
		gameObject.SetActive(false);
	}




//	// Update -------------------------------------------
//	void Update () 
//	{
//	
//	}
}
