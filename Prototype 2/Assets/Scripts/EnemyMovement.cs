using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
	private NavMeshAgent _navAgent;
	
	public float WanderRadius = 5.0f;
	public float WanderTimeMin = 1.0f;
	public float WanderTimeMax = 2.0f;

	private bool _hasPath;

	// Use this for initialization
	void Start ()
	{
		_navAgent = GetComponent<NavMeshAgent>();
		_hasPath = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		/*if (Input.GetMouseButtonDown(0))
		{
			Plane p = new Plane(Vector3.up, Vector3.zero);

			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

			float rayDistance;

			if (p.Raycast(r, out rayDistance))
			{
				_navAgent.SetDestination(r.GetPoint(rayDistance));
			}
		}*/

		//Set has path to false if path is finished
		if (_hasPath && _navAgent.hasPath && _navAgent.remainingDistance <= 0.05)
		{
			_hasPath = false;
		}

		//If you don't have a path then set path
		if (!_hasPath) 
		{
			_navAgent.ResetPath();
			_hasPath = true;
			Invoke("Wander", Random.Range(WanderTimeMin, WanderTimeMax));
		}
	}

	void Wander()
	{
		while (true)
		{
			Vector2 randomDirection = Random.insideUnitCircle * WanderRadius;
			Vector3 random3DDirection = new Vector3(randomDirection.x, 0, randomDirection.y);

			NavMeshHit hit;

			if (NavMesh.SamplePosition(random3DDirection + transform.position, out hit, WanderRadius, 1))
			{
				//Retry if destination too close
				if (Vector3.Distance(hit.position, transform.position) < 1)
				{
					continue;
				}

				_navAgent.SetDestination(hit.position);
			}
			else
			{
				Debug.LogWarning("Can't sample destination?");
			}

			break;
		}
	}
}
