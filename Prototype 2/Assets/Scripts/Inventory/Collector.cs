using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collector : MonoBehaviour {

	public GameObject m_player;
	public List<GameObject> collectedResource;
	protected Transform playerTransform;
	private bool rKeyPressed;
	private Vector2 buttonPos;
	private GameObject currObject;

	// Use this for initialization
	void Start () {
		collectedResource = new List<GameObject>();
		playerTransform = m_player.transform;
		rKeyPressed = false;
		buttonPos = new Vector3(20,40,80);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.E) == true && collectedResource.Count > 0 &&
		    rKeyPressed == false)
		{
			Vector3 pos = playerTransform.position;
			pos += playerTransform.forward * 5;
			GameObject releasedGo = collectedResource[0];
			releasedGo.transform.position = new Vector3(pos.x, pos.y, pos.z);
			releasedGo.SetActive(true);
			collectedResource.RemoveAt(0);

			rKeyPressed = true;
		}

		if (Input.GetKeyUp(KeyCode.E)) 
		{
			rKeyPressed = false;	
		}

		if (Input.GetKeyDown(KeyCode.R) && currObject != null)
		{
			RotateObj(currObject);
		}

		if (Input.GetKey(KeyCode.L) == true)
		{
			if(collectedResource.Count == 0)
				Debug.Log("Log: No resource.");
			else
			{
				foreach (GameObject go in collectedResource) 
				{
					Debug.Log("Log: Resource list: " + go.name + ".");
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Resource")
		{
			collectedResource.Add(other.gameObject);
			other.gameObject.SetActive(false);
		}
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,150, buttonPos.y + 30 + 30 * collectedResource.Count), "Inventory List");

		for(int i = 0; i < collectedResource.Count; ++i) 
		{
			GameObject go = collectedResource[i];
			if(GUI.Button(new Rect(buttonPos.x, buttonPos.y + i * 30, 100,20), go.name)) 
			{
				currObject = go;

				Vector3 pos = playerTransform.position;
				Vector3 direction = playerTransform.forward;

				pos += direction * 5f;
				currObject.transform.position = new Vector3(pos.x, currObject.transform.position.y, pos.z);
				currObject.SetActive(true);
				collectedResource.RemoveAt(i);
			}
		}

		if(GUI.Button(new Rect(buttonPos.x, buttonPos.y + 30 * collectedResource.Count, 100, 20), "Rotation") &&
		   currObject != null) {
			RotateObj(currObject);
		}
		
		if(GUI.Button(new Rect(buttonPos.x, buttonPos.y + 30 + 30 * collectedResource.Count, 100, 20), "Drop") &&
		   currObject != null) {
			currObject = null;
		}
	}

	protected void RotateObj(GameObject go)
	{
		float turnSpeed = 300f;

		go.transform.Rotate(0, Time.deltaTime * turnSpeed, 0, Space.World);
	}
}
