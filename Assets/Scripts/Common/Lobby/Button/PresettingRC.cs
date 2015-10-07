﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PresettingRC : MonoBehaviour {
	string [] Value = {"1루타","2,3루타","홈런","땅볼","뜬공","삼진"};
	PresetAddEvent presetaddevent;
	ContestDataEvent CDE;
	RemoveContestPresetEvent RCPE;
	bool mNeedMove = false;

	//프리셋 등록,삭제
	public void Button(){
		if (this.name == "Close") {
			DialogueMgr.ShowDialogue ("등록 취소", "기존 등록된 내용을 잃을 수 있습니다." , DialogueMgr.DIALOGUE_TYPE.YesNo ,"등록 취소","계속 등록","", DialogueHandler);
		}else if(this.name == "Menu 0"){
			ResetPreset();
		}else if(this.name == "Menu 1"){
			RandomPreset();
		}else if(this.name == "Menu 2"){

		}else if(this.name == "Remove"){
			DialogueMgr.ShowDialogue ("프리셋 삭제", "기존 등록된 프리셋이 삭제됩니다." , DialogueMgr.DIALOGUE_TYPE.YesNo ,"프리셋 삭제","삭제 취소","", RemovePreset);

		} else {
			if(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode == "Update"){
			//	Debug.Log(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode);
				DialogueMgr.ShowDialogue ("예측 완료", "사전 경기 예측 완료!" , DialogueMgr.DIALOGUE_TYPE.Alert ,"","","등록",register);			
				mNeedMove = true;
			}else{
				CDE = new ContestDataEvent (new EventDelegate (this, "MaxCheck"));
				NetMgr.GetContestData (CDE);
			}
		}
	}

	void Remove(){
		if (UserMgr.UsingRuby > 0) {
			UserMgr.UserInfo.userRuby = (int.Parse(UserMgr.UserInfo.userRuby)+UserMgr.UsingRuby).ToString();
			DialogueMgr.ShowDialogue ("프리셋 삭제", UserMgr.UsingRuby.ToString() + " 루비가 환불 되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert,null);
		} else {
			DialogueMgr.ShowDialogue ("프리셋 삭제", "해당 프리셋이 삭제되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}
		transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("PreSet Contest").GetComponent<PresetContestCommander> ().ReCreat ();
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


		//정답지 등록 여부체크
		if (status == CheckStarus.OK) {
			bool SettingCheck = true;
			GameObject Position = transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
			for(int i = 0; i<(Position.transform.childCount);i++){
				if(Position.transform.FindChild("Item " + (i+1).ToString())!=null){
					if(
						Position.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name "+(i+1).ToString()).FindChild("L_name "+(i+1).ToString()+"_pre")
						.FindChild("non").gameObject.activeSelf){
						SettingCheck = false;
					}

					if(
						Position.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name "+(i+1).ToString()).FindChild("R_name "+(i+1).ToString()+"_pre")
						.FindChild("non").gameObject.activeSelf){
						SettingCheck = false;
					}
				}
				if(!SettingCheck){
					break;
				}
			}

			if(SettingCheck){

			transform.root.FindChild("Camera").FindChild("JoinPopUp").gameObject.SetActive(true);
			transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box1")
				.FindChild("Value").GetComponent<UILabel>().text = "루비 " + transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost;

			if("[b]마일리지"!=transform.parent.parent.parent.GetComponent<PreSettingCommander>().item){
			transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box2")
				.FindChild("Value").GetComponent<UILabel>().text = transform.parent.parent.parent.GetComponent<PreSettingCommander>().money+" "+
					transform.parent.parent.parent.GetComponent<PreSettingCommander>().item;
			}else{
				transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box2")
					.FindChild("Value").GetComponent<UILabel>().text = transform.parent.parent.parent.GetComponent<PreSettingCommander>().money;
			}

			}else{

				DialogueMgr.ShowDialogue ("등록취소", "등록된 정답지가 현재 없습니다.\n이대로 컨테스트를 진행하시겠습니까?\n(컨테스트 참여시 루비가 "+UserMgr.UsingRuby.ToString()+"개 소모됩니다)" , DialogueMgr.DIALOGUE_TYPE.YesNo ,Nonpreset);


			}

		}else if(status == CheckStarus.MaxPreset){
			DialogueMgr.ShowDialogue ("등록취소", "이 컨테스트에 등록가능한 개수를 초과하였습니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);

		}else if(status == CheckStarus.MaxMultiEntry){
			DialogueMgr.ShowDialogue ("멀티 엔트리 등록취소", "멀티 엔트리 최대 가능 개수 :"+CheckStarus.MaxMultiEntry.ToString()+" 개\n이 컨테스트에 등록가능한 멀티엔트리 개수를 초과하였습니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);

		}
	}
	void Nonpreset(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			transform.root.FindChild("Camera").FindChild("JoinPopUp").gameObject.SetActive(true);
			transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box1")
				.FindChild("Value").GetComponent<UILabel>().text = "루비 " + transform.parent.parent.parent.GetComponent<PreSettingCommander>().cost;
			
			if("[b]마일리지"!=transform.parent.parent.parent.GetComponent<PreSettingCommander>().item){
				transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box2")
					.FindChild("Value").GetComponent<UILabel>().text = transform.parent.parent.parent.GetComponent<PreSettingCommander>().money+" "+
						transform.parent.parent.parent.GetComponent<PreSettingCommander>().item;
			}else{
				transform.root.FindChild("Camera").FindChild("JoinPopUp").FindChild("Pop").FindChild("Mid").FindChild("Box2")
					.FindChild("Value").GetComponent<UILabel>().text = transform.parent.parent.parent.GetComponent<PreSettingCommander>().money;
			}
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
	//프리셋 랜덤 설정 기능
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
	//프리셋 삭제
	void RemovePreset(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			
			RCPE = new RemoveContestPresetEvent (new EventDelegate (this, "Remove"));
			NetMgr.RemoveContestPreset(UserMgr.CurrentPresetSeq,RCPE);
		}
		
	}

	void DialogueHandler(DialogueMgr.BTNS btn){
//		transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").GetComponent<TopMenu>().Button();
		if (btn == DialogueMgr.BTNS.Btn1) {
			if(transform.parent.parent.parent.GetComponent<PreSettingCommander>().Mode != "Update"){
				ResetPreset();
				transform.parent.parent.parent.parent.FindChild ("Nomal Contest").gameObject.SetActive (true);
				transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
				transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (true);
				transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
				                                                            <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				Debug.Log("Reset Preset");
				transform.root.FindChild ("Scroll").FindChild("Main").FindChild ("Gift").gameObject.SetActive (true);
			}else{
				ResetPreset();
				transform.parent.parent.parent.parent.FindChild ("PreSet Contest").gameObject.SetActive (true);
				transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
				transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (false);
				transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
				                                                                   <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;

			}
		}
	}
	//프리셋 등록
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
	public void PresetUpdate(){
		//Debug.Log ("PresetUpdate");
		presetListEvent = new PresetListEvent(new EventDelegate(this, "GetPresetList"));
		NetMgr.GetPresetList(presetListEvent);

	}
	
	void GetPresetList(){
		UserMgr.PresetList = presetListEvent.Response.data;
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Preset").FindChild ("Num").GetComponent<UILabel> ().text =
			presetListEvent.Response.data.Count.ToString ();

		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("PreSetting").gameObject.SetActive (false);

		if(mNeedMove){
//			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").GetComponent<TopMenu>().Button();
			mNeedMove = false;
		}
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
