using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(LobbyNCCommander))]
public class LobbyNCEditor : Editor
	
	
	
{
	
	LobbyNCCommander _this;
	void OnEnable(){
		
		_this = target as LobbyNCCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("CreateNCItem")) {
			
			
			_this.CreatCItem();
		}
		if (GUILayout.Button ("CreateNC")) {
			
			
			_this.CreateNC();
		}
	
		if (GUILayout.Button ("ResetNC")) {
			
			
			_this.ResetNC();
		}
		
	}
	
	
	
}