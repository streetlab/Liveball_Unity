using UnityEngine;
using System.Collections;

public class ScriptPPTop : MonoBehaviour {

	bool closeClicked = false;
	GetProfileEvent mProfileEvent;
	public void CloseClicked(){
		if(closeClicked)
			return;

		closeClicked = true;
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "ProfileUpdate"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);
	}

	void ProfileUpdate(){
		UserMgr.UserInfo = mProfileEvent.Response.data;
//		Debug.Log (Application.loadedLevelName);
//		if (Application.loadedLevelName.Equals ("SceneLobby")) {
			AutoFade.LoadLevel("SceneLobby");
//		}else if(Application.loadedLevelName.Equals ("SceneMain 1")){
//			AutoFade.LoadLevel("SceneMain 1");
//		}

		
		
	}
}
