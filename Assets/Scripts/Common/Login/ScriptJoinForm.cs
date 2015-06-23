using UnityEngine;
using System.Collections;

public class ScriptJoinForm : MonoBehaviour {

	public GameObject mSelectTeam;
//	GetProfileEvent mEvent;
	JoinMemberInfo mMemInfo;
	string mImgPath;
	byte[] ImageBate;
	public string mJoinError;

	public void Init(string eMail, string pwd, bool pwdEnable)
	{
		transform.FindChild ("InputEmail").GetComponent<UIInput> ().value = eMail;
		transform.FindChild ("InputPwd").GetComponent<UIInput> ().value = pwd;
		transform.FindChild ("InputPwd").GetComponent<UIInput> ().enabled = pwdEnable;
	}

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

	public void NextClicked()
	{
		string value = CheckValidation ();
		if (value == null) {
			mMemInfo = new JoinMemberInfo();
			mMemInfo.MemberEmail = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
			mMemInfo.MemberName = transform.FindChild ("InputNick").GetComponent<UIInput> ().value;
			mMemInfo.MemberPwd = transform.FindChild ("InputPwd").GetComponent<UIInput> ().value;
			mMemInfo.MemImage = "";//preprocess
			mMemInfo.Photo = mImgPath;
			mMemInfo.PhotoBytes = ImageBate;

			gameObject.SetActive(false);
			mSelectTeam.GetComponent<ScriptSelectTeam>().Init(mMemInfo);

			PlayerPrefs.SetString(Constants.PrefEmail, mMemInfo.MemberEmail);
			PlayerPrefs.SetString(Constants.PrefPwd, mMemInfo.MemberPwd);
		} else
		{
			DialogueMgr.ShowDialogue(mJoinError, value, DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null, null);
		}
	}

//	public void JoinComplete()
//	{
//		Debug.Log (mEvent.Response.data.memberEmail);
//	}

	string CheckValidation()
	{
		string value = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
		if (value.Length > 4
				&& value.Contains ("@")
				&& value.Contains (".")) {

		} else
		{
			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrEmailError").Value;
		}

		value = transform.FindChild("InputPwd").GetComponent<UIInput> ().value;
		if(value.Length < 4)
			return transform.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("StrPwdError").Value;

		value = transform.FindChild("InputNick").GetComponent<UIInput> ().value;
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
}
