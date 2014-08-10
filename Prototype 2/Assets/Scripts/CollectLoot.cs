using UnityEngine;
using System.Collections;

public class CollectLoot : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		Loot loot = other.GetComponent<Loot>();

		if (loot != null)
		{
			switch (loot.PickupType)
			{
			case Loot.Type.Logs:
				GameObject.Find("LootWindowController").GetComponent<LootControl>().Loot("wood");
				Debug.Log("Add wood here");
				break;
			default:
				Debug.LogWarning("Loot type not handled: " + loot.PickupType);
				break;
			}

			Destroy(other.gameObject);
		}
	}
}
