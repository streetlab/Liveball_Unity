using UnityEngine;
using System.Collections;

public class LineupControl : MonoBehaviour {
	GameObject ScrollView,S;
	// Use this for initialization
	void Start () {
		getgata ();
	}
	

	void getgata(){

		ScrollView = transform.FindChild ("Scroll View").gameObject;

		
		S = ScrollView.transform.GetChild (0).GetChild (0).FindChild ("S").gameObject;
		for (int i =0; i<S.transform.childCount; i++) {
			//	S.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<UITexture>().mainTexture = "";
			S.transform.GetChild(i).GetChild(1).GetChild(0).GetChild(0).GetComponent<UILabel>().text = "Myname";
		}
		S = ScrollView.transform.GetChild (1).GetChild (0).gameObject;
		for (int i =1; i<S.transform.childCount; i++) {
			//	S.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<UITexture>().mainTexture = "";
			S.transform.GetChild(i).GetChild(1).GetComponent<UILabel>().text = "HE";
			S.transform.GetChild(i).GetChild(1).GetChild (0).GetComponent<UILabel>().text = "HaaaaaaaaaaaaaaaaaaE";
		}

		S = ScrollView.transform.GetChild (2).GetChild (0).gameObject;
		Debug.Log (S);
		for (int i =1; i<S.transform.childCount; i++) {
			//	S.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<UITexture>().mainTexture = "";
			Debug.Log (S.transform.GetChild(i));
			S.transform.GetChild(i).GetChild(1).GetComponent<UILabel>().text = "HE";
			S.transform.GetChild(i).GetChild(1).GetChild (0).GetComponent<UILabel>().text = "HaaaaaaaaaaaaaaaaaaE";
		}

	}
}
