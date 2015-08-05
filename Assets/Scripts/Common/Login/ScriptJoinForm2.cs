using UnityEngine;
using System.Collections;

public class ScriptJoinForm2 : MonoBehaviour {

	public GameObject mSelectTeam;
//	GetProfileEvent mEvent;
	JoinMemberInfo mMemInfo;
	string mImgPath;
	byte[] ImageBate;
	public string mJoinError;

	CheckNickEvent mNickEvent;
	string mConfirmedNick;

//	public void Init(string eMail, string pwd, bool pwdEnable)
//	{
//		transform.FindChild ("InputEmail").GetComponent<UIInput> ().value = eMail;
//		transform.FindChild ("InputPwd").GetComponent<UIInput> ().value = pwd;
//		transform.FindChild ("InputPwd").GetComponent<UIInput> ().enabled = pwdEnable;
//	}

	public void CameraClicked()
	{
		if(Application.platform == RuntimePlatform.Android){
			//need selection window
			AndroidMgr.OpenGallery(new EventDelegate(this, "GotUserImg"));
		} else{
			IOSMgr.OpenGallery(new EventDelegate(this, "GotUserImg"));
		}
	}

	public void GotUserImg()
	{
		string images;
		Debug.Log("OpenGallery");
		if (Application.platform == RuntimePlatform.Android) {
		images = "file://" + AndroidMgr.GetMsg ();
		} else {
			images = "file://" + IOSMgr.GetMsg ();
		}
		Debug.Log("images");
		WWW tLoad= new WWW(images);

		StartCoroutine(LoadImage2(tLoad));
		
	}

	IEnumerator LoadImage2(WWW tLoad)
	{
		yield return tLoad;
		Debug.Log("tLoad");
		//		tDynamicTx= new Texture2D((int)UsetPhotoSize.x, (int)UsetPhotoSize.y);
		Texture2D tDynamicTx= new Texture2D(0, 0);
		Debug.Log("tDynamicTx");
		tLoad.LoadImageIntoTexture(tDynamicTx);
		tLoad.Dispose ();
		
		tDynamicTx = UtilMgr.ScaleTexture (tDynamicTx, 206, 230);
		//transform.FindChild("Photo").GetComponent<UITexture> ().mainTexture = tDynamicTx;
		//Save (tDynamicTx);
		transform.FindChild("PanelPhoto").FindChild("TexPhoto").GetComponent<UITexture>().mainTexture = tDynamicTx;

		ImageBate = tDynamicTx.EncodeToPNG();
		}

	public void BackClicked()
	{
		UtilMgr.OnBackPressed ();
	}

	bool CheckNickConfirmed(){
		string value = transform.FindChild("InputNick").GetComponent<UIInput> ().value;
		if(value.Equals(mConfirmedNick)){
			return true;
		} else{
			return false;
		}
	}

