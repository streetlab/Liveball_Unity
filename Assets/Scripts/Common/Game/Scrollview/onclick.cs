using UnityEngine;
using System.Collections;

public class onclick : MonoBehaviour {
	public GameObject Maincontrols;
	GameObject bgss;
	char[] strings;
	int pn,bn;
	// Use this for initialization
	public void butten(){
		strings = this.gameObject.transform.parent.parent.parent.ToString ().ToCharArray();
	
		pn =int.Parse(strings [5].ToString ());
		strings = this.gameObject.ToString ().ToCharArray();
		bn = int.Parse(strings [3].ToString ());

		Maincontrols.GetComponent<Maincontrol> ().buttening ((pn*5)+bn);

	}
}
