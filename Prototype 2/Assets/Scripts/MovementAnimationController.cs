using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementAnimationController : MonoBehaviour
{
	//Animation
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip sidestepAnimation;
	//public AnimationClip attackAnimation;
	//Add Attack animation ^^^^^^^^^
	
	public float turnSpeed = 2000f;

	public float walkMaxAnimationSpeed = 1.0f;
	public float runMaxAnimationSpeed = 1.0f;
	public float backwalkMaxAnimationSpeed = 0.75f;
	public float sidestepMaxAnimationSpeed = 1.0f;

	public float runningSpeed = 10.0f;

	public GameObject player;
	
	private Animation _animation;

	private float _facingAngle;

	public float FacingAngle
	{
		get { return _facingAngle; }
	}

	// Use this for initialization
	void Start ()
	{
		_animation = player.GetComponent<Animation>();

		if(!_animation)
			Debug.LogWarning("The character you would like to control doesn't have animations. Moving them might look weird.");
		
		if(!idleAnimation)
		{
			_animation = null;
			Debug.LogWarning("No idle animation found. Turning off animations.");
		}
		
		if(!walkAnimation)
		{
			_animation = null;
			Debug.LogWarning("No walk animation found. Turning off animations.");
		}
		
		if(!runAnimation)
		{
			_animation = null;
			Debug.LogWarning("No run animation found. Turning off animations.");
		}

		_facingAngle = transform.rotation.eulerAngles.y;
	}

	public void RotateToFace(float angle)
	{
		_facingAngle = Mathf.MoveTowardsAngle(_facingAngle, angle, turnSpeed * Time.deltaTime);
	}

	public void UpdateAnimation(Vector3 velocity)
	{
		if (_animation)
		{
			Vector2 movement2D = new Vector2(velocity.x, velocity.z);

			float moveSpeed = movement2D.magnitude;

			if (moveSpeed < 0.1)
			{
				_animation.CrossFade(idleAnimation.name);
				return;
			}
			else
			{
				Vector2 facingVector = new Vector2((float)Mathf.Sin(Mathf.Deg2Rad * _facingAngle), (float)Mathf.Cos(Mathf.Deg2Rad * _facingAngle));
				Vector2 sideVector = new Vector2(facingVector.y, facingVector.x);
				
				float forwardAngleDot = Vector2.Dot(movement2D.normalized, facingVector);
				float sideAngleDot = Vector2.Dot(movement2D.normalized, sideVector);
				
				float forwardMovementSpeed = moveSpeed * forwardAngleDot;
				float sidewaysMovementSpeed = moveSpeed * sideAngleDot;

				//If moving forward/backwards faster than sideways
				if (Mathf.Abs(forwardMovementSpeed) > Mathf.Abs(sidewaysMovementSpeed))
				{
					if (forwardMovementSpeed > 0)
					{
						//Walking forwards
						if (forwardMovementSpeed > runningSpeed)
						{
							_animation[runAnimation.name].speed = Mathf.Clamp(forwardMovementSpeed, 0.0f, runMaxAnimationSpeed);
							_animation.CrossFade(runAnimation.name);
						}
						else
						{
							_animation[walkAnimation.name].speed = Mathf.Clamp(forwardMovementSpeed, 0.0f, walkMaxAnimationSpeed);
							_animation.CrossFade(walkAnimation.name);
						}
					}
					else
					{
						//Walking backwards
						_animation[walkAnimation.name].speed = Mathf.Clamp(forwardMovementSpeed, -backwalkMaxAnimationSpeed, 0.0f);
						_animation.CrossFade(walkAnimation.name);
					}
				}
				else
				{
					//Walking sideways
					if (sidewaysMovementSpeed > 0)
					{
						_animation[sidestepAnimation.name].speed = Mathf.Clamp(sidewaysMovementSpeed, 0.0f, sidestepMaxAnimationSpeed);
						_animation.CrossFade(sidestepAnimation.name);
					}
					else
					{
						_animation[sidestepAnimation.name].speed = Mathf.Clamp(sidewaysMovementSpeed, -sidestepMaxAnimationSpeed, 0.0f);
						_animation.CrossFade(sidestepAnimation.name);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		player.transform.rotation = Quaternion.AngleAxis(_facingAngle, Vector3.up);
		//transform.rotation = Quaternion.AngleAxis(_facingAngle, Vector3.up);
	}
}
