using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Collections.Generic;

public class NetMgr : MonoBehaviour{
	
	const float TIMEOUT = 10f;
	WWW mWWW;
	BaseEvent mBaseEvent;
	bool mIsUpload;
	bool mIsLoading;
	
	private static Socket mSocket;// = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	private static AsyncCallback mConnectionCallback = new AsyncCallback(Instance.HandleConnect);
	private static AsyncCallback mSendingCallback = new AsyncCallback(Instance.HandleDataSend);
	private static AsyncCallback mReceivingCallback = new AsyncCallback(Instance.HandleDataReceive);
	private static byte[] mReceiveBuffer = new byte[8142];
	private static byte[] mSendBuffer = new byte[8142];
	private static List<SocketMsgInfo> mSocketMsgList = new List<SocketMsgInfo>();
	private static bool mRecvSemaphore;
	//    BaseEvent mSocketEvent;
	
	private static NetMgr _instance = null;
	public static NetMgr Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(NetMgr)) as NetMgr;
				if(!_instance)
				{
					GameObject container = new GameObject();
					container.name = "NetMgrContainer";
					_instance = container.AddComponent(typeof(NetMgr)) as NetMgr;
				}
			}
			return _instance;
		}
	}
	
	IEnumerator webCSAPIProcess(WWW www, BaseCSEvent baseEvent, bool showLoading)
	{
		if(www == null){
			Debug.Log("www is null");
			yield break;
		}
		
		float timeSum = 0f;
		
		if(showLoading)
			UtilMgr.ShowLoading (showLoading);
		
		while(!www.isDone && 
		      string.IsNullOrEmpty(www.error) && 
		      timeSum < TIMEOUT) { 
			timeSum += Time.deltaTime; 
			yield return 0; 
		} 
		
		
		if(www.error == null && www.isDone)
		{
			Debug.Log(www.text);
			
			if(baseEvent != null)
				baseEvent.Init(www.text);
		}
		else
		{
			Debug.Log(www.error);
			//            //            DialogueMgr.ShowDialogue("네트워크오류", "네트워크 연결이 불안정합니다.\n인터넷 연결을 확인 후 다시 시도해주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			//            DialogueMgr.ShowDialogue("네트워크오류", "네트워크 연결이 불안정합니다.\n인터넷 연결을 확인 후 다시 시도해주세요.",
			//                                     DialogueMgr.DIALOGUE_TYPE.YesNo, "재시도", "", "타이틀로 가기", ConnectHandlerForHttp);
			//            mWWW = www;
			//            mBaseEvent = baseEvent;
			//            mIsUpload = isUpload;
			//            mIsLoading = showLoading;
		}
		
		UtilMgr.DismissLoading ();
	}
	
	IEnumerator webAPIProcess(WWW www, BaseEvent baseEvent, bool showLoading, bool isUpload)
	{
		if(www == null){
			Debug.Log("www is null");
			yield break;
		}
		
		float timeSum = 0f;
		
		if(isUpload){
			UtilMgr.ShowLoading(true, www);
			
			yield return www;
		} else{
			if(showLoading)
				UtilMgr.ShowLoading (showLoading);
			
			while(!www.isDone && 
			      string.IsNullOrEmpty(www.error) && 
			      timeSum < TIMEOUT) { 
				timeSum += Time.deltaTime; 
				yield return 0; 
			} 
		}
		//Debug.Log ();
		if(www.error == null && www.isDone)
		{
			Debug.Log(www.text);
			//            CommonDialogue.Show (www.text);
			if(baseEvent != null){
				Debug.Log("baseEvent != null");
				baseEvent.Init(www.text);
			}
		}
		else
		{
			Debug.Log(www.error);
			//            DialogueMgr.ShowDialogue("네트워크오류", "네트워크 연결이 불안정합니다.\n인터넷 연결을 확인 후 다시 시도해주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			DialogueMgr.ShowDialogue("네트워크오류", "네트워크 연결이 불안정합니다.\n인터넷 연결을 확인 후 다시 시도해주세요.",
			                         DialogueMgr.DIALOGUE_TYPE.YesNo, "재시도", "", "타이틀로 가기", ConnectHandlerForHttp);
			mWWW = www;
			mBaseEvent = baseEvent;
			mIsUpload = isUpload;
			mIsLoading = showLoading;
		}
		
		UtilMgr.DismissLoading ();
	}
	
	void ConnectHandlerForHttp(DialogueMgr.BTNS btn){
		if(btn == DialogueMgr.BTNS.Btn1){
			StartCoroutine(webAPIProcess(mWWW, mBaseEvent, mIsLoading, mIsUpload));
			//            mWWW = null;
			//            mBaseEvent = null;
		} else{
			//            Application.Quit();
			AutoFade.LoadLevel("SceneLogin");
		}
		
	}
	
	private void webAPIUploadProcessEvent(BaseUploadRequest request, BaseEvent baseEvent, bool isTest, bool showLoading)
	{    
		WWWForm form = request.GetRequestWWWForm ();
		
		string host = Constants.AUTH_SERVER_HOST;
		//        if(isTest){
		//            host = Constants.UPLOAD_TEST_SERVER_HOST;
		//            Debug.Log("Send to Test Server");
		//        } else{
		//            Debug.Log("Send to Real Server");
		//        }
		//        host = Constants.UPLOAD_TEST_SERVER_HOST;
		WWW www = new WWW (host, form);
		
		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
			//            return;
		}
		StartCoroutine (webAPIProcess(www, baseEvent, true, true));
	}
	
	private void webAPIProcessEvent(BaseRequest request, BaseEvent baseEvent){
		Debug.Log("webAPIProcessEvent");
		webAPIProcessEvent (request, baseEvent, true);
	}
	
	void webAPIProcessEventForCS(BaseCSRequest request, BaseCSEvent baseEvent, bool showLoading){
		WWW www = new WWW (Constants.CS_SERVER_HOST+request.GetQueryId(),
		                   System.Text.Encoding.UTF8.GetBytes(request.ToRequestString()));
		
		Debug.Log (request.ToRequestString());
		
		StartCoroutine (webCSAPIProcess(www, baseEvent, showLoading));
	}
	
	private void webAPIProcessEventForCheckVersion(BaseRequest request, BaseEvent baseEvent, bool isTest, bool showLoading)
	{        
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
		} else {
		}
		
		string host = Constants.CHECK_SERVER_HOST;
		if(isTest){
			host = Constants.CHECK_TEST_SERVER_HOST;
			Debug.Log("Send to Test Server");
		} else{
			Debug.Log("Send to Real Server");
		}
		//        host = Constants.CHECK_TEST_SERVER_HOST;
		
		WWW www = new WWW (host , System.Text.Encoding.UTF8.GetBytes(reqParam));
		
		Debug.Log (reqParam);
		
		StartCoroutine (webAPIProcess(www, baseEvent, showLoading, false));
	}
	
	private void webAPIProcessEventToAuth(BaseRequest request, BaseEvent baseEvent, bool isTest, bool showLoading)
	{        
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
		} else {
		}
		
		string host = Constants.AUTH_SERVER_HOST;
		//        if(isTest){
		//            host = Constants.CHECK_TEST_SERVER_HOST;
		//            Debug.Log("Send to Test Server");
		//        } else{
		//            Debug.Log("Send to Real Server");
		//        }
		//        host = Constants.CHECK_TEST_SERVER_HOST;
		
		WWW www = new WWW (host , System.Text.Encoding.UTF8.GetBytes(reqParam));
		
		Debug.Log (reqParam);
		
		StartCoroutine (webAPIProcess(www, baseEvent, showLoading, false));
	}
	
	private void webAPIProcessEvent(BaseRequest request, BaseEvent baseEvent, bool showLoading)
	{
		Debug.Log("webAPIProcessEvent2");
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
			//            httpUrl = (Constants.QUERY_SERVER_HOST + reqParam);
			//            httpUrl = reqParam;
		} else {
			//            httpUrl = Constants.QUERY_SERVER_HOST;
		}
		//AUTH_SERVER_HOST
		WWW www = new WWW (Constants.QUERY_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));
		
		Debug.Log (reqParam);
		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
			//            return;
		}
		
		StartCoroutine (webAPIProcess(www, baseEvent, showLoading, false));
	}
	private void webAPIProcessGetScheduleEvent(BaseRequest request, BaseEvent baseEvent, bool showLoading)
	{
		Debug.Log("webAPIProcessEvent2");
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
			//            httpUrl = (Constants.QUERY_SERVER_HOST + reqParam);
			//            httpUrl = reqParam;
		} else {
			//            httpUrl = Constants.QUERY_SERVER_HOST;
		}
		//AUTH_SERVER_HOST
		WWW www = new WWW (Constants.AUTH_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));
		
		Debug.Log (reqParam);
		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
			//            return;
		}
		
		StartCoroutine (webAPIProcess(www, baseEvent, showLoading, false));
	}
	
	private void webAPINanooEvent(BaseNanooRequest request, BaseEvent baseEvent, bool showLoading)
	{
		Debug.Log("webAPINanoo");
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
		} else {
			
		}
		
		//        WWW www = new WWW (request.GetParam(), System.Text.Encoding.UTF8.GetBytes(reqParam));
		WWW www = new WWW(reqParam);
		
		Debug.Log (reqParam);
		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
			//            return;
		}
		
		StartCoroutine (webAPIProcess(www, baseEvent, showLoading, false));
	}
	
	private void tcpAPIProcessEvent(BaseSocketRequest request, BaseEvent baseEvent, bool showLoading)
	{
		if(mSocket == null && !mSocket.Connected){
			//            Instance.socketJoinEvent(baseEvent);
			//req reconnect;
			return;
		}
		
		string reqParam = "";
		string httpUrl = "";
		if (request != null) {
			reqParam = request.ToRequestString();
		} else {
			
		}
		
		//        WWW www = new WWW (Constants.QUERY_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));
		WWW www = new WWW(reqParam);
		
		Debug.Log (reqParam);
		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
			//            return;
		}
		
		StartCoroutine (webAPIProcess(www, baseEvent, showLoading, false));
	}
	private void webProcess2(WWW www, EventDelegate eventd){
		StartCoroutine (webProcess(www,eventd));
	}
	IEnumerator webProcess(WWW www, EventDelegate eventd){
		float timeSum = 0f;
		while(!www.isDone && 
		      string.IsNullOrEmpty(www.error) && 
		      timeSum < 10f) { 
			timeSum += Time.deltaTime; 
			yield return 0; 
		} 
		
		
		if(www.error == null && www.isDone)
		{
			Debug.Log(www.text);
			LobbyGiftCommander.mGift = Newtonsoft.Json.JsonConvert.DeserializeObject<LobbyGiftCommander.GiftListResponse>(www.text);
		}
		
		
		eventd.Execute();
	}
	
	private void socketJoinEvent(){
		if(mSocket != null)
			mSocket.Close();
		
		mSocket = null;
		//        if(mSocket == null || !mSocket.Connected)
		mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		
		try{
			mSocket.BeginConnect(Constants.GAME_SERVER_HOST, Constants.GAME_SERVER_PORT,
			                     mConnectionCallback, null);
		} catch(Exception e){
			Debug.Log ("beginConnect : "+e.Message);
			DialogueMgr.ShowDialogue("서버 연결 오류", "네트워크 연결이 불안정합니다.\n인터넷 연결을 확인 후 다시 시도해주세요.",
			                         DialogueMgr.DIALOGUE_TYPE.YesNo, "재시도", "", "타이틀로 가기", ConnectHandlerForTcp);
		}                
	}
	
	void ConnectHandlerForTcp(DialogueMgr.BTNS btn){
		if(btn == DialogueMgr.BTNS.Btn1){
			socketJoinEvent();
		} else{
			//            Application.Quit();
			AutoFade.LoadLevel("SceneLogin");
		}
		
	}
	
	private void socketExitEvent(BaseEvent baseEvent){
		if(mSocket != null){
			mSocket.Disconnect(false);
		}
		mSocket = null;
	}
	
	//    public static void DoLogin(LoginInfo loginInfo, BaseEvent baseEvent, bool isTest, bool showLoading)
	//    {
	//        Debug.Log("DoLogin");
	//        Instance.webAPIProcessEvent (new LoginRequest(loginInfo), baseEvent);
	//        Instance.webAPIProcessEventToAuth (new LoginGuestRequest(loginInfo), baseEvent, isTest, showLoading);
	//    }
	
	public static void LoginGuest(LoginInfo loginInfo, BaseEvent baseEvent, bool isTest, bool showLoading)
	{
		Debug.Log("LoginGuest");
		//        Instance.webAPIProcessEventToAuth (new LoginGuestRequest(loginInfo), baseEvent, isTest, showLoading);
		Instance.webAPIUploadProcessEvent (new LoginGuestRequest(loginInfo), baseEvent, isTest, showLoading);
	}
	
	//    public static void GetScheduleAll(BaseEvent baseEvent)
	//    {
	//        Instance.webAPIProcessGetScheduleEvent (new GetScheduleAllRequest (), baseEvent,true);
	//    }
	
	public static void GetContestList(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new ContestListRequest (), baseEvent);
	}

	public static void GetContestRanking(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new ContestRankingeRequest (0), baseEvent);
	}
	public static void GetHistoryContestRanking(int GameSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new ContestRankingeRequest (GameSeq), baseEvent);
	}
	
	public static void GetContestData(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new ContestDataRequest (), baseEvent);
	}
	
	public static void GetHistoryList(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new ContestHistoryRequest (), baseEvent);
	}
	public static void GetPresetList(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new PresetListRequest (), baseEvent);
	}
	
	public static void GetPresetData(int presetSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new PresetDataRequest (presetSeq), baseEvent);
	}

	public static void ContestPresetChange(string quizSeq,string presetValue,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new ContestPresetChangeRequest (quizSeq,presetValue), baseEvent);
	}

	public static void PresetAdd(int ContestSeq,List<int> ChoseList,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new PresetAddRequest (ContestSeq,ChoseList), baseEvent);
	}
	public static void PresetUpdate(int ContestSeq,int PresetSeq,List<int> ChoseList,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new PresetUpdateRequest (ContestSeq,PresetSeq,ChoseList), baseEvent);
	}
	public static void GetGift(EventDelegate E)
	{
		WWW www = new WWW("http://appif.liveball.kr:4002/tuby_file/gift/gift.json");
		Instance.webProcess2 (www,E);
	}
	public static void GetScheduleAll(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetScheduleAllRequest (), baseEvent);
	}
	
	public static void GetScheduleToday(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetScheduleTodayRequest (), baseEvent);
	}
	
	public static void GetScheduleMore(string teamCode, int teamSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetScheduleMoreRequest(teamCode, teamSeq), baseEvent);
	}
	public static void GameSposGame(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GameSposGameRequest(), baseEvent);
	}
	
	
	public static void GetUserRankingSeasonForecast(int memSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingSeasonForecast (memSeq), baseEvent);
	}
	
	public static void GetUserRankingSeasonPoint(int memSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingSeasonPoint (memSeq), baseEvent);
	}
	
	public static void GetUserRankingWeeklyForecast(int memSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingWeeklyForecast (memSeq), baseEvent);
	}
	
	public static void GetUserRankingWeeklyPoint(int memSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingWeeklyPoint (memSeq), baseEvent);
	}
	
	public static void GetGameParticipantRanking(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetGameParticipantRankingRequest (), baseEvent);
	}
	
	
	public static void GetInvenItem(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetInvenItemRequest (), baseEvent);
	}
	
	public static void GameJoinNEntryFee(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GameJoinNEntryFee (), baseEvent);
	}
	
	public static void DoneInvenItem(long itemNo,long itemid, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetDoneInvenItemRequest (itemNo,itemid), baseEvent);
	}
	
	public static void DeleteInvenItem(long itemNo,long itemid, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new DeleteInvenItemInfoRequest (itemNo,itemid), baseEvent);
	}
	
	public static void GetUserMailBox(int memSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetMailboxRequest (memSeq), baseEvent);
	}
	
	public static void GetUserDoneMailBox(int memSeq,int mailSeq,int attachSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetDoneMailboxRequset (memSeq,mailSeq,attachSeq), baseEvent);
	}
	
	public static void GetUserCheckMailBox(int memSeq,int mailSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetCheckMailboxRequset (memSeq,mailSeq), baseEvent);
	}
	
	public static void GetGameSposDetailBoard(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetGameSposDetailBoardRequest (), baseEvent, false);
	}
	
	public static void GetGameSposPlayBoard(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetGameSposPlayBoardRequest (), baseEvent, false);
	}
	
	public static void GetCardInven(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetCardInvenRequest (), baseEvent);
	}
	
	public static void GetPreparedQuiz(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetPreparedQuizRequest (), baseEvent);
	}
	
	public static void GetProgressQuiz(int quizListSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetProgressQuizRequest (quizListSeq), baseEvent);
	}

	public static void GetTeamQuiz(string teamCode, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetTeamQuizRequest (teamCode), baseEvent);
	}
	
	public static void GetProfile(int memSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetProfileRequest (memSeq), baseEvent);
	}
	
	public static void ExitGame(BaseEvent baseEvent)
	{
		//        Instance.webAPIProcessEvent (new ExitGameRequest (), baseEvent);
		Instance.socketExitEvent(baseEvent);
	}

	public static void ProcStoreGacha(long product,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new procStoreGachaRequest (product), baseEvent);
	}

	public static void JoinGame()
	{
		//        Instance.webAPIProcessEvent (new JoinGameRequest (), baseEvent);
		//        Instance.tcpAPIProcessEvent(new JoinGameSocketRequest(), baseEvent, true);
		Instance.socketJoinEvent();
	}
	
	public static void JoinQuiz(JoinQuizInfo joinInfo, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new JoinQuizRequest (joinInfo), baseEvent);
	}
	
	public static void GetQuizResult(int quizListSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetQuizResultRequest (quizListSeq), baseEvent, false);
	}
	
	public static void GetSimpleResult(int quizListSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetSimpleResultRequest (quizListSeq), baseEvent, false);
	}
	
	public static void JoinMember(JoinMemberInfo memInfo, BaseEvent baseEvent, bool isTest, bool bShowLoading)
	{
		Instance.webAPIUploadProcessEvent (new JoinMemberRequest (memInfo), baseEvent, isTest, bShowLoading);
	}
	
	public static void GetTeamRanking(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetTeamRankingRequest (), baseEvent);
	}
	
	public static void GetPlayerStatistics(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetPlayerStatisticsRequest (), baseEvent);
	}
	
	public static void CheckVersion(BaseEvent baseEvent, bool isTest)
	{
		Instance.webAPIProcessEventForCheckVersion (new CheckVersionRequest (), baseEvent, isTest, true);
	}
	
	public static void CheckMemberDevice(string deviceId, BaseEvent baseEvent){
		Instance.webAPIProcessEventToAuth(new CheckMemberDeviceRequest(deviceId), baseEvent, false, true);
	}
	
	public static void CheckNickname(string name, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new CheckNickRequest (name), baseEvent);
	}
	
	public static void GetLineup(string teamCode, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new GetLineupRequest(teamCode), baseEvent);
	}
	
	public static void GetItemShopGoldList(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new GetItemShopGoldRequest(), baseEvent);
	}
	
	public static void GetItemShopRubyList(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new GetItemShopRubyRequest(), baseEvent);
	}
	
	public static void GetItemShopItemList(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new GetItemShopItemRequest(), baseEvent);
	}
	
	public static void RequestIAP(int productId, string productCode, bool isTest, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEventToAuth(new RequestIAPRequest(productId, productCode), baseEvent, isTest, true);
	}
	
	public static void ComsumeIAP(int orderNo, string token, bool isTest, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEventToAuth(new ComsumeIAPRequest(orderNo, token), baseEvent, isTest, true);
	}
	
	public static void DoneIAP(int orderNo, bool isTest, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEventToAuth(new DoneIAPRequest(orderNo), baseEvent, isTest, true);
	}
	
	public static void CancelIAP(int orderNo, bool isTest, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEventToAuth(new CancelIAPRequest(orderNo), baseEvent, isTest, true);
	}
	
	public static void GetInAppHistory(bool isTest, BaseEvent baseEvent){
		Instance.webAPIProcessEventToAuth(new GetInAppHistoryRequest(), baseEvent, isTest, true);
	}
	
	public static void InAppPurchase(bool isTest, string productCode, string token, string purchaseKey, BaseEvent baseEvent){
		Instance.webAPIProcessEventToAuth(new InAppPurchaseRequest(productCode, token, purchaseKey), baseEvent, isTest, true);
	}
	
	public static void PurchaseGold(int productId, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new PurchaseGoldRequest(productId), baseEvent);
	}
	
	public static void PurchaseItem(int productId, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new PurchaseItemRequest(productId), baseEvent);
	}
	
	public static void UpdateMemberInfo(JoinMemberInfo memInfo, BaseEvent baseEvent, bool isTest, bool bShowLoading)
	{
		Instance.webAPIUploadProcessEvent(new UpdateMemberInfoRequest(memInfo), baseEvent, isTest, bShowLoading);
	}
	
	public static void ChangGestInfo(LoginInfo loginInfo, BaseEvent baseEvent, bool isTest, bool bShowLoading)
	{
		Instance.webAPIUploadProcessEvent(new ChangeGestRequest(loginInfo), baseEvent, isTest, bShowLoading);
	}
	
	
	public static void GetSposTeamInfo(string teamCode , BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new GetSposTeamInfoRequest(teamCode), baseEvent);
	}
	
	public static void AccuseContent(AccusationInfo accuInfo , BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent(new AccuseContentRequest(accuInfo), baseEvent);
	}
	
	public static void GetEvents(BaseNanooRequest.API_TYPE apiType, BaseEvent baseEvent)
	{
		Instance.webAPINanooEvent(new GetEventsRequest(apiType), baseEvent, true);
	}
	
	public static void CSGetList(BaseCSEvent baseEvent){
		Instance.webAPIProcessEventForCS(new CSGetListRequest(), baseEvent, true);
	}

	public static void CheckMemberPincode(string pincode, BaseEvent baseEvent){
		Instance.webAPIProcessEventToAuth(new CheckMemberPincodeRequest(pincode), baseEvent, false, true);
	}
	
	public static void MergeMembership(string pincode, BaseEvent baseEvent){
		Instance.webAPIProcessEventToAuth(new MergeMembershipRequest(pincode), baseEvent, false, true);
	}
	


	public static void SendSocketMsg(String msg) {
		mSendBuffer = Encoding.UTF8.GetBytes(msg);
		
		if(mSocket == null || !mSocket.Connected){
			Instance.socketJoinEvent();
			Debug.Log("reconnect socket");
			return;
		}
		
		
		try {
			mSocket.BeginSend(mSendBuffer, 0, mSendBuffer.Length, SocketFlags.None,
			                  mSendingCallback, null);
		} catch (Exception ex) {
			Debug.Log("전송 중 오류 발생! : "+ ex.Message);
			Instance.socketJoinEvent();
		}
	}
	
	private void HandleConnect(IAsyncResult ar){
		Debug.Log("Connection Succeed!");
		
		//        if(mReceivingCallback == null)
		//            mReceivingCallback = new AsyncCallback(HandleDataReceive);
		
		mSocket.BeginReceive(mReceiveBuffer, 0, mReceiveBuffer.Length, SocketFlags.None, mReceivingCallback, null);
		
		SendSocketMsg(new JoinGameSocketRequest().ToRequestString());
	}
	
	private void HandleDataReceive(IAsyncResult ar) {
		Int32 recvBytes = 0;
		try{
			recvBytes = mSocket.EndReceive(ar);
		} catch{
			Debug.Log("recv error");
			return;
		}
		
		// 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
		if ( recvBytes > 0 ) {
			mRecvSemaphore = true;
			
			Byte[] msgByte = new Byte[recvBytes];
			Array.Copy(mReceiveBuffer, msgByte, recvBytes);
			string msg = Encoding.UTF8.GetString(msgByte);
			
			// 받은 메세지를 출력
			Debug.Log("Received : "+ msg);
			
			SocketMsgInfo msgInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<SocketMsgInfo>(msg);
			mSocketMsgList.Add(msgInfo);
			mRecvSemaphore = false;
		}
		
		try {
			mSocket.BeginReceive(mReceiveBuffer, 0, mReceiveBuffer.Length, SocketFlags.None, mReceivingCallback, null);
		} catch (Exception ex) {
			// 예외가 발생하면 mReceiveBuffer 종료한다mReceiveBuffer("자료 수신 대기 도중 오류 발생! 메세지: "+ ex.Message);
			Instance.socketJoinEvent();
			return;
		}
	}
	
	private void HandleDataSend(IAsyncResult ar){        
		// 보낸 바이트 수를 저장할 변수 선언
		Int32 sentBytes;
		
		try {
			// 자료를 전송하고, 전송한 바이트를 가져옵니다.
			sentBytes = mSocket.EndSend(ar);
		} catch (Exception ex) {
			// 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
			Debug.Log("자료 송신 도중 오류 발생! 메세지: "+ ex.Message);
			Instance.socketJoinEvent();
			return;
		}
		
		if ( sentBytes > 0 ) {
			// 여기도 마찬가지로 보낸 바이트 수 만큼 배열 선언 후 복사한다.
			Byte[] msgByte = new Byte[sentBytes];
			Array.Copy(mSendBuffer, msgByte, sentBytes);
			
			Debug.Log("Sending : "+ Encoding.UTF8.GetString(msgByte));
		}
	}
	
	void Update(){
		if(mSocketMsgList.Count > 0 && !mRecvSemaphore){
			QuizMgr.SocketReceived(mSocketMsgList[0]);
			mSocketMsgList.RemoveAt(0);
		}
	}
}
