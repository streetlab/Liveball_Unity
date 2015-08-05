using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BattleCommander : MonoBehaviour {
	public GameObject LandingScroll;
	public GameObject Pitcher;
	public GameObject Mid_BG;
	Color S_Abled = new Color(242f/255f,242f/255f,242f/255f);
	Color S_Disabled = new Color(217f/255f,217f/255f,217f/255f);
	Color L_Abled = new Color(0,0,0);
	Color L_Disabled = new Color(157f/255f,157f/255f,157f/255f);
	int a = 0;

	public static string TeamColor;
	List<UILabel> I_LabelList = new List<UILabel>();
	List<string> I_StringList = new List<string>();
	List<UISprite> I_SpriteList = new List<UISprite>();
	
	
	

	GameObject MidBar;
	
	GetSposTeamInfoEvent TeamMain;
	
	// Use this for initialization

	


	
	string Poldname = "";
	public void SetPitcher()
	{
		if (ScriptMainTop.DetailBoard != null) {
			if (ScriptMainTop.DetailBoard.player.Count > 0) {
				
				
				/*
				int num;
				
				num = 0;
				
				
				string playerInfo = ScriptMainTop.DetailBoard.player [num].playerName + "#" + ScriptMainTop.DetailBoard.player [num].playerNumber;

				string playerAVG = ScriptMainTop.DetailBoard.player [num].ERA;

				string strImage = ScriptMainTop.DetailBoard.player [num].imageName;
				if (Poldname != playerInfo) {
					if (ScriptMainTop.DetailBoard.player [num].imagePath != null 
					    && ScriptMainTop.DetailBoard.player [num].imagePath.Length > 0)
						strImage = ScriptMainTop.DetailBoard.player [num].imagePath
							+ ScriptMainTop.DetailBoard.player [num].imageName;
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
					UITexture T = transform.FindChild("GameInfo").FindChild("Pitcher").FindChild("BG").FindChild("Photo")
						.FindChild("PhotoPanel").FindChild("Photo").GetComponent<UITexture>();
					StartCoroutine (GetImage (www, T));
				}
				Poldname = playerInfo;
				*/
			}
		}
	}
	
	
	public void SetHitter(QuizListInfo nextPlayer)
	{
		//Debug.Log ("ScriptMainTop.LandingState : " + ScriptMainTop.LandingState);
		//Debug.Log ("nextPlayer : " + nextPlayer.Count);
		
		if((ScriptMainTop.LandingState==2||ScriptMainTop.LandingState==3)&&nextPlayer!=null){
//			N = nextPlayer.nextPlayer;
			//Now = nextPlayer.nowPlayer;
			Debug.Log("UtilMgr.gameround : : " + UtilMgr.gameround);
			//			if(UtilMgr.gameround%2==0){
			//				
			//				save1=nextPlayer.nextPlayer;
			//				Debug.Log("save1 . playerName : " + save1[0].playerName);
			//			}else{
			//				
			//				save2=nextPlayer.nextPlayer;
			//				Debug.Log("save2 . playerName : " + save2[0].playerName);
			//			}
			
			
			
			//N[i].
		//	strImage = "";
			Debug.Log("SetHitter0");
		//	if (N!=null) {
				//            P_LPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Name").GetComponent<UILabel> ();
				//            P_LBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Batting").GetComponent<UILabel> ();
				//            P_LPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
				//  
			//	Debug.Log("N.Count : " + N.Count);
//				for(int i = 0; i <N.Count;i++ ){
//					if(N[i].type == 1){
//						string playerInfo = N[i].playerName + "#" + N[i].playerNumber;
//						P_LPlayersName.text = playerInfo;
//						Debug.Log("playerInfo : " + playerInfo);
//						string playerAVG = N[i].hitAvg;
//						P_LBatting.text = playerAVG;
//						P_T.text = "타율";
//						P_T.transform.parent.FindChild("M1").FindChild ("S").GetComponent<UISprite> ().color = S_Abled;
//						P_T.transform.parent.FindChild("M2").FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
//						//G_M3.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
//						
//						P_T.transform.parent.FindChild("M1").FindChild ("L").GetComponent<UILabel> ().color = L_Abled;
//						P_T.transform.parent.FindChild("M2").FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
//						strImage = N[i].imageName;
//						P_B.text = N[i].hitAvg;
//						P_B1.text = N[i].hitH.ToString()+"%";
//						P_B2.text = N[i].hit2B.ToString()+"%";
//						P_B3.text = N[i].hitHr.ToString()+"%";
//						P_B4.text = N[i].hitBB.ToString()+"%";
//						P_VS.text = "VS 시즌타율 " + N[i].hitAvg;
//						
//						Debug.Log("nextPlayer[i].hitAvg : " + N[i].hitAvg);
//						if(N[i].hitAvg!=""&&N[i].hitAvg!=null&&N[i].hitAvg!="-"){
//							MidBar.transform.FindChild("Gauge").FindChild("Hits").GetComponent<UISprite>().width =  (int)Mathf.Round(340f*(float.Parse(N[i].hitAvg)));
//							
//							MidBar.transform.FindChild("Gauge").FindChild("L").GetComponent<UILabel>().text = "안타 " + (float.Parse(N[i].hitAvg)*100f).ToString()+"%";
//							MidBar.transform.FindChild("Gauge").FindChild("R").GetComponent<UILabel>().text = ((1-float.Parse(N[i].hitAvg))*100f).ToString()+"% 아웃";
//						}
//						if(Holdname!=strImage){
//							if (N[i].imagePath != null && N[i].imagePath.Length > 0){
//								strImage =N[i] .imagePath + N[i].imageName;
//								WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
//								Debug.Log ("url : " + Constants.IMAGE_SERVER_HOST + strImage);
//								StartCoroutine (GetImage (www, P_LPlayerImage));
//							}
//							
//						}
//					}
			//	}
				//Holdname = strImage;
			}
			
			
		}
		

	


	


IEnumerator GetImage(WWW www, UITexture texture)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		texture.mainTexture = tmpTex;
	}
	
	


}

