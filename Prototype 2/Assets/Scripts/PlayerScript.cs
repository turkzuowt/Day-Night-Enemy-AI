using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float hitPoints;
	public float damagePerHit;


	public void Damage()
	{
		hitPoints -= damagePerHit;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( hitPoints <= 0.0f )
		{
			//player is dead
			print("You dead");
		}
	}
}
