using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(Rankcontrol))]
public class rankedit : Editor
	
	
	
{
	
	Rankcontrol _this;
	void OnEnable(){
		
		_this = target as Rankcontrol;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("setPosition")) {
			
		
			_this.editng();
		};
		
	}
	
	
	
}