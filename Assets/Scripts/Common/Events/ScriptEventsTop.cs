using UnityEngine;
using System.Collections;

public class ScriptEventsTop : MonoBehaviour {

	public GameObject mEvents;

	void Start(){
		PlayerPrefs.SetString (Constants.PrefEvents, UtilMgr.GetDateTime("yyyyMMdd"));
	}
	
	public void CloseClicked(){
		if(mEvents.GetComponent<ScriptEvents>().Page >= mEvents.GetComponent<ScriptEvents>().MAX_PAGE){
			AutoFade.LoadLevel("SceneGame");
		} else{
			mEvents.GetComponent<ScriptEvents>().Page++;
			mEvents.GetComponent<ScriptEvents>().GoToNext();
		}
	}
}
