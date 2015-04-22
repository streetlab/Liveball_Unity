using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(SeasonControl))]
public class Seasonedit : Editor
	
	
	
{
	
	SeasonControl _this;
	void OnEnable(){
		
		_this = target as SeasonControl;
	}
	
	
	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("setPosition")) {
			
			
			_this.editng();
		};
		
	}
	
	
	
}