using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour
{
	public GameObject SpawnRegionContainer;
	public Transform EnemyPrefab;
	public GameObject EnemyContainer;

	public float TimeBetweenCheck = 10;
	public int DesiredEnemyCount = 10;

	private BoxRegion[] _spawnRegions;

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("CheckSpawnEnemies", 0, TimeBetweenCheck);

		_spawnRegions = SpawnRegionContainer.GetComponentsInChildren<BoxRegion>();

		//Don't do anything if no spawn regions
		if (_spawnRegions.Length == 0)
		{
			DestroyImmediate(this);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void CheckSpawnEnemies()
	{
		//Todo: Despawn enemies too far away

		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		//Convert to for loop? (NOT WHILE WILL CRASH)

		if (enemies.Length < DesiredEnemyCount)
		{
			BoxRegion regionToSpawn = _spawnRegions[Random.Range(0, _spawnRegions.Length)];
			Vector3 randomPos = regionToSpawn.GetRandomPoint();

			NavMeshHit hit;

			if (NavMesh.SamplePosition(randomPos, out hit, 1, 1))
			{
				Transform newEnemy = Instantiate(EnemyPrefab, hit.position, Quaternion.identity) as Transform;

				if (EnemyContainer != null)
				{
					newEnemy.parent = EnemyContainer.transform;
				}
			}
			else
			{
				Debug.LogWarning("Sample position in spawning enemies is off map? Check regions.");
				Debug.LogWarning("Region Position: " + regionToSpawn.Origin);
			}
		}
	}
}
