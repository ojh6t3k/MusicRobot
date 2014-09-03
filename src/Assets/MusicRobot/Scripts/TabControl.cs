using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class TabControl : MonoBehaviour 
{
	public int _EnableTabCount = 1;

	public List<GameObject> _lstTab = new List<GameObject>();
	public List<EachTabButton> _lstTabScr = new List<EachTabButton>();



	// Start -----------------------------------------
	void Start () 
	{
		for(int i = 0; i < _lstTab.Count ; i++)
		{
			_lstTabScr.Add(_lstTab[i].GetComponent<EachTabButton>());

			if (i == 0)
				_lstTabScr[i].EnableTab();
			else 
				_lstTabScr[i].DisableTab();


			if (i > _EnableTabCount-1)
			{
				_lstTab[i].SetActive(false);
			}
		}
	}





	// DisableAllTab -----------------------------------------------------
	public void DisableAllTab(GameObject p_Tab)
	{
		for(int i = 0; i < _lstTab.Count ; i++)
		{
			if (p_Tab != _lstTab[i])
				_lstTabScr[i].DisableTab();
		}
	}












	// Update -----------------------------------------
	void Update () 
	{
	
	}
}
