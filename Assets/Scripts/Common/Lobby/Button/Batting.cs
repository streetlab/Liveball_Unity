using UnityEngine;
using System.Collections;

public class Batting : MonoBehaviour {

	public void PlayingButton(){
		//	BattingCommander.ChoseGameObject.transform.FindChild("Label").GetComponent<UILabel>().text = "Non";
		//BattingCommander.ChoseGameObject.transform.FindChild("")FindChild("Label").GetComponent<UILabel>().text = "Non";
		BattingCommander.ChoseGameObject.transform.FindChild (BattingCommander.ChoseGameObject.name
		                                                      + "_pre").FindChild ("use").gameObject.SetActive (true);
		BattingCommander.ChoseGameObject.transform.FindChild (BattingCommander.ChoseGameObject.name
		                                                      + "_pre").FindChild ("non").gameObject.SetActive (false);
		BattingCommander.ChoseGameObject.transform.FindChild(BattingCommander.ChoseGameObject.name
		                                                     +"_pre").FindChild("use").FindChild("Label").GetComponent<UILabel>().text = 
			transform.FindChild("Label").GetComponent<UILabel>().text;
		BattingCommander.ChoseGameObject.transform.parent.parent.localPosition = new Vector3 (0,0,0);
		transform.parent.parent.gameObject.SetActive (false);
		transform.parent.parent.parent.parent.GetComponent<Presetplaying> ().Button ();
		
	}



	public void Button(){
	//	BattingCommander.ChoseGameObject.transform.FindChild("Label").GetComponent<UILabel>().text = "Non";
		//BattingCommander.ChoseGameObject.transform.FindChild("")FindChild("Label").GetComponent<UILabel>().text = "Non";
		BattingCommander.ChoseGameObject.transform.FindChild (BattingCommander.ChoseGameObject.name
		                                                                          + "_pre").FindChild ("use").gameObject.SetActive (true);
		BattingCommander.ChoseGameObject.transform.FindChild (BattingCommander.ChoseGameObject.name
		                                                                          + "_pre").FindChild ("non").gameObject.SetActive (false);
		BattingCommander.ChoseGameObject.transform.FindChild(BattingCommander.ChoseGameObject.name
		                                                                        +"_pre").FindChild("use").FindChild("Label").GetComponent<UILabel>().text = 
			transform.FindChild("Label").GetComponent<UILabel>().text;
		BattingCommander.ChoseGameObject.transform.parent.parent.localPosition = new Vector3 (0,0,0);
		transform.parent.parent.gameObject.SetActive (false);
		if(CheckPreset()){
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Bot").FindChild("Sumit").
				gameObject.SetActive(true);
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
