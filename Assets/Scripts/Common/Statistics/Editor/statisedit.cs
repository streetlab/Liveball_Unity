using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(StatisControl))]
public class statisedit : Editor
	
	
	
{
	
	StatisControl _this;
	void OnEnable(){
		
		_this = target as StatisControl;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("setPosition")) {
			
			
			_this.editng();
		};
		
	}
	
	
	
}