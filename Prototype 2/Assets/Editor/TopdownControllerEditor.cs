using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TopdownController))]
public class TopdownControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		TopdownController topdownController = (TopdownController)target;
		
		topdownController.WalkSpeed = EditorGUILayout.FloatField("Walk Speed", topdownController.WalkSpeed);
		topdownController.RunSpeed = EditorGUILayout.FloatField("Run Speed", topdownController.RunSpeed);
		topdownController.Gravity = EditorGUILayout.FloatField("Gravity", topdownController.Gravity);
		
		topdownController.ForwardAngleOffset = EditorGUILayout.FloatField("Forward Angle Offset", topdownController.ForwardAngleOffset);
		
		/*EditorGUILayout.LabelField("Time between wander (in seconds):");
		
		EditorGUILayout.BeginHorizontal();
		
		EditorGUILayout.MinMaxSlider(ref enemyMovement.WanderTimeMin, ref enemyMovement.WanderTimeMax, 0, 5);
		enemyMovement.WanderTimeMin = EditorGUILayout.FloatField(enemyMovement.WanderTimeMin, GUILayout.Width(30));
		enemyMovement.WanderTimeMax = EditorGUILayout.FloatField(enemyMovement.WanderTimeMax, GUILayout.Width(30));
		
		EditorGUILayout.EndHorizontal();*/
		
		if (GUI.changed)
		{
			topdownController.UpdateFacingTransform();
			EditorUtility.SetDirty(target);
		}
	}
}