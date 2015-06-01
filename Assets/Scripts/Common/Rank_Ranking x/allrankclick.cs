using UnityEngine;
using System.Collections;

public class allrankclick : MonoBehaviour {
	char[] strings;
	public void onhit(){
		strings = gameObject.ToString ().ToCharArray();
		if (strings[0].ToString()=="i") {
			Debug.Log("myname");
		} else {
			Debug.Log ("allgoldrank : "+strings [3].ToString ());
		}
	}
}
