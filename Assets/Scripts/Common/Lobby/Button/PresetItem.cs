using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PresetItem : MonoBehaviour {
	GetScheduleEvent GSE;
	GetGamePresetLineupEvent GGPL;
	List<GamePresetLineupInfo> Lists ;
	List<int> List = new List<int>();

	int count= 0;

	void Start(){
//		StartCoroutine("RollingTitle");
	}

	IEnumerator RollingTitle(){
		
		//Debug.Log ("RollingTitle");
		
		//Debug.Log ("X : " + X);
		
//		while (true) {
//			transform.FindChild("NewTop").FindChild("TitlePanel").FindChild("Title1").localPosition+=new Vector3(-X/speed,0,0);
//			transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").localPosition+=new Vector3(-X/speed,0,0); 
//			transform.FindChild("NewTop").FindChild("TitlePanel").FindChild("Title2").localPosition+=new Vector3(-X/speed,0,0);
//			transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title2").localPosition+=new Vector3(-X/speed,0,0); 
//			if(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").localPosition.x <=-X){
//				transform.FindChild ("NewTop").FindChild("TitlePanel").FindChild ("Title1").localPosition = new Vector3 (X,0,0);
//				transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").localPosition=new Vector3(X,0,0); 
//			}
//			if(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title2").localPosition.x <=-X){
//				transform.FindChild ("NewTop").FindChild("TitlePanel").FindChild ("Title2").localPosition = new Vector3 (X,0,0);
//				transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title2").localPosition=new Vector3(X,0,0); 
//			}
//			yield return new WaitForSeconds(0.04f);
//			//Debug.Log(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").GetComponent<UILabel>().text);
//		}
		yield return null;
	}

	//해당 경기의 라인업이 없을시 라인업을 가저옴
	void GetLineupStart(){

		count = 0;
		if (GGPL.Response.data.Count != 0) {
			for (int i = 0; i<GGPL.Response.data.Count; i++) {
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + GGPL.Response.data [i].imagePath + GGPL.Response.data [i].imageName);
				StartCoroutine (GetImageStart (www, GGPL.Response.data [i]));
			
			}
		} else {
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
	public void Button(){
		//선택된 콘테스트의 참여루비
		UserMgr.UsingRuby = int.Parse(transform.FindChild ("Cost").FindChild ("value").GetComponent<UILabel> ().text.Replace ("[b]", ""));
		ScriptMainTop.OpenBettingCheck = true;
		//선택된 콘테스트의 gameseq
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
			//게임이 진행 중 일때
		

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
			//게임이 시작되지 않았을 때
	
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

		if (GGPL.Response.data.Count != 0) {
			for (int i = 0; i<GGPL.Response.data.Count; i++) {
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + GGPL.Response.data [i].imagePath + GGPL.Response.data [i].imageName);
				StartCoroutine (GetImage (www, GGPL.Response.data [i]));
			
			}

		} else {
			
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
			             transform.FindChild("Title").FindChild("Panel").FindChild("Label").GetComponent<UILabel>().text);

		
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
