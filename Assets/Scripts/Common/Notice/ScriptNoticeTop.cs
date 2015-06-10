using UnityEngine;
using System.Collections;

public class ScriptNoticeTop : MonoBehaviour {

	void Start(){
		PlayerPrefs.SetString (Constants.PrefNotice, UtilMgr.GetDateTime("yyyyMMdd"));
	}

	public void CloseClicked(){
		string value = PlayerPrefs.GetString(Constants.PrefEvents);
		if(value != null && value.Equals(UtilMgr.GetDateTime("yyyyMMdd"))){
			AutoFade.LoadLevel("SceneMain");
		} else{
			AutoFade.LoadLevel("SceneEvents");
		}
	}
}
