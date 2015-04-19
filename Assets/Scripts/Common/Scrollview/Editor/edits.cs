using UnityEngine;
using UnityEditor;
using System;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(Maincontrol))]
public class edits : Editor


	
{

	Maincontrol _this;
	void OnEnable(){
	
		_this = target as Maincontrol;
	}


	
	
	public override void OnInspectorGUI(){
		base.DrawDefaultInspector ();
		if (GUILayout.Button ("setPosition")) {

			Debug.Log("dd");
			_this.editng();
		};

	}
	

	
}