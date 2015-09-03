using UnityEngine;
using System.Collections;

public class infobutten : MonoBehaviour {

	char[] strings;
	public void onhit(){
		strings = gameObject.name.ToCharArray ();
		if (strings [3] == '1') {
//			Debug.Log ("FAQ :" + strings [3]);
			AutoFade.LoadLevel("SceneTutorial");
		} else if((strings [3] == '2')) {
//			Debug.Log ("INFO :" + strings [3]);
			AutoFade.LoadLevel("SceneNotice");
		} else if((strings [3] == '3')) {
			AutoFade.LoadLevel("SceneEvents");
		} else if((strings [3] == '4')) {
			transform.parent.parent.parent.FindChild("bg_g").gameObject.SetActive(false);
			transform.parent.parent.parent.FindChild("bg_g 1").gameObject.SetActive(false);
			transform.parent.parent.parent.FindChild("bg_leave").gameObject.SetActive(true);
			transform.parent.parent.parent.parent.FindChild("Panel").FindChild("Label").GetComponent<UILabel>().text = "회원 탈퇴";
			transform.parent.parent.parent.parent.FindChild("Panel").FindChild("BtnClose").GetComponent<Setting>().IsLeaving = true;
		}
		else  if(gameObject.name == "Change"){

			transform.parent.parent.parent.parent.FindChild("Top").FindChild("Panel").FindChild("BtnMenu").gameObject.SetActive(false);
			transform.parent.parent.parent.parent.FindChild("Top").FindChild("Panel").FindChild("BtnBack").gameObject.SetActive(true);
			transform.parent.parent.parent.gameObject.SetActive(false);
			transform.parent.parent.parent.parent.FindChild("Change Member").gameObject.SetActive(true);
		}
	}
}
