using UnityEngine;
using System.Collections;

public class onhitchose : MonoBehaviour {

	char[] strings;
	// Use this for initialization
	public void onhit(){
		strings = transform.parent.gameObject.ToString ().ToCharArray ();
	
		if (strings.Length == 37) {
			Debug.Log (strings [11]);
		} else if (strings.Length == 38) {
			Debug.Log (int.Parse (strings [11].ToString()) * 10 + int.Parse (strings [12].ToString()));
		} else if (strings.Length == 39) {
			Debug.Log (int.Parse (strings [11].ToString()) * 100 + int.Parse (strings [12].ToString())* 10+ int.Parse (strings [13].ToString()));
		}
	}
}
