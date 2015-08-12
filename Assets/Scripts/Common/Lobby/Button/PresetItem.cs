using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PresetItem : MonoBehaviour {
	GetScheduleEvent GSE;
	List<int> List = new List<int>();
	public void Button(){
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
		if (UserMgr.ContestStatus == 2) {
		//	AutoFade.LoadLevel ("SceneMain 1", 0f, 1f);
			GSE = new GetScheduleEvent(new EventDelegate(this,"GET"));
			NetMgr.GetScheduleToday (GSE);
		} else {
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").gameObject.SetActive(true);
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>()
				.SetTeamName(transform.parent.FindChild("LTeam").FindChild("Label").GetComponent<UILabel>().text,
				             transform.parent.FindChild("RTeam").FindChild("Label").GetComponent<UILabel>().text,
				             transform.FindChild("Title").FindChild("Label").GetComponent<UILabel>().text);

			List<string> List = new List<string>();
			for(int i = 0;i<transform.FindChild("BG").FindChild("presetList").childCount;i++){
				if((i+1)<=9){
				List.Add(transform.FindChild("BG").FindChild("presetList").FindChild("a"+(i+1).ToString()).GetComponent<UILabel>().text);
				}else{
					List.Add(transform.FindChild("BG").FindChild("presetList").FindChild("h"+(i-9+1).ToString()).GetComponent<UILabel>().text);
				}
			}
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>()
				.SetList(List);
		}
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