	public void NextClicked()
	{
		if (CheckNickConfirmed ()) {
			mMemInfo = new JoinMemberInfo();
			mMemInfo.MemberEmail = "";//transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
			mMemInfo.MemberName = transform.FindChild ("InputNick").GetComponent<UIInput> ().value;
			mMemInfo.MemberPwd = "";//transform.FindChild ("InputPwd").GetComponent<UIInput> ().value;
			mMemInfo.MemImage = "";//preprocess
			mMemInfo.Photo = mImgPath;
			mMemInfo.PhotoBytes = ImageBate;

//			gameObject.SetActive(false);
//			mSelectTeam.GetComponent<ScriptSelectTeam>().Init(mMemInfo);
//			mSelectTeam.GetComponent<ScriptSelectTeam>().SetTeam("");
//			mSelectTeam.GetComponent<ScriptSelectTeam>().GoNext();
//
//			PlayerPrefs.SetString(Constants.PrefEmail, mMemInfo.MemberEmail);
//			PlayerPrefs.SetString(Constants.PrefPwd, mMemInfo.MemberPwd);

//			GetComponentInParent<ScriptTitle>().mProfileEvent = 
//				new GetProfileEvent(new EventDelegate(this, "CompletedJoin"));
//			NetMgr.JoinMember(mMemInfo, GetComponentInParent<ScriptTitle>().mProfileEvent, UtilMgr.IsTestServer(), true);
			CompletedJoin();
		} else
		{
			DialogueMgr.ShowDialogue(mJoinError, "닉네임 중복 체크가 필요합니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null, null);
		}
	}

	public void CompletedJoin(){
//		if(GetComponentInParent<ScriptTitle>().mProfileEvent.Response.message != null
//		   && GetComponentInParent<ScriptTitle>().mProfileEvent.Response.message.Length > 0){
//			DialogueMgr.ShowDialogue(GetComponentInParent<ScriptTitle>().mJoinError,
//			                         GetComponentInParent<ScriptTitle>().mProfileEvent.Response.message,
//			                         DialogueMgr.DIALOGUE_TYPE.Alert, "", "", "", null);
//			return;
//		}
//		PlayerPrefs.SetString(Constants.PrefEmail, "1");
//		PlayerPrefs.SetString(Constants.PrefPwd, "1");

		gameObject.SetActive(false);
		mSelectTeam.GetComponent<ScriptSelectTeam>().Init(mMemInfo);
		mSelectTeam.GetComponent<ScriptSelectTeam>().SetTeam("");
		mSelectTeam.GetComponent<ScriptSelectTeam>().GoNext();
	}

//	public void JoinComplete()
//	{
//		Debug.Log (mEvent.Response.data.memberEmail);
//	}

	string CheckValidation()
	{
//		string value = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
//		if (value.Length > 4
//				&& value.Contains ("@")
//				&& value.Contains (".")) {
//
//		} else
//		{
//			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrEmailError").Value;
//		}
//
//		value = transform.FindChild("InputPwd").GetComponent<UIInput> ().value;
//		if(value.Length < 4)
//			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrPwdError").Value;

		string value = transform.FindChild("InputNick").GetComponent<UIInput> ().value;
		if(value.Length < 2)
			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrNickError").Value;

		return null;

	}

	public void OpenCamera()
	{
		#if(UNITY_EDITOR)
		#elif(UNITY_ANDROID)
//		AndroidMgr.OpenGallery(new EventDelegate(this, "GotImage"));
		#else
//		IOSMgr.OpenGallery(new EventDelegate(this, "GotImage"));
		#endif
	}

	public void GotImage()
	{
		#if(UNITY_EDITOR)
		#elif(UNITY_ANDROID)
		mImgPath = AndroidMgr.GetMsg();
		#else
		mImgPath = IOSMgr.GetMsg();
		#endif

//		string filePath = mImgPath;
		if(System.IO.File.Exists(mImgPath)){
			WWW www = new WWW ("file://"+mImgPath);
			StartCoroutine (LoadImage (www));
		} else{
			DialogueMgr.ShowDialogue("Error", "파일을 찾을 수 없습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}


	}

	IEnumerator LoadImage(WWW www)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D(0,0);
		www.LoadImageIntoTexture (tmpTex);
		transform.FindChild ("PanelPhoto").FindChild ("TexPhoto").GetComponent<UITexture> ().mainTexture = tmpTex;
	}

	public void ConfirmedEmail(){
		transform.FindChild ("InputPwd").GetComponent<UIInput>().isSelected = true;
	}
					
	public void ConfirmedPwd(){
		transform.FindChild ("InputNick").GetComponent<UIInput>().isSelected = true;
	}

	public void ConfirmedNick(){
		transform.FindChild ("InputInvite").GetComponent<UIInput>().isSelected = true;
	}

	public void ConfirmedInvitation(){
		NextClicked();
	}

	public void CheckDuplication(){
		string value = CheckValidation ();
		if (value == null) {
			mNickEvent = new CheckNickEvent(new EventDelegate(this, "CheckComplete"));
			string name = transform.FindChild("InputNick").GetComponent<UIInput> ().value;
			NetMgr.CheckNickname(name, mNickEvent);
		} else
		{
			DialogueMgr.ShowDialogue(mJoinError, value, DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null, null);
		}
	}

	void CheckComplete(){
		if(mNickEvent.Response.code == 0){//duplicated
			DialogueMgr.ShowDialogue(mJoinError, "이미 등록된 닉네임 입니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null, null);
		} else{
			DialogueMgr.ShowDialogue(mJoinError, "사용할 수 있는 닉네임 입니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null, null);
			mConfirmedNick = transform.FindChild("InputNick").GetComponent<UIInput> ().value;
		}
	}
}
