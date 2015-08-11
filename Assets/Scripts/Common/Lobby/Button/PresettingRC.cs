using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PresettingRC : MonoBehaviour {
	string [] Value = {"1루타","2,3루타","홈런","땅볼","뜬공","삼진"};
	PresetAddEvent presetaddevent;
	public void Button(){
		if (this.name == "Close") {
			DialogueMgr.ShowDialogue ("등록 취소", "기존 등록된 내용을 잃을 수 있습니다." , DialogueMgr.DIALOGUE_TYPE.YesNo ,"등록 취소","계속 등록","", DialogueHandler);
		}else if(this.name == "Menu 0"){
			ResetPreset();
		}else if(this.name == "Menu 1"){
			RandomPreset();
		}else if(this.name == "Menu 2"){

		} else {
			DialogueMgr.ShowDialogue ("정답지 등록", "참가비 : " + 
			                          transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost+
			                          "\n총 상금 : " +
			                          transform.parent.parent.parent.GetComponent<PreSettingCommander>().money, DialogueMgr.DIALOGUE_TYPE.YesNo ,DialogueHandler2);
		}
	}



	List<int> GetList(){
		List<int> ChoseList = new List<int> ();
		GameObject 
			G= 
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
			if(G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				ChoseList.Add(
					getint(	G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				       FindChild("use").FindChild("Label").GetComponent<UILabel>().text)
					);
			}else{
				ChoseList.Add(0);
			}
			
		}
		for (int i = 0; i<G.transform.childCount; i++) {
			if(G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				ChoseList.Add(
					getint(	G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				       FindChild("use").FindChild("Label").GetComponent<UILabel>().text)
					);
			}else{
				ChoseList.Add(0);
			}
			
		}
		return ChoseList;
	}
	int getint(string S){
		int a = 0;
		for (int i = 0; i<Value.Length; i++) {
			if(Value[i] == S){
				a =i+1;
				break;
			}
		}
		return a;
	}

	void RandomPreset(){
		GameObject 
		G= 
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
			int num = Random.Range(0,6);
		
		//	Debug.Log(num);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				FindChild("use").gameObject.SetActive(true);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				FindChild("use").gameObject.SetActive(true);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				FindChild("use").FindChild("Label").GetComponent<UILabel>().text = Value[num];
			num = Random.Range(0,5);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				FindChild("use").FindChild("Label").GetComponent<UILabel>().text = Value[num];
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				FindChild("non").gameObject.SetActive(false);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				FindChild("non").gameObject.SetActive(false);
		}
	}
	void ResetPreset(){
		GameObject G = 
		transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
			.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
		
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				FindChild("use").gameObject.SetActive(false);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				FindChild("use").gameObject.SetActive(false);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				FindChild("non").gameObject.SetActive(true);
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				FindChild("non").gameObject.SetActive(true);
		}
	}


void DialogueHandler(DialogueMgr.BTNS btn){
	if (btn == DialogueMgr.BTNS.Btn1) {

			if(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode != "Update"){
			ResetPreset();
			transform.parent.parent.parent.parent.FindChild ("Nomal Contest").gameObject.SetActive (true);
			transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
			transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (true);
			transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
			                                                            <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			Debug.Log("Reset Preset");
			}else{
				ResetPreset();
				transform.parent.parent.parent.parent.FindChild ("PreSet Contest").gameObject.SetActive (true);
				transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
				transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (false);
				transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
				                                                                   <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;

			}
			transform.root.FindChild ("Scroll").FindChild("Main").FindChild ("Gift").gameObject.SetActive (true);
		}
	
}

	PresetUpdateEvent presetupdate;
	void DialogueHandler2(DialogueMgr.BTNS btn){
		Debug.Log (UserMgr.UserInfo.userRuby);
		Debug.Log (transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost);
		Debug.Log (btn);
		if (btn == DialogueMgr.BTNS.Btn1) {
			if(int.Parse(UserMgr.UserInfo.userRuby)>=int.Parse(transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost))
			{
				UserMgr.UserInfo.userRuby = (int.Parse(UserMgr.UserInfo.userRuby) - int.Parse(transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost)).ToString();
			Debug.Log(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode);
			if(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode == "Update"){
				Debug.Log(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode);
				presetupdate = new PresetUpdateEvent (new EventDelegate (this, "PresetUpdate"));
				NetMgr.PresetUpdate (UserMgr.CurrentContestSeq,UserMgr.CurrentPresetSeq,GetList(),presetupdate);
			}else{

			presetaddevent = new PresetAddEvent (new EventDelegate (this, "Preset"));
			NetMgr.PresetAdd (UserMgr.CurrentContestSeq,GetList(),presetaddevent);
			}

			}else{
				DialogueMgr.ShowDialogue ("등록 취소", "루비가 부족합니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);
			}
		}
		
	}
	void Preset(){
		ResetPreset();
		transform.parent.parent.parent.parent.FindChild ("Nomal Contest").gameObject.SetActive (true);
		transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (true);
		transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
		                                                                   <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;

		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Preset").FindChild ("Num").GetComponent<UILabel
			> ().text = (int.Parse (

		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Preset").FindChild ("Num").GetComponent<UILabel
				> ().text) + 1).ToString ();
		//add PresetCount

	}
	PresetListEvent presetListEvent;
	void PresetUpdate(){
		//Debug.Log ("PresetUpdate");


		presetListEvent = new PresetListEvent(new EventDelegate(this, "GetPresetList"));
		NetMgr.GetPresetList(presetListEvent);

	}
	
	void GetPresetList(){
		List<PresetListInfo> presetlist;
		presetlist = presetListEvent.Response.data;
		UserMgr.ContestStatus = presetlist [0].contestStatus;
		if (UserMgr.ContestStatus == 2) {
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").FindChild("Label").GetComponent<UILabel>().text = "라이브";
		}
		//Load PresetList
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild("PreSet Contest").GetComponent<PresetContestCommander> ().CreatItem (presetlist);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Nomal Contest").gameObject.SetActive(false);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("PreSet Contest").gameObject.SetActive(true);

		ResetPreset();
		transform.parent.parent.parent.parent.FindChild ("PreSet Contest").gameObject.SetActive (true);
		transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
		                                                                   <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;

	}

	bool CheckPreset(){
		bool b = true;
		GameObject 
			G= 
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
			if(!
			   G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				b = false;
				break;
			}
			if(!
			   G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				b = false;
				break;
			}
			
		}
		return b;
	}
}
