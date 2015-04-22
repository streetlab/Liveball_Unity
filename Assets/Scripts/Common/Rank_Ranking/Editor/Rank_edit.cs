using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(Rank_Control))]
public class Rank_edit : Editor
	
	
	
{
	
	Rank_Control _this;
	void OnEnable(){
		
		_this = target as Rank_Control;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("setPosition")) {
			
			
			_this.editng();
		};
		
	}
	
	
	
}