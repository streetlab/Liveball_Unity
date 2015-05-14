﻿using UnityEngine;
using System.Collections;

public class ScriptSelectTeam : MonoBehaviour {

	public GameObject mBGBtnConfirm;
	bool mSelected;
	string mTeamCode;

	public string mStrJoinError;
	public string mErrorTitle;
	public string mErrorBody;

	JoinMemberInfo mMemInfo;

	static Color SELECTED = new Color (67f / 255f, 169f / 255f, 230f / 255f);
	static Color UNSELECTED = new Color (136f / 255f, 143f / 255f, 157f / 255f);

	void Start()
	{
		mBGBtnConfirm.GetComponent<UISprite> ().color = UNSELECTED;
		transform.FindChild ("ListTeam").GetComponent<UIScrollView> ().ResetPosition ();
	}

	public void Init(JoinMemberInfo memInfo){
		gameObject.SetActive (true);
		mMemInfo = memInfo;
	}

	public void SetTeam(string teamCode)
	{
		mSelected = true;
		mTeamCode = teamCode;
		mBGBtnConfirm.GetComponent<UISprite> ().color = SELECTED;
	}

	public void GoNext()
	{
		if (mSelected) {
			mMemInfo.FavoBB = mTeamCode;
			#if(UNITY_EDITOR)
			mMemInfo.OsType = 0;
			CompleteGCM();
			#elif(UNITY_ANDROID)
			mMemInfo.OsType = 1;
			AndroidMgr.RegistGCM(new EventDelegate(this, "CompleteGCM"));
			#else
			memInfo.OsType = 2;
			#endif
		} else if(!mSelected) {
			DialogueMgr.ShowDialogue (
				mErrorTitle, mErrorBody, DialogueMgr.DIALOGUE_TYPE.Alert,
			                          null, null, null, null);
		}
	}

	public void CompleteGCM()
	{
		Debug.Log ("CompleteGCM");
		string memUID = "";
		#if(UNITY_EDITOR)

		#elif(UNITY_ANDROID)
		memUID = AndroidMgr.GetMsg();
		#else
		#endif
		mMemInfo.MemUID = memUID;
		GetComponentInParent<ScriptTitle>().mProfileEvent = 
			new GetProfileEvent(new EventDelegate(this, "CompletedJoin"));

		NetMgr.JoinMember(mMemInfo, GetComponentInParent<ScriptTitle>().mProfileEvent, UtilMgr.IsTestServer(), true);
	}

	public void CompletedJoin(){
		if(GetComponentInParent<ScriptTitle>().mProfileEvent.Response.message != null
		   && GetComponentInParent<ScriptTitle>().mProfileEvent.Response.message.Length > 0){
			DialogueMgr.ShowDialogue(GetComponentInParent<ScriptTitle>().mJoinError,
			                         GetComponentInParent<ScriptTitle>().mProfileEvent.Response.message,
			                         DialogueMgr.DIALOGUE_TYPE.Alert, "", "", "", null);
			return;
		}
		PlayerPrefs.SetString(Constants.PrefEmail, mMemInfo.MemberEmail);
		PlayerPrefs.SetString(Constants.PrefPwd, mMemInfo.MemberPwd);

		GetComponentInParent<ScriptTitle>().DoLogin(mMemInfo.MemberEmail, mMemInfo.MemberPwd);
	}

	public void BackClicked()
	{
		UtilMgr.OnBackPressed ();
	}



	string TeamCode="";
	public void ChangeTeam(string teamcode){
		TeamCode = teamcode;
	}
	public ProfileManager PM;
	public void GoChangeTeam(){
		if (TeamCode != "") {
		
			PM.SetMemberTeam(TeamCode);
			transform.parent.FindChild("TF_Profile").gameObject.SetActive(true);
			transform.parent.FindChild("Top").gameObject.SetActive(true);
			transform.parent.FindChild("Top").FindChild("Panel").FindChild("ProfileSettings").gameObject.SetActive(true);
			transform.parent.FindChild ("TF_Profile").FindChild ("ProfileSetting").gameObject.SetActive (true);
				this.gameObject.SetActive (false);

		} else {
			DialogueMgr.ShowDialogue ("ERROR", "팀을 선택해 주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}
	}
	public void ChangeTeamBack()
	{
		this.gameObject.SetActive (false);
		transform.parent.FindChild("TF_Profile").gameObject.SetActive(true);
		transform.parent.FindChild("Top").gameObject.SetActive(true);
		transform.parent.FindChild("Top").FindChild("Panel").FindChild("ProfileSettings").gameObject.SetActive(true);
		transform.parent.FindChild ("TF_Profile").FindChild ("ProfileSetting").gameObject.SetActive (true);
		
	}
}
