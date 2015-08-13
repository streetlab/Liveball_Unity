using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PresetContestCommander : MonoBehaviour {

	public GameObject PresetContestItem1;
	public GameObject PresetContestItem2;
	public GameObject LiveContestItem;
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

		Debug.Log ("UserMgr.ContestStatus : " + UserMgr.ContestStatus );
		if (UserMgr.ContestStatus == 2) {


			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").FindChild("Label")
				.GetComponent<UILabel>().text = "라이브";


			transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").gameObject.SetActive(false);
			
			
			if (List == null) {
				CreatItem ();
			} else {
				List<List<int>> SeqList = new List<List<int>> ();
				List<int> nums = new List<int> ();
				Dictionary<int,int> creat = new Dictionary<int,int> ();
				nums.Add (0);
				for (int i = 0; i<List.Count; i++) {
					
					if (i == 0) {
						SeqList.Add (new List<int> ());
						SeqList [0].Add (i);
						creat.Add (List [i].gameSeq, i);
						Debug.Log (List [i].gameSeq);
					} else {
						bool Check = true;
						for (int s = 0; s<nums.Count; s++) {
							if (nums [s] == List [i].gameSeq) {
								Debug.Log (List [i].gameSeq);
								Debug.Log (creat [List [i].gameSeq]);
								SeqList [creat [List [i].gameSeq]].Add (i);
								Check = false;
								break;
							}
						}
						
						if (Check) {
							
							
							
							
							SeqList.Add (new List<int> ());
							SeqList [SeqList.Count - 1].Add (i);
							creat.Add (List [i].gameSeq, creat.Count - 1);
							Debug.Log ("add " + List [i].gameSeq);
						}
					}
					nums.Add (List [i].gameSeq);
				}
				int Count = transform.FindChild ("Scroll View").FindChild ("Position").childCount;
				for (int i = 0; i<Count; i++) {
					DestroyImmediate (transform.FindChild ("Scroll View").FindChild ("Position").GetChild (0).gameObject);
				}
				for (int i = 0; i<SeqList.Count; i++) {
					GameObject Item1 = (GameObject)Instantiate (PresetContestItem1);
					Item1.transform.parent = transform.FindChild ("Scroll View").FindChild ("Position");
					Item1.transform.localScale = new Vector3 (1, 1, 1);
					Debug.Log (List [SeqList [i] [0]].startTime [4].ToString () + List [SeqList [i] [0]].startTime [5].ToString ());
					Debug.Log (List [SeqList [i] [0]].startTime [5]);
					Item1.transform.FindChild ("Time").FindChild ("Label").GetComponent<UILabel> ().text =
						List [SeqList [i] [0]].startTime [4].ToString () + List [SeqList [i] [0]].startTime [5].ToString ()
							+ "월" + List [SeqList [i] [0]].startTime [6].ToString () + List [SeqList [i] [0]].startTime [7].ToString () + "일"
							+ List [SeqList [i] [0]].startTime [8].ToString () + List [SeqList [i] [0]].startTime [9].ToString () + ":"
							+ List [SeqList [i] [0]].startTime [10] + List [SeqList [i] [0]].startTime [11] + " " +
							AMPM (int.Parse ((List [SeqList [i] [0]].startTime [8]).ToString () + List [SeqList [i] [0]].startTime [9].ToString ())) + "경기";
					Debug.Log (List [SeqList [i] [0]].startTime);
					Item1.transform.FindChild ("LTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].aTeamName;
					Item1.transform.FindChild ("RTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].hTeamName;
					Item1.transform.FindChild ("Score").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].aTeamScore + " : " + List [SeqList [i] [0]].hTeamScore;
					Item1.transform.FindChild ("BG").FindChild ("GameSeq").GetComponent<UILabel> ().text = List [SeqList [i] [0]].gameSeq.ToString ();
					Item1.transform.FindChild ("BG").FindChild ("ChildCount").GetComponent<UILabel> ().text = SeqList [i].Count.ToString ();
					
					
					float Y = -((float)i * PCGap) + 556f - (PCGap * 0.5f) - 14f;
					if (i == 0) {
						Item1.transform.localPosition = new Vector3 (0, Y);
					} else {
						float Sum = 0;
						for (int a = 0; a<i; a++) {
							Sum += SeqList [a].Count;
						}
						//Debug.Log ("Sum : " + Sum);
						Item1.transform.localPosition = new Vector3 (0, Y - (Sum * (251)));
					}
					
					Item1.name = "Item " + i.ToString ();
					
					for (int a = 0; a<SeqList[i].Count; a++) {
						
						
						
						
						GameObject Item2 = (GameObject)Instantiate (LiveContestItem);
						Item2.transform.parent = Item1.transform;
						Item2.transform.localScale = new Vector3 (1, 1, 1);
						Y = -(PCGap * 1f) - (a * 251)-55;
						Item2.transform.localPosition = new Vector3 (0, Y, 0);
						Item2.transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text = List [SeqList [i] [a]].presetSeq.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("contestSeq").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestSeq.ToString ();
						Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestName;
						//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(0).name);
						//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(1).name);
						//					Debug.Log(List [SeqList [i] [a]].totalPreset.ToString ());
						//					Debug.Log(List [SeqList [i] [a]].totalEntry.ToString ());
						Item2.transform.FindChild ("Entry").FindChild ("entryentry").GetComponent<UILabel> ().text = List [SeqList [i] [a]].totalPreset.ToString () + " / " + List [SeqList [i] [a]].totalEntry.ToString ();
						Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestName;
						Item2.transform.FindChild ("Cost").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].entryFee.ToString ();
						
						if (List [SeqList [i] [a]].contestType == 1) {
							Item2.transform.FindChild ("Product").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].totalReward;
						} else {
							Item2.transform.FindChild ("Product").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].itemName;
						}
						Item2.name = "Item " + i.ToString () + " Sub " + a.ToString ();
						
						
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a1").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a1.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a2").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a2.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a3").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a3.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a4").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a4.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a5").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a5.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a6").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a6.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a7").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a7.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a8").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a8.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a9").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a9.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h1").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h1.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h2").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h2.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h3").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h3.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h4").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h4.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h5").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h5.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h6").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h6.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h7").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h7.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h8").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h8.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h9").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h9.ToString ();
						Debug.Log((List [SeqList [i] [a]].rewardCount/List [SeqList [i] [a]].totalPreset)*632);





						if(List [SeqList [i] [a]].myRank==0){
							Item2.transform.FindChild ("Bar").FindChild ("BG").FindChild("rewordScore").localPosition = new Vector3(-316+((List [SeqList [i] [a]].rewardCount/List [SeqList [i] [a]].totalPreset)*632),-35);
							Item2.transform.FindChild ("Bar").FindChild ("BG").FindChild("Maker").localPosition = new Vector3(-316+((List [SeqList [i] [a]].myRank/List [SeqList [i] [a]].totalPreset)*632),23);
							

						}else{
							Item2.transform.FindChild ("Bar").FindChild ("BG").FindChild("rewordScore").localPosition = new Vector3(-316+(((List [SeqList [i] [a]].totalPreset-(List [SeqList [i] [a]].myRank-1))/List [SeqList [i] [a]].totalPreset)*632),-35);
							Item2.transform.FindChild ("Bar").FindChild ("BG").FindChild("Maker").localPosition = new Vector3(-316+(((List [SeqList [i] [a]].totalPreset-(List [SeqList [i] [a]].myRank-1))/List [SeqList [i] [a]].totalPreset)*632),23);
							

						}


						Item2.transform.FindChild ("Bar").FindChild ("BG").FindChild("R_bar").GetComponent<UIPanel>().clipOffset = new Vector2(((List [SeqList [i] [a]].totalPreset-(List [SeqList [i] [a]].rewardCount-1))/List [SeqList [i] [a]].totalPreset)*632,0);
						Item2.transform.FindChild ("Bar").FindChild ("BG").FindChild("rewordScore").GetComponent<UILabel>().text = List [SeqList [i] [a]].rewardScore.ToString();
												
						Item2.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(false);
						Item2.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(false);
						if(List [SeqList [i] [a]].guaranteed == 1){
							Item2.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(true);
						}
						if(List [SeqList [i] [a]].multiEntry > 1){
							Item2.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(true);
						}else{
							Item2.transform.FindChild ("Title").FindChild("G").transform.localPosition = new Vector3(325f,0,0);
						}


					}
				}
			}









		}
		else{

			if (List == null) {
				CreatItem ();
			} else {
				List<List<int>> SeqList = new List<List<int>> ();
				List<int> nums = new List<int> ();
				Dictionary<int,int> creat = new Dictionary<int,int> ();
				nums.Add (0);
				for (int i = 0; i<List.Count; i++) {

					if (i == 0) {
						SeqList.Add (new List<int> ());
						SeqList [0].Add (i);
						creat.Add (List [i].gameSeq, i);
						Debug.Log (List [i].gameSeq);
					} else {
						bool Check = true;
						for (int s = 0; s<nums.Count; s++) {
							if (nums [s] == List [i].gameSeq) {
								Debug.Log (List [i].gameSeq);
								Debug.Log (creat [List [i].gameSeq]);
								SeqList [creat [List [i].gameSeq]].Add (i);
								Check = false;
								break;
							}
						}

						if (Check) {
					



							SeqList.Add (new List<int> ());
							SeqList [SeqList.Count - 1].Add (i);
							creat.Add (List [i].gameSeq, creat.Count - 1);
							Debug.Log ("add " + List [i].gameSeq);
						}
					}
					nums.Add (List [i].gameSeq);
				}
				int Count = transform.FindChild ("Scroll View").FindChild ("Position").childCount;
				for (int i = 0; i<Count; i++) {
					DestroyImmediate (transform.FindChild ("Scroll View").FindChild ("Position").GetChild (0).gameObject);
				}
				for (int i = 0; i<SeqList.Count; i++) {
					GameObject Item1 = (GameObject)Instantiate (PresetContestItem1);
					Item1.transform.parent = transform.FindChild ("Scroll View").FindChild ("Position");
					Item1.transform.localScale = new Vector3 (1, 1, 1);
					Debug.Log (List [SeqList [i] [0]].startTime [4].ToString () + List [SeqList [i] [0]].startTime [5].ToString ());
					Debug.Log (List [SeqList [i] [0]].startTime [5]);
					Item1.transform.FindChild ("Time").FindChild ("Label").GetComponent<UILabel> ().text =
					List [SeqList [i] [0]].startTime [4].ToString () + List [SeqList [i] [0]].startTime [5].ToString ()
						+ "월" + List [SeqList [i] [0]].startTime [6].ToString () + List [SeqList [i] [0]].startTime [7].ToString () + "일"
						+ List [SeqList [i] [0]].startTime [8].ToString () + List [SeqList [i] [0]].startTime [9].ToString () + ":"
						+ List [SeqList [i] [0]].startTime [10] + List [SeqList [i] [0]].startTime [11] + " " +
						AMPM (int.Parse ((List [SeqList [i] [0]].startTime [8]).ToString () + List [SeqList [i] [0]].startTime [9].ToString ())) + "경기";
					Debug.Log (List [SeqList [i] [0]].startTime);
					Item1.transform.FindChild ("LTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].aTeamName;
					Item1.transform.FindChild ("RTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].hTeamName;
					Item1.transform.FindChild ("Score").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [0]].aTeamScore + " : " + List [SeqList [i] [0]].hTeamScore;
					Item1.transform.FindChild ("BG").FindChild ("GameSeq").GetComponent<UILabel> ().text = List [SeqList [i] [0]].gameSeq.ToString ();
					Item1.transform.FindChild ("BG").FindChild ("ChildCount").GetComponent<UILabel> ().text = SeqList [i].Count.ToString ();


					float Y = -((float)i * PCGap) + 556f - (PCGap * 0.5f) - 14f;
					if (i == 0) {
						Item1.transform.localPosition = new Vector3 (0, Y);
					} else {
						float Sum = 0;
						for (int a = 0; a<i; a++) {
							Sum += SeqList [a].Count;
						}
						//Debug.Log ("Sum : " + Sum);
						Item1.transform.localPosition = new Vector3 (0, Y - (Sum * (150)));
					}
			
					Item1.name = "Item " + i.ToString ();
			
					for (int a = 0; a<SeqList[i].Count; a++) {

				


						GameObject Item2 = (GameObject)Instantiate (PresetContestItem2);
						Item2.transform.parent = Item1.transform;
						Item2.transform.localScale = new Vector3 (1, 1, 1);
						Y = -(PCGap * 1f) - (a * 150);
						Item2.transform.localPosition = new Vector3 (0, Y, 0);
						Item2.transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text = List [SeqList [i] [a]].presetSeq.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("contestSeq").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestSeq.ToString ();
						Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestName;
//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(0).name);
//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(1).name);
//					Debug.Log(List [SeqList [i] [a]].totalPreset.ToString ());
//					Debug.Log(List [SeqList [i] [a]].totalEntry.ToString ());
						Item2.transform.FindChild ("Entry").FindChild ("entryentry").GetComponent<UILabel> ().text = List [SeqList [i] [a]].totalPreset.ToString () + " / " + List [SeqList [i] [a]].totalEntry.ToString ();
						Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = List [SeqList [i] [a]].contestName;
						Item2.transform.FindChild ("Cost").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].entryFee.ToString ();

						if (List [SeqList [i] [a]].contestType == 1) {
							Item2.transform.FindChild ("Product").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].totalReward;
						} else {
							Item2.transform.FindChild ("Product").FindChild ("value").GetComponent<UILabel> ().text = List [SeqList [i] [a]].itemName;
						}
						Item2.name = "Item " + i.ToString () + " Sub " + a.ToString ();


						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a1").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a1.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a2").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a2.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a3").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a3.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a4").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a4.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a5").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a5.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a6").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a6.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a7").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a7.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a8").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a8.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a9").GetComponent<UILabel> ().text = List [SeqList [i] [a]].a9.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h1").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h1.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h2").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h2.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h3").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h3.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h4").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h4.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h5").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h5.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h6").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h6.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h7").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h7.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h8").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h8.ToString ();
						Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h9").GetComponent<UILabel> ().text = List [SeqList [i] [a]].h9.ToString ();


					}
				}
			}
		}
	}
	string AMPM(int num){
		Debug.Log (num);
string a;
if(num >12){
a ="PM";
}else{
			a = "AM";
}
return a;
	}
	PresetDataEvent PDE;
	ContestDataEvent CDE;
	public void Reset(){
		PDE = new PresetDataEvent(new EventDelegate(this,"ResetPreset"));
		NetMgr.GetPresetData (0,PDE);

	
			CDE = new ContestDataEvent (new EventDelegate (this, "ResetData"));
			NetMgr.GetContestData (CDE);

	}
	void ResetPreset(){
		for (int i = 0; i<transform.FindChild ("Scroll View").FindChild ("Position").childCount; i++) {
			for(int a = 0; a<PDE.Response.data.Count; a++){
//				if(PDE.Response.data[a].gameSeq == int.Parse(transform.FindChild ("Scroll View").FindChild ("Position").
//				   FindChild("Item " + i.ToString()).FindChild("BG").FindChild("GameSeq").
//				                                     GetComponent<UILabel>().text)){
//					transform.FindChild ("Scroll View").FindChild ("Position").
//						FindChild("Item " + i.ToString()).FindChild("Score").FindChild("Label").GetComponent<UILabel>().text = 
//							PDE.Response.data[a].aTeamScore + " : " + PDE.Response.data[a].hTeamScore;
//
//
//				}
				for(int s = 0; s<int.Parse(transform.FindChild ("Scroll View").FindChild ("Position").
				    FindChild("Item " + i.ToString()).FindChild("BG").FindChild("ChildCount")
				    .GetComponent<UILabel>().text)

				    ; s++){
//					Debug.Log("Item " + i.ToString()
//					          + " Sub " + s.ToString());
				if(PDE.Response.data[a].presetSeq == int.Parse(transform.FindChild ("Scroll View").FindChild ("Position").
					                                               FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
					                                             + " Sub " + s.ToString()).FindChild("BG").FindChild("presetSeq").
				                                             GetComponent<UILabel>().text)){
					transform.FindChild ("Scroll View").FindChild ("Position").
							FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
							                                            + " Sub " + s.ToString())
								.FindChild("Entry").FindChild("entryentry").GetComponent<UILabel>().text = 
							PDE.Response.data[a].totalPreset + " / " + PDE.Response.data[a].totalEntry;
						if(UserMgr.ContestStatus == 2){






							if(PDE.Response.data[a].myRank==0){




								transform.FindChild ("Scroll View").FindChild ("Position").
									FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
									                                            + " Sub " + s.ToString())
										.FindChild("Bar").FindChild("BG").FindChild("Maker").localPosition = new Vector3(
											-316f+( (float)PDE.Response.data[a].myRank/ (float)PDE.Response.data[a].totalPreset)*632f,23);
								transform.FindChild ("Scroll View").FindChild ("Position").
									FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
									                                            + " Sub " + s.ToString())
										.FindChild("Bar").FindChild("BG").FindChild("rewordScore").localPosition = new Vector3(
											-316f+( (float)PDE.Response.data[a].myRank/ (float)PDE.Response.data[a].totalPreset)*632f,-35);






							}else{
								transform.FindChild ("Scroll View").FindChild ("Position").
									FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
									                                            + " Sub " + s.ToString())
										.FindChild("Bar").FindChild("BG").FindChild("Maker").localPosition = new Vector3(
											-316f+(( (float)PDE.Response.data[a].totalPreset-((float)PDE.Response.data[a].myRank-1f)/ (float)PDE.Response.data[a].totalPreset)*632f),23);
								transform.FindChild ("Scroll View").FindChild ("Position").
									FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
									                                            + " Sub " + s.ToString())
										.FindChild("Bar").FindChild("BG").FindChild("rewordScore").localPosition = new Vector3(
											-316f+(( (float)PDE.Response.data[a].totalPreset-((float)PDE.Response.data[a].myRank-1f)/ (float)PDE.Response.data[a].totalPreset)*632f),-35);

							}




							transform.FindChild ("Scroll View").FindChild ("Position").
								FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
								                                            + " Sub " + s.ToString())
									.FindChild("Bar").FindChild("BG").FindChild("rewordScore").GetComponent<UILabel>().text = PDE.Response.data[a].rewordScore.ToString();
							transform.FindChild ("Scroll View").FindChild ("Position").
								FindChild("Item " + i.ToString()).FindChild("Item " + i.ToString()
								                                            + " Sub " + s.ToString())
									.FindChild("Bar").FindChild("BG").FindChild("R_bar").GetComponent<UIPanel>().clipOffset = new Vector2(( (float)PDE.Response.data[a].totalPreset-((float)PDE.Response.data[a].rewordCount-1f)/ (float)PDE.Response.data[a].totalPreset)*632f,0);


						}
					
					
				}
				}
			}
		}
	}
	PresetListEvent presetListEvent;
	void ResetData(){

		for (int i = 0; i<transform.FindChild ("Scroll View").FindChild ("Position").childCount; i++) {
			for (int a = 0; a<CDE.Response.data.Count; a++) {
				if(CDE.Response.data[a].gameSeq == int.Parse(transform.FindChild ("Scroll View").FindChild ("Position").
								   FindChild("Item " + i.ToString()).FindChild("BG").FindChild("GameSeq").
								                                     GetComponent<UILabel>().text)){
									transform.FindChild ("Scroll View").FindChild ("Position").
										FindChild("Item " + i.ToString()).FindChild("Score").FindChild("Label").GetComponent<UILabel>().text = 
							CDE.Response.data[a].aTeamScore + " : " + CDE.Response.data[a].hTeamScore;
					//Debug.Log(CDE.Response.data[a].contestStatus);
					if(UserMgr.ContestStatus!=2){
					if(CDE.Response.data[a].contestStatus == 2){
							UserMgr.ContestStatus = CDE.Response.data[a].contestStatus;
					 presetListEvent = new PresetListEvent(new EventDelegate(this, "GetPresetList"));
						NetMgr.GetPresetList(presetListEvent);
					}
				
					}
								}
			}
		}
	}
	void GetPresetList(){
		CreatItem (presetListEvent.Response.data);
	}
	void OnEnable (){
		StartCoroutine ("Resets");
	}
	void OnDisable (){
		StopCoroutine ("Resets");
	}
	IEnumerator Resets(){
		while (true) {
			yield return new WaitForSeconds (60f);
			Reset();
		}
	}
}
