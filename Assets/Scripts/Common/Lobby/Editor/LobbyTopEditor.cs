using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(LobbyTopCommander))]
public class LobbyTopEditor : Editor
	
	
	
{
//	bool SubMenu = false;
	LobbyTopCommander _this;
	string[] SubList;
	void OnEnable(){
		
		_this = target as LobbyTopCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();


	

		if (GUILayout.Button ("CreateTopMenu")) {
			
			
			_this.CreateTop();
		}
		if (GUILayout.Button ("ResetTopMenu")) {
			_this.ResetTop();
		}
//		if (GUILayout.Button ("ResetTop2Menu")) {
//			_this.CreateTop2();
//		}
	//	SubMenu = EditorGUILayout.BeginToggleGroup ("SubMenu", SubMenu);

	}
	
	
	
}