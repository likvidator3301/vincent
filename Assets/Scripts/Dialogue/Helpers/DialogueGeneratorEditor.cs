#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DialogueGenerator))]

public class DialogueGeneratorEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		GUILayout.Space(15);
		DialogueGenerator e = (DialogueGenerator)target;
		if (GUILayout.Button("Generate Dialogue XML"))
		{
			e.Generate();
		}
	}
}
#endif