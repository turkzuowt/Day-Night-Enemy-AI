using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;

public class LootItems : MonoBehaviour {

	//private Rect inventoryWindowRect = new Rect(300, 100, 400, 400);
	//private bool inventoryWindowShow = false;

    ItemClass itemObject;// = GameObject.Find("InventoryControl").GetComponent<ItemClass>();
	public bool fixedLoot = false;
    LootControl lootControl;

	// Dictionary of items
	public List<string> lootDictionary = new List<string>()
		{
			{string.Empty},
			{string.Empty},
			{string.Empty},
			{string.Empty},
			{string.Empty},
			{string.Empty}
		};

	public List<int> lootDictionaryAmounts = new List<int>()
    {
        0, 
        0,
        0,
        0,
        0,
        0
	};

	private Ray mouseRay;
	private RaycastHit rayHit;

    // For fixed loots
	public string firstLoot = string.Empty;
	public string secondLoot = string.Empty;
	public string thirdLoot = string.Empty;
	public string fourthLoot = string.Empty;
	public string fifthLoot = string.Empty;
	public string sixthLoot = string.Empty;

	public int firstLootAmount = 0;
	public int secondLootAmount = 0;
	public int thirdLootAmount = 0;
	public int fourthLootAmount = 0;
	public int fifthLootAmount = 0;
	public int sixthLootAmount = 0;

	// Use this for initialization
	void Start () {

        // Display Dictionary
		//lootDictionary[0] = itemObject.swordItem.name;
		//lootDictionaryAmounts[0] = 1;
		//lootDictionary[1] = itemObject.arrowItem.name;
		//lootDictionaryAmounts[1] = 10;

        //lootControl = GameObject.Find("lootWindowController").GetComponent<LootControl>();

        ItemClass itemObject = GameObject.Find("InventoryControl").GetComponent<ItemClass>();

        // RandomLoot
		if(fixedLoot == false)
		{
			lootDictionary[0] = LootRandomizer();
			lootDictionaryAmounts[0] = AmountRandomizer();
			lootDictionary[1] = LootRandomizer();
			lootDictionaryAmounts[1] = AmountRandomizer();
			lootDictionary[2] = LootRandomizer();
			lootDictionaryAmounts[2] = AmountRandomizer();
		}
		else
		{
            // Fixed Loot
			lootDictionary[0] = firstLoot;
			lootDictionaryAmounts[0] = firstLootAmount;
			lootDictionary[1] = secondLoot;
			lootDictionaryAmounts[1] = secondLootAmount;
			lootDictionary[2] = thirdLoot;
			lootDictionaryAmounts[2] = thirdLootAmount;
			lootDictionary[3] = fourthLoot;
			lootDictionaryAmounts[3] = fourthLootAmount;
			lootDictionary[4] = fifthLoot;
			lootDictionaryAmounts[4] = fifthLootAmount;
			lootDictionary[5] = sixthLoot;
			lootDictionaryAmounts[5] = sixthLootAmount;
		}
	}
	
	// Update is called once per frame
	void Update () {
        DestoryLootable();
	}

    // Get random loot
    public string LootRandomizer()
	{
        //ItemClass items = new ItemClass();
        ItemClass items = GameObject.Find("InventoryControl").GetComponent<ItemClass>();
		string returnString = string.Empty;
		int randomNumber = Random.Range(0, 3);

        switch(randomNumber)
		{
            case 0:
				returnString = items.ironItem.name;
				break;
            case 1:
				returnString = items.woodItem.name;
				break;
            case 2:
				returnString = items.waterItem.name;
				break;
			//case 3:
			//	returnString = items.shieldItem.name;
			//	break;
			//case 4:
			//	returnString = items.potionItem.name;
			//	break;
			//case 5:
			//	returnString = items.bowItem.name;
			//	break;
			//case 6:
			//	returnString = items.waterItem.name;
			//	break;
            default:
                returnString = string.Empty;
                break;
		}
		return returnString;
	}

    public int AmountRandomizer()
	{
		int returnAmount = 1;
		returnAmount = Random.Range(1, 11);

		return returnAmount;
	}

    private void DestoryLootable()
    {
        int emptySlotNum = 0;
        for (int i = 0; i < lootDictionaryAmounts.Count; ++i)
        {
            //emptySlotNum = (lootListAmounts[i] == 0) ? emptySlotNum++ : emptySlotNum;
            if (lootDictionaryAmounts[i] == 0)
            {
                emptySlotNum++;
            }
        }
        if (emptySlotNum != 0 && emptySlotNum == lootDictionaryAmounts.Count)
        {
            //lootControl.InventoryWindowShaw = false; 
            //GameObject.Find("lootWindowController").GetComponent<LootControl>().InventoryWindowShaw = false;
            GameObject.Destroy(transform.gameObject);
            emptySlotNum = 0;
            if (InventoryGUI.overGUI == true)
            {
                /* Wait for 0.5 second, then turn off overGUI;
                 * We can not turn off overGUI in this frame, because player update()
                 * is generating after this update
                 */
                //StartCoroutine(DelayImplement(0.5f, () =>
                //{
                    InventoryGUI.overGUI = false;
                //}));
            }
        }
    }
}
