using UnityEngine;
using System.Collections;

public class ScriptLoadingRoot : ScriptSuperRoot {

	// Use this for initialization
	void Start () {
		AutoFade.LoadLevel("SceneLobby");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
