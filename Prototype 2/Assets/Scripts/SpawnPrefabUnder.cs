using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class SpawnPrefabUnder : MonoBehaviour
{
	public GameObject Container;
	public Transform Prefab;

	public Vector3 Offset;

	public float MinTime = 0.2f;

	float _curTime = 0;

	CharacterController _characterController;

	// Use this for initialization
	void Start ()
	{
		_characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_curTime <= 0 && Input.GetKey(KeyCode.Space))
		{
			Vector3 spawnPos = _characterController.transform.position + _characterController.center - new Vector3(0, _characterController.height / 2, 0) + Offset;
			Transform newEnemy = Instantiate(Prefab, spawnPos, Quaternion.identity) as Transform;

			if (Container != null)
			{
				newEnemy.parent = Container.transform;
			}

			_curTime = MinTime;
		}

		_curTime -= Time.deltaTime;

		if (_curTime < 0)
		{
			_curTime = 0;
		}
	}
}
