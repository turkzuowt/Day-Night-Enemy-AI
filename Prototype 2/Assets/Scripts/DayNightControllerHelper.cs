using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DayNightControlle //r
{
	public static float TimeOfDay = 0;

	public static AnimationCurve NightCurve = null;
	public static AnimationCurve TorchCurve = null;
}

public class DayNightControllerHelper : MonoBehaviour
{
	public float TorchUpdateTime = 0.1f;

	public float FullDayLength;

	[Range(0.0f, 1.0f)]
	public float InitialTimeOfDay;

	public AnimationCurve NightCurve;
	public AnimationCurve TorchCurve;

	public GameObject DayNightCycle;
	public GameObject Torches;

	private DayNightBlendController _dayNightBlendController;
	private DayCycleLightPositionController _dayCycleLightPositionController;

	private TorchLightComponent[] _allTorches;
	
	// Use this for initialization
	void Start()
	{
		InvokeRepeating("UpdateTorches", 0, TorchUpdateTime);
		UpdateTorches();

		DayNightControlle.TimeOfDay = InitialTimeOfDay;
	}
	
	void UpdateTorches()
	{
		if (Torches == null) return;
		_allTorches = Torches.GetComponentsInChildren<TorchLightComponent>();
	}

	float ConvertToDayNightIntensityRange(float value)
	{
		return NightCurve.Evaluate(value);
	}

	float ConvertToTorchIntensityRange(float value)
	{
		return TorchCurve.Evaluate(value);
	}
	
	// Update is called once per frame
	void Update()
	{
		DayNightControlle.TimeOfDay = (Time.time % FullDayLength) / FullDayLength;

		UpdateLighting();
	}

	public void UpdateLighting()
	{
		//Stop doing this in production? Put in Start
		_dayNightBlendController = DayNightCycle.GetComponent<DayNightBlendController>();
		_dayCycleLightPositionController = DayNightCycle.GetComponent<DayCycleLightPositionController>();
		
		_dayNightBlendController.BlendAmount = ConvertToDayNightIntensityRange(DayNightControlle.TimeOfDay);
		_dayCycleLightPositionController.CycleTime = DayNightControlle.TimeOfDay;
		
		foreach (TorchLightComponent torch in _allTorches)
		{
			torch.IntensityPercent = ConvertToTorchIntensityRange(DayNightControlle.TimeOfDay);
		}
	}

	void OnDrawGizmos()
	{
		UpdateTorches();
	}
}
