using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;  
public class LobbyMainCommander : MonoBehaviour {
	public static int MenuStatus = 1; 
	public float RatioTop;
	public float RatioFC;
	public float RatioNC;
	public float RatioBot;
	public AudioClip mAudioWelcome;
	Dictionary<string,float> HightList = new Dictionary<string, float> ();
	checkRecentMessageEvent CRME;
	void CheckRecent(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Cancel) {
			//접속 보상을 받았을시 , 접속 보상을 받아야 할 경우 컨테스트 보상보다 접속보상 팝업이 먼저뜸
			CheckRecentMaessga();
		
		}
	}
	public void CheckRecentMaessga(){
	
			StartCoroutine (CheckingRencet ());

	}
	int EnableCount  = 0;
	void OnEnable(){
		//최초 실행 이후 재실행관련
		if (EnableCount != 0) {
			StartCoroutine("CheckingRencet");
		}
		EnableCount++;
	}
	IEnumerator CheckingRencet(){
		while (true) {
			CRME = new checkRecentMessageEvent (new EventDelegate (this, "checkRecent"));
			NetMgr.CheckRecentMessage (CRME);
			yield return new WaitForSeconds(60f);
		}
	}
	void checkRecent(){

		UserMgr.cntRewardContest = CRME.Response.data.cntRewardContest;
		if (UserMgr.cntRewardContest == 1) {
		
			DialogueMgr.ShowDialogue ("컨테스트 보상 지급", "진행했던 컨테스트 보상이\n[우편함]으로 지급되었습니다." , DialogueMgr.DIALOGUE_TYPE.YesNo ,"보상 확인","","닫기", gotopost);
			//popup
		
		}
	}

	void gotopost(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			transform.root.FindChild("Scroll").FindChild("Bot").FindChild("BtnPost").GetComponent<PostButton>().PowerOn();
		//go to post
		}
		
	}
	void CheckFirst(){

			if (UserMgr.UserInfo != null) {
				if (UtilMgr.IsFirstLanding) {
					UtilMgr.IsFirstLanding = false;
					transform.root.GetComponent<AudioSource> ().PlayOneShot (mAudioWelcome);
					
					CheckAttendance ();
				}
			}


	

	}
	
	void CheckAttendance(){
		WWW www = new WWW(Constants.EXT_SERVER_HOST+Constants.URL_ATTENDANCE+UserMgr.UserInfo.memSeq);
		//		Debug.Log ("Constants.URL_ATTENDANCE+UserMgr.UserInfo.memSeq : " + Constants.URL_ATTENDANCE+UserMgr.UserInfo.memSeq);
		StartCoroutine(RunAttendance(www));
		UtilMgr.ShowLoading(true);
	}
	
	class DailyReward{
		int _result;
		
		public int result {
			get {
				return _result;
			}
			set {
				_result = value;
			}
		}
		
		string _message;
		
		public string message {
			get {
				return _message;
			}
			set {
				_message = value;
			}
		}

		string _legend;

		public string legend {
			get {
				return _legend;
			}
			set {
				_legend = value;
			}
		}
	}
	
	IEnumerator RunAttendance(WWW www){
		yield return www;
		
		UtilMgr.DismissLoading();
		if(www.error != null){
			DialogueMgr.ShowDialogue("attendance error", www.error, DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else{
			Debug.Log("Attendance : " + www.text);
			if(www.text != null && www.text.Length > 0){
				//				mWebview.SetActive(true);
				//				mWebview.GetComponent<ScriptGameWebview>().GoTo(www.text);
				DailyReward dReward = Newtonsoft.Json.JsonConvert.DeserializeObject<DailyReward>(www.text);
				if(dReward.result == 200){
					DialogueMgr.ShowDialogue("접속보상", dReward.message, DialogueMgr.DIALOGUE_TYPE.Alert, CheckRecent);
					Debug.Log("add");
					if (Application.loadedLevelName.Equals ("SceneMain 1")) {
						Debug.Log("add Main");
						transform.root.FindChild("GameObject").FindChild("Top").FindChild("Panel").FindChild("BtnPost").GetComponent<PostButton>().YellowOn();
					}
					
				}else{
					CheckRecentMaessga();
				}

				if(dReward.legend != null && dReward.legend.Length > 0){
					//show n link url
					UserMgr.legend = dReward.legend;
				} else{
					//unshow
//					transform.root.FindChild("Scroll").FindChild("Main").FindChild("Nomal Contest")
//						.FindChild("EventPanel").gameObject.SetActive(false);
//					transform.root.FindChild("Scroll").FindChild("Main").FindChild("Nomal Contest").FindChild("Scroll View2")
//						.GetComponent<UIPanel>().SetRect(720f,1038f);
//					transform.root.FindChild("Scroll").FindChild("Main").FindChild("Nomal Contest").FindChild("Scroll View2")
//						.localPosition += new Vector3(0,75f,0);
//					transform.root.FindChild("Scroll").FindChild("Main").FindChild("Nomal Contest").FindChild("Scroll View2")
//						.GetComponent<UIDraggablePanel2>().ResetPosition();
				}
			} else{
				//				Debug.Log("Attendance is already done");
			}
		}
	}

//	public string Base64Decode(string base64EncodedData) {
//		var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
//		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
//	}

	// Use this for initialization
	void Awake(){
		//Debug.Log ("Base64Decode : " + Base64Decode("64+Z7J287ZWcIO2ctOuMgO2PsOuyiO2YuOuhnCDrs7jsnbjsnbjspp3smpTssq0="));
		//최초 실행시 초기화 관련
		if (UserMgr.ContestStatus == 2) {
			transform.FindChild("Top").FindChild("Preset").FindChild("Label").GetComponent<UILabel>().text = "라이브";
		}
		MenuStatus = 1; 
		for (int i = 0; i<GetComponent<LobbyTopCommander>().mTopMenuName.Length; i++) {
			transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Bar").gameObject.SetActive(false);
			transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
			transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Num").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
		}
		transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[0]).FindChild("Bar").gameObject.SetActive(true);
		transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[0]).FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
		transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[0]).FindChild("Num").GetComponent<UILabel>().color = new Color(1,1,1,1);
		for (int i = 0; i<GetComponent<LobbyAddSub>().SubMenuName.Length; i++) {
			//                        transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild("Label")
			//                            .FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").FindChild("Menu 0").
			//                                gameObject.SetActive(true);
			//            transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild("Label")
			//                .FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").FindChild("Menu 0").
			//                    gameObject.SetActive(false);
			
			transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild(
				GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").gameObject.SetActive(false);
		}
		if (transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("Arrow") != null) {
			transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("Arrow").
				GetComponent<UISprite>().color = Color.yellow;
			transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("white").gameObject
				.SetActive(true);
			transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("yellow").gameObject
				.SetActive(true);
		}
		transform.FindChild ("Top").FindChild ("Sub").FindChild("BG_B").gameObject.SetActive (false);
		transform.FindChild ("Top").FindChild ("Sub").gameObject.SetActive (false);
		transform.FindChild ("Gift").gameObject.SetActive (true);
		transform.FindChild ("Gift").FindChild ("GiftButton").GetComponent<Gift> ().Off ();
		transform.FindChild ("Gift").FindChild ("GiftButton").GetComponent<Gift> ().Button ();
		GetComponent<LobbyNCCommander> ().CreateCItem ();
		transform.FindChild ("Nomal Contest").gameObject.SetActive (true);
		transform.FindChild ("PreSet Contest").GetComponent<PresetContestCommander> ().CreatItem ();
		transform.FindChild ("PreSet Contest").gameObject.SetActive (false);

		transform.parent.FindChild("GameInfo").gameObject.SetActive(false);
		LobbyBotCommander.mBtnState = LobbyBotCommander.BtmState.Main;

		//접속 보상 , 컨테스트 보상 체크
		CheckFirst ();

	}
	void CSComplete(){
		Debug.Log("CSComplete");
	}
	//메뉴 생성,관리부분(현재 사용되지않음)
	public void Ratio () {
		try{
			HightList.Add ("Top",GetComponent<LobbyTopCommander>().TopHight);
			HightList.Add ("FC",GetComponent<LobbyFCCommander>().FCHight);
			HightList.Add ("NC",GetComponent<LobbyNCCommander>().NCHight);
			HightList.Add ("Bot",GetComponent<LobbyBotCommander>().BotHeight);
		}catch{
			Debug.Log("The \"HightList\" already exists.");
		}
		float Sum = RatioTop + RatioFC + RatioNC + RatioBot;
		GetComponent<LobbyTopCommander>().TopHight = (RatioTop / Sum)*1280f;
		GetComponent<LobbyFCCommander>().FCHight = (RatioFC / Sum)*1280f;
		GetComponent<LobbyNCCommander>().NCHight = (RatioNC / Sum)*1280f;
		GetComponent<LobbyBotCommander>().BotHeight = (RatioBot / Sum)*1280f;
		Debug.Log ("Ratio Setting complete");
		
	}
	public void HightListClear(){
		HightList.Clear ();
		Debug.Log ("HightList Clear");
	}
	public void GetHightList(){
		if (HightList.Count > 0) {
			GetComponent<LobbyTopCommander>().TopHight = HightList["Top"];
			GetComponent<LobbyFCCommander>().FCHight = HightList["FC"];
			GetComponent<LobbyNCCommander>().NCHight = HightList["NC"];
			GetComponent<LobbyBotCommander>().BotHeight = HightList["Bot"];
			Debug.Log("HightList Load");
		} else {
			Debug.Log("HightList is empty");
		}
	}
	
	
	public void AllCreate(){
		GetComponent<LobbyTopCommander> ().CreateTop ();
		GetComponent<LobbyFCCommander> ().CreateFC ();
		GetComponent<LobbyNCCommander> ().CreateNC ();
		GetComponent<LobbyBotCommander> ().CreateBot ();
		Debug.Log ("ALL Create Complete");
	}
	
	public void AllReset(){
		Init ();
		
	}
	// Update is called once per frame
	
	void Init() {
		#if UNITY_EDITOR_OSX 
		bool option = UnityEditor.EditorUtility.DisplayDialog(
			"Warning!",
			"The traditional ALL GameObject is deleted when you reset.",
			"ReSet",
			"Cancle");
		if (option) {
			
			if(transform.FindChild("Top")!=null){
				DestroyImmediate(transform.FindChild("Top").gameObject);
				Debug.Log("Destroy Top");
			}
			if(transform.FindChild("Featured Contest")!=null){
				DestroyImmediate(transform.FindChild("Featured Contest").gameObject);    
				Debug.Log("Destroy Featured Contest");
			}
			if(transform.FindChild("Nomal Contest")!=null){
				DestroyImmediate(transform.FindChild("Nomal Contest").gameObject);    
				Debug.Log("Destroy Nomal Contest");
			}
			if(transform.FindChild("Bot")!=null){
				DestroyImmediate(transform.FindChild("Bot").gameObject);
				Debug.Log("Destroy Bot");
			}
			AllCreate();
			Debug.Log ("ALL Reset Complete");
		} else {
			Debug.Log("Cancle");
		}
		#endif 
	}
	
}
