using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MovementAnimationController))]
public class TopdownController : MonoBehaviour
{
	//Variables
	public float WalkSpeed = 6.0f;
	public float RunSpeed = 12.0f;
	public float Gravity = 20.0f;

	public float ForwardAngleOffset = 0;

	private Quaternion _facingOffset;

	private Vector3 _moveDirection = Vector3.zero;
	private float _gravityVelocity = 0.0f;

	private CharacterController _controller;
	private MovementAnimationController _animationController;

	//State
	//private CollisionFlags _collisionFlags;

	// Initialise variables (called when game starts running)
	void Start ()
	{
		_controller = GetComponent<CharacterController>();
		_animationController = GetComponent<MovementAnimationController>();

		UpdateFacingTransform();
	}
	
	// Update is called once per frame
	void Update()
	{
		//Feed moveDirection with input
		_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		//moveDirection = transform.TransformDirection(moveDirection);
		_moveDirection.Normalize ();
		_moveDirection = _facingOffset * _moveDirection;

		float moveSpeed = WalkSpeed;

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		{
			moveSpeed = RunSpeed;
		}
		
		//Set character state
		if (_controller.isGrounded)
		{
			_gravityVelocity = 0;
		}

		Vector3 oldPosition = _controller.transform.position;

		//Making the character move
		_controller.Move(_moveDirection * moveSpeed * Time.deltaTime);

		//Applying gravity to the controller
		_gravityVelocity += Gravity * Time.deltaTime;

		//Simulate gravity
		_controller.Move(new Vector3(0, -_gravityVelocity, 0) * Time.deltaTime);

		Vector3 velocity = _controller.transform.position - oldPosition;
		
		//Rotate to mouse if holding right click
		if (Input.GetMouseButton(1))
		{
			Plane lookPlane = new Plane (Vector3.up, transform.position);
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float dist;
			if(lookPlane.Raycast(ray, out dist))
			{
				Vector3 rayPoint = ray.GetPoint(dist);
				Vector2 anglePoint = new Vector2(rayPoint.x - transform.position.x, rayPoint.z - transform.position.z);

				float rotAngle = Mathf.Rad2Deg * (Mathf.Atan2(anglePoint.x, anglePoint.y));

				_animationController.RotateToFace(rotAngle);
			}
		}
		else //Rotate to facing
		{
			float movementAngle = Mathf.Rad2Deg * (Mathf.Atan2(velocity.x, velocity.z));

			if (Mathf.Abs(new Vector2(velocity.x, velocity.z).magnitude) > 0.01f )
			{
				_animationController.RotateToFace(movementAngle);
			}
		}

		//Update the animaiton based on speed
		_animationController.UpdateAnimation(velocity / Time.deltaTime);
	}

	public void UpdateFacingTransform()
	{
		_facingOffset = Quaternion.AngleAxis(ForwardAngleOffset, Vector3.up);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Vector3 facingDir = Vector3.forward;

		facingDir = _facingOffset * facingDir;

		Gizmos.DrawRay(new Ray(transform.position, facingDir));
	}
}