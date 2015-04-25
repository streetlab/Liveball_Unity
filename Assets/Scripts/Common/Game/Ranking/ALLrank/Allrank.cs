using UnityEngine;
using System.Collections;

public class Allrank : MonoBehaviour {

	int num;
	char[] strings;
	public void onhit(){
		strings = gameObject.ToString ().ToCharArray ();
		num = int.Parse(strings [3].ToString ());
		Debug.Log ("rank : "+num);
	}
}
