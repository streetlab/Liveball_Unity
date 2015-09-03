using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HistoryContestCommander : MonoBehaviour {
	public float PCGap;
	public GameObject PresetContestItem1;
	public GameObject PresetContestItem2;
	public void CreatItem(List<PresetListInfo> List){
		
		Debug.Log ("UserMgr.ContestStatus : " + UserMgr.ContestStatus );

			
			if (List == null) {
			//	CreatItem ();
			} else {
			//	List<int> SeqList = new List<int> ();

			Dictionary<int,List<int>> Seq = new Dictionary<int,List<int>> ();
			List<int> SeqList = new List<int>();
				for (int i = 0; i<List.Count; i++) {
				bool check = true;
				for(int a = 0; a<SeqList.Count;a++){
					if(SeqList[a] == List[i].gameSeq){
						check = false;
					}
				}
				if(check){
					SeqList.Add(List[i].gameSeq);
				}
				try{
				//	Debug.Log("try : " + i.ToString() + " : " + List[i].gameSeq);
				Seq.Add(List[i].gameSeq,new List<int> ());
				Seq[List[i].gameSeq].Add(i);
				}catch{
				//	Debug.Log("catch : " + i.ToString() + " : " + List[i].gameSeq);
					Seq[List[i].gameSeq].Add(i);
				}
				}
			//Debug.Log(List [Seq [List[0].gameSeq][0]].a1);
				int Count = transform.FindChild ("Scroll View").FindChild ("Position").childCount;
				for (int i = 0; i<Count; i++) {
					DestroyImmediate (transform.FindChild ("Scroll View").FindChild ("Position").GetChild (0).gameObject);
				}
		//	Debug.Log("Seq : " + Seq.Count);
			for (int i = 0; i<Seq.Count; i++) {
				//Debug.Log("SeqList[i] : " + SeqList[i]+ " i : " + i);
				GameObject Item1 = (GameObject)Instantiate (PresetContestItem1);
				Item1.transform.parent = transform.FindChild ("Scroll View").FindChild ("Position");
				Item1.transform.localScale = new Vector3 (1, 1, 1);
				//					Debug.Log (List [SeqList [i] [0]].startTime [4].ToString () + List [SeqList [i] [0]].startTime [5].ToString ());
				//					Debug.Log (List [SeqList [i] [0]].startTime [5]);
				Item1.transform.FindChild ("Time").FindChild ("Label").GetComponent<UILabel> ().text =
					List [Seq [SeqList[i]][0]].startTime [4].ToString () + List [Seq [SeqList[i]][0]].startTime [5].ToString ()
						+ "월" + List [Seq [SeqList[i]][0]].startTime [6].ToString () + List [Seq [SeqList[i]][0]].startTime [7].ToString () + "일"
						+ List [Seq [SeqList[i]][0]].startTime [8].ToString () + List [Seq [SeqList[i]][0]].startTime [9].ToString () + ":"
						+ List [Seq [SeqList[i]][0]].startTime [10] + List [Seq [SeqList[i]][0]].startTime [11] + " " +
						AMPM (int.Parse ((List [Seq [SeqList[i]][0]].startTime [8]).ToString () + List [Seq [SeqList[i]][0]].startTime [9].ToString ())) + "경기";
				
//				Debug.Log("text : "+Item1.transform.FindChild ("Time").FindChild ("Label").GetComponent<UILabel> ().text);
				//Debug.Log (List [SeqList [i] [0]].startTime);
				Item1.transform.FindChild ("LTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][0]].aTeamName;
				Item1.transform.FindChild ("RTeam").FindChild ("Label").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][0]].hTeamName;
				Item1.transform.FindChild ("Score").FindChild ("Label").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][0]].aTeamScore + " : " + List [Seq [SeqList[i]][0]].hTeamScore;
				Item1.transform.FindChild ("BG").FindChild ("GameSeq").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][0]].gameSeq.ToString ();
				Item1.transform.FindChild ("BG").FindChild ("ChildCount").GetComponent<UILabel> ().text = Seq [SeqList[i]].Count.ToString ();
				
				
				float Y = -((float)i * PCGap) + 556f - (PCGap * 0.5f) - 14f;
				if (i == 0) {
					Item1.transform.localPosition = new Vector3 (0, Y);
				} else {
					float Sum = 0;
					for (int a = 0; a<i; a++) {
						Sum += Seq [SeqList[a]].Count;
					}
					//Debug.Log ("Sum : " + Sum);
					Item1.transform.localPosition = new Vector3 (0, Y - (Sum * (150)));
				}
				
				Item1.name = "Item " + i.ToString ();
				
				for (int a = 0; a<Seq [SeqList[i]].Count; a++) {
					
					
					
					
					GameObject Item2 = (GameObject)Instantiate (PresetContestItem2);
					Item2.transform.parent = Item1.transform;
					Item2.transform.localScale = new Vector3 (1, 1, 1);
					Y = -(PCGap * 1f) - (a * 150);
					Item2.transform.localPosition = new Vector3 (0, Y, 0);
					Item2.transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].presetSeq.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("contestSeq").GetComponent<UILabel> ().text =List [Seq [SeqList[i]][a]].contestSeq.ToString ();
					Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text ="[b]" + List [Seq [SeqList[i]][a]].contestName;
					//Debug.Log("List [SeqList [i] [a]].contestName : " + List [Seq [SeqList[i]][a]].contestName);
					
					Item2.transform.FindChild ("BG").FindChild ("totalpreset").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].totalPreset.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("totalentry").GetComponent<UILabel> ().text =List [Seq [SeqList[i]][a]].totalEntry.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("myrank").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].myRank.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("rewordscore").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].rewardScore.ToString ();
				//	Debug.Log("List [SeqList [i] [a]].rewordCount.ToString () : " + List [Seq [SeqList[i]][a]].rewardCount.ToString ());
					Item2.transform.FindChild ("BG").FindChild ("rewordcount").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].rewardCount.ToString ();
					
					
					//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(0).name);
					//					Debug.Log(Item2.transform.FindChild ("Entry").GetChild(1).name);
					//					Debug.Log(List [SeqList [i] [a]].totalPreset.ToString ());
					//					Debug.Log(List [SeqList [i] [a]].totalEntry.ToString ());
					Item2.transform.FindChild ("Entry").FindChild ("entryentry").GetComponent<UILabel> ().text ="[b]" +List [Seq [SeqList[i]][a]].totalPreset.ToString () + " / " + List [Seq [SeqList[i]][a]].totalEntry.ToString ();
					Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text ="[b]" + List [Seq [SeqList[i]][a]].contestName;
					Item2.transform.FindChild ("Cost").FindChild ("value").GetComponent<UILabel> ().text ="[b]" + List [Seq [SeqList[i]][a]].entryFee.ToString ();


					if(List [Seq [SeqList [i]] [a]].featured == 1){
						Item2.transform.FindChild ("BGW").GetComponent<UISprite>().color = new Color (1f, 1f, 240f / 255f, 1f);
						Item2.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().color = new Color(16f/255f, 140f/255f, 187f / 255f, 1f);
					}


					if (List [Seq [SeqList[i]][a]].itemID < 100) {
						Item2.transform.FindChild ("Product").FindChild ("value").GetComponent<UILabel> ().text ="[b]" + List [Seq [SeqList[i]][a]].totalReward + " " +List [Seq [SeqList[i]][a]].itemName ;
					} else {
						Item2.transform.FindChild ("Product").FindChild ("value").GetComponent<UILabel> ().text ="[b]" + List [Seq [SeqList[i]][a]].itemName;
					}
					Item2.name = "Item " + i.ToString () + " Sub " + a.ToString ();
					
					
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a1").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a1.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a2").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a2.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a3").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a3.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a4").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a4.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a5").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a5.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a6").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a6.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a7").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a7.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a8").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a8.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("a9").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].a9.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h1").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h1.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h2").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h2.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h3").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h3.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h4").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h4.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h5").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h5.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h6").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h6.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h7").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h7.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h8").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h8.ToString ();
					Item2.transform.FindChild ("BG").FindChild ("presetList").FindChild ("h9").GetComponent<UILabel> ().text = List [Seq [SeqList[i]][a]].h9.ToString ();
					
			
				
				    Item2.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(false);
					Item2.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(false);
					Item2.transform.FindChild ("Title").FindChild("L").gameObject.SetActive(false);
					Item2.transform.FindChild ("Title").FindChild ("G").localPosition = new Vector3 (285f,0,0);
					




					if (List [Seq [SeqList[i]][a]].multiEntry > 1 && List [Seq [SeqList[i]][a]].guaranteed == 1) {
						Item2.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(true);
						Item2.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(true);

						
						if (List [Seq [SeqList[i]][a]].contestType == 5) {
							Item2.transform.FindChild ("Title").FindChild("L").gameObject.SetActive(true);
						}
					} else if (List [Seq [SeqList[i]][a]].multiEntry > 1 && List [Seq [SeqList[i]][a]].guaranteed != 1) {
						
						Item2.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(true);
						if (List [Seq [SeqList[i]][a]].contestType == 5) {
							Item2.transform.FindChild ("Title").FindChild("L").gameObject.SetActive(true);
							Item2.transform.FindChild ("Title").FindChild ("L").localPosition = new Vector3 (285f,0,0);
						}
					} else {
						//Debug.Log(infoList [index].contestType + " : " +infoList [index].guaranteed );
						if (List [Seq [SeqList[i]][a]].contestType == 5&& List [Seq [SeqList[i]][a]].guaranteed == 1) {
							Item2.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(true);
							Item2.transform.FindChild ("Title").FindChild ("G").localPosition = new Vector3 (325f,0,0);

							Item2.transform.FindChild ("Title").FindChild("L").gameObject.SetActive(true);
							Item2.transform.FindChild ("Title").FindChild ("L").localPosition = new Vector3 (285f,0,0);
						}else if(List [Seq [SeqList[i]][a]].contestType == 5&& List [Seq [SeqList[i]][a]].guaranteed != 1){
							Item2.transform.FindChild ("Title").FindChild("L").gameObject.SetActive(true);
							Item2.transform.FindChild ("Title").FindChild ("L").localPosition = new Vector3 (325f,0,0);
						}else{
							Item2.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(true);
							Item2.transform.FindChild ("Title").FindChild ("G").localPosition = new Vector3 (325f,0,0);
						}
					}


					int num = 0;
					for(int m = 1; m<Item2.transform.FindChild ("Title").childCount;m++){
						if(!Item2.transform.FindChild ("Title").GetChild(m).gameObject.activeSelf)
						{num++;}
					}

					Item2.transform.FindChild ("Title").FindChild("Label").GetComponent<UILabel>().SetRect(560f+((float)num*40f),30);











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
}
