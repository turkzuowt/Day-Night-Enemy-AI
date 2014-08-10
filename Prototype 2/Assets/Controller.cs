using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class Controller : MonoBehaviour
{
	public float MoveSpeed;
	public float RotationSpeed;
	CharacterController CharController;

	// Use this for initialization
	void Start () 
	{
		CharController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 forward = Input.GetAxis ("Vertical") * transform.TransformDirection (Vector3.forward) * MoveSpeed;
		//Vector3 back = Input.GetAxis ("Vertical") * -transform.TransformDirection (Vector3.back) * MoveSpeed;

		//Vector3 left = Input.GetAxis ("Horizontal") * -transform.TransformDirection (Vector3.left) * MoveSpeed;
		Vector3 right = Input.GetAxis ("Horizontal") * transform.TransformDirection (Vector3.right) * MoveSpeed;

		//transform.Rotate (new Vector3(0,Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime,0));

		CharController.Move (forward * Time.deltaTime);
		CharController.Move (right * Time.deltaTime);

		CharController.SimpleMove (Physics.gravity);
	}
}
