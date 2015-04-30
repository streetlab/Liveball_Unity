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

	public AudioClip mSoundOpenBet;
	public AudioClip mSoundCloseBet;

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

	void Start () {
		mHighlight.SetActive (true);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		#if(UNITY_EDITOR)
		#elif(UNITY_ANDROID)
		QuizMgr.EnterMain(this);
		#else
		#endif

		InitTopInfo();
	}

	void InitTopInfo(){
		transform.FindChild("TopInfoItem").GetComponent<ScriptTopInfoItem>().SetVSInfo(UserMgr.Schedule);
	}

	void Update(){
		SetTopInfo ();
	}

	void SetTopInfo()
	{
		mLblDia.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
		mLblGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userGoldenBall);
		mLblRuby.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);
	}

	public void AnimateClosing()
	{
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

		mLineup.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		mState = STATE.Highlight;

		SetBoardInfo ();
	}

	void OpenLineup()
	{
		mLineup.SetActive (true);
		mMatchInfoTop.SetActive (false);

		mHighlight.SetActive (false);
		mBingo.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		mState = STATE.Lineup;
	}

	void OpenBingo()
	{
		mBingo.SetActive (true);
		mMatchInfoTop.SetActive (false);

		mHighlight.SetActive (false);
		mLineup.SetActive (false);
		mLivetalk.SetActive (false);
		mBetting.SetActive (false);

		mState = STATE.Bingo;
	}

	void OpenLivetalk()
	{
		mLivetalk.SetActive (true);
		mMatchInfoTop.SetActive (true);

		mHighlight.SetActive (false);
		mLineup.SetActive (false);
		mBingo.SetActive (false);
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
		mLivetalk.SetActive(false);

		if (UtilMgr.HasBackEvent ()) {
			UtilMgr.RunAllBackEvents();
		}
		QuizMgr.QuizInfo = quizInfo;
		QuizMgr.IsBettingOpended = true;
		QuizMgr.JoinCount = 0;

		UtilMgr.AddBackEvent(new EventDelegate(this, "AnimateClosing"));

		mBetting.SetActive (true);
		mBetting.GetComponent<ScriptTF_Betting> ().Init (quizInfo);

		transform.GetComponent<PlayMakerFSM> ().SendEvent ("OpenBetting");
		transform.root.GetComponent<AudioSource>().PlayOneShot (mSoundOpenBet);

		transform.FindChild("TopInfoItem").GetComponent<ScriptTopInfoItem>().SetGoldInfo();
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

			if(!QuizMgr.IsBettingOpended)
				OpenBetting (mEventQuiz.Response.data.quiz[mEventQuiz.Response.data.quiz.Count-1]);
//		}




	}

	void RefreshQuizList(){
		mHighlight.transform.FindChild ("MatchPlaying").GetComponent<ScriptMatchPlaying> ()
			.InitQuizList(mEventQuiz);
	}

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


	public void BtnClicked(string name)
	{
		switch(name)
		{
		case "Highlight":
			OpenHighlight();
			break;
		case "Lineup":
			OpenLineup();
			break;
		case "Statistic":
			OpenBingo();
			break;
		case "Livetalk":
			OpenLivetalk();
			break;
		}
	}
}
