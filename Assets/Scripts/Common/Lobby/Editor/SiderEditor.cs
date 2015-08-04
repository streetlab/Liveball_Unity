using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(Sider))]
public class SliderEditor : Editor
	
	
	
{
//	bool SubMenu = false;
	Sider _this;

	void OnEnable(){
		
		_this = target as Sider;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();


		if (_this.Value.Count != _this.DivisionCount+1) {
			_this.Value.Clear();
			for(int i = 0 ; i< _this.DivisionCount+1 ; i++){
			_this.Value.Add ("");
			}
		}
//		if (GUILayout.Button ("CreateRightSubMenu")) {
//			
//			
//			_this.CreatRightSubMenu();
//		}
//		if (GUILayout.Button ("DeleteRightSubMenu")) {
//			_this.DeleteRightSubMenu();
//		}
	//	SubMenu = EditorGUILayout.BeginToggleGroup ("SubMenu", SubMenu);

	}
	
	
	
}