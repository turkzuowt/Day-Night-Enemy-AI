using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InventoryGUI : MonoBehaviour {
    //private Rect inventoryWindowRect = new Rect(1000, 50, 70, 400);
    private Rect inventoryWindowRect;
	private bool inventoryWindowShow = false;
    static public bool overGUI = false;

    private int sceenWidth;
    private int sceenHeight;

    private string lastTooltip = "";


	// Dictionary of items
	static public Dictionary<int, string> inventoryNameDictionary = new Dictionary<int, string>()
		{
			{0, string.Empty},
			{1, string.Empty},
			{2, string.Empty},
			{3, string.Empty},
			{4, string.Empty},
			{5, string.Empty},
			{6, string.Empty},
			{7, string.Empty},
			{8, string.Empty},
			{9, string.Empty},
			{10, string.Empty},
			{11, string.Empty}
		};

	static public List<int> dictionaryAmounts = new List<int>()
	{
		0, 
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0
	};

    //ItemClass itemObject = new ItemClass();

	// Use this for initialization
	void Start () {
        sceenWidth = Screen.width;
        sceenHeight = Screen.height;
        inventoryWindowRect = new Rect((sceenWidth - 100), 50, 70, 400);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < inventoryNameDictionary.Keys.Count; ++i )
		{
			if(dictionaryAmounts[i] == 0)
			{
				inventoryNameDictionary[i] = string.Empty;
			}
		}
	}

    void OnGUI()
	{
        // Show inventory bar defaulty for debug
        inventoryWindowRect = GUI.Window(
            0, inventoryWindowRect,
            InventoryWindowMethod, "Inventory"
            );
	}

    void InventoryWindowMethod(int windowId)
    {
        // First Row
        GUILayout.BeginArea(new Rect(10, 20, 60, 400));//, new GUIContent("box"));

        GUILayout.BeginVertical();
		//GUILayout.Button(new GUIContent(inventoryNameDictionary[0], "box"), GUILayout.Width(50));
		GUILayout.Button(new GUIContent(StringToTexture2d(inventoryNameDictionary[0]), "MouseOver"), GUILayout.Width(50));
		GUILayout.Box(new GUIContent(dictionaryAmounts[0].ToString(),"MouseOver"),  GUILayout.Width(50));

		//GUILayout.Button(inventoryNameDictionary[1], GUILayout.Width(50));
		GUILayout.Button(new GUIContent(StringToTexture2d(inventoryNameDictionary[1]),"MouseOver"), GUILayout.Width(50));
        GUILayout.Box(new GUIContent(dictionaryAmounts[1].ToString(), "MouseOver"), GUILayout.Width(50));

        GUILayout.Button(new GUIContent(StringToTexture2d(inventoryNameDictionary[2]),"MouseOver"), GUILayout.Width(50));
        GUILayout.Box(new GUIContent(dictionaryAmounts[2].ToString(), "MouseOver"), GUILayout.Width(50));

		GUILayout.Button(new GUIContent(StringToTexture2d(inventoryNameDictionary[3]),"MouseOver"), GUILayout.Width(50));
		GUILayout.Box(new GUIContent(dictionaryAmounts[3].ToString(), "MouseOver"), GUILayout.Width(50));

		GUILayout.Button(new GUIContent(StringToTexture2d(inventoryNameDictionary[4]), "MouseOver"), GUILayout.Width(50));
        GUILayout.Box(new GUIContent(dictionaryAmounts[4].ToString(), "MouseOver"), GUILayout.Width(50));
        GUILayout.EndVertical();
        GUILayout.EndArea();

		if(Event.current.type == EventType.repaint && GUI.tooltip != lastTooltip)
		{
			if(lastTooltip != "")
				//Debug.LogWarning("OnMouseOut");
				overGUI = false;

			if(GUI.tooltip != "")
				//Debug.LogWarning("OnMouseOver");
				overGUI = true;

			lastTooltip = GUI.tooltip;
		}
    }

    public static Texture2D StringToTexture2d(string s)
	{
		switch(s)
		{
			case "wood":
					return ItemClass.woodIcon;
			case "iron":
					return ItemClass.ironIcon;
			case "water":
					return ItemClass.waterIcon;
			default:
					return null;
		}
	}
}
