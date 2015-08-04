using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(PreSettingCommander))]
public class PreSettingEditor : Editor
	
	
	
{
//	bool SubMenu = false;
	PreSettingCommander _this;

	void OnEnable(){
		
		_this = target as PreSettingCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();


	

		if (GUILayout.Button ("CreateItem")) {
			
			
			_this.CreatItem();
		}
		if (GUILayout.Button ("DeleteItem")) {
			_this.DeleteItem();
		}
	//	SubMenu = EditorGUILayout.BeginToggleGroup ("SubMenu", SubMenu);

	}
	
	
	
}