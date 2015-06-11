using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
using System.IO;

public class ScriptMainTop : MonoBehaviour {

	public GameObject mHighlight;
	public GameObject mLineup;
	public GameObject mBingo;
	public GameObject mLivetalk;
	public GameObject mBetting;
	public GameObject mQuizResultPopup;

	public GameObject mMatchInfoTop;

	public GameObject mBtnHighlight;
	public GameObject mBtnLineup;
	public GameObject mBtnBingo;
	public GameObject mBtnLivetalk;

	public GameObject mLblGold;
	public GameObject mLblRuby;
	public GameObject mLblDia;

	public GameObject mLblNewGold;

	public GameObject mLblNewRuby;
	public GameObject mLblNewDia;



	public AudioClip mSoundOpenBet;
	public AudioClip mSoundCloseBet;
	public AudioClip mAudioWelcome;

	public string mStrLive;
	public GameObject gameobj;
	public GameObject mWebview;

	public static int LandingState=4;


	GetQuizEvent mEventQuiz;
	GetGameSposDetailBoardEvent mBoardEvent;
	GetSimpleResultEvent mSimpleEvent;
	JoinQuizEvent mJoinQuizEvent;

	static SposDetailBoard detailBoard;
	public static SposDetailBoard DetailBoard{
		get{return detailBoard;}
		set{detailBoard = value;}
	}

	enum STATE{
		Highlight,
		Lineup,
		Bingo,
		Livetalk,
		Betting
	};

