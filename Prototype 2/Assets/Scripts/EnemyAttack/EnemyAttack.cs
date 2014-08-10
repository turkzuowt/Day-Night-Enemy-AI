using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	private GameObject oriTargetObject;
	GameObject targetBulding;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if(Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, 10))
		{
			if (hit.collider.gameObject.tag == "Building") 
			{
				Debug.LogWarning("hit building");

				targetBulding = hit.collider.gameObject;

				if (oriTargetObject == null) {
					oriTargetObject = transform.GetComponent<AIFollow>().target.gameObject;
				}

				transform.GetComponent<AIFollow>().target = hit.collider.gameObject.transform;

				targetBulding.SendMessage("BeAttacked", .1f, SendMessageOptions.DontRequireReceiver);

				if(!transform.animation.isPlaying)
				{
					transform.animation.Play ("attack");
				}
				
				transform.LookAt(targetBulding.transform.position);

			}
		}

		if (targetBulding == null && oriTargetObject != null) {
			transform.GetComponent<AIFollow>().target = oriTargetObject.transform;
			//transform.GetComponent<AIFollow>().target = GameObject.Find("Character").transform;
		}
	
	}

}
