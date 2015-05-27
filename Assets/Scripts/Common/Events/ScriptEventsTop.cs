using UnityEngine;
using System.Collections;

public class ScriptEventsTop : MonoBehaviour {

	void Start(){
		PlayerPrefs.SetString (Constants.PrefEvents, UtilMgr.GetDateTime("yyyyMMdd"));
	}
	
	public void CloseClicked(){
		AutoFade.LoadLevel("SceneGame");
	}
}
