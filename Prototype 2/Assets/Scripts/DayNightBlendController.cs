using UnityEngine;
using System.Collections;

public class DayNightBlendController : MonoBehaviour
{
	public float LightingUpdateTime = 0.1f;

	[Range(0.0f,1.0f)]
	public float BlendAmount;
	public Material SkyboxMaterial;

	public Light DayLight;
	public Light NightLight;

	[Range(0.0f,2.0f)]
	public float DayLightIntensity;
	[Range(0.0f,2.0f)]
	public float NightLightIntensity;
	
	// Use this for initialization
	void Start()
	{
		InvokeRepeating("UpdateLighting", 0, LightingUpdateTime);
	}

	void UpdateLighting()
	{
		SkyboxMaterial.SetFloat("_Blend", BlendAmount);
		DayLight.intensity = DayLightIntensity * (1 - BlendAmount);
		NightLight.intensity = NightLightIntensity * BlendAmount;
	}

	void OnDrawGizmos()
	{
		UpdateLighting();
	}
	
	// Update is called once per frame
	void Update()
	{
	}
}
