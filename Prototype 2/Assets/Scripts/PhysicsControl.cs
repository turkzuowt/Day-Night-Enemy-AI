using UnityEngine;
using System.Collections;

public class PhysicsControl : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fade"), true);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
