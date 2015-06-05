using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandingManager : MonoBehaviour {
	public GameObject test;
	public GameObject Pitcher;
	int a = 0;
	UILabel I_Gold,I_TeamName,I_RankScore,I_TodayDealWith,I_TodayInfo,I_Memo,I_PlayersName,I_Batting;
    UITexture I_PlayersImage;
	UISprite LineTop,GameInfo,OkStrategy,Item,Community,I_BigLogo,I_TeamImage;
	string Is_Gold,Is_TeamName,Is_RankScore,Is_TodayDealWith,Is_TodayInfo,Is_Memo,Is_PlayersName,Is_Batting;
	string TeamColor;
	List<UILabel> I_LabelList = new List<UILabel>();
	List<string> I_StringList = new List<string>();
	List<UISprite> I_SpriteList = new List<UISprite>();
	GetLineupEvent mlineupEvent;


	UILabel V_LTeamName,V_RTeamName,V_BroadCasting,V_Location,V_time,V_LBatting,V_LPlayerName,V_RBatting,V_RPlayerName,V_Memo;
	UISprite V_LeftTeamImage,V_RightTeamImage;
	UITexture V_LPlayerImage,V_RPlayerImage;

	UILabel P_LTeamName,P_RTeamName,P_GameState,P_Score,P_LPlayersName,P_LBatting,P_RPlayersName,P_RBatting,P_Hit,P_Out;
	UISprite P_LeftTeamImage,P_RightTeamImage,One,Two,Three;
	UITexture P_LPlayerImage,P_RPlayerImage;



	// Use this for initialization
	public void Clcik(){
		if (a % 2 == 0) {
			test.transform.FindChild ("Info").gameObject.SetActive (true);
			test.transform.FindChild ("Playing").gameObject.SetActive (false);
		} else {
			test.transform.FindChild ("Info").gameObject.SetActive (false);
			test.transform.FindChild ("Playing").gameObject.SetActive (true);
		}
		a++;

	

	}




	public void Nongame(){
	


		test.transform.FindChild ("Info").gameObject.SetActive (false);
		test.transform.FindChild ("VS").gameObject.SetActive (true);
		test.transform.FindChild ("Playing").gameObject.SetActive (false);
		V_LeftTeamImage.spriteName = UtilMgr
			.GetTeamEmblem (UserMgr.Schedule.extend[0].imageName);
		V_RightTeamImage.spriteName = UtilMgr
			.GetTeamEmblem (UserMgr.Schedule.extend[1].imageName);

		V_LTeamName.text = 
			UserMgr.Schedule.extend [0].teamName;
		V_RTeamName.text = 
			UserMgr.Schedule.extend [1].teamName;
		V_BroadCasting.text = 
			UserMgr.Schedule.bcastChannel;
		V_Location.text = 
			getarea();
		V_time.text = 
			gettime();

		mlineupEvent = new GetLineupEvent (new EventDelegate (this, "GetNonGameData"));
		NetMgr.GetLineup (UserMgr.Schedule.extend[0].teamCode, mlineupEvent);
	}
	void GetNonGameData(){
	
		if (mlineupEvent.Response.data.pit.Count > 0) {
			
			V_LPlayerName.text = mlineupEvent.Response.data.pit [0].playerName + "#" + mlineupEvent.Response.data.pit [0].playerNumber;

			V_LBatting.text = "";
			

			
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mlineupEvent.Response.data.pit [0].imagePath + mlineupEvent.Response.data.pit [0].imageName);
			StartCoroutine (GetImage (www, V_LPlayerImage));
			//Debug.Log();
			
		}
		mlineupEvent = new GetLineupEvent (new EventDelegate (this, "GetNonGameData2"));
		NetMgr.GetLineup (UserMgr.Schedule.extend[1].teamCode, mlineupEvent);

	}
	void GetNonGameData2(){
		
		if (mlineupEvent.Response.data.pit.Count > 0) {
			
			V_RPlayerName.text = mlineupEvent.Response.data.pit [0].playerName + "#" + mlineupEvent.Response.data.pit [0].playerNumber;
			
			V_RBatting.text = "";
			
			
			
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mlineupEvent.Response.data.pit [0].imagePath + mlineupEvent.Response.data.pit [0].imageName);
			StartCoroutine (GetImage (www, V_RPlayerImage));
			//Debug.Log();
			
		}
	
		
	}
	public void Startgame(){
		test.transform.FindChild ("Info").gameObject.SetActive (false);
		test.transform.FindChild ("VS").gameObject.SetActive (false);
		test.transform.FindChild ("Playing").gameObject.SetActive (true);

		//StartCoroutine (view());
	}

	public void Heamhome(){
		test.transform.FindChild ("Info").gameObject.SetActive (true);
		test.transform.FindChild ("VS").gameObject.SetActive (false);
		test.transform.FindChild ("Playing").gameObject.SetActive (false);
	}
	public void StartHeamhome(){
		test.transform.FindChild ("Info").gameObject.SetActive (true);
		test.transform.FindChild ("VS").gameObject.SetActive (false);
		test.transform.FindChild ("Playing").gameObject.SetActive (false);
	}


	string Poldname = "";
	public void SetPitcher()
	{
		if (ScriptMainTop.DetailBoard != null) {
			if (ScriptMainTop.DetailBoard.player.Count > 0) {
				
		
			P_RPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Players Name").GetComponent<UILabel> ();
			P_RBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Batting").GetComponent<UILabel> ();		
			P_RPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();


		


			string playerInfo = ScriptMainTop.DetailBoard.player [0].playerName + "#" + ScriptMainTop.DetailBoard.player [0].playerNumber;
			P_RPlayersName.text = playerInfo;
			string playerAVG = ScriptMainTop.DetailBoard.player [0].ERA;
			P_RBatting.text = playerAVG;
			string strImage = ScriptMainTop.DetailBoard.player [0].imageName;
			if (Poldname != playerInfo) {
				if (ScriptMainTop.DetailBoard.player [0].imagePath != null 
					&& ScriptMainTop.DetailBoard.player [0].imagePath.Length > 0)
					strImage = ScriptMainTop.DetailBoard.player [0].imagePath
						+ ScriptMainTop.DetailBoard.player [0].imageName;
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
				StartCoroutine (GetImage (www, P_RPlayerImage));
			}
			Poldname = playerInfo;
			}
		}
	}







	public void SetHitter(QuizInfo quizInfo)
	{ 

		Debug.Log("SetHitter0");
		if (quizInfo!=null) {
			P_LPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Name").GetComponent<UILabel> ();
			P_LBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Batting").GetComponent<UILabel> ();
			P_LPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
			
			Debug.Log("SetHitter1");
			string playerInfo = quizInfo.playerName + "#" + quizInfo.playerNumber;
			P_LPlayersName.text = playerInfo;
			string playerAVG = quizInfo.rewardDividend;
			P_LBatting.text = playerAVG;
			
			string strImage = quizInfo.imageName;
			if(Holdname!=strImage){
				if (quizInfo.imagePath != null && quizInfo.imagePath.Length > 0)
					strImage =quizInfo .imagePath + quizInfo.imageName;
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
				Debug.Log ("url : " + Constants.IMAGE_SERVER_HOST + strImage);
				StartCoroutine (GetImage (www, P_LPlayerImage));
			}
			Holdname = strImage;
		}
	}

	string Holdname = "";

	void GetStartPleyer(){





		Debug.Log ("UtilMgr.SelectTeam : " + UtilMgr.SelectTeam);
		if (UtilMgr.SelectTeam.Length > 0) {
			mlineupEvent = new GetLineupEvent (new EventDelegate (this, "GetData1"));
			NetMgr.GetLineup (GetTeamCode (UtilMgr.SelectTeam), mlineupEvent);
		} else {
			mlineupEvent = new GetLineupEvent (new EventDelegate (this, "GetData"));
			NetMgr.GetLineup (UserMgr.UserInfo.GetTeamCode (), mlineupEvent);
		}


	}
	public void Start () {
		PathSettings ();
		//if (!test.transform.FindChild ("Info").gameObject.activeSelf && !test.transform.FindChild ("Info").gameObject.activeSelf && !test.transform.FindChild ("Info").gameObject.activeSelf) {
		//Debug.Log ("ScriptMainTop.LandingState == 0 : " + ScriptMainTop.LandingState);
		if (ScriptMainTop.LandingState == 0||ScriptMainTop.LandingState == 3) {	
			StartHeamhome();

			//GetData ();
			GetStartPleyer ();
		}else if(ScriptMainTop.LandingState == 1){
			Nongame();
		}else if(ScriptMainTop.LandingState == 2){
			Startgame();
		}
			//InPutData ();
			//SetTeamColor ();

	//	}
	}

	
	// Update is called once per frame
	void PathSettings () {

		//Info
		GameObject temp = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BG_W").gameObject;
//		I_Gold = transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("TopGoldbar").FindChild ("Label").GetComponent<UILabel> ();
//		I_LabelList.Add (I_Gold);
		I_TeamName = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BigLogo").FindChild ("Team Name").GetComponent<UILabel> ();
		I_LabelList.Add (I_TeamName);
		I_RankScore = temp.transform.FindChild ("RankScore").GetComponent<UILabel> ();
		I_LabelList.Add (I_RankScore);
		I_TodayDealWith = temp.transform.FindChild ("Today deal with").GetComponent<UILabel> ();
		I_LabelList.Add (I_TodayDealWith);
		I_TodayInfo = temp.transform.FindChild ("TodayInfo").GetComponent<UILabel> ();
		I_LabelList.Add (I_TodayInfo);
		I_Memo = temp.transform.FindChild ("Memo").GetComponent<UILabel> ();
		I_LabelList.Add (I_Memo);
		I_PlayersName = temp.transform.FindChild ("Players Name").GetComponent<UILabel> ();
		I_LabelList.Add (I_PlayersName);
		I_Batting = temp.transform.FindChild ("Batting").GetComponent<UILabel> ();
		I_LabelList.Add (I_Batting);

		I_PlayersImage = temp.transform.FindChild ("Players Image BackGround").GetChild (0).GetChild (0).GetComponent<UITexture> ();
		//VS
		V_LTeamName = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("Ground").FindChild("LeftTeam").FindChild("Label").GetComponent<UILabel>();
		V_RTeamName = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("Ground").FindChild("RightTeam").FindChild("Label").GetComponent<UILabel>();
		V_BroadCasting = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("Ground").FindChild ("Broadcasting").GetComponent<UILabel> ();
		V_Location = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("Ground").FindChild ("Location").GetComponent<UILabel> ();
		V_time = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("Ground").FindChild ("Time").GetComponent<UILabel> ();
		V_LBatting = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("BG_W").FindChild ("Current pitchers L").FindChild ("Batting").GetComponent<UILabel> ();
		V_LPlayerName = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("BG_W").FindChild ("Current pitchers L").FindChild ("Players Name").GetComponent<UILabel> ();
		V_RBatting = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("BG_W").FindChild ("Current pitchers R").FindChild ("Batting").GetComponent<UILabel> ();
		V_RPlayerName = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("BG_W").FindChild ("Current pitchers R").FindChild ("Players Name").GetComponent<UILabel> ();
		V_Memo = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("BG_W").FindChild ("MidBar").FindChild("Memo").GetComponent<UILabel>();

		V_LeftTeamImage = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("Ground").FindChild ("LeftTeam").GetComponent<UISprite> ();
		V_RightTeamImage = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("Ground").FindChild ("RightTeam").GetComponent<UISprite> ();

		V_LPlayerImage = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("BG_W").FindChild ("Current pitchers L").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
		V_RPlayerImage = transform.FindChild ("Scroll View").FindChild ("VS").FindChild ("BG_W").FindChild ("Current pitchers R").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();

		//Playing


		P_LTeamName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild("LeftTeam").FindChild ("Label").GetComponent<UILabel> ();
		P_RTeamName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild("RightTeam").FindChild ("Label").GetComponent<UILabel> ();
		P_GameState = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("Base").FindChild("BlueButten").FindChild ("Label").GetComponent<UILabel> ();
		P_Score = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("Score").GetComponent<UILabel> ();
		P_LPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Name").GetComponent<UILabel> ();
		P_LBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Batting").GetComponent<UILabel> ();
		P_RPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Players Name").GetComponent<UILabel> ();
		P_RBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Batting").GetComponent<UILabel> ();
		P_Hit = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("MidBar").FindChild ("Gauge").FindChild ("L").GetComponent<UILabel> ();
		P_Out = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("MidBar").FindChild ("Gauge").FindChild ("R").GetComponent<UILabel> ();

		P_LeftTeamImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("LeftTeam").GetComponent<UISprite> ();
		P_RightTeamImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("RightTeam").GetComponent<UISprite> ();
		One = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("Base").FindChild ("1").GetComponent<UISprite> ();
		Two = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("Base").FindChild ("2").GetComponent<UISprite> ();
		Three = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("Base").FindChild ("3").GetComponent<UISprite> ();

		P_LPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
		P_RPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
		//Buttens
		LineTop = transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("LineTop").GetComponent<UISprite> ();
		I_SpriteList.Add (LineTop);
		GameInfo = transform.FindChild ("Scroll View").FindChild ("Buttens").FindChild ("GameInfo").GetComponent<UISprite> ();
		I_SpriteList.Add (GameInfo);
		OkStrategy = transform.FindChild ("Scroll View").FindChild ("Buttens").FindChild ("OK Strategy").GetComponent<UISprite> ();
		I_SpriteList.Add (OkStrategy);
		Item = transform.FindChild ("Scroll View").FindChild ("Buttens").FindChild ("Item").GetComponent<UISprite> ();
		I_SpriteList.Add (Item);
		Community = transform.FindChild ("Scroll View").FindChild ("Buttens").FindChild ("Community").GetComponent<UISprite> ();
		I_SpriteList.Add (Community);

		I_BigLogo = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BigLogo").GetComponent<UISprite> ();
		I_TeamImage = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BigLogo").FindChild ("TeamImage").GetComponent<UISprite> ();
	}

	void GetData(){
		TeamColor = "c8004c";
//		UserMgr.Schedule.
//			TeamInfo
//		Is_Gold = UtilMgr.AddsThousandsSeparator ("123456789");
//		I_StringList.Add (Is_Gold);
		Is_TeamName = UserMgr.UserInfo.GetTeamFullName();
		I_StringList.Add (Is_TeamName);
		Is_RankScore = "";
		I_StringList.Add (Is_RankScore);
		if (UserMgr.Schedule.extend [0].teamCode == UserMgr.UserInfo.GetTeamCode()) {
			Is_TodayDealWith = "오늘의 상대 : " + UserMgr.Schedule.extend[1].teamName;
		}else if (UserMgr.Schedule.extend [1].teamCode == UserMgr.UserInfo.GetTeamCode()) {
			Is_TodayDealWith = "오늘의 상대 : " + UserMgr.Schedule.extend[0].teamName;
		}
		I_StringList.Add (Is_TodayDealWith);
		Is_TodayInfo = UserMgr.Schedule.bcastChannel + " | " + getarea() + " | " + gettime();
		I_StringList.Add (Is_TodayInfo);
		Is_Memo = "경기 시작과 함께 시청자 예측이 시작됩니다.";
		I_StringList.Add (Is_Memo);
		if (mlineupEvent.Response.data.pit.Count > 0) {

			Is_PlayersName = mlineupEvent.Response.data.pit [0].playerName + "#" + mlineupEvent.Response.data.pit [0].playerNumber;
			I_StringList.Add (Is_PlayersName);
			Is_Batting = "";

			I_StringList.Add (Is_Batting);

			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mlineupEvent.Response.data.pit [0].imagePath + mlineupEvent.Response.data.pit [0].imageName);
			StartCoroutine (GetImage (www, I_PlayersImage));
			//Debug.Log();

		}
		I_BigLogo.color = new Color (1,1,1,1);
		I_BigLogo.spriteName = UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
		I_TeamImage.color = new Color (1,1,1,1);
		I_TeamImage.spriteName = UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
		InPutData ();
		SetTeamColor ();
	}
	void GetData1(){
		TeamColor = "c8004c";
		//		UserMgr.Schedule.
		//			TeamInfo
		//		Is_Gold = UtilMgr.AddsThousandsSeparator ("123456789");
		//		I_StringList.Add (Is_Gold);
	
		Is_TeamName = GetTeamFullName(UtilMgr.SelectTeam);
		I_StringList.Add (Is_TeamName);
		Is_RankScore = "";
		I_StringList.Add (Is_RankScore);
		if (UserMgr.Schedule.extend [0].teamName == UtilMgr.SelectTeam) {
			Is_TodayDealWith = "오늘의 상대 : " + UserMgr.Schedule.extend[1].teamName;
		}else if (UserMgr.Schedule.extend [1].teamName == UtilMgr.SelectTeam) {
			Is_TodayDealWith = "오늘의 상대 : " + UserMgr.Schedule.extend[0].teamName;
		}
		I_StringList.Add (Is_TodayDealWith);
		Is_TodayInfo = UserMgr.Schedule.bcastChannel + " | " + getarea() + " | " + gettime();
		I_StringList.Add (Is_TodayInfo);
		Is_Memo = "경기 시작과 함께 시청자 예측이 시작됩니다.";
		I_StringList.Add (Is_Memo);
		if (mlineupEvent.Response.data.pit.Count > 0) {
			
			Is_PlayersName = mlineupEvent.Response.data.pit [0].playerName + "#" + mlineupEvent.Response.data.pit [0].playerNumber;
			I_StringList.Add (Is_PlayersName);
			Is_Batting = "";
			
			I_StringList.Add (Is_Batting);
			
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mlineupEvent.Response.data.pit [0].imagePath + mlineupEvent.Response.data.pit [0].imageName);
			StartCoroutine (GetImage (www, I_PlayersImage));
			//Debug.Log();
			
		}
		I_BigLogo.color = new Color (1,1,1,1);
		I_BigLogo.spriteName = UtilMgr.GetTeamEmblem(GetTeamCode(UtilMgr.SelectTeam));
		I_TeamImage.color = new Color (1,1,1,1);
		I_TeamImage.spriteName = UtilMgr.GetTeamEmblem(GetTeamCode(UtilMgr.SelectTeam));
		InPutData ();
		SetTeamColor ();
	}
	void InPutData(){
		for (int i = 0; i < I_StringList.Count; i++) {
			I_LabelList[i].text = I_StringList[i];
		}
		//BigLogo.spriteName = "";
		//TeamImage.spriteName = "";	
	}
	void SetTeamColor(){
		if (TeamColor.Length > 5) {
			int R = int.Parse( TeamColor.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
			int G = int.Parse( TeamColor.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
			int B = int.Parse( TeamColor.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);
			//Debug.Log("R : " +R);
			//Debug.Log("G : " +G);
			//Debug.Log("B : " +B);
			for (int i = 0; i < I_SpriteList.Count; i++) {
				I_SpriteList [i].color = new Color (
					(float)R/255,
					(float)G/255,
					(float)B/255);
			}

		} else {
			for (int i = 0; i < I_SpriteList.Count; i++) {
				I_SpriteList [i].color = new Color (0, 0, 0);
			}
		}
	}
	public void BlueButten(){
		if (!transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W Panel").gameObject.activeSelf) {
			transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W Panel").gameObject.SetActive (true);
		} else {
			transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W Panel").gameObject.SetActive (false);
		}

	}
	public void BlueButtenOff(){
		transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W Panel").gameObject.SetActive (false);
		
	}
	IEnumerator GetImage(WWW www, UITexture texture)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		texture.mainTexture = tmpTex;
	}


	List<string> ch = new List<string>();
	char[] array;
	int num;
	string result;

	string getarea(){
		ch.Clear ();
		array = UserMgr.Schedule.subTitle.ToCharArray ();
		
		for(int z = 0; z<array.Length;z++){
			if(num==3){
				ch.Add (array[z].ToString());
			}
			if(num==2){
				num+=1;	
			}
			if(array[z]==','){
				num+=1;
			}
		}
		num=0;
		result = string.Join("", ch.ToArray());
		return result;
	}
	string gettime(){
		ch.Clear();
		array = UserMgr.Schedule.startTime.ToCharArray ();
		for(int z = 8; z<12;z++){
			ch.Add (array[z].ToString());
			if(z==9){
				ch.Add (":");
			}
		}
		result = string.Join("", ch.ToArray());
		return result;
	}

	string GetTeamCode(string imgName)
	{
		switch(imgName)
		{
		case "LG":
			return "LG";
		case "롯데":
			return "LT";
		case "한화":
			return "HH";	
		case "두산":
			return "OB";
		case "기아":
			return "HT";
		case "삼성":
			return "SS";
		case "넥센":
			return "WO";		
		case "SK":
			return "SK";		
		case "NC":
			return "NC";
		case "KT":
			return "kt";
		}
		return "ic_liveball";
	}
	string GetTeamFullName(string imgName)
	{
		switch(imgName)
		{
		case "LG":
			return "LG 트윈스";
		case "롯데":
			return "롯데 자이언츠";
		case "한화":
			return "한화 이글스";	
		case "두산":
			return "두산 베어스";
		case "기아":
			return "KIA 타이거즈";
		case "삼성":
			return "삼성 라이온즈";
		case "넥센":
			return "넥센 히어로즈";		
		case "SK":
			return "SK 와이번스";		
		case "NC":
			return "NC 다이노스";
		case "KT":
			return "kt wiz";
		}
		return "ic_liveball";
	}
}
