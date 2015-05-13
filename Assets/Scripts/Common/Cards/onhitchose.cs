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
		string name, needxp,nowlv,maxlv,cardclass;
		name = transform.parent.FindChild ("Info").FindChild ("LblName").GetComponent<UILabel> ().text;
		needxp = transform.parent.FindChild ("needexp").GetComponent<UILabel> ().text;
		nowlv = transform.parent.FindChild ("SprLv").FindChild ("LblLv").GetComponent<UILabel> ().text;
		maxlv = transform.parent.FindChild ("Info").FindChild ("LblLv").GetComponent<UILabel> ().text;
		cardclass = transform.parent.FindChild ("Info").FindChild ("LblGrade").GetComponent<UILabel> ().text;
		strings = transform.parent.gameObject.ToString ().ToCharArray ();
	
		if (strings.Length == 37) {
			Debug.Log (strings [11]);
		} else if (strings.Length == 38) {
			Debug.Log (int.Parse (strings [11].ToString ()) * 10 + int.Parse (strings [12].ToString ()));
		} else if (strings.Length == 39) {
			Debug.Log (int.Parse (strings [11].ToString ()) * 100 + int.Parse (strings [12].ToString ()) * 10 + int.Parse (strings [13].ToString ()));
		}
		if (onoff) {
			if (transform.parent.parent.parent.GetComponent<ScriptCardsMiddle> ().cardcountP (name,needxp,nowlv,maxlv,cardclass)) {
			
				on();
			}

		} else {
			if (transform.parent.parent.parent.GetComponent<ScriptCardsMiddle> ().cardcountN (name,needxp,nowlv,maxlv,cardclass)) {
			
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
		transform.parent.FindChild ("SprBack").GetComponent<UISprite> ().spriteName = "bg_card_list_on";
		onoff = false;
}
	public void off(){
		GetComponent<UIButton> ().defaultColor = ButtenColor [0];
		GetComponent<UIButton> ().hover = ButtenColor [0];
		GetComponent<UIButton> ().pressed = ButtenColor [0];
		GetComponent<UIButton> ().disabledColor = ButtenColor [0];
		transform.FindChild ("Label").GetComponent<UILabel> ().text = "선택";
		transform.parent.FindChild ("SprBack").GetComponent<UISprite> ().spriteName = "bg_card_list_off";
		onoff = true;
	}
}