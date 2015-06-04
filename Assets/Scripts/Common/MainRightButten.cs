using UnityEngine;
using System.Collections;

public class MainRightButten : MonoBehaviour {
GameObject Top;
	//char [] array;
	public void Onhit(){
		//array = gameObject.ToString ().ToCharArray ();
		if (transform.parent.parent.parent.parent.parent.parent.parent.name == "UI Root"){
			Top = transform.parent.parent.parent.parent.parent.parent.parent.transform.FindChild ("Top").gameObject;
		}else
		if (transform.parent.parent.parent.parent.parent.parent.parent.parent.name == "UI Root") {
			Top = transform.parent.parent.parent.parent.parent.parent.parent.parent.transform.FindChild ("Top").gameObject;
		} else {
			Top = transform.parent.parent.parent.parent.parent.parent.parent.transform.FindChild ("Top").gameObject;
		}
		//transform.parent.parent.parent.parent.GetComponent<ScriptMainMenuRight> ().buttening (int.Parse( array[4].ToString()));
		Debug.Log (transform.FindChild ("Code").GetComponent<UILabel> ().text + " is teamcode");
		string result = transform.parent.parent.parent.FindChild ("Label").GetComponent<UILabel> ().text;
		result = result [8].ToString () + result [9].ToString ();
		Top.GetComponent<ScriptMainTop> ().GoGame (transform.FindChild("Code").GetComponent<UILabel>().text,result);
	}
}
