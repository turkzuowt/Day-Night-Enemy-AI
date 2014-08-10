using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceableBuilding : MonoBehaviour {
	
	[HideInInspector]
    private bool isSelected;
	public string bName;

    public bool IsSelected
	{
		get { return isSelected; }
		set { isSelected = value; }
	}

	void OnGUI() {
		if (isSelected) {
			GUI.Button(new Rect(Screen.width /2, Screen.height / 20, 100, 30), bName);	
			
		}
		
	}
	
	public void SetSelected(bool s) {
		isSelected = s;	
	}

	
}
