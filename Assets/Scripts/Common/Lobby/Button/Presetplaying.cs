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




	public void SetList(List<string> List){
		Debug.Log (List.Count);
		
		GameObject 
			G= 
				transform.root.FindChild("Scroll").FindChild("ContestIn").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
			
			if(List[i] !="0"&&List[i] !=""){
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
					FindChild("use").gameObject.SetActive(true);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
					FindChild("non").gameObject.SetActive(false);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
					FindChild("use").FindChild("Label").GetComponent<UILabel>().text = Value[int.Parse(List[i])-1];
			}
			
			
			
		}
		for (int i = 0; i<G.transform.childCount; i++) {
			if(List[i] !="0"&&List[i] !=""){
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
					FindChild("use").gameObject.SetActive(true);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
					FindChild("non").gameObject.SetActive(false);
				G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
					FindChild("use").FindChild("Label").GetComponent<UILabel>().text = Value[int.Parse(List[i+8])];
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
