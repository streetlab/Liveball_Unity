using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PresetContestCommander : MonoBehaviour {

	public GameObject PresetContestItem1;
	public GameObject PresetContestItem2;
	public int PCCount1;
	public List<int> PCCount2 = new List<int>();
	public float PCGap;
	public void CreatItem(){
		int Count = transform.FindChild ("Scroll View").FindChild ("Position").childCount;
		for(int i = 0 ; i<Count;i++){
			DestroyImmediate(transform.FindChild("Scroll View").FindChild("Position").GetChild(0).gameObject);
		}
		for (int i = 0; i<PCCount1; i++) {
			GameObject Item1 = (GameObject)Instantiate(PresetContestItem1);
			Item1.transform.parent = transform.FindChild("Scroll View").FindChild("Position");
			Item1.transform.localScale = new Vector3(1,1,1);
			float Y = -((float)i*PCGap)+556f-(PCGap*0.5f)-14f;
			if( i== 0){
			Item1.transform.localPosition = new Vector3 (0, Y);
			}else{
				float Sum=0;
				for(int a = 0; a<i;a++){
					Sum += PCCount2[a];
				}
				Item1.transform.localPosition = new Vector3 (0, Y-(Sum*(150)));
			}
		
			Item1.name = "Item " + i.ToString();
		
			for(int a = 0; a<PCCount2[i];a++){
				GameObject Item2 = (GameObject)Instantiate(PresetContestItem2);
				Item2.transform.parent = Item1.transform;
				Item2.transform.localScale = new Vector3(1,1,1);
				Y = -(PCGap*1f)-(a*150);
				Item2.transform.localPosition = new Vector3(0,Y,0);

				Item2.name = "Item " + i.ToString() + " Sub " + a.ToString();

			}
		}
	}
	public void CreatItem(List<PresetListInfo> List){
		if (List == null) {
			CreatItem ();
		} else {
			List<List<int>> SeqList = new List<List<int>> ();
			int nums = 0;
			for (int i = 0; i<List.Count; i++) {
				Debug.Log ("List[i].contestSeq : " + List [i].contestSeq);
				if (i == 0) {
					SeqList.Add (new List<int> ());
					SeqList [0].Add (i);
				} else {
					if (nums == List [i].contestSeq) {
						SeqList [SeqList.Count - 1].Add (i);
					} else {
						SeqList.Add (new List<int> ());
						SeqList [SeqList.Count - 1].Add (i);
					}
				}
				nums = List [i].contestSeq;
			}
			int Count = transform.FindChild ("Scroll View").FindChild ("Position").childCount;
			for (int i = 0; i<Count; i++) {
				DestroyImmediate (transform.FindChild ("Scroll View").FindChild ("Position").GetChild (0).gameObject);
			}
			for (int i = 0; i<SeqList.Count; i++) {
				GameObject Item1 = (GameObject)Instantiate (PresetContestItem1);
				Item1.transform.parent = transform.FindChild ("Scroll View").FindChild ("Position");
				Item1.transform.localScale = new Vector3 (1, 1, 1);
				Item1.transform.FindChild ("Time").FindChild ("Label").GetComponent<UILabel> ().text =
				(List [SeqList [i] [0]].startTime [4] + List [SeqList [i] [0]].startTime [5]).ToString ()
					+ "월" + (List [SeqList [i] [0]].startTime [6] + List [SeqList [i] [0]].startTime [7]).ToString () + "일"
					+ (List [SeqList [i] [0]].startTime [8] + List [SeqList [i] [0]].startTime [9]).ToString () + ":"
					+ (List [SeqList [i] [0]].startTime [10] + List [SeqList [i] [0]].startTime [1]).ToString () + " " +
					AMPM (int.Parse ((List [SeqList [i] [0]].startTime [8] + List [SeqList [i] [0]].startTime [9]).ToString ())) + "경기";
				Item1.transform.FindChild ("LTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].aTeamName;
				Item1.transform.FindChild ("RTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].hTeamName;
				Item1.transform.FindChild ("Score").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].aTeamScore + " : " + List [SeqList [i] [0]].hTeamScore;
				Item1.transform.FindChild ("BG").FindChild ("contestSeq").GetComponent<UILabel> ().text = List [SeqList [i] [0]].contestSeq.ToString ();
				float Y = -((float)i * PCGap) + 556f - (PCGap * 0.5f) - 14f;
				if (i == 0) {
					Item1.transform.localPosition = new Vector3 (0, Y);
				} else {
					float Sum = 0;
					for (int a = 0; a<i; a++) {
						Sum += SeqList [a].Count;
					}
					Debug.Log ("Sum : " + Sum);
					Item1.transform.localPosition = new Vector3 (0, Y - (Sum * (150)));
				}
			
				Item1.name = "Item " + i.ToString ();
			
				for (int a = 0; a<SeqList[i].Count; a++) {
					GameObject Item2 = (GameObject)Instantiate (PresetContestItem2);
					Item2.transform.parent = Item1.transform;
					Item2.transform.localScale = new Vector3 (1, 1, 1);
					Y = -(PCGap * 1f) - (a * 150);
					Item2.transform.localPosition = new Vector3 (0, Y, 0);
					Item2.transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestSeq.ToString ();
					Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestName;
//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(0).name);
//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(1).name);
//					Debug.Log(List [SeqList [i] [a]].totalPreset.ToString ());
//					Debug.Log(List [SeqList [i] [a]].totalEntry.ToString ());
//					Item2.transform.FindChild ("Entry").FindChild ("entryentry").GetComponent<UILabel> ().text = List [SeqList [i] [a]].totalPreset.ToString () + " / " + List [SeqList [i] [a]].totalEntry.ToString ();
					Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestName;
					Item2.transform.FindChild ("Cost").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].entryFee.ToString ();
					Item2.transform.FindChild ("Product").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].totalReward;
					Item2.name = "Item " + a.ToString () + " Sub " + i.ToString ();

				}
			}
		}
	}
	string AMPM(int num){
string a;
if(num >12){
a ="PM";
}else{
			a = "AM";
}
return a;
	}
	PresetDataEvent PDE;
	public void Reset(){
		PDE = new PresetDataEvent(new EventDelegate(this,"ResetPreset"));
		NetMgr.GetPresetData (PDE);

	}
	void ResetPreset(){
		for (int i = 0; i<transform.FindChild ("Scroll View").FindChild ("Position").childCount; i++) {
			for(int a = 0; a<PDE.Response.data.Count; a++){
				if(PDE.Response.data[a].contestSeq == int.Parse(transform.FindChild ("Scroll View").FindChild ("Position").
				   FindChild("Item " + i.ToString()).FindChild("BG").FindChild("contestSeq").
				                                     GetComponent<UILabel>().text)){
					transform.FindChild ("Scroll View").FindChild ("Position").
						FindChild("Item " + i.ToString()).FindChild("Score").FindChild("Label").GetComponent<UILabel>().text = 
							PDE.Response.data[a].aTeamScore + " : " + PDE.Response.data[a].hTeamScore;


				}
			}
		}
	}

	void OnEnable (){
		StartCoroutine ("Resets");
	}
	void OnDisable (){
		StopCoroutine ("Resets");
	}
	IEnumerator Resets(){
		while (true) {
			yield return new WaitForSeconds (5f);
			Reset();
		}
	}
}
