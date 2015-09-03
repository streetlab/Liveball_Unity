using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LobbyNCCommander : MonoBehaviour {
	public GameObject NCOrigin;
	public GameObject CItem;
	//public GameObject HItem;
	public float NCHight;
	public float FCNCGap;
	public int CCount;
	public float CGap;

	public static UIDraggablePanel2 mNormalList;
	//노말 컨테스트 생성,관리부분(현재 사용되지않음)
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

		//featured
		infoList.Sort(delegate(ContestListInfo x, ContestListInfo y) {
			return y.featured.CompareTo(x.featured);
		});



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
			if(UserMgr.ContestList.Count==0){
				transform.FindChild("Nomal Contest").FindChild("None").gameObject.SetActive(true);
			}else{
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Contest").FindChild("Num").GetComponent<UILabel>().text
				= UserMgr.ContestList.Count.ToString();
			
		

			List<ContestListInfo> sortedList = GetSortedList();

			mNormalList.Init(sortedList.Count, delegate(UIListItem item, int index) {
				InitList(sortedList, item, index);
			});
			}
		}
		if(UserMgr.PresetList !=null){
			if(UserMgr.PresetList.Count==0){
				transform.FindChild("PreSet Contest").FindChild("None").gameObject.SetActive(true);
			}else{


			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").FindChild("Num").GetComponent<UILabel>().text
				= UserMgr.PresetList.Count.ToString();
			}
		}

		if(UserMgr.HistoryList !=null){

			if(UserMgr.HistoryList.Count==0){

				transform.FindChild("History Contest").FindChild("None").gameObject.SetActive(true);
			}else{
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("History").FindChild("Num").GetComponent<UILabel>().text
					= UserMgr.HistoryList.Count.ToString();
			}
		}
		//transform.FindChild ("Nomal Contest").FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}

	static void InitList(List<ContestListInfo> infoList, UIListItem item, int index){







		if (infoList[index].featured == 1) {
	
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").GetComponent<UISprite> ().color = new Color (1f, 1f, 240f / 255f, 1f);
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").GetComponent<UISprite> ().color = new Color (1f, 1f, 240f / 255f, 1f);
			item.Target.gameObject.transform.FindChild("NewTop").GetComponent<UISprite> ().color = new Color (1f, 1f, 240f / 255f, 1f);
			item.Target.gameObject.transform.FindChild("OldBot").GetComponent<UISprite> ().color = new Color (1f, 1f, 240f / 255f, 1f);
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = new Color (16f/255f, 140f/255f, 187f / 255f, 1f);
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = new Color (16f/255f, 140f/255f, 187f / 255f, 1f);
			item.Target.gameObject.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().color = new Color (16f/255f, 140f/255f, 187f / 255f, 1f);
			if (infoList [index].totalEntry == infoList [index].totalPreset) {
				item.Target.GetComponent<UIButton>().enabled = false;
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().color = new Color(155f/255f,155f/255f,155f/255f,1);
			} else {
				item.Target.GetComponent<UIButton>().enabled = true;
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(16f/255f, 140f/255f, 187f / 255f, 1f);
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(16f/255f, 140f/255f, 187f / 255f, 1f);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(16f/255f, 140f/255f, 187f / 255f, 1f);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(16f/255f, 140f/255f, 187f / 255f, 1f);
				item.Target.gameObject.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().color = new Color(16f/255f, 140f/255f, 187f / 255f, 1f);
			}

		} else{

			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").GetComponent<UISprite> ().color = new Color (1f, 1f, 1f, 1f);
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").GetComponent<UISprite> ().color = new Color (1f, 1f, 1f, 1f);
			item.Target.gameObject.transform.FindChild("NewTop").GetComponent<UISprite> ().color = new Color (1f, 1f, 1f, 1f);
			item.Target.gameObject.transform.FindChild("OldBot").GetComponent<UISprite> ().color = new Color (1f, 1f, 1f, 1f);
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = new Color (146f/255f, 39f/255f, 143f / 255f, 1f);
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = new Color (146f/255f, 39f/255f, 143f / 255f, 1f);
			item.Target.gameObject.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().color =  new Color (146f/255f, 39f/255f, 143f / 255f, 1f);

			if (infoList [index].totalEntry == infoList [index].totalPreset) {
				item.Target.GetComponent<UIButton>().enabled = false;
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(155f/255f,155f/255f,155f/255f,1);
				item.Target.gameObject.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().color = 	new Color(155f/255f,155f/255f,155f/255f,1);
			} else {
				item.Target.GetComponent<UIButton>().enabled = true;
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(146f/255f,39f/255f,143f/255f,1);
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(146f/255f,39f/255f,143f/255f,1);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
					new Color(146f/255f,39f/255f,143f/255f,1);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
					new Color(146f/255f,39f/255f,143f/255f,1);
				item.Target.gameObject.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().color = 	new Color(146f/255f,39f/255f,143f/255f,1);

			}
		
		}

		item.Target.gameObject.transform.FindChild ("Team").FindChild ("Label").GetComponent<UILabel> ().text = "[b]" + infoList [index].aTeamName + " : " + infoList [index].hTeamName;
		item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().text = "[b]"+infoList[index].contestName+"";
		item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().text = "[b]"+infoList[index].contestName+"";

		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().text = "[b]"+infoList[index].contestName+"";
		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().text = "[b]"+infoList[index].contestName+"";

		item.Target.gameObject.transform.FindChild("OldBot").FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = "[b]"+infoList[index].totalPreset+"[/b] / "+infoList[index].totalEntry;
		item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text = "[b]"+infoList[index].entryFee.ToString();

		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = "[b]"+infoList[index].totalPreset+"[/b] / "+infoList[index].totalEntry;
		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text = "[b]"+infoList[index].entryFee.ToString();
		if(infoList[index].rewardItem < 100){					
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = "[b]"+infoList[index].totalReward.ToString();
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text = "[b]"+infoList[index].itemName;

			item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = "[b]"+infoList[index].totalReward.ToString();
			item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text = "[b]"+infoList[index].itemName;
		} else if(infoList[index].rewardItem >= 100){
			item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = "[b]"+infoList[index].itemName;
			item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Mileage").FindChild ("Label1").transform.localPosition = new Vector3(0,0,0);
			item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Mileage").FindChild ("Label2").gameObject.SetActive(false);

			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text = "[b]"+infoList[index].itemName;
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Mileage").FindChild ("Label1").transform.localPosition = new Vector3(0,0,0);
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Mileage").FindChild ("Label2").gameObject.SetActive(false);
//			item.Target.gameObject.transform.FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "50 / 50s";
		}

		if(infoList[index].contestType == 1){
			item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "[b]50 / 50s";
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "[b]50 / 50s";
		}else{
			item.Target.gameObject.transform.FindChild("OldBot").FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "[b]Ranking";
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("Ranking").FindChild("Label").GetComponent<UILabel>().text = "[b]Ranking";
		}

		item.Target.gameObject.transform.FindChild ("BG").FindChild ("ContestSeq").GetComponent<UILabel>().text = infoList[index].contestSeq.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel>().text = infoList[index].gameSeq.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel>().text = infoList[index].totalEntry.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("TotalPreset").GetComponent<UILabel>().text = infoList[index].totalPreset.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("status").GetComponent<UILabel>().text = infoList[index].contestStatus.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("MultiEntry").GetComponent<UILabel>().text = infoList[index].multiEntry.ToString();

		item.Target.gameObject.transform.FindChild ("BG").FindChild ("Lteam").GetComponent<UILabel>().text = infoList[index].aTeamName.ToString();
		item.Target.gameObject.transform.FindChild ("BG").FindChild ("Rteam").GetComponent<UILabel>().text = infoList[index].hTeamName.ToString();
		item.Target.gameObject.name = "Contest " + index.ToString ();


	

		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(false);
		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitleIcon").FindChild("M").gameObject.SetActive(false);
		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (false);
		item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitleIcon").FindChild ("G").localPosition = new Vector3 (188.5f,0,0);

		item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(false);
		item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitleIcon").FindChild("M").gameObject.SetActive(false);
		item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitleIcon").FindChild("L").gameObject.SetActive(false);
		item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitleIcon").FindChild ("G").localPosition = new Vector3 (188.5f,0,0);


		if (infoList [index].multiEntry > 1 && infoList [index].guaranteed == 1) {
			item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("M").gameObject.SetActive (true);
			item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("M").gameObject.SetActive (true);

			if (infoList [index].contestType == 5) {
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
			}
		} else if (infoList [index].multiEntry > 1 && infoList [index].guaranteed != 1) {

			item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("M").gameObject.SetActive (true);
			item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("M").gameObject.SetActive (true);
			if (infoList [index].contestType == 5) {
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("L").localPosition = new Vector3 (188.5f, 0, 0);
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("L").localPosition = new Vector3 (188.5f, 0, 0);
			}
		} else {
			//Debug.Log(infoList [index].contestType + " : " +infoList [index].guaranteed );
			if (infoList [index].contestType == 5&& infoList [index].guaranteed == 1) {
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("G").localPosition = new Vector3 (228.5f, 0, 0);
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("G").localPosition = new Vector3 (228.5f, 0, 0);
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("L").localPosition = new Vector3 (188.5f, 0, 0);
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("L").localPosition = new Vector3 (188.5f, 0, 0);
			}else if(infoList [index].contestType == 5&& infoList [index].guaranteed != 1){
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("L").gameObject.SetActive (true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("L").localPosition = new Vector3 (228.5f, 0, 0);
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("L").localPosition = new Vector3 (228.5f, 0, 0);
			}else{
				item.Target.gameObject.transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("NewTop").FindChild ("TitleIcon").FindChild("G").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitleIcon").FindChild ("G").localPosition = new Vector3 (228.5f, 0, 0);
				item.Target.gameObject.transform.FindChild ("NewTop").FindChild ("TitleIcon").FindChild ("G").localPosition = new Vector3 (228.5f, 0, 0);
			}
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

	public void ResetInBackground(){
		//데이터 새로고침
		//NomalContest.cs에서 사용
		CDE = new ContestDataEvent (new EventDelegate (this, "ResetNCData"));
		NetMgr.GetContestDataInBackground (CDE);
	}
	public void ResetNonData(){
		StartCoroutine(Rolliing());
	}

	void getNCData(){
		UserMgr.ContestList = CLE.Response.data;
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Contest").FindChild ("Num")
			.GetComponent<UILabel> ().text = UserMgr.ContestList.Count.ToString();



		ResetList();
	}


	ContestListEvent CLE;

	void ResetNCData(){
		//기존 UserMgr.ContestList 새로고침
		try {
			Debug.Log ("CDE.Response.data.Count : " + CDE.Response.data.Count);
			Debug.Log ("UserMgr.ContestList.Count : " + UserMgr.ContestList.Count);
			if (UserMgr.ContestList != null) {
				if (UserMgr.ContestList.Count != CDE.Response.data.Count) {
					CLE = new ContestListEvent (new EventDelegate (this, "getNCData"));
					NetMgr.GetContestList (CLE);
				} else {
			
					GameObject Count = transform.FindChild ("Nomal Contest").FindChild ("Scroll View2").gameObject;
					for (int a = 0; a < CDE.Response.data.Count; a++) {

						for (int i = 0; i<UserMgr.ContestList.Count; i++) {
							if(UserMgr.ContestList[i].contestSeq == CDE.Response.data[a].contestSeq){

								UserMgr.ContestList [i].aTeamScore = CDE.Response.data [a].aTeamScore;
								UserMgr.ContestList [i].hTeamScore = CDE.Response.data [a].hTeamScore;
								UserMgr.ContestList [i].totalPreset = CDE.Response.data [a].totalPreset;

							}

				
						}

	

					}
				
					//ResetList ();
				}


			}
			
			StartCoroutine(Rolliing());
		} catch {
			Debug.Log ("ArgumentOutOfRangeException: Argument is out of range.");

		}
	}
	//Contest 
	IEnumerator Rolliing(){
		Debug.Log ("Rolliing");
		GameObject Count = transform.FindChild ("Nomal Contest").FindChild ("Scroll View2").gameObject;
		for (int i = 0; i<Count.transform.childCount-2; i++) {
			for(int a = 0; a<UserMgr.ContestList.Count; a++){
			//Debug.Log ("i : "+i);
			//Debug.Log((int.Parse(Count.transform.GetChild(i+2).name.Replace("Contest ","").Replace("item index:",""))));
				if(int.Parse(Count.transform.GetChild(i+2).FindChild ("BG").FindChild("ContestSeq").GetComponent<UILabel>().text)==UserMgr.ContestList[a].contestSeq){
			Count.transform.GetChild(i+2).FindChild("ChangeTopBot").FindChild("NewBot").FindChild ("RankingValue").FindChild ("Label").GetComponent<UILabel> ().text = "[b]"+UserMgr.ContestList [a].totalPreset + "[/b] / " +
				Count.transform.GetChild(i+2).FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel> ().text;
					Count.transform.GetChild(i+2).FindChild ("BG").FindChild ("TotalPreset").GetComponent<UILabel> ().text =UserMgr.ContestList [a].totalPreset.ToString();
			if (Count.transform.GetChild(i+2).FindChild ("BG").FindChild ("TotalPreset").GetComponent<UILabel> ().text == Count.transform.GetChild(i+2).FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel> ().text) {
				Count.transform.GetChild(i+2).GetComponent<UIButton> ().enabled = false;
				Count.transform.GetChild(i+2).FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color = 
													new Color (155f / 255f, 155f / 255f, 155f / 255f, 1);
				Count.transform.GetChild(i+2).FindChild("NewTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color = 
													new Color (155f / 255f, 155f / 255f, 155f / 255f, 1);


											} 
			StartCoroutine(ItemRoll(Count.transform.GetChild(i+2).gameObject));

			yield return new WaitForSeconds(0.5f);
				}
			}

		}
	}
	IEnumerator ItemRoll(GameObject G){
		for (int i = 0; i < 8; i++) {

			if(i >= 3){
				G.transform.FindChild ("ChangeTopBot").localRotation =  Quaternion.Euler(((float)i+1f)*22.5f, 180f, 180f);
				G.transform.FindChild ("ChangeTopBot").FindChild("NewBot").gameObject.SetActive(true);
				G.transform.FindChild ("ChangeTopBot").FindChild("OldTop").gameObject.SetActive(false);
			}else{
				G.transform.FindChild ("ChangeTopBot").localRotation =  Quaternion.Euler(((float)i+1f)*22.5f, 0, 0);
			}
			yield return new WaitForSeconds (0.02f);
		}
		G.transform.FindChild ("ChangeTopBot").localRotation = Quaternion.Euler(0, 0, 0);
		G.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color =
G.transform.FindChild ("NewTop").FindChild ("TitlePanel").FindChild ("Title1").GetComponent<UILabel> ().color;
		G.transform.FindChild ("ChangeTopBot").FindChild ("OldTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color =
			G.transform.FindChild ("NewTop").FindChild ("TitlePanel").FindChild ("Title2").GetComponent<UILabel> ().color;
	
		G.transform.FindChild ("OldBot").FindChild("RankingValue").FindChild("Label").GetComponent<UILabel> ().text = 
			G.transform.FindChild ("ChangeTopBot").FindChild ("NewBot").FindChild("RankingValue").FindChild("Label").GetComponent<UILabel> ().text;
		G.transform.FindChild ("ChangeTopBot").FindChild("NewBot").gameObject.SetActive(false);
		G.transform.FindChild ("ChangeTopBot").FindChild("OldTop").gameObject.SetActive(true);

	}

}