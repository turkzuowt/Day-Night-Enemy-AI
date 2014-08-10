using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DayNightControllerHelper))]
public class DayNightControllerHelperEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DayNightControllerHelper dayNightController = (DayNightControllerHelper)target;
		
		/*public float TorchUpdateTime = 0.1f;

		public float FullDayLength;

		[Range(0.0f, 1.0f)]
		public float TimeOfDay;

		public AnimationCurve NightCurve;
		public AnimationCurve TorchCurve;

		public GameObject DayNightCycle;
		public GameObject Torches;*/

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Torch update time (in seconds):");
		dayNightController.TorchUpdateTime = EditorGUILayout.FloatField(dayNightController.TorchUpdateTime);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Full Day Length (in seconds):");
		dayNightController.FullDayLength = EditorGUILayout.FloatField(dayNightController.FullDayLength);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Full Day Length (in minutes):");
		dayNightController.FullDayLength = EditorGUILayout.FloatField(dayNightController.FullDayLength / 60) * 60;
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Initial Time of day:", GUILayout.Width(80));
		dayNightController.InitialTimeOfDay = EditorGUILayout.Slider(dayNightController.InitialTimeOfDay, 0, 1);
		EditorGUILayout.EndHorizontal();

		dayNightController.NightCurve = EditorGUILayout.CurveField("Night Curve:", dayNightController.NightCurve);
		dayNightController.TorchCurve = EditorGUILayout.CurveField("Torch Curve:", dayNightController.TorchCurve);

		dayNightController.DayNightCycle = (GameObject)EditorGUILayout.ObjectField("Day Night Cycle: ", dayNightController.DayNightCycle, typeof(GameObject), true);
		dayNightController.Torches = (GameObject)EditorGUILayout.ObjectField("Torch Container: ", dayNightController.Torches, typeof(GameObject), true);
		
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
			dayNightController.UpdateLighting();
		}
	}
}