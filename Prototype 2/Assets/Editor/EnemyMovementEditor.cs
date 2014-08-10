using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyMovement))]
public class EnemyMovementEditor : Editor
{
	public override void OnInspectorGUI()
	{
		EnemyMovement enemyMovement = (EnemyMovement)target;

		enemyMovement.WanderRadius = EditorGUILayout.Slider("Wander radius:", enemyMovement.WanderRadius, 1, 5);

		EditorGUILayout.LabelField("Time between wander (in seconds):");

		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.MinMaxSlider(ref enemyMovement.WanderTimeMin, ref enemyMovement.WanderTimeMax, 0, 5);
		enemyMovement.WanderTimeMin = EditorGUILayout.FloatField(enemyMovement.WanderTimeMin, GUILayout.Width(30));
		enemyMovement.WanderTimeMax = EditorGUILayout.FloatField(enemyMovement.WanderTimeMax, GUILayout.Width(30));

		EditorGUILayout.EndHorizontal();

		if (GUI.changed)
			EditorUtility.SetDirty(target);
	}
}