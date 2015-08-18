using UnityEngine;
using System.Collections;

public class CloseInven : MonoBehaviour {

	public void Button(){
		ScriptMainTop.OpenBettingCheck = true;
		transform.root.FindChild ("Item").gameObject.SetActive (false);
	}
}
