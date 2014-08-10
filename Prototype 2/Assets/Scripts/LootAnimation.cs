using UnityEngine;
using System.Collections;

public class LootAnimation : MonoBehaviour
{
	public float RotationsPerSecond = 0.5f;
	public float HoversPerSecond = 0.5f;
	public float HoverHeight = 0.2f;

	Vector3 _initialPosition;
	float _initialTime;

	// Use this for initialization
	void Start ()
	{
		_initialPosition = transform.position;
		_initialTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Quaternion.AngleAxis((Time.timeSinceLevelLoad - _initialTime) * 360 * RotationsPerSecond, Vector3.up);

		float height = FloatExtension.Remap(Mathf.Sin(Time.timeSinceLevelLoad * Mathf.PI * 2 * HoversPerSecond), -1, 1, 0, 1) * HoverHeight;
		transform.position = _initialPosition + new Vector3(0, height, 0);
	}
}
