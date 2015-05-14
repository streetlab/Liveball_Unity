using UnityEngine;
using System.Collections;

public class OkButten : MonoBehaviour {
	public ProfileManager PM;
	public void Ok(){
		if (name == "NO") {
			//transform.parent.parent.FindChild ("SetNamePage").FindChild("Input").FindChild("Label").GetComponent<UILabel>().text;

			if(transform.parent.parent.FindChild ("SetNamePage").FindChild("Input").FindChild("Label").GetComponent<UILabel>().text.Length>1){
				PM.SetMemberName(transform.parent.parent.FindChild ("SetNamePage").FindChild("Input").FindChild("Label").GetComponent<UILabel>().text);
			}else{
				DialogueMgr.ShowDialogue ("ERROR", "최소 2자 이상 입력해 주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			}
			} else if (name == "SO") {
		
		}
		transform.parent.FindChild ("Input").GetComponent<UIInput> ().value = "";
		transform.parent.parent.FindChild ("ProfileSetting").GetComponent<ProfileSetting> ().back();
	}

}
