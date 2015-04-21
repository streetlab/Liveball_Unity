using UnityEngine;
using System.Collections;

public class stclick : MonoBehaviour {
	char[] strings;
	public void stclicks(){
		//Debug.Log (gameObject.transform.parent.parent);
		strings = gameObject.transform.parent.parent.gameObject.ToString ().ToCharArray ();
		switch (int.Parse (strings [5].ToString())) {
		case 0:
			Debug.Log("0");
			break;
		case 1:
			Debug.Log("1");
			break;
		case 2:
			Debug.Log("2");
			break;
		
		}
	}
}
