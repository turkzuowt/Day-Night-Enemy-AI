using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootControl : MonoBehaviour
{

	private Rect inventoryWindowRect;
	private bool inventoryWindowShow;
    public bool InventoryWindowShaw
    {
        set { inventoryWindowShow = value; }
    }

	private LootItems lootItems;

	private int sceenWidth;
	private int sceenHeight;

    private string lastTooltip = "";

	GameObject lootableGo = null;

	// Dictionary of items
	private List<string> lootList = new List<string>();
		//{
		//	{string.Empty},
		//	{string.Empty},
		//	{string.Empty},
		//	{string.Empty},
		//	{string.Empty},
		//	{string.Empty},
		//};

	private List<int> lootListAmounts = new List<int>();
	//{
	//	0, 
	//	0,
	//	0,
	//	0,
	//	0,
	//	0
	//};

	//ItemClass itemObject = new ItemClass();

	private Ray mouseRay;
	private RaycastHit rayHit;

	void Start()
	{
		sceenWidth = Screen.width;
		sceenHeight = Screen.height;
		inventoryWindowRect = new Rect((sceenWidth - 400) * 0.5f, 100, 400, 400);
		inventoryWindowShow = false;
	}

	// Update is called once per frame
	void Update()
	{
		mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Input.GetButtonDown("Fire1"))
		{
			Physics.Raycast(mouseRay, out rayHit);
			if(rayHit.collider && rayHit.collider.transform.tag == "Lootable")
			{
				lootableGo = rayHit.collider.gameObject;
				lootItems = rayHit.collider.gameObject.GetComponent<LootItems>();
				lootList = lootItems.lootDictionary;
				lootListAmounts = lootItems.lootDictionaryAmounts;
				inventoryWindowShow = true;
			}
		}

		// Close loot window
		if(Input.GetKeyDown("1"))
		{
			inventoryWindowShow = false;
            InventoryGUI.overGUI = false;
		}
	}

	void OnGUI()
	{
		if(inventoryWindowShow)
		{
			inventoryWindowRect = GUI.Window(1, inventoryWindowRect, InventoryWindowMethod, "Loot");
		}
	}

	void InventoryWindowMethod(int windowId)
	{
		if(GUI.Button(new Rect(360, 20, 30, 30), "X"))
			inventoryWindowShow = false;

		GUILayout.BeginArea(new Rect(10, 40, 400, 400));


		// First item
		GUILayout.BeginHorizontal();
		if(GUILayout.Button(new GUIContent(InventoryGUI.StringToTexture2d(lootList[0]), "MouseOver"), GUILayout.Width(50), GUILayout.Height(50)))
		{
			if(lootList[0] != string.Empty && lootListAmounts[0] != 0)
			{
				Loot(0);
			}
			if(lootListAmounts[0] == 0)
			{
				lootList[0] = string.Empty;
			}
		}
		GUILayout.Box(new GUIContent(lootListAmounts[0].ToString(), "MouseOver"), GUILayout.Height(50));

		// Second item
		GUILayout.BeginHorizontal();
		if(GUILayout.Button(new GUIContent(InventoryGUI.StringToTexture2d(lootList[1]), "MouseOver"), GUILayout.Width(50), GUILayout.Height(50)))
		{
			if(lootList[1] != string.Empty && lootListAmounts[1] != 0)
			{
				Loot(1);
			}
			if(lootListAmounts[1] == 0)
			{
				lootList[1] = string.Empty;
			}
		}
		GUILayout.Box(new GUIContent(lootListAmounts[1].ToString(), "MouseOver"), GUILayout.Height(50));


		// Third item
		GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent(InventoryGUI.StringToTexture2d(lootList[2]), "MouseOver"), GUILayout.Width(50), GUILayout.Height(50)))
		{
			if(lootList[2] != string.Empty && lootListAmounts[2] != 0)
			{
				Loot(2);
			}
			if(lootListAmounts[2] == 0)
			{
				lootList[2] = string.Empty;
			}
		}
        GUILayout.Box(new GUIContent(lootListAmounts[2].ToString(), "MouseOver"), GUILayout.Height(50));

		GUILayout.EndHorizontal();


		GUILayout.EndArea();

		if(Event.current.type == EventType.repaint && GUI.tooltip != lastTooltip)
		{
			if(lastTooltip != "")
				//Debug.LogWarning("OnMouseOut");
				InventoryGUI.overGUI = false;

			if(GUI.tooltip != "")
				//Debug.LogWarning("OnMouseOver");
				InventoryGUI.overGUI = true;

			lastTooltip = GUI.tooltip;
		}
	}

	void Loot(int index)
	{
        string item = lootList[index];
        
        // If there is the same item in inventory
        foreach (var inventoryItem in InventoryGUI.inventoryNameDictionary)
        {
            if (inventoryItem.Value == item)
            {
                if(lootListAmounts[index] != 0)
				{
                    lootListAmounts[index] -= 1;
                    InventoryGUI.dictionaryAmounts[inventoryItem.Key] += 1; 
                }
                else if(lootListAmounts[index] == 0)
                {
                    lootList[index] = string.Empty;
                }
				return;
            } 
        }

        // If there is not the same item in inventory
        // Need to find the first empty slot to put new item in
        foreach(var inventoryItem1 in InventoryGUI.inventoryNameDictionary)
        {
            if(inventoryItem1.Value == string.Empty)
            {
                InventoryGUI.inventoryNameDictionary[inventoryItem1.Key] = item;

                if(lootListAmounts[index] != 0)
                {
                    lootListAmounts[index] -= 1;
                    InventoryGUI.dictionaryAmounts[inventoryItem1.Key] += 1; 
                }
                else if(lootListAmounts[index] == 0)
                {
                    lootList[index] = string.Empty;
                }
                return;
            }
        }
	}

	public void Loot(string item)
	{
        // If there is the same item in inventory
		foreach(var inventoryItem in InventoryGUI.inventoryNameDictionary)
		{
			if(inventoryItem.Value == item)
			{
				InventoryGUI.dictionaryAmounts[inventoryItem.Key] += 1;
				return;
			}
		}

        // If there is not the same item in inventory
        // Need to find the first empty slot to put new item in
		foreach(var inventoryItem1 in InventoryGUI.inventoryNameDictionary)
		{
			if(inventoryItem1.Value == string.Empty)
			{
				InventoryGUI.inventoryNameDictionary[inventoryItem1.Key] = item;

				InventoryGUI.dictionaryAmounts[inventoryItem1.Key] += 1;
				return;
			}
		}
	}

}
