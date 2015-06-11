﻿using UnityEngine;
using System.Collections;
using System.IO;
public class ProfileManager : MonoBehaviour {
	public string UserImagePath;
	public string UserImageName;
	public string UserName;
	public string UserEmail;
	public string UserState;
	public string UserTeamCode;
	public string UserTeamFullName;
	public Texture2D UserImage;
	public byte[] UserImagebyte;
	public GameObject Profile,SettingPage;
	public Vector2 UsetPhotoSize;

	public string SetName;

	public string SetTeamCode;
	
	public byte[] Setimagebyte;
	public Texture2D Setimage;
	
 
	GetProfileEvent mProfileEvent;
	public bool Sett = false;

	public UITexture LeftProfileImg;
	void Start(){
		UsetPhotoSize = new Vector2 (206, 230);
		Set ();
	}

	public void Set(){
		Sett = false;
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "Setting"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);
	}
	void Setting(){
		UserImagePath = mProfileEvent.Response.data.imagePath;
		UserImageName = mProfileEvent.Response.data.imageName;
		UserName = mProfileEvent.Response.data.memberName;
		//Debug.Log ("UserName : " + mProfileEvent.Response.data.memberName);
		UserEmail = mProfileEvent.Response.data.memberEmail;
		UserState = mProfileEvent.Response.data.memberEmail;
		UserTeamCode = mProfileEvent.Response.data.GetTeamCode();
		//Debug.Log ("GetTeamCode : " + mProfileEvent.Response.data.GetTeamCode());
		UserTeamFullName = mProfileEvent.Response.data.GetTeamFullName ();
		//Debug.Log ("UserTeamFullName : " + mProfileEvent.Response.data.GetTeamFullName ());
		string imgName = UtilMgr.GetTeamEmblem (UserTeamCode);
		UserTeamCode = imgName;

		//Debug.Log ("UserMgr.UserInfo.teamCode be : " + UserMgr.UserInfo.GetTeamCode());
		UserMgr.UserInfo.memberName = UserName;
		UserMgr.UserInfo.memberEmail = UserEmail;
		UserMgr.UserInfo.favoBB.teamCode = mProfileEvent.Response.data.GetTeamCode();
		UserMgr.UserInfo.favoBB.teamFullName = mProfileEvent.Response.data.GetTeamFullName();
	//	Debug.Log ("mProfileEvent.Response.data.GetTeamCode(): " + mProfileEvent.Response.data.GetTeamCode());
	//	Debug.Log ("UserMgr.UserInfo.teamCode af: " + UserMgr.UserInfo.GetTeamCode());
		SetName = UserName;
		SetTeamCode = mProfileEvent.Response.data.GetTeamCode ();

		string images = Constants.IMAGE_SERVER_HOST+ UserImagePath + UserImageName;
		//Profile.transform.FindChild ("LblNick").GetComponent<UILabel> ().text = UserName;
		//Profile.transform.FindChild ("LblStatus").GetComponent<UILabel> ().text = UserState;
		//Profile.transform.FindChild ("SprEmblem").GetComponent<UISprite> ().spriteName = UserTeamCode;
		if (UserImageName != "") {
			WWW www = new WWW (images);
			StartCoroutine (GetImage (www));
		}

		//if (UserImageName != "") {
			Profile.transform.FindChild ("LblNick").GetComponent<UILabel> ().text = UserName;
			Profile.transform.FindChild ("LblStatus").GetComponent<UILabel> ().text = UserState;
			Profile.transform.FindChild ("SprEmblem").GetComponent<UISprite> ().spriteName = UserTeamCode;
		//}
	}
	public void SetSame(){
		SetName = UserName;
		SetTeamCode = mProfileEvent.Response.data.GetTeamCode ();
		Setimagebyte = UserImagebyte;
	}
	public void SetMainPage(){
		bool B = Profile.activeSelf;
	if (!Profile.activeSelf) {
			Profile.SetActive (true);
		}
		Profile.transform.FindChild ("LblNick").GetComponent<UILabel> ().text = UserName;
		Profile.transform.FindChild ("LblStatus").GetComponent<UILabel> ().text = UserState;
		Profile.transform.FindChild ("SprEmblem").GetComponent<UISprite> ().spriteName = UserTeamCode;
		if (UserImage != null) {
			UserImage.LoadImage(UserImagebyte);
			Profile.transform.FindChild ("Panel").FindChild ("Photo").GetComponent<UITexture> ().mainTexture = UserImage;
		
		}
		Profile.SetActive (B);

	}
	public void SetSettingPage(){

		bool B = SettingPage.activeSelf;
		if (!SettingPage.activeSelf) {
			SettingPage.SetActive (true);
		}
		SettingPage.transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var1").FindChild ("name").FindChild ("name 1").GetComponent<UILabel> ().text = UserName;
		SettingPage.transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var2").FindChild ("team").FindChild ("img").GetComponent<UISprite> ().spriteName = UserTeamCode;
		SettingPage.transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var2").FindChild ("team").FindChild ("team name").GetComponent<UILabel> ().text = UserTeamFullName;
		if (UserImage != null) {

		
			SettingPage.transform.FindChild ("Panel").FindChild ("Photo").GetComponent<UITexture> ().mainTexture = UserImage;
		}
		SettingPage.SetActive (B);
	}

	public void SetMemberName(string name){
		Sett = true;
		bool B = SettingPage.activeSelf;
		if (!SettingPage.activeSelf) {
			SettingPage.SetActive (true);
		}
		SetName = name;
		SettingPage.transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var1").FindChild ("name").FindChild ("name 1").GetComponent<UILabel> ().text = SetName;

		SettingPage.SetActive (B);
	}

	public void SetMemberTeam(string Team,string FullName){
		Sett = true;
		//Debug.Log ("Team : "+Team);
		bool B = SettingPage.activeSelf;
		if (!SettingPage.activeSelf) {
			SettingPage.SetActive (true);
		}

		string imgName = UtilMgr.GetTeamEmblem (Team);
		SetTeamCode = Team;

	
		SettingPage.transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var2").FindChild ("team").FindChild ("img").GetComponent<UISprite> ().spriteName = imgName;

		//Set 
		SettingPage.transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var2").FindChild ("team").FindChild ("team name").GetComponent<UILabel> ().text = FullName;
		//Set

		SettingPage.SetActive (B);
		
	}
	public void SetMemberPhoto(byte[] Photo){
		Sett = true;
		bool B = SettingPage.activeSelf;
		if (!SettingPage.activeSelf) {
			SettingPage.SetActive (true);
		}
		Setimagebyte = Photo;
		Setimage.LoadImage (Setimagebyte);
	//	if (Setimage != null) {
			SettingPage.transform.FindChild ("Panel").FindChild ("Photo").GetComponent<UITexture> ().mainTexture = Setimage;


		//}
		SettingPage.SetActive (false);
		SettingPage.SetActive (true);
		SettingPage.SetActive (B);
		
		
	}
	public void SetMemberPhoto(Texture2D texture){
		Sett = true;
		bool B = SettingPage.activeSelf;
		if (!SettingPage.activeSelf) {
			SettingPage.SetActive (true);
		}
//		Setimagebyte = Photo;
		Setimagebyte = File.ReadAllBytes(IOSMgr.GetMsg());
		//		Setimage.LoadImage (Setimagebyte);
		//	if (Setimage != null) {
		SettingPage.transform.FindChild ("Panel").FindChild ("Photo").GetComponent<UITexture> ().mainTexture = texture;
		
		
		//}
		SettingPage.SetActive (false);
		SettingPage.SetActive (true);
		SettingPage.SetActive (B);
		
		
	}
	void CancelSet(){
		SetSettingPage ();
	}

	public void SetMemberInfo(){
		if (Sett) {
			DialogueMgr.ShowDialogue ("프로필 편집", "변경된 프로필을 저장하시겠습니까?", DialogueMgr.DIALOGUE_TYPE.YesNo, DialogueHandler);
		} else {
			DialogueMgr.ShowDialogue ("프로필 편집", "변경된 내용이 없습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}
	
		
		
	}
	public void SetMemberInfoOut(){

		JoinMemberInfo memInfo = new JoinMemberInfo ();
		UpdateMemberInfoEvent event1 = null;
		//memInfo.Photo;
		//memInfo.MemberID
		memInfo.MemberName = SetName;
		memInfo.MemberEmail = UserMgr.UserInfo.memberEmail;
		memInfo.MemImage = UserMgr.UserInfo.memberEmail;
		Debug.Log("send Team code : " + SetTeamCode);
		memInfo.FavoBB = SetTeamCode;
		memInfo.PhotoBytes = Setimagebyte;
		event1 = new UpdateMemberInfoEvent (new EventDelegate (this, "Set"));
		NetMgr.UpdateMemberInfo (memInfo, event1, UtilMgr.IsTestServer (), false);
	}
	
	void DialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
		
			JoinMemberInfo memInfo = new JoinMemberInfo ();
			UpdateMemberInfoEvent event1 = null;
			//memInfo.Photo;
			//memInfo.MemberID
			memInfo.MemberName = SetName;
			memInfo.MemberEmail = UserMgr.UserInfo.memberEmail;
			memInfo.MemImage = UserMgr.UserInfo.memberEmail;
			Debug.Log("send Team code : " + SetTeamCode);
			memInfo.FavoBB = SetTeamCode;
			memInfo.PhotoBytes = Setimagebyte;
			event1 = new UpdateMemberInfoEvent (new EventDelegate (this, "Set"));
			NetMgr.UpdateMemberInfo (memInfo, event1, UtilMgr.IsTestServer (), false);
			SettingPage.GetComponent<ProfileSetting>().Save();
		}
	}
			
	//GetGallryImage

	 Texture2D tDynamicTx;
	 WWW tLoad;
	 string images;
	
	public void SetPhoto(){
		Debug.Log("OpenGallery!!!");
		#if(UNITY_ANDROID)
		AndroidMgr.OpenGallery(new EventDelegate(this, "OpenGallery"));
		#else
		IOSMgr.OpenGallery(new EventDelegate(this, "OpenGallery"));
		#endif
	}
	void OpenGallery(){
		Debug.Log("OpenGallery");
		#if(UNITY_ANDROID)
		images = "file://"+ AndroidMgr.GetMsg();
		#else
		images = "file://"+ IOSMgr.GetMsg();
		#endif
		Debug.Log("images");
		tLoad= new WWW(images);
		StartCoroutine(LoadImage(tLoad));
		
	}

	IEnumerator LoadImage(WWW www)
	{
		yield return www;
		Debug.Log("tLoad");
		//		tDynamicTx= new Texture2D((int)UsetPhotoSize.x, (int)UsetPhotoSize.y);
		tDynamicTx= new Texture2D(0, 0);
		Debug.Log("tDynamicTx");
		tLoad.LoadImageIntoTexture(tDynamicTx);
		tLoad.Dispose ();

		tDynamicTx = UtilMgr.ScaleTexture (tDynamicTx, (int)UsetPhotoSize.x, (int)UsetPhotoSize.y);
		//transform.FindChild("Photo").GetComponent<UITexture> ().mainTexture = tDynamicTx;
		//Save (tDynamicTx);
		Debug.Log("Image name : " + images);
		byte[] bytes = tDynamicTx.EncodeToPNG();
		SetMemberPhoto (bytes);
//		SetMemberPhoto(tDynamicTx);
	}
//	public void Save(Texture2D t) {
//		
//		
//		byte[] bytes = t.EncodeToPNG();
//		Debug.Log ("Start Save");
//		
//		
//		File.WriteAllBytes("path"  + ".png", bytes);
//		Debug.Log ("End Save");
//		//Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
//		
//		
//	}    

	//

	IEnumerator GetImage(WWW www)
	{
		
		yield return www;
		
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		Debug.Log ("temp : " + temp.EncodeToPNG ().Length);
	
			UserImagebyte = temp.EncodeToPNG ();
	
			UserImage = temp;
		UserMgr.UserInfo.Textures=temp;
		LeftProfileImg.mainTexture = temp;
		Setimagebyte = temp.EncodeToPNG ();
			Setimage = temp;

		SetMainPage();

	
			SetSettingPage();

	}


}
