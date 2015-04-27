using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ScriptTFProfile : MonoBehaviour {

	public GameObject mListProfile;
	public GameObject mProfile;
	public GameObject mPoint;
	public GameObject mMessages;
	public GameObject mLogs;
	GetProfileEvent mProfileEvent;
	// Use this for initialization
	void setarrray(){
		Debug.Log (mProfileEvent.Response.data.imageName);
		//mProfile.transform.GetChild (3).GetChild(1).GetComponent<UISprite> ().spriteName = "popop";
		string imgName = UtilMgr.GetTeamEmblem(mProfileEvent.Response.data.GetTeamName());
		mProfile.transform.GetChild (4).GetComponent<UISprite> ().spriteName = imgName;
		mProfile.transform.GetChild (5).GetComponent<UILabel> ().text = mProfileEvent.Response.data.memberName;
		mProfile.transform.GetChild (6).GetComponent<UILabel> ().text = mProfileEvent.Response.data.memberEmail;

		mPoint.transform.GetChild (2).GetChild (2).GetComponent<UILabel> ().text = mProfileEvent.Response.data.userRuby;
		mPoint.transform.GetChild (3).GetChild (2).GetComponent<UILabel> ().text = mProfileEvent.Response.data.userGoldenBall;
		mPoint.transform.GetChild (4).GetChild (2).GetComponent<UILabel> ().text = mProfileEvent.Response.data.userDiamond;
		mPoint.transform.GetChild (5).GetChild (2).GetComponent<UILabel> ().text = mProfileEvent.Response.data.userDiamond;
	}
	void Start () {

		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "setarrray"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);







		mProfile.SetActive (true);
		mPoint.SetActive (true);
		mMessages.SetActive (false);
		mLogs.SetActive (false);

		mProfile.transform.localPosition = new Vector3 (0f, 0f, 0f);
		float height = mProfile.transform.FindChild ("SprBG").GetComponent<UISprite> ().height + 70f;
		mPoint.transform.localPosition = new Vector3 (0f, -height, 0f);
		height += mPoint.transform.FindChild ("SprBG").GetComponent<UISprite> ().height + 200f;
		mMessages.transform.localPosition = new Vector3 (0f, -height, 0f);
		height += mMessages.transform.FindChild ("SprBG").GetComponent<UISprite> ().height + 80f;
		mLogs.transform.localPosition = new Vector3 (0f, -height, 0f);

		mListProfile.GetComponent<UIScrollView> ().ResetPosition ();
	}
	

}
