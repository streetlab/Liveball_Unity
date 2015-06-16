using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptTitle : MonoBehaviour {

	LoginEvent mLoginEvent;
	public GetProfileEvent mProfileEvent;
	GetCardInvenEvent mCardEvent;
	public LoginInfo mLoginInfo;
	CheckVersionEvent mVersionEvent;

	bool mFBInitialized;
	bool mMustUpdate;

//	public bool mServerIsTest = false;

	public string mJoinError;
	public string mStrVersionTitle;
	public string mStrMustUpdate;
	public string mStrRecommendUpdate;
	public string mStrBtnUpdate;
	public string mStrBtnExit;
	public string mStrBtnContinue;

	void Start()
	{
		#if(UNITY_EDITOR)
		CheckPrefsForEditor();
		#else
		Init ();
		#endif
//		Debug.Log("uid : "+SystemInfo.deviceUniqueIdentifier);
	}

	void CheckPrefsForEditor(){
		#if(UNITY_EDITOR)
		if(UnityEditor.EditorUtility.DisplayDialog("Delete player preferences",
		                               "Are you sure delete player preferences?",
		                               "Yes",
		                               "No")){
			//			PlayerPrefs.SetString (Constants.PrefEmail, "");
			//			PlayerPrefs.SetString (Constants.PrefPwd, "");
			PlayerPrefs.DeleteAll();
			Init ();
		} else
			Init ();
		#endif
	}

	void InitConstants(){

		#if(UNITY_ANDROID)
//		AndroidMgr.GetHeightStatusBar();
		#elif(UNITY_EDITOR)
		#else
		#endif
		Debug.Log("ScreenSize.x : "+Screen.width+", y : "+Screen.height);

		Constants.SCREEN_HEIGHT_ORIGINAL = Screen.height;
//		Debug.Log("height : "+Screen.height+", width : "+Screen.width);
//		Debug.Log("GetScaledPositionY : "+UtilMgr.GetScaledPositionY());
		Debug.Log("SystemInfo.deviceModel is "+SystemInfo.deviceModel);
		if(SystemInfo.deviceModel.Contains("iPhone7,1")){
			Constants.WEBVIEW_GAB_TOP = 34f;
		} else
		if(SystemInfo.deviceModel.Contains("iPhone")){
			Constants.WEBVIEW_GAB_TOP = 48f;
		}

		try{
//		Constants.UPLOAD_SERVER_HOST = mVersionEvent.Response.data.FILE_SVR;//[0].serviceURL;
			Constants.AUTH_SERVER_HOST = mVersionEvent.Response.data.AUTH_SVR;
			Constants.IMAGE_SERVER_HOST = mVersionEvent.Response.data.FILE_PATH;
			Constants.QUERY_SERVER_HOST = mVersionEvent.Response.data.APPS_SVR;//[0].serviceURL;
			Constants.GAME_SERVER_HOST = mVersionEvent.Response.data.GAME_SVR;
			Constants.GAME_SERVER_PORT = int.Parse(mVersionEvent.Response.data.GAME_PORT);
		} catch{
			Debug.Log("catch!!!!");
		}
	}

	public void Init()
	{
		transform.FindChild ("ContainerBtns").localPosition = new Vector3(0, UtilMgr.GetScaledPositionY()*2, 0);
		
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("WindowEmail").gameObject.SetActive (false);
		transform.FindChild ("FormJoin").gameObject.SetActive (false);
		transform.FindChild ("SelectTeam").gameObject.SetActive (false);
		transform.FindChild ("Certification").gameObject.SetActive (false);

		transform.FindChild ("SprLogo").gameObject.SetActive (true);
		
//		CheckPreference ();

		mVersionEvent = new CheckVersionEvent(new EventDelegate(this, "ReceivedVersion"));
		NetMgr.CheckVersion(mVersionEvent, UtilMgr.IsTestServer());
	}

	public void ReceivedVersion(){
		Debug.Log("Application.version is "+Application.version);
		Debug.Log("Recent.version is "+mVersionEvent.Response.data.recentVer);
		Debug.Log("Support.version is "+mVersionEvent.Response.data.supportVer);

		int aFirst = int.Parse(Application.version.Substring(0, 1));
		int aSecond = int.Parse(Application.version.Substring(2, 1));
		int aThird = int.Parse(Application.version.Substring(4, 1));

		int rFirst = int.Parse(mVersionEvent.Response.data.recentVer.Substring(0, 1));
		int rSecond = int.Parse(mVersionEvent.Response.data.recentVer.Substring(2, 1));
		int rThird = int.Parse(mVersionEvent.Response.data.recentVer.Substring(4, 1));

		int sFirst = int.Parse(mVersionEvent.Response.data.supportVer.Substring(0, 1));
		int sSecond = int.Parse(mVersionEvent.Response.data.supportVer.Substring(2, 1));
		int sThird = int.Parse(mVersionEvent.Response.data.supportVer.Substring(4, 1));

		if(aFirst < sFirst){
			MustUpdate(); return;
		} else if(aFirst == sFirst){
			if(aSecond < sSecond){
				MustUpdate(); return;
			} else if(aSecond == sSecond){
				if(aThird < sThird){
					MustUpdate(); return;
				}
			}
		}

		if(aFirst < rFirst){
			ReqUpdate(); return;
		} else if(aFirst == rFirst){
			if(aSecond < rSecond){
				ReqUpdate(); return;
			} else if(aSecond == rSecond){
				if(aThird < rThird){
					ReqUpdate(); return;
				}
			}
		}
		CheckPreference();
	}

	void MustUpdate(){
		mMustUpdate = true;
		DialogueMgr.ShowDialogue(mStrVersionTitle, mStrMustUpdate, DialogueMgr.DIALOGUE_TYPE.YesNo,
		                         mStrBtnUpdate, "", mStrBtnExit,
		                         DialogueClickHandler);
	}

	void ReqUpdate(){
		mMustUpdate = false;
		DialogueMgr.ShowDialogue(mStrVersionTitle, mStrRecommendUpdate, DialogueMgr.DIALOGUE_TYPE.YesNo,
		                         mStrBtnUpdate, "", mStrBtnContinue,
		                         DialogueClickHandler);
	}

	public void DialogueClickHandler(DialogueMgr.BTNS btn){
		if(mMustUpdate){
			if(btn == DialogueMgr.BTNS.Btn1){
				Debug.Log("Go to Store");
				#if(UNITY_ANDROID)
				Application.OpenURL(Constants.STORE_GOOGLE);
				#else
				Application.OpenURL(Constants.STORE_IPHONE);
				#endif
			} else{
				UtilMgr.Quit();
			}
		} else{
			if(btn == DialogueMgr.BTNS.Btn1){
				Debug.Log("Go to Store");
				#if(UNITY_ANDROID)
				Application.OpenURL(Constants.STORE_GOOGLE);
				#else
				Application.OpenURL(Constants.STORE_IPHONE);
				#endif
			} else{
				CheckPreference();
			}
		}
	}

	void CheckPreference()
	{
		
		InitConstants();

		string email = PlayerPrefs.GetString (Constants.PrefEmail);
		string pwd = PlayerPrefs.GetString (Constants.PrefPwd);

		if (email == null || email.Length < 1 || pwd == null || pwd.Length < 1) {
			StopLogin();
		}
		else{
			Login(email, pwd);
		}
	}

	void StopLogin()
	{
		transform.FindChild ("ContainerBtns").gameObject.SetActive (true);
	}

	public void Login(string eMail, string pwd)
	{
		mLoginInfo = new LoginInfo ();
		mLoginInfo.memberEmail = eMail;
		mLoginInfo.memberName = "";
		mLoginInfo.memberPwd = pwd;
		mLoginEvent = new LoginEvent(new EventDelegate(this, "LoginComplete"));
//		UtilMgr.ShowLoading (true);

		PlayerPrefs.SetString (Constants.PrefEmail, eMail);
		PlayerPrefs.SetString (Constants.PrefPwd, pwd);

		if (Application.platform == RuntimePlatform.Android) {
			mLoginInfo.osType = 1;
			AndroidMgr.RegistGCM(new EventDelegate(this, "SetGCMId"));
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			mLoginInfo.osType = 2;
			if(CheckPushAgree()){
				mLoginInfo.memUID = "";
//				NetMgr.DoLogin (mLoginInfo, mLoginEvent);
				SetGCMId();
			} else{
				IOSMgr.RegistAPNS(new EventDelegate(this, "SetGCMId"));
			}
		} else if(Application.platform == RuntimePlatform.OSXEditor){
			mLoginInfo.osType = 1;
			mLoginInfo.memUID = "";
//			NetMgr.DoLogin (mLoginInfo, mLoginEvent);
			SetGCMId();
		}
	}

	public void DialogueExitHandler(DialogueMgr.BTNS btn){
		Application.Quit();
	}

	public bool CheckPushAgree(){
#if(UNITY_ANDROID)
#else
		UnityEngine.iOS.NotificationType type = UnityEngine.iOS.NotificationServices.enabledNotificationTypes;
		if(type == UnityEngine.iOS.NotificationType.None || type == 0){
			return true;
		}
#endif
		return false;

	}

	public void FBReceived(){
		string jsonStr = AndroidMgr.GetMsg ();
		Dictionary<string, string> dic = new Dictionary<string, string>();
//		dic = Newtonsoft.Json.JsonConvert.Deserialize<Dictionary<string, string>> (jsonStr);

		foreach (string key in dic.Keys) {
			Debug.Log(key+" : "+dic[key]);
		}
	}

	public void OpenFacebook()
	{
		#if(UNITY_EDITOR)

		#elif(UNITY_ANDROID)
		AndroidMgr.OpenFB(new EventDelegate(this, "FBReceived"));
		#else

		#endif
//		if (mFBInitialized) {
//			FBLogin();
//		} else{
//			FB.Init (SetInit, FB.OnHideUnity);
//		}
			
	}

	void SetInit()
	{
		Debug.Log("SetInit");
		enabled = true;
		mFBInitialized = true;
	}

//	void FBLogin()
//	{
//		if(FB.IsLoggedIn)
//		{
//			Debug.Log("FB.IsLoggedIn");
//			OnLoggedIn();
//		}
//		else
//		{
//			Debug.Log("FB.Login");
//			FB.Login("email,publish_actions", LoginCallback);
//		}
//	}

//	void LoginCallback(FBResult result)
//	{
//		if (result.Error != null)
//			Debug.Log ("Error Response:\n" + result.Error);
//		else if (!FB.IsLoggedIn)
//		{
//			Debug.Log ("Login cancelled by Player");
//		}
//		else
//		{
//			Debug.Log ("Login was successful!");
//		}
//	}

//	void OnLoggedIn()
//	{
//		DialogueMgr.ShowDialogue ("", FB.UserId + "", DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null);
//		FB.API("/me?fields=id,first_name,friends."
//		FB.API ("/me/picture?type=large", Facebook.HttpMethod.GET, APICallback);
//		StartCoroutine (GetPicture());
//	}

//	IEnumerator GetPicture()
//	{
//		WWW www = new WWW("http://graph.facebook.com/" + FB.UserId + "/picture");
//		yield return www;
//		Texture2D texture = new Texture2D (0, 0);
//		www.LoadImageIntoTexture(texture);
//		transform.FindChild ("Texture").GetComponent<UITexture> ().mainTexture = texture; 
//	}

//	void APICallback(FBResult result)
//	{
//		if (result.Error != null)
//						Debug.Log ("AIP Error Response:\n" + result.Error);
//				else
//						transform.FindChild ("Texture").GetComponent<UITexture> ().mainTexture = result.Texture;
//			
//	}

	public void OpenKakao()
	{

	}

	public void OpenEmail()
	{
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("SprLogo").gameObject.SetActive (false);	
		transform.FindChild ("FormJoin").gameObject.SetActive (false);
		transform.FindChild ("SelectTeam").gameObject.SetActive (false);
		transform.FindChild ("Certification").gameObject.SetActive (false);		
		transform.FindChild ("SprLogo").gameObject.SetActive (false);

		transform.FindChild ("WindowEmail").gameObject.SetActive (true);

	}

	public void SetGCMId()
	{
		#if(UNITY_EDITOR)
		mLoginInfo.memUID = AndroidMgr.GetMsg();
		mLoginInfo.DeviceID = SystemInfo.deviceUniqueIdentifier;
		DoLogin();
		#elif(UNITY_ANDROID)
		mLoginInfo.memUID = AndroidMgr.GetMsg();
		mLoginInfo.DeviceID = SystemInfo.deviceUniqueIdentifier;
		DoLogin();
		#else
		mLoginInfo.memUID = IOSMgr.GetMsg();
		EventDelegate eventd = new EventDelegate(this, "DoLogin");
		IOSMgr.GetUID("", eventd);
		#endif
	}

	public void DoLogin(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			mLoginInfo.DeviceID = IOSMgr.GetMsg();
		}
		Debug.Log("ID is "+mLoginInfo.DeviceID);
		NetMgr.DoLogin (mLoginInfo, mLoginEvent);
	}

	void LoginComplete()
	{
		UtilMgr.DismissLoading ();
		if (mLoginEvent.Response.code > 0) {
			Debug.Log("error : "+mLoginEvent.Response.message);
//			if(mLoginEvent.Response.code == 100){
				LoginFailed();
//			}
//			UtilMgr.DismissLoading ();
			return;
		}
		mLoginInfo = mLoginEvent.Response.data;

		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "GotProfile"));

		NetMgr.GetProfile (mLoginInfo.memSeq, mProfileEvent);
	}

	void LoginFailed()
	{
		PlayerPrefs.SetString(Constants.PrefEmail, "");
		PlayerPrefs.SetString(Constants.PrefPwd, "");
		UtilMgr.RemoveAllBackEvents();
		Init ();
		string title = gameObject.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("loginFailedTitle").Value;
		string body = gameObject.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("loginFailedBody").Value;
		DialogueMgr.ShowDialogue(
		                         title, body, DialogueMgr.DIALOGUE_TYPE.Alert, "", "", "", null);
		UtilMgr.AddBackEvent (new EventDelegate (transform.root.GetComponent<ScriptLoginRoot>(), "DismissDialogue"));
	}
	
		public void GotProfile()
	{
		UserMgr.UserInfo = mProfileEvent.Response.data;

		string images = Constants.IMAGE_SERVER_HOST+ UserMgr.UserInfo.imagePath +  UserMgr.UserInfo.imageName;

		if (UserMgr.UserInfo.imageName != "") {
			WWW www = new WWW (images);
			StartCoroutine (GetImage (www));
		}

		if(mProfileEvent.Response.message != null
		   && mProfileEvent.Response.message.Length > 0){
			//			UtilMgr.OnBackPressed();
			
			DialogueMgr.ShowDialogue(mJoinError, mProfileEvent.Response.message,
			                         DialogueMgr.DIALOGUE_TYPE.Alert, "", "", "", null);
			
			return;
		}
		
		Debug.Log("UserMgr.UserInfo.activeAuth is "+UserMgr.UserInfo.activeAuth);
		//Check Auth
		//		if(UserMgr.UserInfo.activeAuth < 1){
		//			OpenCert();
		//			return;
		//		}
		
		Debug.Log("UserMgr.UserInfo.GetTeamCode() : "+UserMgr.UserInfo.GetTeamCode());
		
		//		if (mLoginInfo != null) {
		//			UserMgr.UserInfo.teamCode = mLoginInfo.teamCode;
		//			UserMgr.UserInfo.teamSeq = mLoginInfo.teamSeq;
		//
		//			Debug.Log("2 UserMgr.UserInfo.GetTeamCode() : "+UserMgr.UserInfo.GetTeamCode());
		//		}
		
		Debug.Log ("GotProfile");
		mCardEvent = new GetCardInvenEvent (new EventDelegate (this, "GotCardInven"));
		NetMgr.GetCardInven (mCardEvent);
		

	}

	
	IEnumerator GetImage(WWW www)
	{
		
		yield return www;
		
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		
		
		UserMgr.UserInfo.Textures = temp;
		
		

		
		
	}

	public void GotCardInven()
	{
		Debug.Log ("GotCardInven");
		UserMgr.CardInvenInfo = mCardEvent.Response.data;

		UtilMgr.RemoveAllBackEvents ();
//		AutoFade.LoadLevel ("SceneTeamHome", 0f, 1f);
		string value = PlayerPrefs.GetString (Constants.PrefTutorial);
		if(value != null && value.Equals("1")){
			value = PlayerPrefs.GetString(Constants.PrefNotice);
			if(value != null && value.Equals(UtilMgr.GetDateTime("yyyyMMdd"))){
				AutoFade.LoadLevel ("SceneMain");
			} else{
				AutoFade.LoadLevel ("SceneNotice");
			}
		}
		else{
			AutoFade.LoadLevel("SceneTutorial");
		}
	}

	public void OpenCert(){
		transform.FindChild ("ContainerBtns").gameObject.SetActive (false);
		transform.FindChild ("WindowEmail").gameObject.SetActive (false);
		transform.FindChild ("FormJoin").gameObject.SetActive (false);
		transform.FindChild ("SelectTeam").gameObject.SetActive (false);
		transform.FindChild ("SprLogo").gameObject.SetActive (false);

		transform.FindChild ("Certification").gameObject.SetActive (true);
	}

	public void EmailClicked(){
//		Debug.Log("EmailClicked");
		OpenEmail();
	}

	public void KakaoClicked(){
		OpenKakao();
	}

	public void FacebookClicked(){
		OpenFacebook();
	}

//	public void BtnClicked(string name)
//	{
//		Debug.Log("BtnClicked : "+name);
//		UtilMgr.AddBackEvent (new EventDelegate (this, "Init"));
//		Debug.Log("added");
//		switch(name)
//		{
//		case "BtnFacebook":
//			OpenFacebook();
//			break;
//		case "BtnKakao":
//			OpenKakao();
//			break;
//		case "BtnEmail":
//			OpenEmail();
//			break;
//
//		
//		}
//	}
}
