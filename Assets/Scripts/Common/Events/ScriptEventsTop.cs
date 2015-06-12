using UnityEngine;
using System.Collections;

public class ScriptEventsTop : MonoBehaviour {
	bool closeClicked = false;

	public GameObject mEvents;

	void Start(){
		PlayerPrefs.SetString (Constants.PrefEvents, UtilMgr.GetDateTime("yyyyMMdd"));
	}
	
	public void CloseClicked(){
		if(closeClicked)
			return;

		if(mEvents.GetComponent<ScriptEvents>().Page >= mEvents.GetComponent<ScriptEvents>().MAX_PAGE){
			closeClicked = true;
			AutoFade.LoadLevel("SceneMain");
		} else{
			mEvents.GetComponent<ScriptEvents>().Page++;
			mEvents.GetComponent<ScriptEvents>().GoToNext();
		}


	}
}
