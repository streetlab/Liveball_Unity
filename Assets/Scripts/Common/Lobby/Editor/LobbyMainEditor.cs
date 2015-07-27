using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(LobbyMainCommander))]
public class LobbyMainEditor : Editor
	
	
	
{
	
	LobbyMainCommander _this;
	void OnEnable(){
		
		_this = target as LobbyMainCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("Ratio Hight")) {

			
			_this.Ratio();
		}
		if (GUILayout.Button ("GetHightList")) {
			_this.GetHightList();
		}
		if (GUILayout.Button ("HightListClear")) {
			_this.HightListClear();
		}
		if (GUILayout.Button ("AllCreate")) {
			_this.AllCreate();
		}
		if (GUILayout.Button ("AllReset")) {
			_this.AllReset();
		}
		
	}
	
	
	
}