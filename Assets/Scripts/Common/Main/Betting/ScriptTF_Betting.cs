using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptTF_Betting : MonoBehaviour {

	public GameObject mTop;
	public GameObject mBatting;
	public GameObject mSprHit;
	public GameObject mSprOut;
	public GameObject mSprCard;
	public GameObject mSprBetting;
	public GameObject mSprLoaded;
	public GameObject mScrollView;

	public GameObject mBtnBatter;
	public GameObject mBtnPitcher;
	public GameObject mBtnStrategy;

	public GameObject mSpark1;
	public GameObject mSpark2;

	public AudioClip mBoom;

	public GameObject Landing;
	public GameObject mLivetalk;
	public GameObject shadow;
	public List<JoinQuizInfo> mListJoin = new List<JoinQuizInfo>();

//	QuizInfo quizInfo;
//	public QuizInfo QuizInfo
//	{
//		get{return quizInfo;}
//	}
//	int mStartSec;
//	int mStartMilSec;
	long mStartTime;
	bool mTimeOut;
	bool backCheck;
	bool first = true;

	static Color YELLOW = new Color(1f, 1f, 0f);
	static Color WHITE = new Color(1f, 1f, 1f);
	static Color RED = new Color(1f, 0f, 0f);

	const float MAX_TIME = 1600f;
	const float BAR_WIDTH = 321f;

	bool isDragging;

	void Start()
	{
//		transform.FindChild ("Lightning Spark").GetComponent<ScriptParticleResizer> ().ResizeRatio (0.5f);

	//	mSpark1.GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 10010;
	//	mSpark2.GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 10010;
//		transform.FindChild ("Lightning Spark").FindChild("Lightning").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
//		transform.FindChild ("Lightning Spark").FindChild("Spakles").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
//		transform.FindChild ("Lightning Spark").FindChild("Ring").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
//		transform.FindChild ("Lightning Spark").FindChild("Ray").GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;

//		mScrollView.GetComponent<UIScrollView>().onDragStarted += OnDragStarted;
//		mScrollView.GetComponent<UIScrollView>().onDragFinished += OnDragFinished;
		//mScrollView.transform.localPosition = new Vector3 (0,-72,0);
//		mScrollView.GetComponent<UIPanel> ().clipOffset = new Vector2 (0,-221.5f);
//		mScrollView.GetComponent<UIPanel> ().clipRange = new Vector4 (0,-72,720,747);
	}

//	void OnDragStarted(){
//		isDragging = true;
//	}
//
//	void OnDragFinished(){
//		isDragging = false;
//	}

	void Awake(){
//		mScrollView.transform.localPosition = new Vector3(0, 0, 0);
		Debug.Log("Awake y is "+mScrollView.transform.localPosition.y);
	}

	void Update()
	{
//		if(isDragging){
//			Debug.Log("y is "+mScrollView.transform.localPosition.y);
//		-53f;
			if((mScrollView.transform.localPosition.y) > (-72f+(UtilMgr.GetScaledPositionY() * 2f))){
				Vector3 pos = new Vector3(0 ,(-72f+(UtilMgr.GetScaledPositionY() * 2f)), 0);
					mScrollView.transform.localPosition = pos;

				Vector2 offset = new Vector2(0, -(-72f+UtilMgr.GetScaledPositionY()));
					mScrollView.GetComponent<UIPanel>().clipOffset = offset;
			}
//		}
	
		if (mTimeOut) {
			if(backCheck){		
				//backCheck = false;
		
			}
			//if(UtilMgr.HasBackEvent)
			//UtilMgr.RunAllBackEvents();
		//	UtilMgr.RemoveBackEvent();

			return;
		}

		CalcTime ();
	}

	void CalcTime()
	{
		long diff = mStartTime - System.DateTime.Now.ToFileTime();

		int sec = (int)(diff / 10000000L);
		int milSec = (int)(diff % 10000000L);
		
		if (sec < -15) {
			mTimeOut = true;
//			if(mSprBetting.activeSelf){
//				//UtilMgr.OnBackPressed ();
//				//UtilMgr.OnBackPressed ();
//				//UtilMgr.RunAllBackEvents();
//			}else{
//			//	UtilMgr.OnBackPressed ();
//			}
			CloseAnimation();
		

//			UtilMgr.RunAllBackEvents();

			mLivetalk.transform.FindChild ("Panel").FindChild ("Input").GetComponent<UIInput> ().OpenKeboard ();

			sec = 0;
			milSec = 0;
		} else{
			//Debug.Log("sec : " + sec);
			sec = (15 + sec);

			milSec = milSec / 100000;//
			milSec = (99 + milSec);
		}
		

		SetTimer (sec, milSec);
	}

	void SetTimer(int sec, int milSec)
	{
		string strSec = "" + sec;
		if (sec < 10)
			strSec = "0" + sec;

		string strMilSec = "" + milSec;
		if (milSec < 10)
			strMilSec = "0" + milSec;
				
		mBatting.transform.FindChild("Timer").FindChild("Sprite").FindChild ("LblTimer").GetComponent<UILabel> ().text
			= strSec + " : " + strMilSec;

		float now = float.Parse (strSec + strMilSec);
		float width = BAR_WIDTH * (now / MAX_TIME);
//		Debug.Log ("now : " + now);
//		Debug.Log ("width : " + width);
//		Debug.Log ("width to int : " + (int)width); 

//		if (sec < 6) {
//			mSprComb.transform.FindChild("Timer").FindChild ("LblTimer").GetComponent<UILabel> ().color = RED;
//			mSprComb.transform.FindChild ("Timer").FindChild ("Progress").FindChild ("SprBar")
//				.GetComponent<UISprite> ().color = RED;
//		} else {
//			mSprComb.transform.FindChild("Timer").FindChild ("LblTimer").GetComponent<UILabel> ().color = YELLOW;
//			mSprComb.transform.FindChild ("Timer").FindChild ("Progress").FindChild ("SprBar")
//				.GetComponent<UISprite> ().color = YELLOW;
//		}

		mBatting.transform.FindChild ("Timer").FindChild ("SprBar")
			.GetComponent<UIPanel> ().clipOffset = new Vector2 (-(BAR_WIDTH-(width)),0);



	}

	public void Init(QuizInfo quizInfo)
	{
		if (quizInfo != null) {
			transform.parent.FindChild ("TF_Highlight").FindChild ("MatchPlaying").FindChild ("ListHighlight").FindChild ("Label").gameObject.SetActive (false);
			if (!transform.parent.FindChild ("GameObject").FindChild ("TF_Landing").FindChild ("Scroll View").FindChild ("Playing").gameObject.activeSelf) {

				ScriptMainTop.LandingState = 2;
				transform.parent.FindChild ("GameObject").FindChild ("TF_Landing").GetComponent<LandingManager> ().Start ();

			}
			Debug.Log ("Init");
			mListJoin.Clear ();
//		quizInfo = quizInfo;
			SetHitter (quizInfo);
			Debug.Log ("Hitter");
			SetPitcher ();
			Debug.Log ("Pitcher");
			//SetBases ();
			Debug.Log ("Bases");
			SetBtns ();
			Debug.Log ("Btns");
			mSprBetting.SetActive (false);

			mStartTime = System.DateTime.Now.ToFileTime ();
			mTimeOut = false;
			backCheck = true;
//		mStartSec = System.DateTime.Now.Second;
//		mStartMilSec = System.DateTime.Now.Millisecond / 10;
			TweenAlpha.Begin (mBatting, 0f, 0f);
			TweenAlpha.Begin (mBatting, 1f, 1.0f);

			mSpark1.SetActive (false);
			mSpark2.SetActive (false);
			mSprBetting.SetActive (false);

			mBtnBatter.GetComponent<ScriptBettingCard> ().Init ();
			mBtnPitcher.GetComponent<ScriptBettingCard> ().Init ();
			mBtnStrategy.GetComponent<ScriptBettingCard> ().Init ();

			mScrollView.transform.localPosition = new Vector3 (0, -72f, 0);
			mScrollView.GetComponent<UIPanel> ().clipOffset = new Vector2 (0, UtilMgr.GetScaledPositionY ());


		}
	}

	void SetBtns()
	{
		if (QuizMgr.QuizInfo.typeCode.Contains ("_QZD_")) {
			mSprHit.SetActive(true);
			mSprOut.SetActive(true);
			mSprLoaded.SetActive(false);
			if(QuizMgr.QuizInfo.order==null){
				Debug.Log("order is nul");
			} else{
				Debug.Log("order count is " + QuizMgr.QuizInfo.order.Count);
			}
			if(QuizMgr.QuizInfo.order!=null){

				if(QuizMgr.QuizInfo.order.Count>5){
					mSprHit.transform.FindChild ("BtnHit1").FindChild ("LblGP").GetComponent<UILabel> ().text = "［"+QuizMgr.QuizInfo.order [0].ratio+"］";
					mSprHit.transform.FindChild ("BtnHit2").FindChild ("LblGP").GetComponent<UILabel> ().text = "［"+QuizMgr.QuizInfo.order [1].ratio+"］";
					mSprHit.transform.FindChild ("BtnHit3").FindChild ("LblGP").GetComponent<UILabel> ().text = "［"+QuizMgr.QuizInfo.order [2].ratio+"］";
		//	mSprHit.transform.FindChild ("BtnHit4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [3].ratio+"x";
					mSprOut.transform.FindChild ("BtnOut1").FindChild ("LblGP").GetComponent<UILabel> ().text = "［"+QuizMgr.QuizInfo.order [3].ratio+"］";
					mSprOut.transform.FindChild ("BtnOut2").FindChild ("LblGP").GetComponent<UILabel> ().text = "［"+QuizMgr.QuizInfo.order [4].ratio+"］";
					mSprOut.transform.FindChild ("BtnOut3").FindChild ("LblGP").GetComponent<UILabel> ().text = "［"+QuizMgr.QuizInfo.order [5].ratio+"］";
		//	mSprOut.transform.FindChild ("BtnOut4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [7].ratio+"x";
			}
			}
				Debug.Log ("Checking");
			mSprHit.transform.FindChild ("BtnHit1").GetComponent<BoxCollider2D> ().enabled = true;
			mSprHit.transform.FindChild ("BtnHit2").GetComponent<BoxCollider2D> ().enabled = true;
			mSprHit.transform.FindChild ("BtnHit3").GetComponent<BoxCollider2D> ().enabled = true;
			//mSprHit.transform.FindChild ("BtnHit4").GetComponent<BoxCollider2D> ().enabled = true;
			mSprOut.transform.FindChild ("BtnOut1").GetComponent<BoxCollider2D> ().enabled = true;
			mSprOut.transform.FindChild ("BtnOut2").GetComponent<BoxCollider2D> ().enabled = true;
			mSprOut.transform.FindChild ("BtnOut3").GetComponent<BoxCollider2D> ().enabled = true;
			//mSprOut.transform.FindChild ("BtnOut4").GetComponent<BoxCollider2D> ().enabled = true;

			mSprHit.transform.FindChild ("BtnHit1").GetComponent<ScriptBettingItem>().Reset();
			mSprHit.transform.FindChild ("BtnHit2").GetComponent<ScriptBettingItem>().Reset();
			mSprHit.transform.FindChild ("BtnHit3").GetComponent<ScriptBettingItem>().Reset();
			//mSprHit.transform.FindChild ("BtnHit4").GetComponent<ScriptBettingItem>().Reset();
			mSprOut.transform.FindChild ("BtnOut1").GetComponent<ScriptBettingItem>().Reset();
			mSprOut.transform.FindChild ("BtnOut2").GetComponent<ScriptBettingItem>().Reset();
			mSprOut.transform.FindChild ("BtnOut3").GetComponent<ScriptBettingItem>().Reset();
			//mSprOut.transform.FindChild ("BtnOut4").GetComponent<ScriptBettingItem>().Reset();
		} else if (QuizMgr.QuizInfo.typeCode.Contains ("_QZC_")) {
			mSprHit.SetActive(false);
			mSprOut.SetActive(false);
			mSprLoaded.SetActive(true);

			mSprLoaded.transform.FindChild("SprQuestionBack").FindChild("Label").GetComponent<UILabel>().text
				= QuizMgr.QuizInfo.quizTitle;

			mSprLoaded.transform.FindChild ("BtnLoaded1").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded2").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded3").GetComponent<ScriptBettingItem>().Reset();
			mSprLoaded.transform.FindChild ("BtnLoaded4").GetComponent<ScriptBettingItem>().Reset();

			if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_FULL")){
				Set4Btns();
			} else if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_LOS")){
				Set2Btns();
			} else if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_EOH")){
				Set2Btns();
			} else if(QuizMgr.QuizInfo.typeCode.Equals("BB_QZC_EOS")){
				Set2Btns();
			}
		}
	}

	void Set4Btns()
	{
		mSprLoaded.transform.FindChild ("BtnLoaded1").localPosition = new Vector3 (-260f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded2").localPosition = new Vector3 (-86f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded3").localPosition = new Vector3 (86f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded4").localPosition = new Vector3 (260f, -210f, 0);
		if (QuizMgr.QuizInfo.order != null) {
			
			if (QuizMgr.QuizInfo.order.Count > 5) {
				mSprLoaded.transform.FindChild ("BtnLoaded1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [0].ratio ;
				mSprLoaded.transform.FindChild ("BtnLoaded2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [1].ratio ;
				mSprLoaded.transform.FindChild ("BtnLoaded3").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [2].ratio ;
				mSprLoaded.transform.FindChild ("BtnLoaded4").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [3].ratio ;
			}
		}
	}

	void Set2Btns()
	{
		mSprLoaded.transform.FindChild ("BtnLoaded1").localPosition = new Vector3 (-164f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded2").localPosition = new Vector3 (164f, -210f, 0);
		mSprLoaded.transform.FindChild ("BtnLoaded3").gameObject.SetActive (false);
		mSprLoaded.transform.FindChild ("BtnLoaded4").gameObject.SetActive (false);
		if (QuizMgr.QuizInfo.order != null) {
			
			if (QuizMgr.QuizInfo.order.Count > 5) {
				mSprLoaded.transform.FindChild ("BtnLoaded1").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [0].ratio ;
				mSprLoaded.transform.FindChild ("BtnLoaded2").FindChild ("LblGP").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [1].ratio ;
			}
		}
	}

	void SetBases()
	{
		PlayInfo playInfo = ScriptMainTop.DetailBoard.play;
		if (playInfo == null)
			return;

		Transform tfStatus = mBatting.transform.FindChild("SprPitcher").FindChild ("StatusInfo");
		tfStatus.FindChild ("LblNum").GetComponent<UILabel> ().text = string.Format("{0}",playInfo.playRound);
		tfStatus.FindChild ("LblRound").GetComponent<UILabel> ().text = UtilMgr.GetRoundString (playInfo.playRound);
		tfStatus.FindChild ("SprUp").GetComponent<UISprite> ().color = WHITE;
		tfStatus.FindChild ("SprDown").GetComponent<UISprite> ().color = WHITE;
		if(playInfo.playInningType > 0)
			tfStatus.FindChild ("SprDown").GetComponent<UISprite> ().color = YELLOW;
		else
			tfStatus.FindChild ("SprUp").GetComponent<UISprite> ().color = YELLOW;
		tfStatus.FindChild ("SprOut1").GetComponent<UISprite> ().color = WHITE;
		tfStatus.FindChild ("SprOut2").GetComponent<UISprite> ().color = WHITE;
		switch (playInfo.outCount) {
		case 2:
			tfStatus.FindChild ("SprOut2").GetComponent<UISprite> ().color = RED;
			goto case 1;
		case 1:
			tfStatus.FindChild ("SprOut1").GetComponent<UISprite> ().color = RED;
			break;
		}
		tfStatus.FindChild ("SprBases").FindChild ("Base1").gameObject.SetActive (false);
		tfStatus.FindChild ("SprBases").FindChild ("Base2").gameObject.SetActive (false);
		tfStatus.FindChild ("SprBases").FindChild ("Base3").gameObject.SetActive (false);
		if(playInfo.base1st > 0)
			tfStatus.FindChild ("SprBases").FindChild ("Base1").gameObject.SetActive (true);
		if(playInfo.base2nd > 0)
			tfStatus.FindChild ("SprBases").FindChild ("Base2").gameObject.SetActive (true);
		if(playInfo.base3rd > 0)
			tfStatus.FindChild ("SprBases").FindChild ("Base3").gameObject.SetActive (true);

	}

	void SetPitcher()
	{
		if (ScriptMainTop.DetailBoard.player.Count < 2) {
			Debug.Log("No Pitcher");
		}
		Landing.GetComponent<LandingManager> ().SetPitcher ();
		Transform tfPitcher = mBatting.transform.FindChild ("SprPitcher");
//		string playerInfo = ScriptMainTop.DetailBoard.player [0].playerName + " No." + ScriptMainTop.DetailBoard.player [0].playerNumber;
//		tfPitcher.FindChild ("LblName").GetComponent<UILabel> ().text = playerInfo;
//		string playerAVG = ScriptMainTop.DetailBoard.player [0].ERA;
//		tfPitcher.FindChild ("LblSave").GetComponent<UILabel> ().text = playerAVG;
//		string strImage = ScriptMainTop.DetailBoard.player [0].imageName;
//		if (ScriptMainTop.DetailBoard.player [0].imagePath != null 
//		    && ScriptMainTop.DetailBoard.player [0].imagePath.Length > 0)
//			strImage = ScriptMainTop.DetailBoard.player [0].imagePath
//				+ ScriptMainTop.DetailBoard.player [0].imageName;
//		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
//		StartCoroutine (GetImage(www, tfPitcher.FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
	}
	string Holdname;
	void SetHitter(QuizInfo quiz)
	{ 


		if(LandingManager.N==null){
			Transform tfHitter = mBatting.transform.FindChild ("SprHitter");
			string playerInfo = QuizMgr.QuizInfo.playerName + "#"+ QuizMgr.QuizInfo.playerNumber;
			mBatting.transform.FindChild("Sprite").FindChild("Current hitter").FindChild("Players Name")
				.GetComponent<UILabel>().text = playerInfo;

			string playerAVG = ScriptMainTop.DetailBoard.player [ScriptMainTop.DetailBoard.player.Count - 1].AVG;
			mBatting.transform.FindChild("Sprite").FindChild("Current hitter").FindChild("Batting")
				.GetComponent<UILabel>().text = playerAVG;
		
			string strImage = QuizMgr.QuizInfo.imageName;
			if (QuizMgr.QuizInfo.imagePath != null && QuizMgr.QuizInfo.imagePath.Length > 0)
				strImage = QuizMgr.QuizInfo.imagePath + QuizMgr.QuizInfo.imageName;
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
			Debug.Log ("url : " + Constants.IMAGE_SERVER_HOST + strImage);
		if(gameObject.transform.FindChild("Scroll View").gameObject.activeSelf){
			StartCoroutine (GetImage (www, mBatting.transform.FindChild("Sprite").FindChild("Current hitter").
			                          FindChild("Players Image BackGround").GetChild(0).GetChild(0).
			                          GetComponent<UITexture>()));

			}
			first = false;
			Holdname = strImage;

		} else {
		


				List<nextPlayerInfo> nowPlayer = LandingManager.N;
				LandingManager.Old = nowPlayer;
				string strImage = "";
				Debug.Log("SetHitter0");
			
					//            P_LPlayersName = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Name").GetComponent<UILabel> ();
					//            P_LBatting = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Batting").GetComponent<UILabel> ();
					//            P_LPlayerImage = transform.FindChild ("Scroll View").FindChild ("Playing").FindChild ("BG_W").FindChild ("Current hitter").FindChild ("Players Image BackGround").FindChild ("Players Image Mask").FindChild ("Players Image Texture").GetComponent<UITexture> ();
					//            
				for(int i = 0; i <nowPlayer.Count;i++ ){
					if(nowPlayer[i].type == 1){
						string playerInfo = nowPlayer[i].playerName + "#" + nowPlayer[i].playerNumber;
						mBatting.transform.FindChild("Sprite").FindChild("Current hitter").FindChild("Players Name")
							.GetComponent<UILabel>().text = playerInfo;
			
						string playerAVG = nowPlayer[i].hitAvg;

						mBatting.transform.FindChild("Sprite").FindChild("Current hitter").FindChild("Batting")
							.GetComponent<UILabel>().text = playerAVG;

							
							strImage = nowPlayer[i].imageName;
						mBatting.transform.FindChild("Sprite").FindChild("Mid_BG").FindChild("Bot")
							.GetComponent<UILabel>().text = nowPlayer[i].hitAvg;
						mBatting.transform.FindChild("Sprite").FindChild("Mid_BG").FindChild("Bot").FindChild("Top 1")
							.GetComponent<UILabel>().text = nowPlayer[i].hitH.ToString()+"%";
						mBatting.transform.FindChild("Sprite").FindChild("Mid_BG").FindChild("Bot").FindChild("Top 2")
							.GetComponent<UILabel>().text = nowPlayer[i].hit2B.ToString()+"%";
						mBatting.transform.FindChild("Sprite").FindChild("Mid_BG").FindChild("Bot").FindChild("Top 3")
							.GetComponent<UILabel>().text = nowPlayer[i].hitBB.ToString()+"%";
						mBatting.transform.FindChild("Sprite").FindChild("Mid_BG").FindChild("Bot").FindChild("Top 4")
							.GetComponent<UILabel>().text = nowPlayer[i].hitAvg;
			
							
						Debug.Log("nextPlayer[i].hitAvg : " + nowPlayer[i].hitAvg);
			
							if(Holdname!=strImage){
								if (nowPlayer[i].imagePath != null && nowPlayer[i].imagePath.Length > 0){
									strImage =nowPlayer[i] .imagePath + nowPlayer[i].imageName;
									WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
									Debug.Log ("url : " + Constants.IMAGE_SERVER_HOST + strImage);
						//	gameObject.transform.FindChild("Scroll View").gameObject.SetActive(true);
								if(gameObject.transform.FindChild("Scroll View").gameObject.activeSelf){
									StartCoroutine (GetImage (www, mBatting.transform.FindChild("Sprite").FindChild("Current hitter").
		                          FindChild("Players Image BackGround").GetChild(0).GetChild(0).
		                          GetComponent<UITexture>()));
							}
								
							}
						}
					}
					Holdname = strImage;
				
			}

		}
//		Debug.Log ("quiz.gameRound : " + quiz.gameRound);
//		int round = (2 * quiz.gameSeq) + (quiz.inningType - 1);
//		if (UtilMgr.gameround < round&&UtilMgr.gameround>1) {
//			Landing.GetComponent<LandingManager> ().GetRank();
//		}
//			UtilMgr.gameround = round;
		//if(quiz.gameRound)

		Landing.GetComponent<LandingManager> ().SetHitter (QuizMgr.NextPlayerInfo);
	}


	IEnumerator GetImage(WWW www, UITexture texture)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		texture.mainTexture = tmpTex;
	}

//	void OnEnable()
//	{
//		ScriptMainTop smt = mTop.GetComponent<ScriptMainTop> ();
//		smt.mBtnHighlight.SetActive (false);
//		smt.mBtnLineup.SetActive (false);
//		smt.mBtnLivetalk.SetActive (false);
//		//smt.mBtnBingo.SetActive (false);
//	}
//
//	void OnDisable()
//	{
//		ScriptMainTop smt = mTop.GetComponent<ScriptMainTop> ();
//		smt.mBtnHighlight.SetActive (true);
//		smt.mBtnLineup.SetActive (true);
//		smt.mBtnLivetalk.SetActive (true);
//		//smt.mBtnBingo.SetActive (true);
//	}

	public void AnimateVS()
	{
		StartCoroutine ("VSAnimation");
//		transform.FindChild ("Lightning Spark").GetComponent<ParticleSystem> ().Simulate (5f, true, false);
	}

	IEnumerator VSAnimation(){
		yield return new WaitForSeconds (1.1f);

		mSpark1.SetActive (true);
		mSpark2.SetActive (true);
		mSpark1.GetComponent<ParticleSystem> ().Play ();
		mSpark2.GetComponent<ParticleSystem> ().Play ();
//		transform.root.GetComponent<AudioSource>().PlayOneShot (mBoom);

		GameObject go = mBatting.transform.FindChild("Panel").FindChild("SprVS").gameObject;
		TweenAlpha.Begin (go, 0f, 0f);
		TweenScale.Begin (go, 0f, new Vector3(1f, 1f, 1f));
		TweenAlpha.Begin (go, 0.5f, 1f);

		yield return new WaitForSeconds (1f);

		TweenAlpha.Begin (go, 0.5f, 0f);
		TweenScale.Begin(go, 0.5f, new Vector3(1.5f, 1.5f, 1.5f));

	}
	IEnumerator OpenAnimations(){

		transform.FindChild ("SprBG").gameObject.SetActive (true);

		shadow.SetActive (true);
		QuizMgr.IsBettingOpended = true;
		GameObject Menu = transform.FindChild ("Scroll View").FindChild ("GameObject").gameObject;
		Menu.transform.localPosition=new Vector3(0,-651f,0);
		transform.FindChild ("Scroll View").gameObject.SetActive (true);
		for (int i = 0; i<5; i++) {
			Menu.transform.localPosition+=new Vector3(0,655f/5f,0);
			if(Menu.transform.localPosition.y==3){
				break;
			}
			yield return new WaitForSeconds(0.05f);
		}

	}
	IEnumerator CloseAnimations(){

		GameObject Menu = transform.FindChild ("Scroll View").FindChild ("GameObject").gameObject;
		Menu.transform.localPosition=new Vector3(0,3,0);
		for (int i = 0; i<5; i++) {
			Menu.transform.localPosition-=new Vector3(0,651f/5f,0);
			if(Menu.transform.localPosition.y==-651){
				break;
			}
			yield return new WaitForSeconds(0.05f);
		}

		for (int i = 0; i <3; i++) {
			transform.FindChild ("Scroll View").FindChild ("GameObject").FindChild("Batting").FindChild("SprHit")
				.FindChild("BtnHit"+(i+1).ToString()).GetComponent<UIButton>().isEnabled = true;
		}
		for (int i = 0; i <3; i++) {
			transform.FindChild ("Scroll View").FindChild ("GameObject").FindChild("Batting").FindChild("SprOut")
				.FindChild("BtnOut"+(i+1).ToString()).GetComponent<UIButton>().isEnabled = true;
		}
		transform.FindChild ("Scroll View").FindChild ("GameObject").FindChild("SprBetting").GetComponent<ScriptBetting> ().BtnConfirm ();
		transform.parent.FindChild ("Top").GetComponent<ScriptMainTop> ().PostData ();
		QuizMgr.IsBettingOpended = false;
		transform.FindChild ("SprBG").gameObject.SetActive (false);
		shadow.SetActive (false);
		transform.FindChild ("Scroll View").gameObject.SetActive (false);
		gameObject.SetActive (false);
	
		
	}
	public void OpenAnimation()
	{
		StartCoroutine (OpenAnimations());
	}
	public void CloseAnimation()
	{
		StartCoroutine (CloseAnimations());
	}
	public void CloseBetting()
	{
	//	gameObject.SetActive(false);
		Debug.Log ("CloseBetting");
	}

}
