using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PresetItem : MonoBehaviour {
	GetScheduleEvent GSE;
	public void Button(){
		transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>
			().Mode = "Update";
		UserMgr.CurrentContestSeq = int.Parse (transform.FindChild ("BG").FindChild ("contestSeq").GetComponent<UILabel> ().text);
		UserMgr.CurrentPresetSeq = int.Parse (transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text);

		Debug.Log ("GETGETGET");

		GSE = new GetScheduleEvent(new EventDelegate(this,"GET"));
		NetMgr.GetScheduleToday (GSE);
	

//		if (UserMgr.ContestStatus == 2) {
//			AutoFade.LoadLevel ("SceneMain 1", 0f, 1f);
//		} else {
//			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").gameObject.SetActive(true);
//			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>()
//				.SetTeamName(transform.parent.FindChild("LTeam").FindChild("Label").GetComponent<UILabel>().text,
//				             transform.parent.FindChild("RTeam").FindChild("Label").GetComponent<UILabel>().text,
//				             transform.FindChild("Title").FindChild("Label").GetComponent<UILabel>().text);
//
//			List<string> List = new List<string>();
//			for(int i = 0;i<transform.FindChild("BG").FindChild("presetList").childCount;i++){
//				if((i+1)<=9){
//				List.Add(transform.FindChild("BG").FindChild("presetList").FindChild("a"+(i+1).ToString()).GetComponent<UILabel>().text);
//				}else{
//					List.Add(transform.FindChild("BG").FindChild("presetList").FindChild("h"+(i-9+1).ToString()).GetComponent<UILabel>().text);
//				}
//			}
//			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").GetComponent<PreSettingCommander>()
//				.SetList(List);
//		}
	}
	void GET(){
		UserMgr.Schedule = GSE.Response.data [0];
		AutoFade.LoadLevel ("SceneMain 1", 0f, 1f);
	}
}
