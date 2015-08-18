using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PreSetPlayingCommander : MonoBehaviour {
	string [] Value = {"1루타","2,3루타","홈런","땅볼","뜬공","삼진"};
	public string Mode;
	public GameObject PreSettingItem;
	public string cost;
	public string money;

	public void SetTeamName(string L,string R,string Title){

		if (Mode == "Add") {
			transform.FindChild("Top").FindChild("Top Menu").gameObject.SetActive(true);
			transform.FindChild("Top").FindChild("Top Menu2").gameObject.SetActive(false);
		} else {
			transform.FindChild("Top").FindChild("Top Menu2").gameObject.SetActive(true);
			transform.FindChild("Top").FindChild("Top Menu").gameObject.SetActive(false);
			transform.FindChild("Top").FindChild("Top Menu2").FindChild("Label").GetComponent<UILabel>().text = Title;
		}
		transform.FindChild ("Mid").FindChild ("BG").FindChild ("Team 1").FindChild ("Label").GetComponent<UILabel> ().text
			= L;
		transform.FindChild ("Mid").FindChild ("BG").FindChild ("Team 2").FindChild ("Label").GetComponent<UILabel> ().text
			= R; 
	}
	public void Ruby(string R,string M){
		cost = "";
		for (int i = 3; i<R.Length; i++) {
			cost+= R[i].ToString();
		}
		//cost = R;
		money = M;
	}
	public void SetList(List<string> List){
		Debug.Log (List.Count);
	
		GameObject 
			G= 
				transform.FindChild("Mid").FindChild("Scroll View")
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

}
