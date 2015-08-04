using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(PresetContestCommander))]
public class PreSetContestEditor : Editor
	
	
	
{
//	bool SubMenu = false;
	PresetContestCommander _this;

	void OnEnable(){
		
		_this = target as PresetContestCommander;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();

		if(_this.PCCount1!=_this.PCCount2.Count){
			_this.PCCount2.Clear();
			for(int i = 0; i<_this.PCCount1;i++){
				_this.PCCount2.Add(0);
			}
		}

		if (GUILayout.Button ("CreateItem")) {
			
			
			_this.CreatItem();
		}
//		if (GUILayout.Button ("DeleteItem")) {
//			_this.DeteleItem();
//		}
	//	SubMenu = EditorGUILayout.BeginToggleGroup ("SubMenu", SubMenu);

	}
	
	
	
}