	STATE mState = STATE.Highlight;
	static GetScheduleEvent mScheduleEvent;
	void Start () {
		QuizMgr.EnterMain(this);
		if (LandingState == 4) {
			LandingState = 0;
		
			transform.FindChild ("TopInfoItem").FindChild ("BtnMenu 1").gameObject.SetActive (false);
			mBtnHighlight.GetComponent<UIButton> ().isEnabled = false;
			mHighlight.SetActive (false);
			mLineup.SetActive (false);
			mBingo.SetActive (false);
			mLivetalk.SetActive (false);
			mBetting.SetActive (false);
			gameobj.SetActive (false);
			QuizMgr.EnterMain (this);



			mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "SetSchedule"));
			NetMgr.GetScheduleAll (mScheduleEvent);



		} else if (LandingState == 0) {
			transform.FindChild ("TopInfoItem").FindChild ("BtnMenu 1").gameObject.SetActive (false);
			mBtnHighlight.GetComponent<UIButton> ().isEnabled = false;
			mHighlight.SetActive (false);
			mLineup.SetActive (false);
			mBingo.SetActive (false);
			mLivetalk.SetActive (false);
			mBetting.SetActive (false);
			gameobj.SetActive (false);
			QuizMgr.EnterMain (this);
			
			
			
			mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "SetSchedule1"));
			NetMgr.GetScheduleAll (mScheduleEvent);
		}

		CheckFirst();
	}

	void CheckFirst(){

		if(UserMgr.UserInfo.IsFirstLanding){
			UserMgr.UserInfo.IsFirstLanding = false;
			transform.root.GetComponent<AudioSource>().PlayOneShot(mAudioWelcome);
			
			CheckAttendance();
		}
	}
	
	void CheckAttendance(){
		WWW www = new WWW(Constants.URL_ATTENDANCE+UserMgr.UserInfo.memSeq);
		Debug.Log ("Constants.URL_ATTENDANCE+UserMgr.UserInfo.memSeq : " + Constants.URL_ATTENDANCE+UserMgr.UserInfo.memSeq);
		StartCoroutine(RunAttendance(www));
		UtilMgr.ShowLoading(true);
	}
	
	IEnumerator RunAttendance(WWW www){
		yield return www;
		
		UtilMgr.DismissLoading();
		if(www.error != null){
			DialogueMgr.ShowDialogue("attendance error", www.error, DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else{
			if(www.text != null && www.text.Length > 0){
				//popup webview
				mWebview.SetActive(true);
				mWebview.GetComponent<ScriptGameWebview>().GoTo(www.text);
			} else{
				Debug.Log("Attendance is already done");
			}
		}
	}

	void SetSchedule1(){
		
		
		
		//bool chek = false;
		
		
		List<string> ch = new List<string> ();
		
		
		for (int p = 0; p < 7; p++) {
			
			for (int i = 0; i<mScheduleEvent.Response.data.Count; i++) {
				char [] array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
				for (int z = 6; z<array.Length; z++) {
					ch.Add (array [z].ToString ());
				}
				string result = string.Join ("", ch.ToArray ());
				
				
				ch.Clear ();
				int num = p;
				if (System.DateTime.Now.Day+num>31) {
					num = System.DateTime.Now.Day+num-31;
					num = num-System.DateTime.Now.Day;
				}
				if (System.DateTime.Now.Day+num == int.Parse(result)) {
				//	Debug.Log("SelectTeam : " + UtilMgr.SelectTeam);
					//chek = true;
					if (mScheduleEvent.Response.data [i].extend [0].teamName == UtilMgr.SelectTeam) {
						UserMgr.Schedule = mScheduleEvent.Response.data [i];
						
						gameobj.SetActive (true);
						mHighlight.SetActive (true);
						
						InitTopInfo();
						return;
					} else if (mScheduleEvent.Response.data [i].extend [1].teamName == UtilMgr.SelectTeam) {
						UserMgr.Schedule = mScheduleEvent.Response.data [i];
						
						gameobj.SetActive (true);
						mHighlight.SetActive (true);
						
						InitTopInfo();
						return;
					}
				}
				
			}
		}
		
		
		
		QuizMgr.EnterMain(this);
		gameobj.SetActive (true);
		mHighlight.SetActive (true);
		
		InitTopInfo();
	}
	void SetSchedule(){
	


		//bool chek = false;
		
		
		List<string> ch = new List<string> ();
		
		
		for (int p = 0; p < 7; p++) {
			
			for (int i = 0; i<mScheduleEvent.Response.data.Count; i++) {
				char [] array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
				for (int z = 6; z<array.Length; z++) {
					ch.Add (array [z].ToString ());
				}
				string result = string.Join ("", ch.ToArray ());
				
				
				ch.Clear ();
				int num = p;
				if (System.DateTime.Now.Day+num>31) {
					num = System.DateTime.Now.Day+num-31;
					num = num-System.DateTime.Now.Day;
				}
				if (System.DateTime.Now.Day+num == int.Parse(result)) {
				
						//chek = true;
					if (mScheduleEvent.Response.data [i].extend [0].teamCode == UserMgr.UserInfo.GetTeamCode()) {
						UserMgr.Schedule = mScheduleEvent.Response.data [i];
						if(mScheduleEvent.Response.data [i].gameStatus == 1){
							
							LandingState = 2;
						}else if(mScheduleEvent.Response.data [i].gameStatus == 2){
							LandingState = 3;
						}
				
						gameobj.SetActive (true);
						mHighlight.SetActive (true);
						
						InitTopInfo();
						return;
					} else if (mScheduleEvent.Response.data [i].extend [1].teamCode == UserMgr.UserInfo.GetTeamCode()) {
						UserMgr.Schedule = mScheduleEvent.Response.data [i];
						if(mScheduleEvent.Response.data [i].gameStatus == 1){
							
							LandingState = 2;
						}else if(mScheduleEvent.Response.data [i].gameStatus == 2){
							LandingState = 3;
						}
					
						gameobj.SetActive (true);
						mHighlight.SetActive (true);
						
						InitTopInfo();
						return;
					}
				}
				
			}
		}



		QuizMgr.EnterMain(this);
		gameobj.SetActive (true);
		mHighlight.SetActive (true);

		InitTopInfo();
	}

	public void GoGame(string teamC,string nowday){
		
		//bool chek = false;
		
		
		List<string> ch = new List<string> ();


		for (int p = 0; p < 7; p++) {

			for (int i = 0; i<mScheduleEvent.Response.data.Count; i++) {
				char [] array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
				for (int z = 6; z<array.Length; z++) {
					ch.Add (array [z].ToString ());
				}
				string result = string.Join ("", ch.ToArray ());
			
			
				ch.Clear ();
		
				int num = p;
				if (System.DateTime.Now.Day+num>31) {
					num = System.DateTime.Now.Day+num-31;
					num = num-System.DateTime.Now.Day;
				}
				if (int.Parse(nowday) == int.Parse(result)) {
				//	chek = true;
				
					if (mScheduleEvent.Response.data [i].extend [0].teamCode == teamC) {
						UserMgr.Schedule = mScheduleEvent.Response.data [i];
					//	Debug.Log("UserMgr.Schedule.extend [0].teamCode : " + UserMgr.Schedule.extend [0].teamCode );
					//	Debug.Log("UserMgr.Schedule.gameStatus : " + UserMgr.Schedule.gameStatus );
					//	Debug.Log("UserMgr.Schedule.startDate : " + UserMgr.Schedule.startDate );

						if (UserMgr.Schedule.gameStatus == ScheduleInfo.GAME_READY) {
							//non
							LandingState =1;
							AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);	
						} else {
							//Startgame();
							LandingState =2;
							AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);	
						}
						return;
					} else if (mScheduleEvent.Response.data [i].extend [1].teamCode == teamC) {
						UserMgr.Schedule = mScheduleEvent.Response.data [i];
					//	Debug.Log("UserMgr.Schedule.extend [0].teamCode : " + UserMgr.Schedule.extend [0].teamCode );
					//	Debug.Log("UserMgr.Schedule.gameStatus : " + UserMgr.Schedule.gameStatus );
					//	Debug.Log("UserMgr.Schedule.startDate : " + UserMgr.Schedule.startDate );
						
						if (UserMgr.Schedule.gameStatus == ScheduleInfo.GAME_READY) {
							//non
							//Nongame();
							LandingState =1;
							AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);	
						} else {
							//Startgame();
							LandingState =1;
							AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);	
						}
						return;
					}
				}

			}
		}
	}
	void InitTopInfo(){
		transform.FindChild("TopInfoItem").GetComponent<ScriptTopInfoItem>().SetVSInfo(UserMgr.Schedule);
		if (UserMgr.Schedule != null) {
			if (UserMgr.Schedule.gameStatus == ScheduleInfo.GAME_PLAYING) {
				mBtnHighlight.transform.FindChild ("Label").GetComponent<UILabel> ().text = mStrLive;
			}
		}
	}

	void Update(){
		SetTopInfo ();
//		Debug.Log("delta time is "+Time.deltaTime);
	}

	void SetTopInfo()
	{
		if(UserMgr.UserInfo == null)
			return;

		mLblDia.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
		mLblGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userGoldenBall);
		mLblRuby.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);

		mLblNewGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userGoldenBall);

		mLblNewDia.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
		mLblNewRuby.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);
	}

