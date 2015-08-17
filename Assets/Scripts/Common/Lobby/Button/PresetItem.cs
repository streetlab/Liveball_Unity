using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PresetItem : MonoBehaviour {
	GetScheduleEvent GSE;
	GetGamePresetLineupEvent GGPL;
	List<GamePresetLineupInfo> Lists ;
	List<int> List = new List<int>();

	int count= 0;
	void GetLineupStart(){

		count = 0;
		for(int i = 0; i<GGPL.Response.data.Count;i++){
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + GGPL.Response.data[i].imagePath + GGPL.Response.data[i].imageName);
			StartCoroutine(GetImageStart(www,GGPL.Response.data[i]));
			
		}

	}
	public void Button(){
		ScriptMainTop.OpenBettingCheck = true;
		UserMgr.GameSeq = int.Parse (transform.parent.FindChild ("BG").FindChild ("GameSeq").GetComponent<UILabel> ().text);
		transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>
			().Mode = "Update";
		UserMgr.CurrentContestSeq = int.Parse (transform.FindChild ("BG").FindChild ("contestSeq").GetComponent<UILabel> ().text);
		UserMgr.CurrentPresetSeq = int.Parse (transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text);

		Debug.Log ("GETGETGET");


		for (int i = 0; i<transform.FindChild("BG").FindChild("presetList").childCount; i++) {
			List.Add(int.Parse(transform.FindChild("BG").FindChild("presetList").GetChild(i).GetComponent<UILabel>().text));
		}

	
//		GSE = new GetScheduleEvent(new EventDelegate(this,"GET"));
//		NetMgr.GetScheduleToday (GSE);
//		UserMgr.ContestStatus = 1;
		if (UserMgr.ContestStatus == 2) {
		

			try{
				if(UserMgr.LineUpList[transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text]!=null){
					
					Lists =UserMgr.LineUpList[transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text];
					GSE = new GetScheduleEvent(new EventDelegate(this,"GET"));
					NetMgr.GetScheduleToday (GSE);
					
					
					
					
				}else{
					Debug.Log("else");
					GGPL = new GetGamePresetLineupEvent(new EventDelegate(this,"GetLineupStart"));
					NetMgr.GetGamePresetLineup (int.Parse(transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text),GGPL);
					
				}
			}catch{
				Debug.Log("Catch");
				GGPL = new GetGamePresetLineupEvent(new EventDelegate(this,"GetLineupStart"));
				NetMgr.GetGamePresetLineup (int.Parse(transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text),GGPL);
				
			}





		} else {

	
			try{
			if(UserMgr.LineUpList[transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text]!=null){

					Lists =UserMgr.LineUpList[transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text];
				PresetChange();



			
			}else{
					Debug.Log("else");
				GGPL = new GetGamePresetLineupEvent(new EventDelegate(this,"GetLineup"));
					NetMgr.GetGamePresetLineup (int.Parse(transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text),GGPL);

			}
			}catch{
				Debug.Log("Catch");
				GGPL = new GetGamePresetLineupEvent(new EventDelegate(this,"GetLineup"));
				NetMgr.GetGamePresetLineup (int.Parse(transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text),GGPL);

			}
	}

	}

	IEnumerator GetImage(WWW www, GamePresetLineupInfo index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		
		www.LoadImageIntoTexture (tmpTex);
		index.texture = tmpTex;
		count += 1;
		if (count == GGPL.Response.data.Count) {


			Debug.Log ("GetLineup");
			try{
				UserMgr.LineUpList.Add (transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text,GGPL.Response.data);
				
			}catch{
				Debug.Log("Same key");
			}
			
			Lists =UserMgr.LineUpList[transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text];
			PresetChange();

		}
		
	}

	IEnumerator GetImageStart(WWW www, GamePresetLineupInfo index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		
		www.LoadImageIntoTexture (tmpTex);
		index.texture = tmpTex;
		count += 1;
		if (count == GGPL.Response.data.Count) {

			try{
			UserMgr.LineUpList.Add (transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text,GGPL.Response.data);
			}
			catch{
				Debug.Log("Same");
			}
			GSE = new GetScheduleEvent(new EventDelegate(this,"GET"));
			NetMgr.GetScheduleToday (GSE);
			
		}
		
	}






	void GetLineup(){
		count = 0;
		for(int i = 0; i<GGPL.Response.data.Count;i++){
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + GGPL.Response.data[i].imagePath + GGPL.Response.data[i].imageName);
			StartCoroutine(GetImage(www,GGPL.Response.data[i]));
			
		}




	}

	void PresetChange(){

		Debug.Log ("PresetChange");


		string Ateam = UtilMgr.GetTeamCode (transform.parent.FindChild ("LTeam").FindChild("Label").GetComponent<UILabel>().text.Replace("[b]",""));
		string Hteam = UtilMgr.GetTeamCode (transform.parent.FindChild ("RTeam").FindChild("Label").GetComponent<UILabel>().text.Replace("[b]",""));
		Debug.Log ("List.Count : " + List.Count);
		
		for (int i = 0; i<Lists.Count; i++) {
			if(Lists[i].team == Ateam){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid")
					.FindChild("Scroll View").FindChild("Position").FindChild("Item " +Lists[i].lineup.ToString())
						.FindChild("L_name "+ Lists[i].lineup.ToString()).FindChild("Label")
						.GetComponent<UILabel>().text = Lists[i].player;
				
			}else if(Lists[i].team == Hteam){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid")
					.FindChild("Scroll View").FindChild("Position").FindChild("Item " +Lists[i].lineup.ToString())
						.FindChild("R_name "+ Lists[i].lineup.ToString()).FindChild("Label")
						.GetComponent<UILabel>().text = Lists[i].player;
			}
		}













		transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").gameObject.SetActive(true);
		transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>()
			.SetTeamName(transform.parent.FindChild("LTeam").FindChild("Label").GetComponent<UILabel>().text,
			             transform.parent.FindChild("RTeam").FindChild("Label").GetComponent<UILabel>().text,
			             transform.FindChild("Title").FindChild("Label").GetComponent<UILabel>().text);
		
		List<string> presetList = new List<string>();
		for(int i = 0;i<transform.FindChild("BG").FindChild("presetList").childCount;i++){
			if((i+1)<=9){
				presetList.Add(transform.FindChild("BG").FindChild("presetList").FindChild("a"+(i+1).ToString()).GetComponent<UILabel>().text);
			}else{
				presetList.Add(transform.FindChild("BG").FindChild("presetList").FindChild("h"+(i-9+1).ToString()).GetComponent<UILabel>().text);
			}
		}
		transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>()
			.SetList(presetList);
	}
	void GET(){

//		UserMgr.Schedule = GSE.Response.data[0];
//		UserMgr.Schedule.gameSeq = 1743;
//		UtilMgr.SelectTeam = "NC";
//		ScriptMainTop.LandingState = 2 ;
//		AutoFade.LoadLevel ("SceneMain 1", 0f, 1f);
		UserMgr.PresetChooseList = List;
		for (int i = 0; i<GSE.Response.data.Count; i++) {
			Debug.Log("GSE.Response.data [i].gameSeq : " +GSE.Response.data [i].gameSeq);
		if(int.Parse(transform.parent.FindChild("BG").FindChild("GameSeq").GetComponent<UILabel>().text)==
			   GSE.Response.data [i].gameSeq){
				UserMgr.Schedule = GSE.Response.data [i];
				UtilMgr.SelectTeam = UserMgr.Schedule.extend[0].teamName;
				ScriptMainTop.LandingState = 2 ;
				AutoFade.LoadLevel ("SceneMain 1", 0f, 1f);
				break;
			}
		}

		//AutoFade.LoadLevel ("SceneMain 1", 0f, 1f);
	}
}
