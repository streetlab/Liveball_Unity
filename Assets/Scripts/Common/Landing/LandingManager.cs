using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandingManager : MonoBehaviour {
	public GameObject LandingScroll;
	public GameObject Pitcher;
	public GameObject Mid_BG;
	int a = 0;
	UILabel I_Gold,I_TeamName,I_RankScore,I_TodayDealWith,I_TodayInfo,I_Memo,I_PlayersName,I_Batting;
	UITexture I_PlayersImage;
	UISprite LineTop,GameInfo,OkStrategy,Item,Community,I_BigLogo,I_TeamImage;
	string Is_Gold,Is_TeamName,Is_RankScore,Is_TodayDealWith,Is_TodayInfo,Is_Memo,Is_PlayersName,Is_Batting;
	public static string TeamColor;
	List<UILabel> I_LabelList = new List<UILabel>();
	List<string> I_StringList = new List<string>();
	List<UISprite> I_SpriteList = new List<UISprite>();
	
	

	UILabel V_LTeamName,V_RTeamName,V_BroadCasting,V_Location,V_time,V_LBatting,V_LPlayerName,V_RBatting,V_RPlayerName,V_Memo;
	UISprite V_LeftTeamImage,V_RightTeamImage;
	UITexture V_LPlayerImage,V_RPlayerImage;
	
	UILabel P_LTeamName,P_RTeamName,P_GameState,P_Score,P_LPlayersName,P_LBatting,P_RPlayersName,P_RBatting,P_Hit,P_Out,P_B1,P_B2,P_B3,P_B4,P_B,P_T,P_VS;
	UISprite P_LeftTeamImage,P_RightTeamImage,One,Two,Three;
	UITexture P_LPlayerImage,P_RPlayerImage;
	GameObject MidBar;
	
	GetSposTeamInfoEvent TeamMain;
	
	// Use this for initialization
	public void Clcik(){
		if (a % 2 == 0) {
			LandingScroll.transform.FindChild ("Info").gameObject.SetActive (true);
			LandingScroll.transform.FindChild ("Playing").gameObject.SetActive (false);
		} else {
			LandingScroll.transform.FindChild ("Info").gameObject.SetActive (false);
			LandingScroll.transform.FindChild ("Playing").gameObject.SetActive (true);
		}
		a++;
		
		
		
	}
	
	
	
	
	public void Nongame(){
		
		
		
		LandingScroll.transform.FindChild ("Info").gameObject.SetActive (false);
		LandingScroll.transform.FindChild ("VS").gameObject.SetActive (true);
		LandingScroll.transform.FindChild ("Playing").gameObject.SetActive (false);
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
		
		if (UserMgr.Schedule != null) {
			
			TeamMain = new GetSposTeamInfoEvent (new EventDelegate (this, "GetNonGameData"));
			NetMgr.GetSposTeamInfo (UserMgr.Schedule.extend [1].teamCode, TeamMain);
			
			
		}
	}
	void GetNonGameData(){
		
		if (TeamMain.Response.data != null) {
			if(TeamMain.Response.data.myTeam!= null){
				TeamColor = TeamMain.Response.data.myTeam.teamColor;
				TeamColor = TeamColor.Replace("#","");
				SetTeamColor(TeamColor);
			}
			if(TeamMain.Response.data.other!=null){
				if(TeamMain.Response.data.other.pitcher != null){
					
					Debug.Log("V_LPlayerName : " + V_LPlayerName);
					V_LPlayerName.text = TeamMain.Response.data.other.pitcher.playerName + "#" + TeamMain.Response.data.other.pitcher.playerNumber;
					
					V_LBatting.text = TeamMain.Response.data.other.pitcher.ERA;
					
					V_Location.text="";
					V_time.text ="";
					V_Location.text = 
						getarea(TeamMain.Response.data);
					V_time.text = 
						gettime(TeamMain.Response.data);
					
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + TeamMain.Response.data.other.pitcher.imagePath + TeamMain.Response.data.other.pitcher.imageName);
					StartCoroutine (GetImage (www, V_LPlayerImage));
					if(TeamMain.Response.data.myTeam!= null){
						V_RPlayerName.text = TeamMain.Response.data.myTeam.pitcher.playerName + "#" + TeamMain.Response.data.myTeam.pitcher.playerNumber;
						
						V_RBatting.text = TeamMain.Response.data.myTeam.pitcher.ERA;
						
						
						
						www = new WWW (Constants.IMAGE_SERVER_HOST + TeamMain.Response.data.myTeam.pitcher.imagePath + TeamMain.Response.data.myTeam.pitcher.imageName);
						StartCoroutine (GetImage (www, V_RPlayerImage));
						//Debug.Log();
					}
				}
			}
		}
		
	}
	
	public void Startgame(){
		LandingScroll.transform.FindChild ("Info").gameObject.SetActive (false);
		LandingScroll.transform.FindChild ("VS").gameObject.SetActive (false);
		LandingScroll.transform.FindChild ("Playing").gameObject.SetActive (true);
		transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("RankBG").gameObject.SetActive (true);
		if (ScriptMainTop.LandingState == 3) {
			LandingScroll.transform.FindChild ("Playing").FindChild("Ground").FindChild("END").gameObject.SetActive (true);
			transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("RankBG").gameObject.SetActive (false);
			if(int.Parse(UserMgr.Schedule.myEntryFee)>0){
				if(UserMgr.Schedule.doneGame!=null){
					if(UserMgr.Schedule.doneGame=="0"){
						DialogueMgr.ShowDialogue ("정산중", "경기가 모두 종료되면 정산 됩니다.\n랭킹에 따른 상품은 익일 지급 됩니다.", DialogueMgr.DIALOGUE_TYPE.Alert , null);
					}else{
						transform.root.FindChild("Ranking Reward").gameObject.SetActive(true);
					}
				}else{
					DialogueMgr.ShowDialogue ("정산중", "경기가 모두 종료되면 정산 됩니다.\n랭킹에 따른 상품은 익일 지급 됩니다.", DialogueMgr.DIALOGUE_TYPE.Alert , null);
				}
				//DialogueMgr.ShowDialogue ("정산중", "모든 경기 종료 후\n집계 뒤 보상이 지급됩니다.", DialogueMgr.DIALOGUE_TYPE.Alert , null);
			//	transform.root.FindChild("Ranking Reward").gameObject.SetActive(true);
			}
		}
		Debug.Log (		transform.parent.parent.FindChild ("TF_Highlight").FindChild ("MatchPlaying").FindChild ("ListHighlight").FindChild ("Label").gameObject.activeSelf);
		transform.parent.parent.FindChild ("TF_Highlight").FindChild ("MatchPlaying").FindChild ("ListHighlight").FindChild ("Label").gameObject.SetActive (false);
		Debug.Log (		transform.parent.parent.FindChild ("TF_Highlight").FindChild ("MatchPlaying").FindChild ("ListHighlight").FindChild ("Label").gameObject.activeSelf);
		Debug.Log ("UserMgr.UserInfo.myEntryFee : : " + UserMgr.Schedule.myEntryFee);
		
		if (UserMgr.Schedule.myEntryFee != null) {
			if (int.Parse (UserMgr.Schedule.myEntryFee) > 0) {
				transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("RankBG").FindChild ("RankJoinButton").gameObject.SetActive (false);
				transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("RankBG").FindChild ("RakingInfo").gameObject.SetActive (true);
				
			}
		}
		//StartCoroutine (view());
	}
	
	
	public void StartHeamhome(){
		LandingScroll.transform.FindChild ("Info").gameObject.SetActive (true);
		LandingScroll.transform.FindChild ("VS").gameObject.SetActive (false);
		LandingScroll.transform.FindChild ("Playing").gameObject.SetActive (false);
	}
	
	
	string Poldname = "";
	public void SetPitcher()
	{
		if (ScriptMainTop.DetailBoard != null) {
			if (ScriptMainTop.DetailBoard.player.Count > 0) {
				
				
				P_RPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Players Name").GetComponent<UILabel> ();
				P_RBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Batting").GetComponent<UILabel> ();        
				P_RPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current pitchers").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
				int num;
				
				num = 0;
				
				
				string playerInfo = ScriptMainTop.DetailBoard.player [num].playerName + "#" + ScriptMainTop.DetailBoard.player [num].playerNumber;
				P_RPlayersName.text = playerInfo;
				string playerAVG = ScriptMainTop.DetailBoard.player [num].ERA;
				P_RBatting.text = playerAVG;
				string strImage = ScriptMainTop.DetailBoard.player [num].imageName;
				if (Poldname != playerInfo) {
					if (ScriptMainTop.DetailBoard.player [num].imagePath != null 
					    && ScriptMainTop.DetailBoard.player [num].imagePath.Length > 0)
						strImage = ScriptMainTop.DetailBoard.player [num].imagePath
							+ ScriptMainTop.DetailBoard.player [num].imageName;
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
					StartCoroutine (GetImage (www, P_RPlayerImage));
				}
				Poldname = playerInfo;
			}
		}
	}
	
	
	
	
	
	
	static public List<nextPlayerInfo> N;
	List<nextPlayerInfo> save1 = null;
	List<nextPlayerInfo> save2 = null;
	int gameround;
	static public List<nextPlayerInfo> Old;
	string strImage;
	public void SetHitter(QuizListInfo nextPlayer)
	{
		//Debug.Log ("ScriptMainTop.LandingState : " + ScriptMainTop.LandingState);
		//Debug.Log ("nextPlayer : " + nextPlayer.Count);
		
		if((ScriptMainTop.LandingState==2||ScriptMainTop.LandingState==3)&&nextPlayer!=null){
			N = nextPlayer.nextPlayer;
			Debug.Log("UtilMgr.gameround : : " + UtilMgr.gameround);
			if(UtilMgr.gameround%2==0){
				
				save1=nextPlayer.nextPlayer;
				Debug.Log("save1 . playerName : " + save1[0].playerName);
			}else{
				
				save2=nextPlayer.nextPlayer;
				Debug.Log("save2 . playerName : " + save2[0].playerName);
			}
			
			
			
			//N[i].
			strImage = "";
			Debug.Log("SetHitter0");
			if (N!=null) {
				//            P_LPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Name").GetComponent<UILabel> ();
				//            P_LBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Batting").GetComponent<UILabel> ();
				//            P_LPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
				//  
				Debug.Log("N.Count : " + N.Count);
				for(int i = 0; i <N.Count;i++ ){
					if(N[i].type == 1){
						string playerInfo = N[i].playerName + "#" + N[i].playerNumber;
						P_LPlayersName.text = playerInfo;
						Debug.Log("playerInfo : " + playerInfo);
						string playerAVG = N[i].hitAvg;
						P_LBatting.text = playerAVG;
						
						strImage = N[i].imageName;
						P_B.text = N[i].hitAvg;
						P_B1.text = N[i].hitH.ToString()+"%";
						P_B2.text = N[i].hit2B.ToString()+"%";
						P_B3.text = N[i].hitHr.ToString()+"%";
						P_B4.text = N[i].hitBB.ToString()+"%";
						P_VS.text = "VS 시즌타율 " + N[i].hitAvg;
						
						Debug.Log("nextPlayer[i].hitAvg : " + N[i].hitAvg);
						if(N[i].hitAvg!=""&&N[i].hitAvg!=null&&N[i].hitAvg!="-"){
							MidBar.transform.FindChild("Gauge").FindChild("Hits").GetComponent<UISprite>().width =  (int)Mathf.Round(340f*(float.Parse(N[i].hitAvg)));
							
							MidBar.transform.FindChild("Gauge").FindChild("L").GetComponent<UILabel>().text = "안타 " + (float.Parse(N[i].hitAvg)*100f).ToString()+"%";
							MidBar.transform.FindChild("Gauge").FindChild("R").GetComponent<UILabel>().text = ((1-float.Parse(N[i].hitAvg))*100f).ToString()+"% 아웃";
						}
						if(Holdname!=strImage){
							if (N[i].imagePath != null && N[i].imagePath.Length > 0){
								strImage =N[i] .imagePath + N[i].imageName;
								WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
								Debug.Log ("url : " + Constants.IMAGE_SERVER_HOST + strImage);
								StartCoroutine (GetImage (www, P_LPlayerImage));
							}
							
						}
					}
				}
				Holdname = strImage;
			}
			
			
		}
		gameround = UtilMgr.gameround;
	}
	public void CheckGameRound(){
		Debug.Log("UtilMgr.gameround : " + UtilMgr.gameround);
		Debug.Log("gameround : " + gameround);
		if(UtilMgr.gameround!=gameround){
			Debug.Log("UtilMgr.gameround!=gameround!!");
			if(UtilMgr.gameround%2==0){
				if(save1!=null){
					Debug.Log("save1 . playerName re : " + save1[0].playerName);
				}else{
					Debug.Log("save1 is null");
				}
				N =save1;
			}else{
				if(save2!=null){
					Debug.Log("save2 . playerName re : " + save2[0].playerName);
				}else{
					Debug.Log("save2 is null");
				}
				N =save2;
			}
		}
	}
	public void M1(GameObject bot){
		Debug.Log ("FirstLinup : " + FirstLinup);
		if (FirstLinup) {
		
			P_T.text = "타율";
			P_B.text = Lineup.hitAvg;
			P_B1.text = Lineup.hitH.ToString () + "%";
			P_B2.text = Lineup.hit2B.ToString () + "%";
			P_B3.text = Lineup.hitHr.ToString () + "%";
			P_B4.text = Lineup.hitBB.ToString () + "%";

		} else {
		
		
			if (bot == null) {
				P_T.text = "타율";
				P_B.text = "";
				P_B1.text = "";
				P_B2.text = "";
				P_B3.text = "";
				P_B4.text = "";
				if (N != null) {
					for (int i = 0; i <N.Count; i++) {
						if (N [i].type == 1) {
							P_B.text = N [i].hitAvg;
							P_B1.text = N [i].hitH.ToString () + "%";
							P_B2.text = N [i].hit2B.ToString () + "%";
							P_B3.text = N [i].hitHr.ToString () + "%";
							P_B4.text = N [i].hitBB.ToString () + "%";
						}
					}
				}
			} else {
				bot.transform.parent.FindChild ("Top").GetComponent<UILabel> ().text = "타율";
				bot.GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 1").GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 2").GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 3").GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 4").GetComponent<UILabel> ().text = "";
				if (Old != null) {
					for (int i = 0; i <Old.Count; i++) {
						if (Old [i].type == 1) {
						
							bot.GetComponent<UILabel> ().text = Old [i].title;
							bot.transform.FindChild ("Top 1").GetComponent<UILabel> ().text = Old [i].hitH.ToString () + "%";
							bot.transform.FindChild ("Top 2").GetComponent<UILabel> ().text = Old [i].hit2B.ToString () + "%";
							bot.transform.FindChild ("Top 3").GetComponent<UILabel> ().text = Old [i].hitHr.ToString () + "%";
							bot.transform.FindChild ("Top 4").GetComponent<UILabel> ().text = Old [i].hitBB.ToString () + "%";
						}
					}
				}
			
			}
		}
	}
	public void M2(GameObject bot){
		if (FirstLinup) {
			P_T.text = "기준";
			P_B.text = "";
			P_B1.text = "";
			P_B2.text = "";
			P_B3.text = "";
			P_B4.text = "";
		} else {
			if (bot == null) {
				P_T.text = "기준";
				P_B.text = "";
				P_B1.text = "";
				P_B2.text = "";
				P_B3.text = "";
				P_B4.text = "";
				if (Old != null) {
					for (int i = 0; i <Old.Count; i++) {
						if (Old [i].type == 2) {
						
							P_B.text = Old [i].title;
							P_B1.text = Old [i].hitH.ToString () + "%";
							P_B2.text = Old [i].hit2B.ToString () + "%";
							P_B3.text = Old [i].hitHr.ToString () + "%";
							P_B4.text = Old [i].hitBB.ToString () + "%";
						}
					}
				}
			} else {
				bot.transform.parent.FindChild ("Top").GetComponent<UILabel> ().text = "기준";
				bot.GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 1").GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 2").GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 3").GetComponent<UILabel> ().text = "";
				bot.transform.FindChild ("Top 4").GetComponent<UILabel> ().text = "";
				if (Old != null) {
					for (int i = 0; i <Old.Count; i++) {
						if (Old [i].type == 2) {
						
							bot.GetComponent<UILabel> ().text = Old [i].title;
							bot.transform.FindChild ("Top 1").GetComponent<UILabel> ().text = Old [i].hitH.ToString () + "%";
							bot.transform.FindChild ("Top 2").GetComponent<UILabel> ().text = Old [i].hit2B.ToString () + "%";
							bot.transform.FindChild ("Top 3").GetComponent<UILabel> ().text = Old [i].hitHr.ToString () + "%";
							bot.transform.FindChild ("Top 4").GetComponent<UILabel> ().text = Old [i].hitBB.ToString () + "%";
						}
					}
				}
			}
		}
	}
	string Holdname = "";
	
	void GetStartPleyer(){
		Debug.Log ("UtilMgr.SelectTeam : " + UtilMgr.SelectTeam);
		if (UtilMgr.SelectTeam.Length > 0) {
			Debug.Log ("GetDate");
			//if (UserMgr.Schedule != null) {
			Debug.Log ("GetDate");
			
			TeamColor = Getteamcolor(GetTeamFullName(UtilMgr.SelectTeam));
			
			TeamColor = TeamColor.Replace ("#", "");
			Is_TeamName = GetTeamFullName(UtilMgr.SelectTeam);
			I_BigLogo.color = new Color (1,1,1,1);
			I_BigLogo.spriteName = UtilMgr.GetTeamEmblem(GetTeamCode(UtilMgr.SelectTeam));
			I_TeamImage.color = new Color (1,1,1,1);
			I_TeamImage.spriteName = UtilMgr.GetTeamEmblem(GetTeamCode(UtilMgr.SelectTeam));
			TeamMain = new GetSposTeamInfoEvent (new EventDelegate (this, "GetData"));
			NetMgr.GetSposTeamInfo (GetTeamCode (UtilMgr.SelectTeam), TeamMain);
			//}
		} else {
			Debug.Log ("GetDate1");
			//if (UserMgr.Schedule != null) {
			Debug.Log ("GetDate1");
			TeamColor = UserMgr.UserInfo.favoBB.teamColor.ToString();
			
			TeamColor = TeamColor.Replace ("#", "");
			Is_TeamName = UserMgr.UserInfo.GetTeamFullName();
			I_BigLogo.color = new Color (1,1,1,1);
			I_BigLogo.spriteName = UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
			I_TeamImage.color = new Color (1,1,1,1);
			I_TeamImage.spriteName = UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
			TeamMain = new GetSposTeamInfoEvent (new EventDelegate (this, "GetData1"));
			NetMgr.GetSposTeamInfo (UserMgr.UserInfo.GetTeamCode(), TeamMain);
			//}
		}
		
		
	}
	public bool nonstart = false;
	
	
	
	public void Start () {
		UtilMgr.gameround = 0;
		if (UserMgr.Schedule != null) {
			int inning;
			UtilMgr.gameround = UserMgr.Schedule.playRound*2;

				for(int i = 0;i<UserMgr.Schedule.inningType.Length;i++){
					if(UserMgr.Schedule.inningType[i]=='초'){
						UtilMgr.gameround-=1;
					}
				}

			if(UtilMgr.gameround>1){
				if (UserMgr.Schedule.myEntryFee != null) {
					if (int.Parse (UserMgr.Schedule.myEntryFee) > 0) {
						GetRank ();
					}
				}
			}
		}
		UtilMgr.gameround = 0;
		Debug.Log ("ScriptMainTop.LandingState : " + ScriptMainTop.LandingState);
		N = null;
		Old = null;
		if (!nonstart) {
			PathSettings ();
			if (TeamColor != null) {
				TeamColor = TeamColor.Replace ("#", "");
				SetTeamColor (TeamColor);
			}
			
			
			//if (!test.transform.FindChild ("Info").gameObject.activeSelf && !test.transform.FindChild ("Info").gameObject.activeSelf && !test.transform.FindChild ("Info").gameObject.activeSelf) {
			//Debug.Log ("ScriptMainTop.LandingState == 0 : " + ScriptMainTop.LandingState);
			if (ScriptMainTop.LandingState == 0 || ScriptMainTop.LandingState == 4) {    
				StartHeamhome ();
				
				//GetData ();
				GetStartPleyer ();
			} else if (ScriptMainTop.LandingState == 1) {
				Nongame ();
			} else if (ScriptMainTop.LandingState == 2 || ScriptMainTop.LandingState == 3) {
				Startgame ();
			}
			//InPutData ();
			//SetTeamColor ();
			
			//    }
		}
	}
	
	
	// Update is called once per frame
	bool CheckPath = false;
	void PathSettings () {
		Debug.Log ("PathSettings");
		LandingScroll.transform.FindChild ("Playing").FindChild("Ground").FindChild("END").gameObject.SetActive (false);
		//Info
		GameObject temp = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BG_W").gameObject;
		//        I_Gold = transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("TopGoldbar").FindChild ("Label").GetComponent<UILabel> ();
		//        I_LabelList.Add (I_Gold);
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
		
		P_B1 = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Mid_BG").FindChild ("Bot").FindChild ("Top 1").GetComponent<UILabel> ();
		P_B2 = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Mid_BG").FindChild ("Bot").FindChild ("Top 2").GetComponent<UILabel> ();
		P_B3 = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Mid_BG").FindChild ("Bot").FindChild ("Top 3").GetComponent<UILabel> ();
		P_B4 = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Mid_BG").FindChild ("Bot").FindChild ("Top 4").GetComponent<UILabel> ();
		P_B = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Mid_BG").FindChild ("Bot").GetComponent<UILabel> ();
		P_T = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Mid_BG").FindChild ("Top").GetComponent<UILabel> ();
		P_VS = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Mid_BG").FindChild ("VS").GetComponent<UILabel> ();
		MidBar = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("MidBar").gameObject;
		
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
		
		I_SpriteList.Add (transform.parent.parent.FindChild("TF_Highlight").FindChild("Panel").FindChild("LineTopBlue").GetComponent<UISprite> ());
		I_SpriteList.Add (transform.parent.parent.FindChild("TF_Lineup").FindChild("Panel").FindChild("LineTopBlue").GetComponent<UISprite> ());
		I_SpriteList.Add (transform.parent.parent.FindChild("TF_Livetalk").FindChild("Panel 1").FindChild("LineTopBlue").GetComponent<UISprite> ());
		//I_SpriteList.Add (transform.parent.parent.FindChild("TF_Items").FindChild("TF_Items").FindChild("Top").FindChild("Panel").FindChild("LineTopBlue").GetComponent<UISprite> ());
		I_SpriteList.Add (transform.parent.parent.FindChild("TF_Betting").FindChild("Panel").FindChild("LineTop").GetComponent<UISprite> ());
		I_SpriteList.Add (transform.parent.parent.FindChild("GameObject").FindChild("Top").FindChild("Panel").FindChild("BtnPost").FindChild("TF_Post").FindChild("Panel").FindChild("LineTopBlue").GetComponent<UISprite> ());
		
		I_BigLogo = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BigLogo").GetComponent<UISprite> ();
		I_TeamImage = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BigLogo").FindChild ("TeamImage").GetComponent<UISprite> ();
		CheckPath = true;
	}
	
	
	void GetData(){
		Debug.Log ("GetDate");
		SposTeamInfo T = TeamMain.Response.data;
		if (T.myTeam != null) {
			TeamColor = T.myTeam.teamColor;
			TeamColor = TeamColor.Replace ("#", "");
			//        UserMgr.Schedule.
			//            TeamInfo
			//        Is_Gold = UtilMgr.AddsThousandsSeparator ("123456789");
			//        I_StringList.Add (Is_Gold);
		}
		Is_TeamName = GetTeamFullName(UtilMgr.SelectTeam);
		I_StringList.Add (Is_TeamName);
		Is_RankScore = "";
		if (T.myTeam != null) {
			Is_RankScore = T.myTeam.ranking + "위 " + T.myTeam.countWin + "승 "
				+ T.myTeam.countLose + "패 " + T.myTeam.countDraw + "무 ";
		}
		I_StringList.Add (Is_RankScore);
		Is_TodayDealWith = "오늘의 상대 : ";
		if (T.other != null) {
			if (T.other.teamName != null) {
				Is_TodayDealWith = "오늘의 상대 : " + T.other.teamName;
			}
		}
		I_StringList.Add (Is_TodayDealWith);
		Is_TodayInfo = "";
		if (T.schedule != null) {
			Is_TodayInfo = T.schedule.bcastChannel + " | " + getarea (T) + " | " + gettime (T);
		}
		I_StringList.Add (Is_TodayInfo);
		Is_Memo = "경기 시작과 함께 시청자 예측이 시작됩니다.";
		I_StringList.Add (Is_Memo);
		I_BigLogo.color = new Color (1,1,1,1);
		I_BigLogo.spriteName = UtilMgr.GetTeamEmblem(GetTeamCode(UtilMgr.SelectTeam));
		I_TeamImage.color = new Color (1,1,1,1);
		I_TeamImage.spriteName = UtilMgr.GetTeamEmblem(GetTeamCode(UtilMgr.SelectTeam));
		if (T.myTeam != null) {
			if (T.myTeam.pitcher != null) {
				
				Is_PlayersName = T.myTeam.pitcher.playerName + "#" + T.myTeam.pitcher.playerNumber.ToString ();
				I_StringList.Add (Is_PlayersName);
				Is_Batting = T.myTeam.pitcher.ERA;
				
				I_StringList.Add (Is_Batting);
				
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + T.myTeam.pitcher.imagePath + T.myTeam.pitcher.imageName);
				StartCoroutine (GetImage (www, I_PlayersImage));
				//Debug.Log();
				
			}
			
		}
		
		InPutData ();
		SetTeamColor (TeamColor);
		
	}
	void GetData1(){
		Debug.Log ("GetDate1");
		
		SposTeamInfo T = TeamMain.Response.data;
		
		
		if (T.myTeam != null) {
			TeamColor = T.myTeam.teamColor;
			
			TeamColor = TeamColor.Replace ("#", "");
		}
		//        UserMgr.Schedule.
		//            TeamInfo
		//        Is_Gold = UtilMgr.AddsThousandsSeparator ("123456789");
		//        I_StringList.Add (Is_Gold);
		
		Is_TeamName = UserMgr.UserInfo.GetTeamFullName();
		I_StringList.Add (Is_TeamName);
		Is_RankScore = "";
		if (T.myTeam != null) {
			Is_RankScore = T.myTeam.ranking + "위 " + T.myTeam.countWin + "승 "
				+ T.myTeam.countLose + "패 " + T.myTeam.countDraw + "무 ";
		}
		I_StringList.Add (Is_RankScore);
		Is_TodayDealWith = "오늘의 상대 : ";
		if (T.other != null){
			if (T.other.teamName != null) {
				Is_TodayDealWith = "오늘의 상대 : " + T.other.teamName;
			}
		}
		I_StringList.Add (Is_TodayDealWith);
		Is_TodayInfo = "";
		if (T.schedule != null) {
			Is_TodayInfo = T.schedule.bcastChannel + " | " + getarea (T) + " | " + gettime (T);
		}
		I_StringList.Add (Is_TodayInfo);
		Is_Memo = "경기 시작과 함께 시청자 예측이 시작됩니다.";
		I_StringList.Add (Is_Memo);
		I_BigLogo.color = new Color (1,1,1,1);
		I_BigLogo.spriteName = UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
		I_TeamImage.color = new Color (1,1,1,1);
		I_TeamImage.spriteName = UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
		if (T.myTeam != null) {
			if (T.myTeam.pitcher != null) {
				
				Is_PlayersName = T.myTeam.pitcher.playerName + "#" + T.myTeam.pitcher.playerNumber.ToString ();
				I_StringList.Add (Is_PlayersName);
				Is_Batting = T.myTeam.pitcher.ERA;
				
				I_StringList.Add (Is_Batting);
				
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + T.myTeam.pitcher.imagePath + T.myTeam.pitcher.imageName);
				StartCoroutine (GetImage (www, I_PlayersImage));
				//Debug.Log();
				
			}
		}
		InPutData ();
		SetTeamColor (TeamColor);
		
	}
	void InPutData(){
		for (int i = 0; i < I_StringList.Count; i++) {
			I_LabelList[i].text = I_StringList[i];
		}
		//BigLogo.spriteName = "";
		//TeamImage.spriteName = "";    
	}
	public void SetTeamColor(string teamcolor){
		if (!CheckPath) {
			PathSettings ();
		}
		TeamColor = teamcolor;
		Debug.Log ("TeamColor is : " + teamcolor);
		if (teamcolor.Length > 5) {
			int R = int.Parse( teamcolor.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
			int G = int.Parse( teamcolor.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
			int B = int.Parse( teamcolor.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);
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
			transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild("Base").FindChild("BlueButten")
				.GetComponent<UIButton>().defaultColor = new Color(225f/255f,200f/255f,150f/255f);
		} else {
			transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W Panel").gameObject.SetActive (false);
			transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild("Base").FindChild("BlueButten")
				.GetComponent<UIButton>().defaultColor = new Color(1f,1f,1f);
		}
		
	}
	public void BlueButtenOff(){
		transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W Panel").gameObject.SetActive (false);
		transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild("Base").FindChild("BlueButten")
			.GetComponent<UIButton>().defaultColor = new Color(1f,1f,1f);
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
	
	string getarea(SposTeamInfo T){
		ch.Clear ();
		array = T.schedule.scheduleName.ToCharArray ();
		//Debug.Log ("ScheduleName : " + T.schedule.scheduleName);
		for(int z = array.Length-2; z<array.Length;z++){
			ch.Add (array[z].ToString());
		}
		
		
		
		
		result = string.Join("", ch.ToArray());
		return result;
	}
	string gettime(SposTeamInfo T){
		ch.Clear();
		array = T.schedule.startTime.ToCharArray ();
		for(int z = 8; z<12;z++){
			ch.Add (array[z].ToString());
			if(z==9){
				ch.Add (":");
			}
		}
		result = string.Join("", ch.ToArray());
		return result;
	}
	
	public string GetTeamCode(string imgName)
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
	public string GetTeamFullName(string imgName)
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
	
	public string Getteamcolor(string teamname){
		
		
		
		switch (teamname) {
		case "삼성 라이온즈":
			
			return "005bac";
			break;
			
		case "두산 베어스":
			
			return "211c3d";
			break;
			
		case "SK 와이번스":
			
			return "e0002a";
			break;
			
		case "넥센 히어로즈":
			
			return "9b134e";
			break;
			
		case "NC 다이노스":
			
			return "0c274a";
			break;
			
		case "한화 이글스":
			
			return "ea5415";
			break;
		case "KIA 타이거즈":
		case "기아 타이거즈":
			
			return "e60014";
			break;
			
		case "롯데 자이언츠":
			
			return "e8410d";
			break;
			
		case "LG 트윈스":
			
			return "c8004c";
			break;
		case "KT 위즈":
		case "kt wiz":
			return "1f2021";
			break;
		};
		
		return "c1c1c1";
		
		
	}
	GameJoinNEntryFeeEvent gje;
	public void joingame(){
		DialogueMgr.ShowDialogue ("랭킹전 입장", "[루비"+UserMgr.Schedule.entryFee+"개] 를 사용하여 입장\n하시겠습니까?" , DialogueMgr.DIALOGUE_TYPE.YesNo , DialogueHandler);
		
	}
	void ex(){
		Debug.Log ("UserMgr.Schedule.myEntryFee : " + UserMgr.Schedule.myEntryFee);
		if(
			UserMgr.Schedule.myEntryFee==null||int.Parse(UserMgr.Schedule.myEntryFee)==0
			){
			UserMgr.Schedule.myEntryFee =UserMgr.Schedule.entryFee.ToString();
			UserMgr.UserInfo.userRuby=(int.Parse(UserMgr.UserInfo.userRuby)-int.Parse(UserMgr.Schedule.entryFee)).ToString();
			transform.parent.FindChild("Top").FindChild("Panel").FindChild("RankBG").FindChild("RankJoinButton").gameObject.SetActive(false);
			transform.parent.FindChild("Top").FindChild("Panel").FindChild("RankBG").FindChild("RakingInfo").gameObject.SetActive(true);
			
		}
	}
	
	
	void DialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			if (int.Parse (UserMgr.UserInfo.userRuby) < int.Parse(UserMgr.Schedule.entryFee)) {
				DialogueMgr.ShowDialogue ("입장 실패", "루비가 부족합니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			} else {
				gje = new GameJoinNEntryFeeEvent (new EventDelegate (this, "ex"));
				NetMgr.GameJoinNEntryFee (gje);
			}
		}
		
	}
	GameSposGameEvent GSG;
	public void GetRank(){
		GSG = new GameSposGameEvent (new EventDelegate (this, "SetRank"));
		NetMgr.GameSposGame (GSG);
	}
	void SetRank(){
		
		//float num;
		Debug.Log ("GSG.Response.data.myRank : " + GSG.Response.data.myRank);
		Debug.Log ("GSG.Response.data.joinerCount : " + GSG.Response.data.joinerCount);
	//	num = float.Parse(GSG.Response.data.myRank)/float.Parse(GSG.Response.data.joinerCount)*100f;
		float numF = float.Parse(GSG.Response.data.myRank)/float.Parse(GSG.Response.data.joinerCount)*100f;
		Debug.Log ("numF : " + numF);
		int num = Mathf.CeilToInt(numF);
		//		num = (float)getnum (num);
	
		Debug.Log ("num : " + num);
		//num = (float)getnum (num);
		//Debug.Log ("num2 : " + num);
		transform.parent.FindChild("Top").FindChild("Panel").FindChild("RankBG").FindChild("RakingInfo").FindChild("Dia").
			GetComponent<UILabel>().text = "0";
		if (num <= 50&&float.Parse (GSG.Response.data.myRank) != 0) {
			transform.parent.FindChild("Top").FindChild("Panel").FindChild("RankBG").FindChild("RakingInfo").FindChild("Dia").
				GetComponent<UILabel>().text = UserMgr.Schedule.rewardValue;
		


		}
		if (float.Parse (GSG.Response.data.myRank) == 0) {
			transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("RankBG").FindChild ("RakingInfo").FindChild ("Rank").
				GetComponent<UILabel> ().text = "[ff0000]100%[-]";
		} else {
			if (num <= 50){
			transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("RankBG").FindChild ("RakingInfo").FindChild ("Rank").
			GetComponent<UILabel> ().text = "[00ff00]"+num + "%[-]";
			}else{
				transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("RankBG").FindChild ("RakingInfo").FindChild ("Rank").
					GetComponent<UILabel> ().text = "[ff0000]"+num + "%[-]";
			}

		}
//			GetComponent<UILabel>().text = num.ToString()+"%";
//	}
//	int getnum(float num){
//		int number = (int)num;
//		if ((float)number < num) {
//			number+=1;
//		}
//		if (number > 100) {
//			number = 100;
//		}
//		return number;
	
	}
	public bool FirstLinup = false;
	GetLineupEvent mlineupEvent;
	PlayerInfo Lineup;
	public PlayerInfo Lineup2;
	public void GetLineUp(){
		mlineupEvent = new GetLineupEvent (new EventDelegate (this, "sethitter"));
		NetMgr.GetLineup (UserMgr.Schedule.extend [0].teamCode, mlineupEvent);
	}
	void sethitter(){
		if (mlineupEvent.Response.data != null) {
			if (mlineupEvent.Response.data.hit.Count > 0) {
				FirstLinup = true;
				Lineup = mlineupEvent.Response.data.hit [0];

				P_LPlayersName.text = mlineupEvent.Response.data.hit [0].playerName + "#" + mlineupEvent.Response.data.hit [0].playerNumber;
				P_LBatting.text = mlineupEvent.Response.data.hit [0].hitAvg;

				P_B.text = mlineupEvent.Response.data.hit [0].hitAvg;
				P_B1.text = mlineupEvent.Response.data.hit [0].hitH.ToString () + "%";
				P_B2.text = mlineupEvent.Response.data.hit [0].hit2B.ToString () + "%";
				P_B3.text = mlineupEvent.Response.data.hit [0].hitHr.ToString () + "%";
				P_B4.text = mlineupEvent.Response.data.hit [0].hitBB.ToString () + "%";
				P_VS.text = "VS 시즌타율 " + mlineupEvent.Response.data.hit [0].hitAvg;

				Mid_BG.transform.FindChild ("Bot").GetComponent<UILabel> ().text = mlineupEvent.Response.data.hit [0].hitAvg;
				Mid_BG.transform.FindChild ("Bot").FindChild ("Top 1").GetComponent<UILabel> ().text = mlineupEvent.Response.data.hit [0].hitH.ToString () + "%";
				Mid_BG.transform.FindChild ("Bot").FindChild ("Top 2").GetComponent<UILabel> ().text = mlineupEvent.Response.data.hit [0].hit2B.ToString () + "%";
				Mid_BG.transform.FindChild ("Bot").FindChild ("Top 3").GetComponent<UILabel> ().text = mlineupEvent.Response.data.hit [0].hitHr.ToString () + "%";
				Mid_BG.transform.FindChild ("Bot").FindChild ("Top 4").GetComponent<UILabel> ().text = mlineupEvent.Response.data.hit [0].hitBB.ToString () + "%";

				MidBar.transform.FindChild ("Gauge").FindChild ("Hits").GetComponent<UISprite> ().width = (int)Mathf.Round (340f * (float.Parse (mlineupEvent.Response.data.hit [0].hitAvg)));
		
				MidBar.transform.FindChild ("Gauge").FindChild ("L").GetComponent<UILabel> ().text = "안타 " + (float.Parse (mlineupEvent.Response.data.hit [0].hitAvg) * 100f).ToString () + "%";
				MidBar.transform.FindChild ("Gauge").FindChild ("R").GetComponent<UILabel> ().text = ((1 - float.Parse (mlineupEvent.Response.data.hit [0].hitAvg)) * 100f).ToString () + "% 아웃";

				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mlineupEvent.Response.data.hit [0].imagePath + mlineupEvent.Response.data.hit [0].imageName);
				StartCoroutine (GetImage (www, P_LPlayerImage));
				mlineupEvent = new GetLineupEvent (new EventDelegate (this, "setPitcher"));
				NetMgr.GetLineup (UserMgr.Schedule.extend [1].teamCode, mlineupEvent);
			}
		}
	}
	void setPitcher(){
		Lineup2 = mlineupEvent.Response.data.hit [0];
		P_RPlayersName.text = mlineupEvent.Response.data.pit [0].playerName+"#"+mlineupEvent.Response.data.pit[0].playerNumber;
		if (mlineupEvent.Response.data.pit [0].ERA ==null||mlineupEvent.Response.data.pit [0].ERA == "") {
			P_RBatting.text = "0.000";
		} else {
			P_RBatting.text = mlineupEvent.Response.data.pit [0].ERA;
		}
		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mlineupEvent.Response.data.pit[0].imagePath + mlineupEvent.Response.data.pit[0].imageName);
		StartCoroutine (GetImage (www, P_RPlayerImage));
	}
//	void OnApplicationPause(bool pause){
//	}
}