//	void OnApplicationFocus(bool focus){
//		if(!focus){
//			NetMgr.ExitGame(null);
//		}
//	}
	
	void OnApplicationPause(bool pause){
		if(pause){
			NetMgr.ExitGame(null);
		} else{
			if(!transform.parent.FindChild ("TF_Items").gameObject.activeSelf){
				AutoFade.LoadLevel(Application.loadedLevelName);
			}
		}
	}

	public void AnimateClosing()
	{
//		transform.FindChild("TopInfoItem").FindChild("BtnMenu 1").gameObject.SetActive(true);
		transform.FindChild("TopInfoItem").FindChild("BtnBack").gameObject.SetActive(true);
		transform.FindChild("TopInfoItem").FindChild("BtnVS").gameObject.SetActive(true);
		transform.FindChild("TopInfoItem").FindChild("BtnCancel").gameObject.SetActive(false);

		transform.root.GetComponent<AudioSource>().PlayOneShot (mSoundCloseBet);
		transform.GetComponent<PlayMakerFSM> ().SendEvent ("CloseBetting");
		TweenAlpha.Begin (mBetting.GetComponent<ScriptTF_Betting>().mSprComb, 1f, 0f);

		CheckAndJoinQuiz();
	}

	void CheckAndJoinQuiz(){
		transform.FindChild("TopInfoItem").GetComponent<ScriptTopInfoItem>().SetVSInfo(UserMgr.Schedule);

		if (mBetting.GetComponent<ScriptTF_Betting> ().mListJoin.Count > 0) {
			mJoinQuizEvent = new JoinQuizEvent(new EventDelegate(this, "CompleteJoinQuiz"));
			NetMgr.JoinQuiz (mBetting.GetComponent<ScriptTF_Betting> ().mListJoin[0], mJoinQuizEvent);
			mBetting.GetComponent<ScriptTF_Betting>().mSprBetting
				.GetComponent<ScriptBetting>().UpdateHitterItem(
					mBetting.GetComponent<ScriptTF_Betting> ().mListJoin[0]);
		}
	}

	public void CompleteJoinQuiz(){
		mBetting.GetComponent<ScriptTF_Betting> ().mListJoin.RemoveAt (0);
		CheckAndJoinQuiz ();

		UserMgr.UserInfo.userGoldenBall = mJoinQuizEvent.Response.data.userGoldenBall;
		UserMgr.UserInfo.userRuby = mJoinQuizEvent.Response.data.userRuby;
		UserMgr.UserInfo.userDiamond = mJoinQuizEvent.Response.data.userDiamond;

	}

	void GoPreState()
	{
		QuizMgr.IsBettingOpended = false;

		if (QuizMgr.MoreQuiz) {
			QuizMgr.MoreQuiz = false;
			RequestQuiz();
		}

		Debug.Log ("GoPreState");

		switch(mState)
		{
		case STATE.Highlight:
			OpenHighlight();
			break;
		case STATE.Lineup:
			OpenLineup();
			break;
		case STATE.Livetalk:
			OpenLivetalk();
			break;
		case STATE.Bingo:
			OpenBingo();
			break;
		}
	}

	void OpenHighlight()
	{
		mHighlight.SetActive (true);
		mMatchInfoTop.SetActive (true);

//		mLineup.SetActive (false);
//		mBingo.SetActive (false);
//		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		mState = STATE.Highlight;

		SetBoardInfo ();
	}

	void OpenLineup()
	{
		mLineup.SetActive (true);
		mMatchInfoTop.SetActive (true);

//		mHighlight.SetActive (false);
//		mBingo.SetActive (false);
//		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		mState = STATE.Lineup;

		mLineup.GetComponent<LineupControl> ().view ();
	}

	void OpenBingo()
	{
		mBingo.SetActive (true);
		mMatchInfoTop.SetActive (false);

//		mHighlight.SetActive (false);
//		mLineup.SetActive (false);
//		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		mState = STATE.Bingo;
	}

	void OpenLivetalk()
	{
		mLivetalk.SetActive (true);
		mMatchInfoTop.SetActive (true);

//		mHighlight.SetActive (false);
//		mLineup.SetActive (false);
//		mBingo.SetActive (false);
		mBetting.SetActive (false);

		mState = STATE.Livetalk;

	}

	public void OpenBettingForSample(){

		QuizInfo quizInfo = new QuizInfoSample (QuizMgr.SequenceQuiz+1);

		ScriptMainTop.DetailBoard.player.Clear ();
		PlayerInfo player = new PlayerInfoSample (0);
		ScriptMainTop.DetailBoard.player.Add(player);
		player = new PlayerInfoSample (1);
		ScriptMainTop.DetailBoard.player.Add(player);

		mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ()
			.AddQuizList (quizInfo);

		OpenBetting (quizInfo);

		StartCoroutine (SampleResult ());
	}

	IEnumerator SampleResult(){
		yield return new WaitForSeconds (16f);

		GetSimpleResultEvent simpleEvent = new GetSimpleResultEvent (null);
		simpleEvent.Response = new GetSimpleResultResponse ();
		simpleEvent.Response.data = new List<SimpleResultInfo> ();
		simpleEvent.Response.data.Add(new SimpleResultInfoSample (QuizMgr.SequenceQuiz));

		QuizMgr.InitSimpleResult (simpleEvent,
		                          mBetting.GetComponent<ScriptTF_Betting>().mSprBetting.GetComponent<ScriptBetting>(),
		                          transform.FindChild("QuizResultPopup").GetComponent<ScriptQuizResult>());
	}

	public void OpenBetting(QuizInfo quizInfo)
	{
		#if(UNITY_EDITOR)

		#elif(UNITY_ANDROID)

		#else

		#endif

//		if (UtilMgr.HasBackEvent ()) {
//			UtilMgr.RunAllBackEvents ();
//		}
		QuizMgr.QuizInfo = quizInfo;
		QuizMgr.IsBettingOpended = true;
		QuizMgr.JoinCount = 0;
		
		UtilMgr.AddBackEvent (new EventDelegate (this, "AnimateClosing"));
		
		


			//mLivetalk.SetActive(false);

		if (!transform.parent.FindChild ("TF_Items").gameObject.activeSelf) {
			mBetting.SetActive (true);
		} 
		if (mLivetalk.activeSelf) {
	
			mLivetalk.transform.FindChild ("Panel").FindChild ("Input").GetComponent<UIInput> ().CloseKeboard ();
		}
	
			mBetting.GetComponent<ScriptTF_Betting> ().Init (quizInfo);
		if (!transform.parent.FindChild ("TF_Items").gameObject.activeSelf) {
			transform.GetComponent<PlayMakerFSM> ().SendEvent ("OpenBetting");
			transform.root.GetComponent<AudioSource> ().PlayOneShot (mSoundOpenBet);
		}

			transform.FindChild ("TopInfoItem").GetComponent<ScriptTopInfoItem> ().SetGoldInfo ();
//		transform.FindChild("TopInfoItem").FindChild("BtnMenu 1").gameObject.SetActive(false);
			transform.FindChild ("TopInfoItem").FindChild ("BtnBack").gameObject.SetActive (false);
			transform.FindChild ("TopInfoItem").FindChild ("BtnVS").gameObject.SetActive (false);
			transform.FindChild ("TopInfoItem").FindChild ("BtnCancel").gameObject.SetActive (true);

	
	}


	public void AllCancel(){
		mBetting.GetComponent<ScriptTF_Betting> ().mListJoin.Clear();
		UtilMgr.OnBackPressed();
		if (mLivetalk.activeSelf) {
			mLivetalk.transform.FindChild ("Panel").FindChild ("Input").GetComponent<UIInput> ().OpenKeboard ();
		}
	}

	public void RequestBoardInfo()
	{
		mBoardEvent = new  GetGameSposDetailBoardEvent(new EventDelegate (this, "GotBoard"));

		if (QuizMgr.NeedsDetailInfo) {
			NetMgr.GetGameSposDetailBoard (mBoardEvent);
			QuizMgr.NeedsDetailInfo = false;
		}	else
			NetMgr.GetGameSposPlayBoard(mBoardEvent);
	}

	public void GotBoard()
	{
		Debug.Log("GotBoard");
		DetailBoard.play = mBoardEvent.Response.data.play;
		DetailBoard.player = mBoardEvent.Response.data.player;
	
	
		SetBoardInfo ();

		Debug.Log("HasQuiz is "+QuizMgr.HasQuiz);
		
		if(QuizMgr.HasQuiz){
			QuizMgr.HasQuiz = false;
			RequestQuiz();
		}
	}

	public void RequestQuiz()
	{
		mEventQuiz = new GetQuizEvent (new EventDelegate (this, "GotQuiz"));
		Debug.Log ("QuizMgr.SequenceQuiz : " + QuizMgr.SequenceQuiz);
		NetMgr.GetProgressQuiz (QuizMgr.SequenceQuiz, mEventQuiz);
	}

	public void GotQuiz()
	{
		Debug.Log("GotQuiz");
		if (mEventQuiz.Response.data.quiz == null
						|| mEventQuiz.Response.data.quiz.Count < 1) {
			Debug.Log("NoQuiz");
			return;
		}

//		if(QuizMgr.SequenceQuiz < 1){
//			RefreshQuizList();
//
//			if(!QuizMgr.IsBettingOpended)
//				OpenBetting (mEventQuiz.Response.data.quiz[0]);
//		} else{
			if (mEventQuiz.Response.data.quiz.Count > 1) {
				QuizMgr.MoreQuiz = true;
			}
			AddQuizIntoList ();
			if (!QuizMgr.IsBettingOpended)
			QuizMgr.NextPlayerInfo = mEventQuiz.Response.data.nextPlayer;

				OpenBetting (mEventQuiz.Response.data.quiz[mEventQuiz.Response.data.quiz.Count-1]);
//		}



	}

