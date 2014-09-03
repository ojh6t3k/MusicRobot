using UnityEngine;
using System.Collections;



public class EachTabButton : MonoBehaviour 
{
	public string _Name = "Tab";

	public GameObject _TabOn;
	public GameObject _TabOff;

	public UILabel _UILabelOn;
	public UILabel _UILabelOff;

	public GameObject _goPanel;



	// Start -----------------------------------------
	void Start () 
	{
		_UILabelOn.text = _Name;
		_UILabelOff.text = _Name;
	}



	public void EnableTab()
	{
		_TabOn.SetActive(true);
		_TabOff.SetActive(false);
		_goPanel.SetActive(true);

	}

	public void DisableTab()
	{
		_TabOn.SetActive(false);
		_TabOff.SetActive(true);
		_goPanel.SetActive(false);
	}



}
