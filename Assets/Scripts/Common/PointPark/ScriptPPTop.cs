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
		
		AutoFade.LoadLevel("SceneMain");
		
		
	}
}
