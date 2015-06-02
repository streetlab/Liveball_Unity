using UnityEngine;
using System.Collections;



public class NetMgr : MonoBehaviour{

	const float TIMEOUT = 10f;
	WWW mWWW;
	BaseEvent mBaseEvent;
	bool mIsUpload;
	bool mIsLoading;

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
		
		if(www.error == null && www.isDone)
		{
			Debug.Log(www.text);
//			CommonDialogue.Show (www.text);
			if(baseEvent != null)
				baseEvent.Init(www.text);
		}
		else
		{
//			DialogueMgr.ShowDialogue("네트워크오류", "네트워크 연결이 불안정합니다.\n인터넷 연결을 확인 후 다시 시도해주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			DialogueMgr.ShowDialogue("네트워크오류", "네트워크 연결이 불안정합니다.\n인터넷 연결을 확인 후 다시 시도해주세요.",
			                         DialogueMgr.DIALOGUE_TYPE.YesNo, "재시도", "", "종료하기", ConnectHandler);
			mWWW = www;
			mBaseEvent = baseEvent;
			mIsUpload = isUpload;
			mIsLoading = showLoading;
		}

		UtilMgr.DismissLoading ();
	}

	void ConnectHandler(DialogueMgr.BTNS btn){
		if(btn == DialogueMgr.BTNS.Btn1){
			StartCoroutine(webAPIProcess(mWWW, mBaseEvent, mIsLoading, mIsUpload));
//			mWWW = null;
//			mBaseEvent = null;
		} else{
			Application.Quit();
		}

	}

	private void webAPIUploadProcessEvent(BaseUploadRequest request, BaseEvent baseEvent, bool isTest, bool showLoading)
	{	
		WWWForm form = request.GetRequestWWWForm ();

		string host = Constants.UPLOAD_SERVER_HOST;
		if(isTest){
			host = Constants.UPLOAD_TEST_SERVER_HOST;
			Debug.Log("Send to Test Server");
		} else{
			Debug.Log("Send to Real Server");
		}
		WWW www = new WWW (host, form);

		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
//			return;
		}
		StartCoroutine (webAPIProcess(www, baseEvent, true, true));
	}

	private void webAPIProcessEvent(BaseRequest request, BaseEvent baseEvent){
		Debug.Log("webAPIProcessEvent");
		webAPIProcessEvent (request, baseEvent, true);
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
//		host = Constants.CHECK_SERVER_HOST2;
		
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
//			httpUrl = (Constants.QUERY_SERVER_HOST + reqParam);
//			httpUrl = reqParam;
		} else {
//			httpUrl = Constants.QUERY_SERVER_HOST;
		}

	
		//WWW www = new WWW (Constants.QUERY_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));
			WWW www = new WWW (Constants.UPLOAD_TEST_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));
		Debug.Log (reqParam);
		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
//			return;
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
		
//		WWW www = new WWW (Constants.QUERY_SERVER_HOST , System.Text.Encoding.UTF8.GetBytes(reqParam));
		WWW www = new WWW(reqParam);
		
		Debug.Log (reqParam);
		if(UtilMgr.OnPause){
			Debug.Log("Request is Canceled cause OnPause");
			//			return;
		}
		
		StartCoroutine (webAPIProcess(www, baseEvent, showLoading, false));
	}

	public static void DoLogin(LoginInfo loginInfo, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new LoginRequest(loginInfo), baseEvent);
	}

	public static void GetScheduleAll(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetScheduleAllRequest (), baseEvent);
	}

	public static void GetScheduleMore(string teamCode, int teamSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetScheduleMoreRequest(teamCode, teamSeq), baseEvent);
	}

	public static void GetUserRankingDailyForecast(int memSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingDailyForecast (memSeq), baseEvent);
	}

	public static void GetUserRankingDailyGold(int memSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingDailyGold (memSeq), baseEvent);
	}

	public static void GetUserRankingWeeklyForecast(int memSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingWeeklyForecast (memSeq), baseEvent);
	}

	public static void GetUserRankingWeeklyGold(int memSeq,BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetUserRankingWeeklyGold (memSeq), baseEvent);
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

	public static void GetProfile(int memSeq, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new GetProfileRequest (memSeq), baseEvent);
	}

	public static void ExitGame(BaseEvent baseEvent)
	{
		Debug.Log("ExitGame");
		Instance.webAPIProcessEvent (new ExitGameRequest (), baseEvent);
	}

	public static void JoinGame(BaseEvent baseEvent)
	{
		Instance.webAPIProcessEvent (new JoinGameRequest (), baseEvent);
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
		Instance.webAPIProcessEventForCheckVersion(new RequestIAPRequest(0, productCode), baseEvent, isTest, true);
	}

	public static void ComsumeIAP(int orderNo, string token, bool isTest, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEventForCheckVersion(new ComsumeIAPRequest(orderNo, token), baseEvent, isTest, true);
	}

	public static void DoneIAP(int orderNo, bool isTest, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEventForCheckVersion(new DoneIAPRequest(orderNo), baseEvent, isTest, true);
	}

	public static void CancelIAP(int orderNo, bool isTest, BaseEvent baseEvent)
	{
		Instance.webAPIProcessEventForCheckVersion(new CancelIAPRequest(orderNo), baseEvent, isTest, true);
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

	public static void GetEvents(BaseEvent baseEvent)
	{
		Instance.webAPINanooEvent(new GetEventsRequest(), baseEvent, true);
	}
}
