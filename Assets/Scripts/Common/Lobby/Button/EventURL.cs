using UnityEngine;
using System.Collections;

public class EventURL : MonoBehaviour {

	public void Button(){
		Debug.Log ("UserMgr.legend : " + UserMgr.legend);
		Application.OpenURL(UserMgr.legend);
	}
}
