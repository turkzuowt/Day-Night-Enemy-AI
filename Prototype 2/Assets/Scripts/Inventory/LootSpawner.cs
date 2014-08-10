using UnityEngine;
using System.Collections;

public class LootSpawner : MonoBehaviour {

    public GameObject m_entityToSpawn = null;

    public int m_minSpawn = 5;
    public int m_maxSpawn = 10;

    public float m_spawnRadius = 50;

	public float m_respawnDuration = 60;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = m_respawnDuration;
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			//Debug.Log ("Should have spawned loot");
			Spawn();
			timer = m_respawnDuration;
		}
	}

    // Draw Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_spawnRadius);
    }

    void Spawn()
	{
        int count = Random.Range(m_minSpawn, m_maxSpawn);
		for(int i = 0; i < count; i++)
		{
			Vector3 position = new Vector3(Random.Range(-m_spawnRadius, m_spawnRadius), 0,
											Random.Range(-m_spawnRadius, m_spawnRadius));
			//position.Normalize();
			//position += transform.position;

			GameObject.Instantiate(m_entityToSpawn, transform.position + position, Quaternion.identity);
		}
	}
}
