using UnityEngine;
using System.Collections;

public class rankbutten : MonoBehaviour {
	int num;
	char[] strings;
	public void onhit(){
		strings = gameObject.ToString ().ToCharArray ();
		num = int.Parse(strings [3].ToString ());
		Debug.Log ("rankbutten : "+num);
	}
}
