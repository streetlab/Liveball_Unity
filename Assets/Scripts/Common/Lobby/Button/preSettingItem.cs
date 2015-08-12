using UnityEngine;
using System.Collections;

public class preSettingItem : MonoBehaviour {


	public void ButtonPlaying(){
		Debug.Log (name);
		
		//Key is this.name
		//		if (!CheckPreset ()) {
		if (int.Parse (transform.parent.name [5].ToString ()) > 4) {
			transform.parent.parent.transform.localPosition = new Vector3 (0, (int.Parse (transform.parent.name [5].ToString ()) - 4) * 95);
		}
		
		transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").GetComponent<BattingCommander> ().SetBatting (this.gameObject);
		
		transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").gameObject.SetActive (true);
		//	}
	}

	public void Button(){
		//Key is this.name
		if (!CheckPreset ()) {
			if (int.Parse (transform.parent.name [5].ToString ()) > 4) {
				transform.parent.parent.transform.localPosition = new Vector3 (0, (int.Parse (transform.parent.name [5].ToString ()) - 4) * 95);
			}

			transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").GetComponent<BattingCommander> ().SetBatting (this.gameObject);

			transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").gameObject.SetActive (true);
		} else {
			transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Sumit").gameObject.SetActive (true);
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
