using UnityEngine;
using System.Collections;

public class CoverFlow : MonoBehaviour {

	void Press(){
		Debug.Log ("Press");
	}
	void LateUpdate(){
		Debug.Log ("LateUpdate");
	}
	public void Drag(Vector2 V){
		Debug.Log ("Drag : " + V);
	}
}
