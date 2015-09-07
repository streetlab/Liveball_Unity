using UnityEngine;
using System.Collections;

public class ScriptNoticeTop : MonoBehaviour {

	bool closeClicked = false;

	void Start(){
		PlayerPrefs.SetString (Constants.PrefNotice, UtilMgr.GetDateTime("yyyyMMdd"));
	}

	public void CloseClicked(int cnt){
		if(closeClicked)
			return;

		closeClicked = true;

		string value = PlayerPrefs.GetString(Constants.PrefEvents);
		if(value != null && value.Equals(UtilMgr.GetDateTime("yyyyMMdd"))){
			if(cnt < 1){
				DialogueMgr.ShowDialogue("공지사항", "공지사항이 없습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, DialogHandler);
			} else
				AutoFade.LoadLevel("SceneLobby");
		} else{
			AutoFade.LoadLevel("SceneEvents");
		}

	}

	void DialogHandler(DialogueMgr.BTNS btn){
		AutoFade.LoadLevel("SceneLobby");
	}
}
