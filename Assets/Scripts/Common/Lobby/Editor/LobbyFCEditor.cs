using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(LobbyFCCommander))]
public class LobbyFCEditor : Editor
	
	
	
{
	
	LobbyFCCommander _this;
	void OnEnable(){
		
		_this = target as LobbyFCCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("CreateFC")) {
			
			
			_this.CreateFC();
		}
	
		if (GUILayout.Button ("ResetFC")) {
			
			
			_this.ResetFC();
		}
		
	}
	
	
	
}