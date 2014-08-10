using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;

    public float fireRate;
    private float canShootCounter;

    void Shoot()
    {
        //GameObject newBullet;
		//newBullet = Instantiate(bullet, transform.position + new Vector3(0, 0, 0), transform.rotation) as GameObject;
    }

	// Use this for initialization
	void Start () 
    {
	
	} 
	
	// Update is called once per frame
	void Update () 
    {
        if (canShootCounter <= 0 && Input.GetMouseButton(0) == true) 
        {
            Shoot();
            canShootCounter = fireRate;
        }

        canShootCounter -= Time.deltaTime;
	}
}