using UnityEngine;
using System.Collections;

public class ScriptNoticeTop : MonoBehaviour {

	bool closeClicked = false;

	void Start(){
		PlayerPrefs.SetString (Constants.PrefNotice, UtilMgr.GetDateTime("yyyyMMdd"));
	}

	public void CloseClicked(){
		if(closeClicked)
			return;

		closeClicked = true;

		string value = PlayerPrefs.GetString(Constants.PrefEvents);
		if(value != null && value.Equals(UtilMgr.GetDateTime("yyyyMMdd"))){
			AutoFade.LoadLevel("SceneMain");
		} else{
			AutoFade.LoadLevel("SceneEvents");
		}

	}
}
