using UnityEngine;
using System.Collections;

public class CameraFader : MonoBehaviour
{

	public Transform Player;

	GameObject Fadeable;

	float blendTime;
	float transperency;

	// Use this for initialization
	void Start ()
	{
		blendTime = 0;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		transperency = Mathf.Lerp (1, 0.2f, blendTime);

	 	if (Physics.Raycast(transform.position, Player.position - transform.position, out hit, 1000))
		{
			if (hit.collider.CompareTag("Fadeable") & blendTime < 1.2)
			{
				blendTime += 8 * Time.deltaTime;
				Fadeable = hit.collider.gameObject;
			}
			else if(blendTime > 0)
			{
				blendTime += -8 * Time.deltaTime;
			}
		}

		if (Fadeable != null)
		{
			Fadeable.renderer.material.SetFloat ("_TransValue", transperency);
		}
	}
}
