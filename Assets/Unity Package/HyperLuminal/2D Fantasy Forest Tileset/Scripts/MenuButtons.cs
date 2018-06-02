using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour 
{
	void OnGUI()
	{
		GUI.backgroundColor = Color.green;
		GUIStyle style = new GUIStyle();
		style.fontSize = 32;
		style.normal.textColor = Color.white;
		GUILayout.BeginHorizontal();
		GUILayout.Space (Screen.width * 0.035f);
		GUILayout.BeginVertical();
		GUILayout.Space (Screen.height * 0.035f);
		if (GUILayout.Button ("Day Scene", GUILayout.Width (200), GUILayout.Height (40))) Application.LoadLevel(0);
		if (GUILayout.Button ("Night Scene", GUILayout.Width (200), GUILayout.Height (40))) Application.LoadLevel(1);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}
}
