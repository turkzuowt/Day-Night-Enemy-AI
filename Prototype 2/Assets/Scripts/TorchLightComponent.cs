using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TorchLightComponent : MonoBehaviour
{
	//public Light TorchLight;

	[Range(0.0f, 5.0f)]
	public float InitialTorchIntensity;

	[Range(0.0f, 1.0f)]
	public float IntensityPercent;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//TorchLight.intensity = InitialTorchIntensity * Intensity;
		this.gameObject.light.intensity = InitialTorchIntensity * IntensityPercent;
	}
}
