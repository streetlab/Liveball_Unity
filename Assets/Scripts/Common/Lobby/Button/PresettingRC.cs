using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PresettingRC : MonoBehaviour {
	string [] Value = {"1루","2,3루","홈런","땅볼","뜬공","삼진"};
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
			DialogueMgr.ShowDialogue ("정답지 등록", "zzzzz." , DialogueMgr.DIALOGUE_TYPE.YesNo ,DialogueHandler2);
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
			ResetPreset();
			transform.parent.parent.parent.parent.FindChild ("Nomal Contest").gameObject.SetActive (true);
			transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
			transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (true);
			transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
			                                                            <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			Debug.Log("Reset Preset");
	}
	
}

	void DialogueHandler2(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			presetaddevent = new PresetAddEvent (new EventDelegate (this, "Preset"));
			NetMgr.PresetAdd (UserMgr.CurrentContestSeq,GetList(),presetaddevent);

		}
		
	}
	void Preset(){
		ResetPreset();
		transform.parent.parent.parent.parent.FindChild ("Nomal Contest").gameObject.SetActive (true);
		transform.parent.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (true);
		transform.parent.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
		                                                                   <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		//add PresetCount

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
