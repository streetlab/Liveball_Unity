using UnityEngine;
using System.Collections;

public class infobutten : MonoBehaviour {

	char[] strings;
	public void onhit(){
		strings = gameObject.ToString ().ToCharArray ();
		if (strings [3] == '1') {
			Debug.Log ("FAQ :" + strings [3]);
		} else {
			Debug.Log ("INFO :" + strings [3]);
		}
	}
}
