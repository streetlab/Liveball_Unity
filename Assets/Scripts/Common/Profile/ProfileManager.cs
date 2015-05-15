using UnityEngine;
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
	void Start(){
		UsetPhotoSize = new Vector2 (100, 100);
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
		UserEmail = mProfileEvent.Response.data.memberEmail;
		UserState = mProfileEvent.Response.data.memberEmail;
		UserTeamCode = mProfileEvent.Response.data.GetTeamCode();
		UserTeamFullName = mProfileEvent.Response.data.GetTeamFullName ();

		string imgName = UtilMgr.GetTeamEmblem (UserTeamCode);
		UserTeamCode = imgName;

		UserMgr.UserInfo.memberName = UserName;
		UserMgr.UserInfo.memberEmail = UserEmail;
		UserMgr.UserInfo.teamCode = mProfileEvent.Response.data.GetTeamCode();

		SetName = UserName;
		SetTeamCode = mProfileEvent.Response.data.GetTeamCode ();

		string images = Constants.IMAGE_SERVER_HOST+ UserImagePath + UserImageName;
	
    	WWW www= new WWW(images);
		StartCoroutine (GetImage(www));
	
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
		if (Setimage != null) {
			SettingPage.transform.FindChild ("Panel").FindChild ("Photo").GetComponent<UITexture> ().mainTexture = Setimage;

		}
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
	

	void DialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			JoinMemberInfo memInfo = new JoinMemberInfo ();
			UpdateMemberInfoEvent event1 = null;
			//memInfo.Photo;
			//memInfo.MemberID
			memInfo.MemberName = SetName;
			memInfo.MemberEmail = UserMgr.UserInfo.memberEmail;
			memInfo.MemImage = UserMgr.UserInfo.memberEmail;
			memInfo.FavoBB = SetTeamCode;
			memInfo.PhotoBytes = Setimagebyte;
			event1 = new UpdateMemberInfoEvent (new EventDelegate (this, "Set"));
			NetMgr.UpdateMemberInfo (memInfo, event1, UtilMgr.IsTestServer (), false);
		}
	}
			
	//GetGallryImage

	 Texture2D tDynamicTx;
	 WWW tLoad;
	 string images;
	
	public void SetPhoto(){

		AndroidMgr.OpenGallery(new EventDelegate(this, "OpenGallery"));
	}
	void OpenGallery(){
		
		images = "file://"+ AndroidMgr.GetMsg();
		tLoad= new WWW(images);
		tDynamicTx= new Texture2D((int)UsetPhotoSize.x, (int)UsetPhotoSize.y);
		tLoad.LoadImageIntoTexture(tDynamicTx);
		
		tDynamicTx = ScaleTexture (tDynamicTx, (int)UsetPhotoSize.x, (int)UsetPhotoSize.y);
		//transform.FindChild("Photo").GetComponent<UITexture> ().mainTexture = tDynamicTx;
		//Save (tDynamicTx);
		Debug.Log("Image name : " + images);
		byte[] bytes = tDynamicTx.EncodeToPNG();
		SetMemberPhoto (bytes);
		
		Debug.Log ("tDynamicTx : " + tDynamicTx.width + " , " + tDynamicTx.height);
		
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
	private Texture2D ScaleTexture(Texture2D source,int targetWidth,int targetHeight) {
		Texture2D result=new Texture2D(targetWidth,targetHeight,source.format,true);
		Color[] rpixels=result.GetPixels(0);
		float incX=((float)1/source.width)*((float)source.width/targetWidth);
		float incY=((float)1/source.height)*((float)source.height/targetHeight);
		for(int px=0; px<rpixels.Length; px++) {
			rpixels[px] = source.GetPixelBilinear(incX*((float)px%targetWidth),
			                                      incY*((float)Mathf.Floor(px/targetWidth)));
		}
		result.SetPixels(rpixels,0);
		result.Apply();
		return result;
	}




	IEnumerator GetImage(WWW www)
	{
		
		yield return www;
		
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		Debug.Log ("temp : " + temp);
		UserImagebyte =temp.EncodeToPNG();
		UserImage = temp;
		Setimagebyte =temp.EncodeToPNG();
		Setimage = temp;
		SetMainPage();

	
			SetSettingPage();

	}


}
