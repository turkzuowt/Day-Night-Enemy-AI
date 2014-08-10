using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponComponent : MonoBehaviour
{
	public GameObject[] OnFireParticleEmitters;
	List<ParticleSystem> _onFireParticles;

	bool _particlesEnabled = true;

	// Use this for initialization
	void Start ()
	{
		_onFireParticles = new List<ParticleSystem>();
		foreach (GameObject o in OnFireParticleEmitters)
		{
			ParticleSystem pSystem = o.GetComponent<ParticleSystem>();

			if (pSystem != null)
			{
				//Color newCol = new Color(pSystem.startColor.r, pSystem.startColor.g, pSystem.startColor.b, 1) * 0.2f;
				//newCol.a = 1;
				//pSystem.startColor = newCol;
				_onFireParticles.Add(pSystem);
			}
		}

		if (_onFireParticles.Count == 0)
		{
			Debug.LogWarning("No particles defined for weapon");
			_particlesEnabled = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{

            // Shot at building, cause dmg
			Vector3 shottingDirction = -transform.forward;
			RaycastHit hit;
			if(Physics.Raycast(transform.position, shottingDirction, out hit, 20))
			{
				Debug.Log("Shotting" + hit.collider.gameObject.name);
				hit.collider.gameObject.SendMessage("BeAttacked", 2f, SendMessageOptions.DontRequireReceiver);
			}

			if (_particlesEnabled)
			{
				//if (_onFireParticles.TrueForAll(x => x.isStopped))
				{
					foreach (ParticleSystem system in _onFireParticles)
					{
						//if (!system.isStopped) continue;
						system.Clear();

						system.Play();
					}
				}
			}
		}
	}
}
