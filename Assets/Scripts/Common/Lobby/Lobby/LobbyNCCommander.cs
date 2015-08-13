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

	public static UIDraggablePanel2 mNormalList;
	
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

	static List<ContestListInfo> GetSortedList(){
		List<ContestListInfo> infoList = new List<ContestListInfo>();

		if(SubInSub.SelectedTeamname.Equals("모든 팀")){
			foreach(ContestListInfo cInfo in UserMgr.ContestList) infoList.Add(cInfo);
		} else{
			foreach(ContestListInfo cInfo in UserMgr.ContestList){
				if(cInfo.aTeamName.Equals(SubInSub.SelectedTeamname)
				   || cInfo.hTeamName.Equals(SubInSub.SelectedTeamname))
					infoList.Add(cInfo);
			}
		}

		if(SubInSub.SelectedKind == 1){//ranking
			for(int i = 0; i < infoList.Count; i++){
				ContestListInfo cInfo = infoList[i];
				if(cInfo.contestType == 1){
					infoList.RemoveAt(i);
					i--;
				}
			}
		} else if(SubInSub.SelectedKind == 2){//50
			for(int i = 0; i < infoList.Count; i++){
				ContestListInfo cInfo = infoList[i];
				if(cInfo.contestType == 2){
					infoList.RemoveAt(i);
					i--;
				}
			}
		}
//		Debug.Log("SubInSub.SelectedFeeLow is "+SubInSub.SelectedFeeLow);
//		Debug.Log("SubInSub.SelectedFeeHigh is "+SubInSub.SelectedFeeHigh);
		if(SubInSub.SelectedFeeLow > 0){
			for(int i = 0; i < infoList.Count; i++){
				ContestListInfo cInfo = infoList[i];
				if(cInfo.entryFee < SubInSub.SelectedFeeLow){
					infoList.RemoveAt(i);
					i--;
				}
			}
		}
		if(SubInSub.SelectedFeeHigh < 1000){
			for(int i = 0; i < infoList.Count; i++){
				ContestListInfo cInfo = infoList[i];
				if(cInfo.entryFee > SubInSub.SelectedFeeHigh){
					infoList.RemoveAt(i);
					i--;
				}
			}
		}

		if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.PeopleAsc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return x.totalPreset.CompareTo(y.totalPreset);
			});
		} else if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.PeopleDesc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return y.totalPreset.CompareTo(x.totalPreset);
			});
		} else if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.NameAsc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return x.contestName.CompareTo(y.contestName);
			});
		} else if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.NameDesc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return y.contestName.CompareTo(x.contestName);
			});
		} else if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.TotalAsc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return x.totalEntry.CompareTo(y.totalEntry);
			});
		} else if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.TotalDesc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return y.totalEntry.CompareTo(x.totalEntry);
			});
		} else if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.FeeAsc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return x.entryFee.CompareTo(y.entryFee);
			});
		} else if(SubInSub.SelectedPeople == SubInSub.PeopleSorting.FeeDesc){
			infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
				return y.entryFee.CompareTo(x.entryFee);
			});
		}



		return infoList;
	}

	public static void ResetList(){
		if(mNormalList == null){
			Debug.Log("NormalList is null");
			return;
		}

		mNormalList.RemoveAll();

		List<ContestListInfo> infoList = GetSortedList();

		mNormalList.Init(infoList.Count, delegate(UIListItem item, int index) {
			InitList(infoList, item, index);
		});
		
		mNormalList.ResetPosition ();
	}
	
	public void CreateCItem(){
//		int Count = transform.FindChild ("Nomal Contest").FindChild ("Scroll View").childCount;
//		for (int i = 0; i<Count; i++) {
//			DestroyImmediate(transform.FindChild ("Nomal Contest").FindChild ("Scroll View").GetChild(0).gameObject);
//		}
		SubInSub.SelectedTeamname = "모든 팀";
		SubInSub.SelectedKind = 0;
		SubInSub.SelectedFeeLow = 0;
		SubInSub.SelectedFeeHigh = 1000;
		SubInSub.SelectedPeople = SubInSub.PeopleSorting.PeopleAsc;
		mNormalList = transform.FindChild("Nomal Contest").FindChild("Scroll View2").GetComponent<UIDraggablePanel2>();
		if (UserMgr.ContestList != null) {
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Contest").FindChild("Num").GetComponent<UILabel>().text
				= UserMgr.ContestList.Count.ToString();
			
			if(UserMgr.PresetList !=null){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").FindChild("Num").GetComponent<UILabel>().text
					= UserMgr.PresetList.Count.ToString();
			}
			if(UserMgr.HistoryList !=null){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("History").FindChild("Num").GetComponent<UILabel>().text
					= UserMgr.HistoryList.Count.ToString();
			}

			List<ContestListInfo> sortedList = GetSortedList();

			mNormalList.Init(sortedList.Count, delegate(UIListItem item, int index) {
				InitList(sortedList, item, index);
			});
		}
		//transform.FindChild ("Nomal Contest").FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}

	static void InitList(List<ContestListInfo> infoList, UIListItem item, int index){
		if (index < 3) {
			for (int a = 0; a<item.Target.gameObject.transform.childCount; a++) {
				if (item.Target.gameObject.transform.GetChild (a).name != "BG") {
					item.Target.gameObject.transform.GetChild (a).GetComponent<UISprite> ().color = new Color (1f, 238f / 255f, 253f / 255f, 1f);
				}
			}
		} else{
			for (int a = 0; a<item.Target.gameObject.transform.childCount; a++) {
				if (item.Target.gameObject.transform.GetChild (a).name != "BG") {
					item.Target.gameObject.transform.GetChild (a).GetComponent<UISprite> ().color = new Color (1f, 1f, 1f, 1f);
				}
			}
		}

		item.Target.gameObject.transform.FindChild ("Team").FindChild ("LT").GetComponent<UILabel> ().text = infoList[index].aTeamName;
		item.Target.gameObject.transform.FindChild ("Team").FindChild ("Score").GetComponent<UILabel> ().text = infoList[index].aTeamScore  +" : " +infoList[index].hTeamScore;
		item.Target.gameObject.transform.FindChild ("Team").FindChild ("RT").GetComponent<UILabel> ().text = infoList[index].hTeamName;
		item.Target.gameObject.transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = infoList[index].contestName;
		item.Target.gameObject.transform.FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = infoList[index].totalPreset+"/"+infoList[index].totalEntry;
		item.Target.gameObject.transform.FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text = "루비 " + infoList[index].entryFee.ToString();
		if(infoList[index].rewardItem < 100){					
			item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = infoList[index].totalReward.ToString();
			item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text = infoList[index].itemName;
		} else if(infoList[index].rewardItem >= 100){
			item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = infoList[index].itemName;
			item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label1").transform.localPosition = new Vector3(0,0,0);
			item.Target.gameObject.transform.FindChild ("Mileage").FindChild ("Label2").gameObject.SetActive(false);
//			item.Target.gameObject.transform.FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "50 / 50s";
		}

		if(infoList[index].contestType == 1){
			item.Target.gameObject.transform.FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "50 / 50s";
		} else{
			item.Target.gameObject.transform.FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "Ranking";
		}

		item.Target.gameObject.transform.FindChild ("BG").FindChild ("ContestSeq").GetComponent<UILabel>().text = infoList[index].contestSeq.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel>().text = infoList[index].gameSeq.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel>().text = infoList[index].totalEntry.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("status").GetComponent<UILabel>().text = infoList[index].contestStatus.ToString();
		item.Target.gameObject.name = "Contest " + index.ToString ();
		
		item.Target.gameObject.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(false);
		item.Target.gameObject.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(false);
		if(infoList[index].guaranteed == 1){
			item.Target.gameObject.transform.FindChild ("Title").FindChild("G").gameObject.SetActive(true);
		}
		if(infoList[index].multiEntry > 1){
			item.Target.gameObject.transform.FindChild ("Title").FindChild("M").gameObject.SetActive(true);
		}
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
		try{
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
		}catch{
			Debug.Log("ArgumentOutOfRangeException: Argument is out of range.");
		}

		ResetList();
	}
}