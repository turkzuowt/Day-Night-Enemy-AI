using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Resource : MonoBehaviour {

	public GameObject m_player;
	private List<GameObject> collectedResource;

	// Use this for initialization
	void Start () {
		collectedResource = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddResource(GameObject go)
	{
		collectedResource.Add(go);
	}

	void ReleaseResource(GameObject go)
	{
		return;
	}
}