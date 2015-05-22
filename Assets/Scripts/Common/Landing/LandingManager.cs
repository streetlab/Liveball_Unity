using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandingManager : MonoBehaviour {
	UILabel I_Gold,I_TeamName,I_RankScore,I_TodayDealWith,I_TodayInfo,I_Memo,I_PlayersName,I_Batting;
    UITexture I_PlayersImage;
	UISprite LineTop,GameInfo,OkStrategy,Item,Community,I_BigLogo,I_TeamImage;
	string Is_Gold,Is_TeamName,Is_RankScore,Is_TodayDealWith,Is_TodayInfo,Is_Memo,Is_PlayersName,Is_Batting;
	string TeamColor;
	List<UILabel> I_LabelList = new List<UILabel>();
	List<string> I_StringList = new List<string>();
	List<UISprite> I_SpriteList = new List<UISprite>();


	UILabel V_LTeamName,V_RTeamName,V_BroadCasting,V_Location,V_time,V_LBatting,V_LPlayerName,V_RBatting,V_RPlayerName,V_Memo;
	UISprite V_LeftTeamImage,V_RightTeamImage;
	UITexture V_LPlayerImage,V_RPlayerImage;

	UILabel P_LTeamName,P_RTeamName,P_GameState,P_Score,P_LPlayersName,P_LBatting,P_RPlayersName,P_RBatting,P_Hit,P_Out;
	UISprite P_LeftTeamImage,P_RightTeamImage,One,Two,Three;
	UITexture P_LPlayerImage,P_RPlayerImage;
	// Use this for initialization
	void Start () {
		PathSettings ();
		GetData ();
		InPutData ();
		SetTeamColor ();
	}

	
	// Update is called once per frame
	void PathSettings () {

		//Info
		GameObject temp = transform.FindChild ("Scroll View").FindChild ("Info").FindChild ("BG_W").gameObject;
		I_Gold = transform.parent.FindChild ("Top").FindChild ("Panel").FindChild ("TopGoldbar").FindChild ("Label").GetComponent<UILabel> ();
		I_LabelList.Add (I_Gold);
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
		UILabel P_LTeamName,P_RTeamName,P_GameState,P_Score,P_LPlayersName,P_LBatting,P_RPlayersName,P_RBatting,P_Hit,P_Out;
		UISprite P_LeftTeamImage,P_RightTeamImage,One,Two,Three;
		UITexture P_LPlayerImage,P_RPlayerImage;

		P_LTeamName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild("LeftTeam").FindChild ("Label").GetComponent<UILabel> ();
		P_RTeamName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild("RightTeam").FindChild ("Label").GetComponent<UILabel> ();
		P_GameState = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("Ground").FindChild ("Base").FindChild ("Label").GetComponent<UILabel> ();
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
		TeamColor = "005bac";
		Is_Gold = UtilMgr.AddsThousandsSeparator ("123456789");
		I_StringList.Add (Is_Gold);
		Is_TeamName = "";
		I_StringList.Add (Is_TeamName);
		Is_RankScore = "";
		I_StringList.Add (Is_RankScore);
		Is_TodayDealWith = "";
		I_StringList.Add (Is_TodayDealWith);
		Is_TodayInfo = "";
		I_StringList.Add (Is_TodayInfo);
		Is_Memo = "";
		I_StringList.Add (Is_Memo);
		Is_PlayersName = "";
		I_StringList.Add (Is_PlayersName);
		Is_Batting = "";
		I_StringList.Add (Is_Batting);
	}
	void InPutData(){
		for (int i = 0; i < I_LabelList.Count; i++) {
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
}
