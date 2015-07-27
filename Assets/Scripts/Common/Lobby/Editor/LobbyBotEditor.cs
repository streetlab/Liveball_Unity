using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(LobbyBotCommander))]
public class LobbyBotEditor : Editor
	
	
	
{
	
	LobbyBotCommander _this;
	void OnEnable(){
		
		_this = target as LobbyBotCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("CreateBotMenu")) {
			
			
			_this.CreateBot();
		}
		if (GUILayout.Button ("ResetBotMenu")) {
			_this.ResetBot();
		}
		
	}
	
	
	
}