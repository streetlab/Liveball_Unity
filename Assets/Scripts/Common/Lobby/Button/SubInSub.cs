using UnityEngine;
using System.Collections;

public class SubInSub : MonoBehaviour {

	public void Button(){
		Debug.Log ("MenuStatus " + LobbyMainCommander.MenuStatus + " : " + transform.parent.name + " : " + name);
	}
}
