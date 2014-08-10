using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DayCycleLightPositionController : MonoBehaviour
{
	[Range(0.0f, 1.0f)]
	public float CycleTime;

	[Range(0.0f, 360.0f)]
	public float Tilt;

	public GameObject Lights;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		Lights.transform.rotation = Quaternion.AngleAxis(CycleTime * 360.0f, Vector3.up) * Quaternion.AngleAxis(Tilt, Vector3.right);
	}
}
