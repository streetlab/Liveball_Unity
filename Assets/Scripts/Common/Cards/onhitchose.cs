using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class onhitchose : MonoBehaviour {

	char[] strings;
	bool onoff =true;
	List<Color> ButtenColor = new List<Color>();
	// Use this for initialization

	void Start(){
		ButtenColor.Add (GetComponent<UIButton> ().defaultColor);
		ButtenColor.Add (GetComponent<UIButton> ().hover);
		ButtenColor.Add (GetComponent<UIButton> ().pressed);
		ButtenColor.Add (GetComponent<UIButton> ().disabledColor);
		GetComponent<UIButton> ().hover = ButtenColor [0];
	}
	public void onhit(){
		strings = transform.parent.gameObject.ToString ().ToCharArray ();
	
		if (strings.Length == 37) {
			Debug.Log (strings [11]);
		} else if (strings.Length == 38) {
			Debug.Log (int.Parse (strings [11].ToString ()) * 10 + int.Parse (strings [12].ToString ()));
		} else if (strings.Length == 39) {
			Debug.Log (int.Parse (strings [11].ToString ()) * 100 + int.Parse (strings [12].ToString ()) * 10 + int.Parse (strings [13].ToString ()));
		}
		if (onoff) {
			if (transform.parent.parent.parent.GetComponent<ScriptCardsMiddle> ().cardcountP ()) {
				onoff = false;
				on();
			}

		} else {
			if (transform.parent.parent.parent.GetComponent<ScriptCardsMiddle> ().cardcountN ()) {
				onoff = true;
				off();
			}
			;
		}
	}
	void on(){

		GetComponent<UIButton> ().defaultColor = ButtenColor [3];
		GetComponent<UIButton> ().hover = ButtenColor [3];
		GetComponent<UIButton> ().pressed = ButtenColor [3];
		GetComponent<UIButton> ().disabledColor = ButtenColor [3];
		transform.FindChild ("Label").GetComponent<UILabel> ().text = "취소";
}
	void off(){
		GetComponent<UIButton> ().defaultColor = ButtenColor [0];
		GetComponent<UIButton> ().hover = ButtenColor [0];
		GetComponent<UIButton> ().pressed = ButtenColor [0];
		GetComponent<UIButton> ().disabledColor = ButtenColor [0];
		transform.FindChild ("Label").GetComponent<UILabel> ().text = "선택";
	}
}