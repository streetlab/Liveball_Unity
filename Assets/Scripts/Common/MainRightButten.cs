using UnityEngine;
using System.Collections;

public class MainRightButten : MonoBehaviour {
	public GameObject Top;
	//char [] array;
	public void Onhit(){
		//array = gameObject.ToString ().ToCharArray ();

		//transform.parent.parent.parent.parent.GetComponent<ScriptMainMenuRight> ().buttening (int.Parse( array[4].ToString()));
		Debug.Log (transform.FindChild ("Code").GetComponent<UILabel> ().text + " is teamcode");
		Top.GetComponent<ScriptMainTop> ().GoGame (transform.FindChild("Code").GetComponent<UILabel>().text);
	}
}
