using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(LobbyAddSub))]
public class LobbyAddSubEditor : Editor
	
	
	
{
	
	LobbyAddSub _this;
	void OnEnable(){
		
		_this = target as LobbyAddSub;

	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("AddSub")) {
			
			
			_this.AddSub();
		}
		if (GUILayout.Button ("DeleteSub")) {
			
			
			_this.DeleteSub();
		}
		
	}
	
	
	
}