using UnityEngine;
using System.Collections;


public class ScriptSelectTeam : MonoBehaviour {

	public GameObject mBGBtnConfirm;
	bool mSelected;
	string mTeamCode;
	bool IsGuest;

	public string mStrJoinError;
	public string mErrorTitle;
	public string mErrorBody;

	bool GotToken = false;
	string mToken;

	JoinMemberInfo mMemInfo;
	LoginEvent mLoginEvent;

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
		IsGuest = false;
	}

	public void InitGuest(LoginEvent loginEvent){
		gameObject.SetActive (true);
		IsGuest = true;
		mLoginEvent = loginEvent;
	}

	public void SetTeam(string teamCode)
	{
		mSelected = true;
		mTeamCode = teamCode;
		mBGBtnConfirm.GetComponent<UISprite> ().color = SELECTED;
	}

	public void GoNext()
	{
		if (mSelected && IsGuest) {
			GetUID();
		} else if(mSelected){
			mMemInfo.FavoBB = mTeamCode;
			#if(UNITY_EDITOR)
			mMemInfo.OsType = 1;
			CompleteGCM();
			#elif(UNITY_ANDROID)
			mMemInfo.OsType = 1;
			AndroidMgr.RegistGCM(new EventDelegate(this, "CompleteGCM"));
			#else
			mMemInfo.OsType = 2;
			if(GetComponentInParent<ScriptTitle>().CheckPushAgree()){
				CompleteGCM();
			} else{
				IOSMgr.RegistAPNS(new EventDelegate(this, "SetGCMId"));
			}
//			IOSMgr.RegistAPNS(new EventDelegate(this, "CompleteGCM"));
			#endif
		} else if(!mSelected) {
			DialogueMgr.ShowDialogue (
				mErrorTitle, mErrorBody, DialogueMgr.DIALOGUE_TYPE.Alert,
			                          null, null, null, null);
		}
	}

	void GetUID(){
		if(Application.platform == RuntimePlatform.IPhonePlayer)
			IOSMgr.GetUID("", new EventDelegate(this, "GotUID"));
		else{
			GotUID();
		}
	}
	
	public void GotUID(){
		LoginInfo loginInfo = new LoginInfo();
		loginInfo.teamCode = mTeamCode;
		if(Application.platform == RuntimePlatform.IPhonePlayer)
			loginInfo.DeviceID = IOSMgr.GetMsg();
		else
			loginInfo.DeviceID = SystemInfo.deviceUniqueIdentifier;

		NetMgr.LoginGuest(loginInfo, mLoginEvent, UtilMgr.IsTestServer(), true);
	}

	public void CompleteGCM()
	{
		Debug.Log ("CompleteGCM");
		#if(UNITY_EDITOR)
		mMemInfo.MemUID = "";
		mMemInfo.DeviceID = SystemInfo.deviceUniqueIdentifier;
		DoJoin();
		#elif(UNITY_ANDROID)
		mMemInfo.MemUID = AndroidMgr.GetMsg();
		mMemInfo.DeviceID = SystemInfo.deviceUniqueIdentifier;
		DoJoin();
		#else
		mMemInfo.MemUID = IOSMgr.GetMsg();
		EventDelegate eventd = new EventDelegate(this, "DoJoin");
		IOSMgr.GetUID("", eventd);
		#endif



	}

	public void DoJoin(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			mMemInfo.DeviceID = IOSMgr.GetMsg();
		}
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

		GetComponentInParent<ScriptTitle>().Login(mMemInfo.MemberEmail, mMemInfo.MemberPwd);
	}

	public void BackClicked()
	{
		UtilMgr.OnBackPressed ();
	}



	string TeamCode="";
	string TeamFullName="";
	public void ChangeTeam(string teamcode,string teamfullname){
		TeamCode = teamcode;
		TeamFullName = teamfullname;
	}
	public ProfileManager PM;
	public void GoChangeTeam(){
		if (TeamCode != "") {
		
			PM.SetMemberTeam(TeamCode,TeamFullName);
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
