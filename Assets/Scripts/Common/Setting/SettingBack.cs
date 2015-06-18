using UnityEngine;
using System.Collections;

public class SettingBack : MonoBehaviour {

	public void Onhit(){
		transform.parent.FindChild ("BtnMenu").gameObject.SetActive (true);
		transform.parent.parent.parent.FindChild ("Scroll").gameObject.SetActive (true);
		transform.parent.parent.parent.FindChild ("Change Member").gameObject.SetActive (false);
		gameObject.SetActive (false);
	}
}
