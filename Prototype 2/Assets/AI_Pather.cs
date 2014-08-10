using UnityEngine;
using System.Collections;
using Pathfinding;


public class AI_Pather : MonoBehaviour 
{
	public Transform target;

	Seeker seeker;
	Path path;
	int currentWaypoint;

	public float speed = 10;

	CharacterController charController;

	float maxWayPointDistance = 0.5f;

	// Use this for initialization
	void Start ()
	{
		seeker = GetComponent<Seeker>();
		seeker.StartPath( transform.position, target.position, OnPathComplete );
		charController = GetComponent<CharacterController> ();
	}

	public void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
		else
		{
			Debug.Log(p.error);
		}
	}

	void FixedUpdate()
	{
		if (path == null)
		{
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count)
		{
			return;
		}

		//update the position of the unit
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized * speed;
		charController.SimpleMove (dir);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < maxWayPointDistance)
		{
			currentWaypoint++;
		}
	}
}
