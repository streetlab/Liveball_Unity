using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Presetplaying : MonoBehaviour {
	PresetUpdateEvent presetupdate;
	string [] Value = {"1루타","2,3루타","홈런","땅볼","뜬공","삼진"};
	public void Button(){

		presetupdate = new PresetUpdateEvent (new EventDelegate (this, "PresetUpdate"));
		NetMgr.PresetUpdate (UserMgr.CurrentContestSeq,UserMgr.CurrentPresetSeq,GetList(),presetupdate);

	}

	void PresetUpdate(){
//		transform.root.FindChild("Scroll").FindChild("ContestIn").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
//			.FindChild("Position").gameObject;

		Debug.Log ("PresetUpdate Complete");
		UserMgr.PresetChooseList = GetList();

	}

	List<int> GetList(){
		List<int> ChoseList = new List<int> ();
		GameObject 
			G= 
				transform.root.FindChild("Scroll").FindChild("ContestIn").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
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

	void SetName(){
		transform.FindChild ("Mid").FindChild ("BG").FindChild ("Team 1").FindChild ("Label").GetComponent<UILabel> ().
			text = 
				transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild ("GameInfo")
				.FindChild ("Mid").FindChild ("Info").FindChild ("L_TeamName").FindChild ("Label").GetComponent<UILabel> ().text;

		transform.FindChild ("Mid").FindChild ("BG").FindChild ("Team 2").FindChild ("Label").GetComponent<UILabel> ().
			text = 
				transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild ("GameInfo")
				.FindChild ("Mid").FindChild ("Info").FindChild ("R_TeamName").FindChild ("Label").GetComponent<UILabel> ().text;


		string Ateam = UtilMgr.GetTeamCode (transform.FindChild ("Mid").FindChild ("BG").FindChild ("Team 1").FindChild ("Label").GetComponent<UILabel> ().
		                                    text.Replace("[b]",""));
		string Hteam = UtilMgr.GetTeamCode (transform.FindChild ("Mid").FindChild ("BG").FindChild ("Team 2").FindChild ("Label").GetComponent<UILabel> ().
		                                    text.Replace("[b]",""));

		List<GamePresetLineupInfo> List= UserMgr.LineUpList[UserMgr.Schedule.gameSeq.ToString()];
		for (int i = 0; i<List.Count; i++) {
			if(List[i].team == Ateam){
				transform.root.FindChild("Scroll").FindChild("ContestIn").FindChild("PreSetting").FindChild("Mid")
					.FindChild("Scroll View").FindChild("Position").FindChild("Item " +List[i].lineup.ToString())
						.FindChild("L_name "+ List[i].lineup.ToString()).FindChild("Label")
						.GetComponent<UILabel>().text = List[i].player;
				
			}else if(List[i].team == Hteam){
				transform.root.FindChild("Scroll").FindChild("ContestIn").FindChild("PreSetting").FindChild("Mid")
					.FindChild("Scroll View").FindChild("Position").FindChild("Item " +List[i].lineup.ToString())
						.FindChild("R_name "+ List[i].lineup.ToString()).FindChild("Label")
						.GetComponent<UILabel>().text = List[i].player;
			}
		}

	}



	public void SetList(List<int> List){
		SetName ();
		Debug.Log (List.Count);
		
		GameObject 
			G= 
				transform.root.FindChild("Scroll").FindChild("ContestIn").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
			
			if(List[i] !=0&&List[i] !=null){
				Debug.Log("List[i] : "  + List[i]);
				Debug.Log("i : "  + i);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
					FindChild("use").gameObject.SetActive(true);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
					FindChild("non").gameObject.SetActive(false);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
					FindChild("use").FindChild("Label").GetComponent<UILabel>().text = Value[List[i]-1];
			}
			
			
			
		}
		for (int i = 0; i<G.transform.childCount; i++) {
			if(List[i+9] !=0&&List[i+9] !=null){
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
					FindChild("use").gameObject.SetActive(true);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
					FindChild("non").gameObject.SetActive(false);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
					FindChild("use").FindChild("Label").GetComponent<UILabel>().text = Value[List[i+9]-1];
			}
			
			
		}
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
}
