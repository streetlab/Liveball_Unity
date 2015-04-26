using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class allrankonclick : MonoBehaviour {

	char[] strings;
	List<string> ch = new List<string>();
	string aa;

	string aa2;
	public void onhit(){
		strings = gameObject.ToString ().ToCharArray();

		//Debug.Log (strings.Length);

	
			for (int z = 3; z<strings.Length-25; z++) {
				
				ch.Add (strings [z].ToString ());
				
			}
			aa = string.Join ("", ch.ToArray ());

		strings = transform.parent.parent.parent.ToString ().ToCharArray();
			

			

		aa2 = strings [14].ToString ();
		

		//Debug.Log ("allgoldrank : " + aa);
//		strings = transform.parent.parent.parent.gameObject.ToString ().ToCharArray ();
//		aa = strings [14].ToString();
//		Debug.Log ("allgoldrank : " + aa);
		transform.parent.parent.parent.parent.gameObject.GetComponent<fanleaguecontrol> ().inallview (int.Parse(aa),int.Parse(aa2));
		ch.Clear ();


	}
}
