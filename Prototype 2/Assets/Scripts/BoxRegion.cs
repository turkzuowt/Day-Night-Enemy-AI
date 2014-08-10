using UnityEngine;
using System.Collections;

public class BoxRegion : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public Vector3 Origin
	{
		get { return transform.position; }
	}

	public Vector2 Size
	{
		get { return new Vector2(transform.localScale.x, transform.localScale.z); }
	}

	public Vector3 GetRandomPoint()
	{
		float rx = Origin.x - Size.x / 2.0f + Random.Range(0.0f, Size.x);
		float rz = Origin.z - Size.y / 2.0f + Random.Range(0.0f, Size.y);

		return new Vector3(rx, 0, rz);
	}

	void OnDrawGizmos()
	{
		Vector3[] corners = {
			new Vector3(Origin.x - Size.x / 2.0f, Origin.y, Origin.z - Size.y / 2.0f),
			new Vector3(Origin.x + Size.x / 2.0f, Origin.y, Origin.z - Size.y / 2.0f),
			new Vector3(Origin.x + Size.x / 2.0f, Origin.y, Origin.z + Size.y / 2.0f),
			new Vector3(Origin.x - Size.x / 2.0f, Origin.y, Origin.z + Size.y / 2.0f)
		};

		Gizmos.DrawLine(corners[0], corners[1]);
		Gizmos.DrawLine(corners[1], corners[2]);
		Gizmos.DrawLine(corners[2], corners[3]);
		Gizmos.DrawLine(corners[3], corners[0]);

		Gizmos.color = Color.red * 0.5f;
		Gizmos.DrawCube(Origin, new Vector3(Size.x, 0, Size.y));
	}
}