//	void RefreshQuizList(){
//		mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ()
//			.InitQuizList(mEventQuiz);
//	}

	void AddQuizIntoList()
	{
		mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ()
			.AddQuizList (mEventQuiz.Response.data.quiz[mEventQuiz.Response.data.quiz.Count-1]);
//		mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ().InitQuizList (mEventQuiz);
	}

	public void SetBoardInfo()
	{
//		Debug.Log("SetBoardInfo");
		mMatchInfoTop.GetComponent<ScriptMatchInfo> ().SetBoard ();
		if(mBoardEvent != null
			&& mBoardEvent.Response.data.awayScore != null
		   && mBoardEvent.Response.data.awayScore.Count > 0){
//			Debug.Log("NeedsDetailInfo!!!!");
			mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ().InitScoreBoard(mBoardEvent);
		}
	}

	public void GetSimpleResult(int quizListSeq){
		mSimpleEvent = new GetSimpleResultEvent(new EventDelegate(this, "GotSimpleResult"));
		NetMgr.GetSimpleResult (quizListSeq, mSimpleEvent);
	}

	public void GotSimpleResult()
	{
//		GetSimpleResultEvent simpleEvent
//			, ScriptBetting scriptBetting, ScriptQuizResult scriptQuizResult
		QuizMgr.InitSimpleResult (mSimpleEvent,
		                          mBetting.GetComponent<ScriptTF_Betting>().mSprBetting.GetComponent<ScriptBetting>(),
		                          mQuizResultPopup.GetComponent<ScriptQuizResult>());
	}

	void EnableBtns(){
		mBtnHighlight.GetComponent<UIButton>().isEnabled = true;
		mBtnLineup.GetComponent<UIButton>().isEnabled = true;
		mBtnBingo.GetComponent<UIButton>().isEnabled = true;
		mBtnLivetalk.GetComponent<UIButton>().isEnabled = true;
	}

	public void GoPreScene(){
		UtilMgr.OnBackPressed();
	}

	public void CloseAttendance(){
		mWebview.SetActive(false);
		mWebview.GetComponent<ScriptGameWebview>().CloseWebview();
	}

	public void BtnClicked(string name)
	{
		EnableBtns();
		switch(name)
		{
		case "Highlight":
			mBtnHighlight.GetComponent<UIButton>().isEnabled = false;
			OpenHighlight();
			break;
		case "Lineup":
			mBtnLineup.GetComponent<UIButton>().isEnabled = false;
			OpenLineup();
			break;
		case "Bingo":
			mBtnBingo.GetComponent<UIButton>().isEnabled = false;
			OpenBingo();
			break;
		case "Livetalk":
			mBtnLivetalk.GetComponent<UIButton>().isEnabled = false;
			OpenLivetalk();
			break;
		}
	}
}
