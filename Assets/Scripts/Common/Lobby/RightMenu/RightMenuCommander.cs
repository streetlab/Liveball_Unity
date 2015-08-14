using UnityEngine;
using System.Collections;
using System.IO;

public class RightMenuCommander : MonoBehaviour {
	public GameObject RightSubMenu;
	public float TopGap;
	public Vector2 Size;
	public string[] MenuName;
	public string[] MenuValue;
	void Awake(){
		GetComponent<BoxCollider2D> ().enabled = false;
		Proflie ();

	}
	public void Proflie(){
		if (UserMgr.UserInfo != null) {
			if(UserMgr.UserInfo.Textures!=null){
		transform.FindChild("Top").FindChild("UserPhoto").FindChild("Photo").FindChild("Texture").GetComponent<UITexture>().
				mainTexture = UserMgr.UserInfo.Textures;
			}

			transform.FindChild("Top").FindChild("UserName").GetComponent<UILabel>().text = UserMgr.UserInfo.memberName;
			transform.FindChild("Top").FindChild("Ruby").FindChild("RubyValue").GetComponent<UILabel>().text
				= UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);
			transform.FindChild("Top").FindChild("Mileage").FindChild("MileageValue").GetComponent<UILabel>().text
				= UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
			transform.FindChild("Top").FindChild("PinCode").FindChild("PinCodeValue").GetComponent<UILabel>().text = UserMgr.UserInfo.memPIN;
		}
	}
	// Use this for initialization
	public void CreatRightSubMenu(){
		if (transform.FindChild ("Bot").childCount != MenuName.Length) {
			for(int i = 0; i<transform.FindChild("Bot").childCount;i++){
				DestroyImmediate(transform.FindChild("Bot").GetChild(0).gameObject);
			}

			for(int i = 0; i<MenuName.Length;i++){
				GameObject Temp = (GameObject)Instantiate(RightSubMenu);
				Temp.name = MenuName[i];
				Temp.transform.parent = transform.FindChild ("Bot");
				Temp.GetComponent<UISprite>().SetRect(Size.x,Size.y);
				Temp.GetComponent<BoxCollider2D>().size = Size;
				Temp.transform.localScale = new Vector3(1,1,1);
				Temp.transform.localPosition = new Vector3(0,-((TopGap+(Size.y*0.5f))+(i*Size.y)),0);
				Temp.transform.FindChild("Label").GetComponent<UILabel>().text = MenuValue[i];
				Temp.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = MenuName[i];
			}
		}
	}
	public void DeleteRightSubMenu(){
		int Count = transform.FindChild ("Bot").childCount;
		for(int i = 0; i<Count;i++){
			DestroyImmediate(transform.FindChild("Bot").GetChild(0).gameObject);
		}
	}





	public Texture2D UserImage;
	public byte[] UserImagebyte;

	public Vector2 UsetPhotoSize;
	
	public string SetName;
	
	public string SetTeamCode;
	
	public byte[] Setimagebyte;
	public Texture2D Setimage;

	
	GetProfileEvent mProfileEvent;
	public bool Sett = false;
	
	public UITexture LeftProfileImg;
	public UILabel LeftTeamName;
	public UILabel LeftUserName;

	
	public void SetMemberName(string name){
	
		SetName = name;
		transform.FindChild ("Top").FindChild ("UserName").GetComponent<UILabel> ().text = SetName;
		Set2 ();
		SetMemberInfoOut ();

	}
	

	public void SetMemberPhoto(byte[] Photo){
		Sett = true;
	
		if (Photo == null) {
			Debug.Log ("Photo Null");
		} else {
			Debug.Log ("Photo not Null");
		}
		Debug.Log ("Photo.l" + Photo.Length);
		Setimagebyte = Photo;
		Debug.Log ("LoadImage");
		//Setimage.LoadImage (Photo);
		//	if (Setimage != null) {
		Debug.Log ("mainTexture");
		Set ();
		Proflie ();
		SetMemberInfoOut ();

		
		
	}
	public void SetMemberPhoto(Texture2D texture){
		Sett = true;

		//		Setimagebyte = Photo;
		Setimagebyte = File.ReadAllBytes(IOSMgr.GetMsg());
		//		Setimage.LoadImage (Setimagebyte);
		//	if (Setimage != null) {

		
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
		
		Debug.Log("send Team code : " + SetTeamCode);
		memInfo.FavoBB = SetTeamCode;
		if(GalleryCheck){
			memInfo.MemImage = UserMgr.UserInfo.memberEmail;
			memInfo.PhotoBytes = Setimagebyte;
			GalleryCheck = false;
		}
		Debug.Log ("memInfo.MemberName : " + memInfo.MemberName);
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

			Debug.Log("send Team code : " + SetTeamCode);
			memInfo.FavoBB = SetTeamCode;
			if(GalleryCheck){
				memInfo.MemImage = UserMgr.UserInfo.memberEmail;
				if(Setimagebyte == null){
					Setimagebyte = UserMgr.UserInfo.Textures.EncodeToPNG();
				}
				memInfo.PhotoBytes = Setimagebyte;
				Setimagebyte = null;
				GalleryCheck = false;
			}
			event1 = new UpdateMemberInfoEvent (new EventDelegate (this, "Set"));
			NetMgr.UpdateMemberInfo (memInfo, event1, UtilMgr.IsTestServer (), false);

		}
	}
	
	//GetGallryImage
	
	Texture2D tDynamicTx;
	WWW tLoad;
	string images;
	bool GalleryCheck = false;
	bool CheckInGallery = false;
	public void SetNames(){
		transform.root.FindChild("Camera").FindChild ("PopUp").gameObject.SetActive (true);
		PopUp.Status = "Profile";
	}
	public void SetPhoto(){
		
		Debug.Log("OpenGallery!!!");
		#if(UNITY_ANDROID)
		AndroidMgr.OpenGallery(new EventDelegate(this, "OpenGallery"));
		#else
		IOSMgr.OpenGallery(new EventDelegate(this, "OpenGallery"));
		#endif
	}
	void OpenGallery(){
		CheckInGallery = true;
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
		tDynamicTx= new Texture2D(175, 175);
		Debug.Log("tDynamicTx");
		tLoad.LoadImageIntoTexture(tDynamicTx);
		tLoad.Dispose ();
		
		tDynamicTx = UtilMgr.ScaleTexture (tDynamicTx, (int)175, (int)175);
		//transform.FindChild("Photo").GetComponent<UITexture> ().mainTexture = tDynamicTx;
		//Save (tDynamicTx);
		Debug.Log("Image name : " + images);
		Setimage= tDynamicTx;
		byte[] bytes = tDynamicTx.EncodeToPNG();
		if (CheckInGallery) {
			GalleryCheck = true;
			CheckInGallery = false;
		}
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
		
		//SetMainPage();
		//transform.FindChild("Top").FindChild("")
		
		//	SetSettingPage();
	
		
	}
	string UserName;
	string UserEmail;
	void Set(){
//		UserMgr.UserInfo.memberName = UserName;
//		UserMgr.UserInfo.memberEmail = UserEmail;
		UserMgr.UserInfo.Textures = Setimage;

		PlayerPrefs.SetString(Constants.PrefNick, SetName);
	}
	void Set2(){
		UserMgr.UserInfo.memberName = SetName;
	}
	void Update(){
		transform.FindChild ("Top").FindChild ("Ruby").FindChild ("RubyValue").GetComponent<UILabel> ().text
			= UtilMgr.AddsThousandsSeparator (UserMgr.UserInfo.userRuby);
		transform.FindChild ("Top").FindChild ("Mileage").FindChild ("MileageValue").GetComponent<UILabel> ().text
			= UtilMgr.AddsThousandsSeparator (UserMgr.UserInfo.userDiamond);
	}

}
