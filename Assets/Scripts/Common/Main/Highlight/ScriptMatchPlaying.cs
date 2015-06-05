using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMatchPlaying : MonoBehaviour {
	public GameObject BG_G;
	public GameObject mScoreBoard;
	public GameObject mList;
	public GameObject mItemHitter;
//	public GameObject itemRound;
	public GameObject itemPoll;
	public GameObject itemInfo;
	public GameObject mTop;
	public GameObject mItemDetail;
	public GameObject TF_Landing;
	
	float mPreItemSize;
	float mPosGuide;
	float mAccumulatedY;
	//	int mSequenceQuiz;
	bool mPreGame;
	bool mFirstLoading;
	int mGameRoundCounter;
	int mGameRound;
	int mInningType;
	int mInningCounter;
	bool mIsJoined;
	public bool mNeedResponse;
	public int mCntAlive;
	public const int TIME_MAX_ALIVE = 1200;// 20 frame per second * minute
	const float TIMEOUT = 10f;

	QuizInfo mSelectedQuiz;
	public bool mDetailOpened;
	public Vector3 mLocalPosList;
	public Vector2 mClipOffsetPanel;
	
	GetGameSposDetailBoardEvent mEventDetail;
	//	GetQuizEvent mEventPreQuiz;
	GetQuizEvent mEventProgQuiz;
	
//	public List<GameObject> mQuizListItems = new List<GameObject>();
	
	public void Start () {
		//		UtilMgr.ResizeList (mList);
		mFirstLoading = true;
		QuizMgr.IsBettingOpended = false;
		QuizMgr.SequenceQuiz = 0;
		//		mPosGuide = 0f;
		mPreGame = true;
//		mGameRoundCounter = 99;
		mInningCounter = 0;
//		NetMgr.JoinGame (new JoinGameEvent (new EventDelegate (this, "CompleteJoin")));
		NetMgr.JoinGame();

		if(UserMgr.Schedule.gameStatus == ScheduleInfo.GAME_READY){
			mList.transform.FindChild("Label").gameObject.SetActive(true);
			//TF_Landing.GetComponent<LandingManager>().Nongame();
		} else{
			mList.transform.FindChild("Label").gameObject.SetActive(false);
			//TF_Landing.GetComponent<LandingManager>().Startgame();
		}
		mItemDetail.SetActive(false);
		mItemHitter.SetActive(false);
	}
	
	//	void OnApplicationFocus(bool focus){
	//		if(!focus){
	//			NetMgr.ExitGame(null);
	//		}
	//	}
	//
	//	void OnApplicationPause(bool pause){
	////		if(pause){
	////			NetMgr.ExitGame(new ExitGameEvent(new EventDelegate(this, "CompleteExit")));
	////		} else{
	//		if(!pause){
	//			AutoFade.LoadLevel(Application.loadedLevelName);
	//		}
	//	}
	//
	//	public void CompleteExit(){
	//		Debug.Log("CompleteExit");
	//	}
	
	public void CompleteJoin()
	{
		Debug.Log("CompleteJoin");
		mIsJoined = true;
		SetScoreBoard ();
	}
	
	void SetScoreBoard()
	{
		mScoreBoard.transform.FindChild ("Const").gameObject.SetActive (false);
		mScoreBoard.transform.FindChild ("TeamTop").gameObject.SetActive (false);
		mScoreBoard.transform.FindChild ("TeamBottom").gameObject.SetActive (false);

		BG_G.transform.FindChild ("Num").gameObject.SetActive (false);
		BG_G.transform.FindChild ("TopTeam").gameObject.SetActive (false);
		BG_G.transform.FindChild ("BotTeam").gameObject.SetActive (false);
	//	Debug.Log("Playing");
		//transform.parent.parent.FindChild ("GameObject").FindChild ("TF_Landing").GetComponent<LandingManager> ().SetHitter ();

		//Progressing
		mEventDetail = new GetGameSposDetailBoardEvent (new EventDelegate (this, "GotDetailBoard"));
		NetMgr.GetGameSposDetailBoard (mEventDetail);
	}
	
	public void GotDetailBoard()
	{
		InitScoreBoard (mEventDetail);
		
		if (mFirstLoading) {
			SetProgQuiz (0);
			//			UtilMgr.ShowLoading (true);
		}
		
		mTop.GetComponent<ScriptMainTop> ().SetBoardInfo ();
	}
	
	public void InitScoreBoard(GetGameSposDetailBoardEvent eventDetail)
	{
		mScoreBoard.transform.FindChild ("Const").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamTop").gameObject.SetActive (true);
		mScoreBoard.transform.FindChild ("TeamBottom").gameObject.SetActive (true);

		BG_G.transform.FindChild ("Num").gameObject.SetActive (true);
		BG_G.transform.FindChild ("TopTeam").gameObject.SetActive (true);
		BG_G.transform.FindChild ("BotTeam").gameObject.SetActive (true);

	
		ScriptMainTop.DetailBoard = eventDetail.Response.data;
		if (ScriptMainTop.DetailBoard.player != null) {
			if (ScriptMainTop.DetailBoard.player.Count > 0) {
				for(int i = 0 ; i < ScriptMainTop.DetailBoard.player.Count;i++){
				Debug.Log ("ScriptMainTop.DetailBoard : " + ScriptMainTop.DetailBoard.player[i].playerName);
				}
				transform.parent.parent.FindChild ("GameObject").FindChild ("TF_Landing").GetComponent<LandingManager> ().SetPitcher ();

			}
		}

		SetAwayScore (ScriptMainTop.DetailBoard.awayScore);
		SetHomeScore (ScriptMainTop.DetailBoard.homeScore);
		SetAwayRHEB (ScriptMainTop.DetailBoard.infoBoard[0]);
		SetHomeRHEB (ScriptMainTop.DetailBoard.infoBoard[1]);
		
		mTop.transform.FindChild("TopInfoItem").GetComponent<ScriptTopInfoItem>().SetVSInfo(UserMgr.Schedule);
	}
	
	void SetProgQuiz(int quizListSeq)
	{
		Debug.Log ("quizListSeq : " + quizListSeq);
		mEventProgQuiz = new GetQuizEvent (new EventDelegate (this, "InitQuizFirst"));
		NetMgr.GetProgressQuiz (quizListSeq, mEventProgQuiz);
	}
	
	public void InitQuizFirst()
	{
		if (mEventProgQuiz.Response.data != null) {
			if(mEventProgQuiz.Response.data.quiz.Count>0){
				Debug.Log("InitQuizFirst");
				Debug.Log("mEventProgQuiz.Response.data.quiz[0] : " + mEventProgQuiz.Response.data.quiz[0].playerName);
				transform.parent.parent.FindChild("GameObject").FindChild("TF_Landing").GetComponent<LandingManager>().
					SetHitter(mEventProgQuiz.Response.data.quiz[0]);
			
			}
		}

		UtilMgr.DismissLoading ();
		QuizMgr.SetQuizList (mEventProgQuiz.Response.data.quiz);
		ResetList();
		mFirstLoading = false;
	}

	public void SetList(QuizInfo quiz){
		mSelectedQuiz = quiz;
		mList.GetComponent<UIDraggablePanel2>().Init(1, delegate(UIListItem item, int index){
			ScriptItemHitterHighlight sItem = item.Target.GetComponent<ScriptItemHitterHighlight>();
			sItem.Init (this, mSelectedQuiz, transform.FindChild ("ItemDetail").gameObject);				
			item.Target.transform.FindChild("Round").gameObject.SetActive(false);
		});
		mList.GetComponent<UIDraggablePanel2>().ResetPosition();
	}

	public void ResetList(){
		ResetList(Vector3.zero, Vector2.zero);
	}

	public void ResetList(Vector3 vector3, Vector2 vector2){		
		Debug.Log("list Cnt : "+QuizMgr.QuizList.Count);
		mGameRoundCounter = 99;
		foreach(QuizInfo quizInfo in QuizMgr.QuizList){
			quizInfo.mShowInningFlag = false;
			if(quizInfo.typeCode.Contains("_QZD_")){
				if(mGameRoundCounter > quizInfo.gameRound){
					if(mGameRoundCounter < 99){
					} else{
						mGameRound = quizInfo.gameRound;
						mInningType = quizInfo.inningType;
					}					
					mGameRoundCounter = quizInfo.gameRound;
					mInningCounter = quizInfo.inningType;
					quizInfo.mShowInningFlag = true;
				} else if(mInningCounter != quizInfo.inningType){
					mInningCounter = quizInfo.inningType;
					quizInfo.mShowInningFlag = true;
				}
			}
		}

		mList.GetComponent<UIDraggablePanel2> ().Init (QuizMgr.QuizList.Count,
		                                               delegate(UIListItem item, int index) {
			
			ScriptItemHitterHighlight sItem = item.Target.GetComponent<ScriptItemHitterHighlight>();
			QuizInfo quizInfo = mEventProgQuiz.Response.data.quiz[index];
			sItem.Init (this, quizInfo, transform.FindChild ("ItemDetail").gameObject);				
			item.Target.transform.FindChild("Round").gameObject.SetActive(false);
			if(quizInfo.mShowInningFlag){
				item.Target.transform.FindChild("Round").gameObject.SetActive(true);
				item.Target.transform.FindChild("Round").FindChild("LblRound")
					.GetComponent<UILabel>().text = ""+quizInfo.gameRound;
				if(quizInfo.inningType == 0){
					item.Target.transform.FindChild("Round").FindChild("SprArrow")
						.GetComponent<UISprite>().flip = UIBasicSprite.Flip.Nothing;
				} else{
					item.Target.transform.FindChild("Round").FindChild("SprArrow")
						.GetComponent<UISprite>().flip = UIBasicSprite.Flip.Vertically;
				}
			}
			
		});
		mList.GetComponent<UIDraggablePanel2> ().ResetPosition();
//		if(vector3 == Vector3.zero){
//			Debug.Log("no vector");
//		} else{
//			Debug.Log("vector : "+vector3.y);
//			mList.transform.localPosition = vector3;
//			mList.GetComponent<UIPanel>().clipOffset = vector2;
//		}
	}
	
	public void AddQuizList(QuizInfo quizInfo)
	{	
		Debug.Log("AddQuizList");		
		QuizMgr.AddQuizList (quizInfo);
		ResetList();
		
		mGameRound = quizInfo.gameRound;
		mInningType = quizInfo.inningType;
	}
	
//	void RepositionItems(float size)
//	{
//		foreach(GameObject tmp in mQuizListItems){
//			Vector3 vector = tmp.transform.localPosition;
//			vector.y -= size;
//			tmp.transform.localPosition = vector;
//			if(tmp.GetComponent<ScriptItemHitterHighlight>() != null)		
//				tmp.GetComponent<ScriptItemHitterHighlight>().mPositionY += size;
//		}
//	}
	
	void SetAwayScore(List<ScoreInfo> listScore)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamTop");
		team.FindChild ("LblName").GetComponent<UILabel> ().text = UserMgr.Schedule.extend [0].teamName;
		
		string strRnd = "LblRnd";
		for(int i = 0; i < listScore.Count; i++)
		{
			ScoreInfo info = listScore[i];
			team.FindChild (strRnd + info.playRound).GetComponent<UILabel> ().text = info.score;
		}


		team = BG_G.transform.FindChild ("TopTeam");
		team.FindChild ("Name").GetComponent<UILabel> ().text = UserMgr.Schedule.extend [0].teamName;
		
		//string strRnd = "LblRnd";
		for(int i = 0; i < listScore.Count; i++)
		{
			ScoreInfo info = listScore[i];
			team.FindChild("ScoreBar").FindChild (info.playRound.ToString()).GetComponent<UILabel> ().text = info.score;
		}
	}
	
	void SetHomeScore(List<ScoreInfo> listScore)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamBottom");
		team.FindChild ("LblName").GetComponent<UILabel> ().text = UserMgr.Schedule.extend [1].teamName;
		
		string strRnd = "LblRnd";
		for(int i = 0; i < listScore.Count; i++)
		{
			ScoreInfo info = listScore[i];
			team.FindChild (strRnd + info.playRound).GetComponent<UILabel> ().text = info.score;
		}


		team = BG_G.transform.FindChild ("BotTeam");
		team.FindChild ("Name").GetComponent<UILabel> ().text = UserMgr.Schedule.extend [1].teamName;
		
		//string strRnd = "LblRnd";
		for(int i = 0; i < listScore.Count; i++)
		{
			ScoreInfo info = listScore[i];
			team.FindChild("ScoreBar").FindChild (info.playRound.ToString()).GetComponent<UILabel> ().text = info.score;
		}
	}
	
	void SetAwayRHEB(HEBInfo info)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamTop");
		team.FindChild ("LblR").GetComponent<UILabel> ().text = info.score;
		team.FindChild ("LblH").GetComponent<UILabel> ().text = info.countOfH;
		team.FindChild ("LblE").GetComponent<UILabel> ().text = info.countOfE;
		team.FindChild ("LblB").GetComponent<UILabel> ().text = info.countOfB;
		UserMgr.Schedule.extend[0].score = int.Parse(info.score);
	}
	
	void SetHomeRHEB(HEBInfo info)
	{
		Transform team = mScoreBoard.transform.FindChild ("TeamBottom");
		team.FindChild ("LblR").GetComponent<UILabel> ().text = info.score;
		team.FindChild ("LblH").GetComponent<UILabel> ().text = info.countOfH;
		team.FindChild ("LblE").GetComponent<UILabel> ().text = info.countOfE;
		team.FindChild ("LblB").GetComponent<UILabel> ().text = info.countOfB;
		UserMgr.Schedule.extend[1].score = int.Parse(info.score);
	}

	void Update(){
		if(mIsJoined){
			if(mCntAlive++ >= TIME_MAX_ALIVE){
				mCntAlive = 0;
				NetMgr.SendSocketMsg(new AliveRequest().ToRequestString());
				mNeedResponse = true;
				StartCoroutine("WaitingForResponse");
			}
		}
	}

	IEnumerator WaitingForResponse()
	{		
		float timeSum = 0f;

		while(mNeedResponse && 
			      timeSum < TIMEOUT) { 
				timeSum += Time.deltaTime; 
				yield return 0; 
		}
		
		if(mNeedResponse)
		{
			NetMgr.JoinGame();
		}
	}
}
