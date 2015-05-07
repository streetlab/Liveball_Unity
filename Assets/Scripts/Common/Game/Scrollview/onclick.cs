using UnityEngine;
using System.Collections;

public class onclick : MonoBehaviour {
	public GameObject Maincontrols;
	GameObject bgss;
	char[] strings;
	int pn,bn;
	// Use this for initialization
	public void butten(){
		strings = this.gameObject.ToString ().ToCharArray();
		Debug.Log (strings [4].ToString () + " : " + strings [6].ToString ());
		pn =int.Parse(strings [4].ToString ());
		strings = this.gameObject.ToString ().ToCharArray();
		bn = int.Parse(strings [6].ToString ());
		
		Maincontrols.GetComponent<Maincontrol> ().buttening (pn,bn);
		
	}
}
