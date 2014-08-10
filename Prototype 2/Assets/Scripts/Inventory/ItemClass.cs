using UnityEngine;
using System.Collections;

public class ItemClass : MonoBehaviour {

	// Icons
	//static public Texture2D swordIcon;
	//static public Texture2D arrowIcon;
	//static public Texture2D breadIcon;
	//static public Texture2D shieldIcon;
	//static public Texture2D potionIcon;
	public Texture2D ewoodIcon;
	public Texture2D eironIcon;
	public Texture2D ewaterIcon;

	static public Texture2D woodIcon;
	static public Texture2D ironIcon;
	static public Texture2D waterIcon;

	// Items
	//public ItemCreatorClass swordItem = new ItemCreatorClass(0, "Sword", swordIcon, "Cut!!!");
	//public ItemCreatorClass arrowItem = new ItemCreatorClass(1, "Arrow", arrowIcon, "Shot!!!");
	//public ItemCreatorClass breadItem = new ItemCreatorClass(2, "Bread", breadIcon, "Food!!!");
	//public ItemCreatorClass shieldItem = new ItemCreatorClass(3, "Shield", breadIcon, "Protect!!!");
	//public ItemCreatorClass potionItem = new ItemCreatorClass(4, "Potion", breadIcon, "Drink!!!");
	//public ItemCreatorClass bowItem = new ItemCreatorClass(5, "Bow", breadIcon, "Bow!!!");
	//public ItemCreatorClass waterItem = new ItemCreatorClass(6, "Water", breadIcon, "Drink!!!");
	public ItemCreatorClass woodItem = new ItemCreatorClass(0, "wood", woodIcon, "Wood!!!");
	public ItemCreatorClass ironItem = new ItemCreatorClass(1, "iron", ironIcon, "Forge!!!");
	public ItemCreatorClass waterItem = new ItemCreatorClass(2, "water", waterIcon, "Drink!!!");

	// Use this for initialization
	void Start () {
		woodIcon = ewoodIcon;
		ironIcon = eironIcon;
		waterIcon = ewaterIcon;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public class ItemCreatorClass
	{
		public int id;
		public string name;
		public Texture2D icon;
		public string description;

		public ItemCreatorClass(int a_id, string a_name, Texture2D a_ico, string a_des)
		{
			id = a_id;
			name = a_name;
			icon = a_ico;
			description = a_des;
		}
	}
}
