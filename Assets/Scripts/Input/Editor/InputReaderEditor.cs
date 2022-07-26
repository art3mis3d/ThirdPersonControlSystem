using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputReader))]
public class InputReaderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (!Application.isPlaying)
			return;

		ScriptableObjectHelper.GenerateButtonsForEvents<InputReader>(target);
		Debug.Log("Generated Buttons for Events in UI");
	}
}
