using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PresettingRC : MonoBehaviour {
	string [] Value = {"1루타","2,3루타","홈런","땅볼","뜬공","삼진"};
	PresetAddEvent presetaddevent;
	ContestDataEvent CDE;
	public void Button(){
		if (this.name == "Close") {
			DialogueMgr.ShowDialogue ("등록 취소", "기존 등록된 내용을 잃을 수 있습니다." , DialogueMgr.DIALOGUE_TYPE.YesNo ,"등록 취소","계속 등록","", DialogueHandler);
		}else if(this.name == "Menu 0"){
			ResetPreset();
		}else if(this.name == "Menu 1"){
			RandomPreset();
		}else if(this.name == "Menu 2"){

		} else {


			if(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode == "Update"){
			//	Debug.Log(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode);

				DialogueMgr.ShowDialogue ("정답지 등록", "정답지 등록." , DialogueMgr.DIALOGUE_TYPE.Alert ,"","","등록",register);
			


			}else{
			CDE = new ContestDataEvent (new EventDelegate (this, "MaxCheck"));
			NetMgr.GetContestData (CDE);
			}


//			DialogueMgr.ShowDialogue ("정답지 등록", "참가비 : " + 
//			                          transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost+
//			                          "\n총 상금 : " +
//			                          transform.parent.parent.parent.GetComponent<PreSettingCommander>().money, DialogueMgr.DIALOGUE_TYPE.YesNo ,DialogueHandler2);
	
		
		
		
		}
	}
	void register(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Cancel) {
			presetupdate = new PresetUpdateEvent (new EventDelegate (this, "PresetUpdate"));
			NetMgr.PresetUpdate (UserMgr.CurrentContestSeq, UserMgr.CurrentPresetSeq, GetList (), presetupdate);
		}
	}
	enum CheckStarus{
		OK,
		MaxPreset,
		MaxMultiEntry,

	}
	void MaxCheck(){
		CheckStarus status = CheckStarus.OK;
		for (int i = 0; i<CDE.Response.data.Count; i++) {
			if(CDE.Response.data[i].contestSeq == UserMgr.CurrentContestSeq){
				if(UserMgr.CurrentContestTotalEntry ==CDE.Response.data[i].totalPreset){
					Debug.Log("CDE.Response.data[i].totalEntry : " + CDE.Response.data[i].totalPreset);
					status = CheckStarus.MaxPreset;

				}

				int presetCount = 0;;
			
				for(int a = 0; a<UserMgr.PresetList.Count;a++){
					if(UserMgr.PresetList[a].contestSeq == CDE.Response.data[i].contestSeq){
						Debug.Log("UserMgr.PresetList[a].contestSeq : " + UserMgr.PresetList[a].contestSeq);
						presetCount += 1;

					}
				}





				if(UserMgr.CurrentContestMultiEntry==presetCount){
					Debug.Log(UserMgr.CurrentContestMultiEntry);
					if(status != CheckStarus.MaxPreset){
						status = CheckStarus.MaxMultiEntry;
					}
				}


				break;
			}
		}


		if (status == CheckStarus.OK) {
		
			transform.root.FindChild("Camera").FindChild("JoinPopUp").gameObject.SetActive(true);
			transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box1")
				.FindChild("Value").GetComponent<UILabel>().text = "루비 " + transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost;
			transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box2")
				.FindChild("Value").GetComponent<UILabel>().text = transform.parent.parent.parent.GetComponent<PreSettingCommander>().money+" "+
					transform.parent.parent.parent.GetComponent<PreSettingCommander>().item;

		}else if(status == CheckStarus.MaxPreset){
			DialogueMgr.ShowDialogue ("등록취소", "이 컨테스트에 등록가능한 개수를 초과하였습니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);

		}else if(status == CheckStarus.MaxMultiEntry){
			DialogueMgr.ShowDialogue ("멀티 엔트리 등록취소", "멀티 엔트리 최대 가능 개수 :"+CheckStarus.MaxMultiEntry.ToString()+" 개\n이 컨테스트에 등록가능한 멀티엔트리 개수를 초과하였습니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);

		}
	}

	List<int> GetList(){
		List<int> ChoseList = new List<int> ();
		GameObject 
			G= 
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
			Debug.Log("L_name + (i+1).ToString() : " + "L_name " + (i+1).ToString());
			if(G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				ChoseList.Add(
					getint(	G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				       FindChild("use").FindChild("Label").GetComponent<UILabel>().text)
					);
				Debug.Log("Add Int : " + getint(	G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
				                                FindChild("use").FindChild("Label").GetComponent<UILabel>().text).ToString());
			}else{
				ChoseList.Add(0);
				Debug.Log("0" );
			}
			
		}
		for (int i = 0; i<G.transform.childCount; i++) {
			Debug.Log("R_name + (i+1).ToString() : " + "R_name " + (i+1).ToString());
			if(G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				ChoseList.Add(
					getint(	G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				       FindChild("use").FindChild("Label").GetComponent<UILabel>().text)
					);
				Debug.Log("Add Int : " +getint(	G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
				                               FindChild("use").FindChild("Label").GetComponent<UILabel>().text).ToString() );
			}else{
				ChoseList.Add(0);
				Debug.Log("0" );
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

	public void JoinButton(){
		
		if(int.Parse(UserMgr.UserInfo.userRuby)>=int.Parse(transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost))
		{
			UserMgr.UserInfo.userRuby = (int.Parse(UserMgr.UserInfo.userRuby) - int.Parse(transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost)).ToString();
			//Debug.Log(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode);

				
				presetaddevent = new PresetAddEvent (new EventDelegate (this, "Preset"));
				NetMgr.PresetAdd (UserMgr.CurrentContestSeq,GetList(),presetaddevent);

			
		}else{
			DialogueMgr.ShowDialogue ("등록 취소", "루비가 부족합니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);
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
			
			Debug.Log(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode);
			if(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode == "Update"){
				Debug.Log(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode);
				presetupdate = new PresetUpdateEvent (new EventDelegate (this, "PresetUpdate"));
				NetMgr.PresetUpdate (UserMgr.CurrentContestSeq,UserMgr.CurrentPresetSeq,GetList(),presetupdate);
			}else{
					UserMgr.UserInfo.userRuby = (int.Parse(UserMgr.UserInfo.userRuby) - int.Parse(transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost)).ToString();
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
		transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().Reset ();
//		presetListEvent = new PresetListEvent(new EventDelegate(this, "GetPresetList"));
//				NetMgr.GetPresetList(presetListEvent);
		PresetUpdate ();
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Preset").FindChild ("Num").GetComponent<UILabel> ().text = 
			(int.Parse (transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Preset").FindChild ("Num").GetComponent<UILabel> ().text) + 1).ToString ();
		//add PresetCount

	}
	PresetListEvent presetListEvent;
	void PresetUpdate(){
		//Debug.Log ("PresetUpdate");


		presetListEvent = new PresetListEvent(new EventDelegate(this, "GetPresetList"));
		NetMgr.GetPresetList(presetListEvent);

	}
	
	void GetPresetList(){
		UserMgr.PresetList = presetListEvent.Response.data;
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Preset").FindChild ("Num").GetComponent<UILabel> ().text =
			presetListEvent.Response.data.Count.ToString ();

		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("PreSetting").gameObject.SetActive (false);

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
