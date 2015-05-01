using UnityEngine;
using System.Collections;

public class MainRightButten : MonoBehaviour {
	char [] array;
	public void Onhit(){
		array = gameObject.ToString ().ToCharArray ();

		transform.parent.parent.parent.parent.GetComponent<ScriptMainMenuRight> ().buttening (int.Parse( array[4].ToString()));
	}
}
