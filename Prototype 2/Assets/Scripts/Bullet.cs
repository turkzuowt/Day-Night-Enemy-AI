using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    
	/*private Spawner spawnManager;

    public float lifeTime;
    private float startTime;

    public float bulletSpeed;

    void OnTriggerEnter(Collider OtherObject)
    {
		if (OtherObject.gameObject.tag == "Player")
		{
			return;
		}
		if (OtherObject.gameObject.name == "Foe(Clone)")
        {
			spawnManager.Kill(OtherObject.gameObject);
        }
        Destroy(this.gameObject);
    }

	// Use this for initialization
	void Start () 
    {
        startTime = Time.timeSinceLevelLoad;
		spawnManager = GameObject.Find("SpawnObject").GetComponent<Spawner> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += bulletSpeed * transform.forward;

        float totalTime = Time.timeSinceLevelLoad - startTime;
        if( totalTime >= lifeTime )
        {
            Destroy(this.gameObject);
        }
	}*/
}
