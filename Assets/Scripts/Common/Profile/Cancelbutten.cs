using UnityEngine;
using System.Collections;

public class Cancelbutten : MonoBehaviour {

	public void Cancle(){
		transform.parent.FindChild ("Input").GetComponent<UIInput> ().value = "";
		transform.parent.parent.FindChild ("ProfileSetting").GetComponent<ProfileSetting> ().back();
//		if (name == "NC") {
//
//			transform.parent.FindChild ("Input").FindChild ("Label").GetComponent<UILabel> ().text = 
//			transform.parent.parent.FindChild ("ProfileSetiing").GetComponent<ProfileSetting> ().UserName;
//		} else if (name == "SC") {
//			transform.parent.FindChild ("Input").FindChild ("Label").GetComponent<UILabel> ().text = 
//				transform.parent.parent.FindChild ("ProfileSetiing").GetComponent<ProfileSetting> ().UserState;
//		}
	}

}
