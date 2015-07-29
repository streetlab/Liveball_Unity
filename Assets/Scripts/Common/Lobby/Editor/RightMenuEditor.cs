using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(RightMenuCommander))]
public class RightMenuEditor : Editor
	
	
	
{
//	bool SubMenu = false;
	RightMenuCommander _this;

	void OnEnable(){
		
		_this = target as RightMenuCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();


	

		if (GUILayout.Button ("CreateRightSubMenu")) {
			
			
			_this.CreatRightSubMenu();
		}
		if (GUILayout.Button ("DeleteRightSubMenu")) {
			_this.DeleteRightSubMenu();
		}
	//	SubMenu = EditorGUILayout.BeginToggleGroup ("SubMenu", SubMenu);

	}
	
	
	
}