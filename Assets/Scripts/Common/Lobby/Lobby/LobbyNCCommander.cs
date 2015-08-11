using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LobbyNCCommander : MonoBehaviour {
	public GameObject NCOrigin;
	public GameObject CItem;
	public GameObject HItem;
	public float NCHight;
	public float FCNCGap;
	public int CCount;
	public float CGap;
	
	public void SetNCPosition(){
		if (transform.FindChild ("Nomal Contest") != null) {
			transform.FindChild ("Nomal Contest").localPosition = new Vector3 (-360f, (1280f / 2f) - GetComponent<LobbyTopCommander> ().TopHight-GetComponent<LobbyFCCommander>().FCHight);
		} else {
			Debug.Log("The \"Nomal Contest\" does not exist.");
		}
	}
	public void CreateNC(){
		if (transform.FindChild ("Nomal Contest") == null) {
			GameObject NC = (GameObject)Instantiate (NCOrigin, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
			NC.transform.name = "Nomal Contest";
			NC.transform.parent = transform;
			NC.transform.localScale = new Vector3 (1, 1, 1);
			NC.transform.localPosition = new Vector3 (-360f, (1280f * 0.5f) - GetComponent<LobbyTopCommander> ().TopHight-GetComponent<LobbyFCCommander>().FCHight);
			NC.transform.FindChild("BackGround Panel").FindChild ("BackGround").GetComponent<UISprite> ().SetRect (GetComponent<LobbyTopCommander> ().Width - (GetComponent<LobbyTopCommander> ().Gap.x * 2), NCHight-(FCNCGap/2));
			Debug.Log ("Nomal Contest Create Complete");
		} else {
			Debug.Log("The \"Nomal Contest\" already exists.");
		}
		
	}
	public void ResetNC(){
		Init ();
	}
	void Init() {
		#if UNITY_EDITOR_OSX 
		bool option = UnityEditor.EditorUtility.DisplayDialog(
			"Warning!",
			"The traditional \"Nomal Contest\" GameObject is deleted when you reset.",
			"ReSet",
			"Cancle");
		if (option) {
			
			if(transform.FindChild("Nomal Contest")!=null){
				DestroyImmediate(transform.FindChild("Nomal Contest").gameObject);
				Debug.Log("Destroy Nomal Contest");
			}
			CreateNC();
			Debug.Log ("\"Nomal Contest\" Reset Complete");
		} else {
			Debug.Log("Cancle");
		}
		#endif 
	}
	
	public void CreatCItem(){
		int Count = transform.FindChild ("Nomal Contest").FindChild ("Scroll View").childCount;
		for (int i = 0; i<Count; i++) {
			DestroyImmediate(transform.FindChild ("Nomal Contest").FindChild ("Scroll View").GetChild(0).gameObject);
		}
		List<ContestListInfo> List = UserMgr.ContestList;
		if (List != null) {
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Contest").FindChild("Num").GetComponent<UILabel>().text
				= List.Count.ToString();
			
			if(UserMgr.PresetList !=null){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").FindChild("Num").GetComponent<UILabel>().text
					= UserMgr.PresetList.Count.ToString();
			}
			if(UserMgr.HistoryList !=null){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("History").FindChild("Num").GetComponent<UILabel>().text
					= UserMgr.HistoryList.Count.ToString();
			}
			
			List = UserMgr.HistoryList;
			Debug.Log(List.Count);
			for (int i = 0; i<List.Count; i++) {
				GameObject Temp = (GameObject)Instantiate (CItem);
				Temp.transform.parent = transform.FindChild ("History Contest").FindChild ("Scroll View");
				Temp.transform.localScale = new Vector3 (1, 1, 1);
				Temp.transform.localPosition = new Vector3 (0, -(float)i * CGap, 0);
				//                if (i < 3) {
				//                    for (int a = 0; a<Temp.transform.childCount; a++) {
				//                        if (Temp.transform.GetChild (a).name != "BG") {
				//                            Temp.transform.GetChild (a).GetComponent<UISprite> ().color = new Color (1f, 238f / 255f, 253f / 255f, 1f);
				//                        }
				//                    }
				//                }
				Temp.transform.FindChild ("Team").FindChild ("LT").GetComponent<UILabel> ().text = List[i].aTeamName;
				Temp.transform.FindChild ("Team").FindChild ("Score").GetComponent<UILabel> ().text = List[i].aTeamScore  +" : " +List[i].hTeamScore;
				Temp.transform.FindChild ("Team").FindChild ("RT").GetComponent<UILabel> ().text = List[i].hTeamName;
				Temp.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = List[i].contestName;
				Temp.transform.FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = "660/17,241";
				Temp.transform.FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text = "루비 " + List[i].totalEntry.ToString();
				if(List[i].rewardItem == 1){
					Temp.transform.FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = List[i].rewardValue.ToString();
					Temp.transform.FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text = List[i].itemName;
				}else if(List[i].rewardItem == 2){
					Temp.transform.FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = List[i].itemName;
					Temp.transform.FindChild ("Mileage").FindChild ("Label1").transform.localPosition = new Vector3(0,0,0);
					Temp.transform.FindChild ("Mileage").FindChild ("Label2").gameObject.SetActive(false);
				}
				if(List[i].contestStatus == 2){
					Temp.transform.FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "50 / 50s";
				}
				Temp.transform.FindChild ("BG").FindChild ("ContestSeq").GetComponent<UILabel>().text = List[i].contestSeq.ToString();
				Temp.transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel>().text = List[i].gameSeq.ToString();
				Temp.transform.FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel>().text = List[i].totalEntry.ToString();
				Temp.transform.FindChild ("BG").FindChild ("status").GetComponent<UILabel>().text = List[i].contestStatus.ToString();

				Temp.name = "Contest " + i.ToString ();
			}
			
			
			
			

			
			transform.FindChild("Nomal Contest").FindChild("Scroll View2").GetComponent<UIDraggablePanel2>().Init(UserMgr.ContestList.Count, 
			                                                                                                      delegate(UIListItem item, int index) {
				
				if (index < 3) {
					for (int a = 0; a<item.Target.gameObject.transform.childCount; a++) {
						if (item.Target.gameObject.transform.GetChild (a).name != "BG") {
							item.Target.gameObject.transform.GetChild (a).GetComponent<UISprite> ().color = new Color (1f, 238f / 255f, 253f / 255f, 1f);
						}
					}
				}else{
					for (int a = 0; a<item.Target.gameObject.transform.childCount; a++) {
						if (item.Target.gameObject.transform.GetChild (a).name != "BG") {
							item.Target.gameObject.transform.GetChild (a).GetComponent<UISprite> ().color = new Color (1f, 1f, 1f, 1f);
						}
					}
				}
				item.Target.gameObject.transform.FindChild ("Team").FindChild ("LT").GetComponent<UILabel> ().text = UserMgr.ContestList[index].aTeamName;
				item.Target.gameObject.transform.FindChild ("Team").FindChild ("Score").GetComponent<UILabel> ().text = UserMgr.ContestList[index].aTeamScore  +" : " +UserMgr.ContestList[index].hTeamScore;
				item.Target.gameObject.transform.FindChild ("Team").FindChild ("RT").GetComponent<UILabel> ().text = UserMgr.ContestList[index].hTeamName;
				item.Target.gameObject.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = UserMgr.ContestList[index].contestName;
				item.Target.gameObject.transform.FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = UserMgr.ContestList[index].totalPreset+"/"+UserMgr.ContestList[index].totalEntry;
				item.Target.gameObject.transform.FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text = "루비 " + UserMgr.ContestList[index].entryFee.ToString();
				if(UserMgr.ContestList[index].rewardItem < 100){
					
					item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = UserMgr.ContestList[index].totalReward.ToString();
					item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text = UserMgr.ContestList[index].itemName;
				}else if(UserMgr.ContestList[index].rewardItem >= 100){
					item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = UserMgr.ContestList[index].itemName;
					item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label1").transform.localPosition = new Vector3(0,0,0);
					item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label2").gameObject.SetActive(false);
					item.Target.gameObject.transform.FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "50 / 50s";
				}
				//                if(List[i].contestStatus == 2){
				//                    Temp.transform.FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "50 / 50s";
				//                }
				item.Target.gameObject.transform.FindChild ("BG").FindChild ("ContestSeq").GetComponent<UILabel>().text = UserMgr.ContestList[index].contestSeq.ToString();
				item.Target.gameObject.transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel>().text = UserMgr.ContestList[index].gameSeq.ToString();
				item.Target.gameObject.transform.FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel>().text = UserMgr.ContestList[index].totalEntry.ToString();
				item.Target.gameObject.transform.FindChild ("BG").FindChild ("status").GetComponent<UILabel>().text = UserMgr.ContestList[index].contestStatus.ToString();
				item.Target.gameObject.name = "Contest " + index.ToString ();
				
				item.Target.gameObject.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(false);
				item.Target.gameObject.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(false);
				if(UserMgr.ContestList[index].guaranteed == 1){
					item.Target.gameObject.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(true);
				}
				if(UserMgr.ContestList[index].multiEntry > 1){
					item.Target.gameObject.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(true);
				}
			});
			
			transform.FindChild("Nomal Contest").FindChild("Scroll View2").GetComponent<UIDraggablePanel2> ().ResetPosition ();
			
			
			
			
			
		} else {
			for (int i = 0; i<CCount; i++) {
				GameObject Temp = (GameObject)Instantiate (CItem);
				Temp.transform.parent = transform.FindChild ("Nomal Contest").FindChild ("Scroll View");
				Temp.transform.localScale = new Vector3 (1, 1, 1);
				Temp.transform.localPosition = new Vector3 (0, -(float)i * CGap, 0);
				if (i < 3) {
					for (int a = 0; a<Temp.transform.childCount; a++) {
						if (Temp.transform.GetChild (a).name != "BG") {
							Temp.transform.GetChild (a).GetComponent<UISprite> ().color = new Color (1f, 238f / 255f, 253f / 255f, 1f);
						}
					}
				}
				//Temp.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().text = "[2f2f2f]NC  [696969] 0 : 0 [-]  삼성[-]";
				Temp.transform.FindChild ("Team").FindChild ("LT").GetComponent<UILabel> ().text = "NC";
				Temp.transform.FindChild ("Team").FindChild ("Score").GetComponent<UILabel> ().text = "0 : 0";
				Temp.transform.FindChild ("Team").FindChild ("RT").GetComponent<UILabel> ().text = "삼성";
				Temp.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = "박진감 넘치는 토너먼트 경기";
				Temp.transform.FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = "660/17,241";
				Temp.transform.FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text = "루비 1000";
				Temp.transform.FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = "100,000,000";
				
				Temp.name = "Contest " + i.ToString ();
			}
		}
		//transform.FindChild ("Nomal Contest").FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}
	public void NCUpDown(string Value){
		float Y =  (1280f / 2f) - GetComponent<LobbyTopCommander> ().TopHight-GetComponent<LobbyFCCommander>().FCHight;
		if (Value == "Up") {
			transform.FindChild ("Nomal Contest").localPosition = new Vector3 (-360, Y);
		} else if(Value == "Down"){
			transform.FindChild ("Nomal Contest").localPosition = new Vector3 (-360, Y - GetComponent<LobbyAddSub> ().SubHight);
			
		}
		
	}
	ContestDataEvent CDE;
	public void Reset(){
		CDE = new ContestDataEvent (new EventDelegate (this, "ResetNCData"));
		NetMgr.GetContestData (CDE);
	}
	void ResetNCData(){
		GameObject Count = transform.FindChild ("Nomal Contest").FindChild ("Scroll View").gameObject;
		for (int a = 0; a < CDE.Response.data.Count; a++) {

			for (int i = 0; i<Count.transform.childCount; i++) {
				if(CDE.Response.data[a].contestSeq == int.Parse(Count.transform.FindChild ("Contest " + i.ToString ()).
				                                                FindChild("BG").FindChild("ContestSeq").GetComponent<UILabel>().text)){
					
					Count.transform.FindChild ("Contest " + i.ToString ()).FindChild ("Team").FindChild ("Score").GetComponent<UILabel> ().text = CDE.Response.data[a].aTeamScore+" : "+CDE.Response.data[a].hTeamScore;
					Count.transform.FindChild ("Contest " + i.ToString ()).FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = CDE.Response.data[a].totalPreset+" / "+
						Count.transform.FindChild ("Contest " + i.ToString ()).FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel> ().text;
					//                    if(UserMgr.CurrentContestSeq !=2){
					//                    if(CDE.Response.data[a].contestStatus == 2){
					//                            UserMgr.CurrentContestSeq = CDE.Response.data[a].contestStatus;
					//                            transform.FindChild("PreSet Contest").GetComponent<PresetContestCommander>().CreatItem();
					//                    }
					//                    }
					
				}
				
			}

	
			UserMgr.ContestList[a].aTeamScore = CDE.Response.data[a].aTeamScore;
			UserMgr.ContestList[a].hTeamScore = CDE.Response.data[a].hTeamScore;
			UserMgr.ContestList[a].totalPreset = CDE.Response.data[a].totalPreset;
		}
		Debug.Log("CDE.Response.data.Count : " + CDE.Response.data.Count);
		Debug.Log("UserMgr.ContestList.Count : " + UserMgr.ContestList.Count);
	}
}