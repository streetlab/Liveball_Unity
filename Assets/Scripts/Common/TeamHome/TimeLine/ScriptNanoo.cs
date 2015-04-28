using UnityEngine;
using System.Collections;

public class ScriptNanoo : MonoBehaviour {

	const string SAMSUNG_URL = "https://game.nanoo.so/liveball/board?cd=185";
	const string NEXEN_URL = "https://game.nanoo.so/liveball/board?cd=186";
	const string NC_URL = "https://game.nanoo.so/liveball/board?cd=187";
	const string LG_URL = "https://game.nanoo.so/liveball/board?cd=188";
	const string SK_URL = "https://game.nanoo.so/liveball/board?cd=189";
	const string DOOSAN_URL = "https://game.nanoo.so/liveball/board?cd=190";
	const string LOTTE_URL = "https://game.nanoo.so/liveball/board?cd=191";
	const string KIA_URL = "https://game.nanoo.so/liveball/board?cd=192";
	const string HANHWA_URL = "https://game.nanoo.so/liveball/board?cd=193";
	const string KT_URL = "https://game.nanoo.so/liveball/board?cd=194";

	private UniWebView mWebView;
	enum STATE_WEBVIEW{
		VISIBLE,
		INVISIBLE
	}
	STATE_WEBVIEW mStateWebview;

	public GameObject mMainMenu;
	public GameObject mTop;
	bool StatusBarIsHidden;

	// Use this for initialization
	void Start () {
		Debug.Log("Nanoo Start!");
		InitNanoo ();
	}

	void Update(){
		CheckVisible();
		CheckStatusBar();
	}

	void OnApplicationPause(bool pause){
		Debug.Log("pause is "+pause);
		if(pause){
			HideWebView();
		} else{
			ShowWebView();
		}
	}

	void CheckStatusBar(){
//		Debug.Log("y is "+mTop.transform.localPosition.y);
//		if(Screen.height > Constants.SCREEN_HEIGHT_ORIGINAL){		
//			int diff = Screen.height - Constants.SCREEN_HEIGHT_ORIGINAL;
//			Debug.Log("diff is"+diff);
//			mTop.transform.localPosition = new Vector3(0 , -25f+(diff/2)-10, 0);
//		} else
//			mTop.transform.localPosition = new Vector3(0 , -25f, 0);



	}

	void CheckVisible(){
		string menuStatus = mMainMenu.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("StatusAnimation").Value;
		
		if (menuStatus.Equals ("Closed")) {
			ShowWebView();
		} else {
			HideWebView();
		}
	}

	void InitNanoo(){
		mStateWebview = STATE_WEBVIEW.INVISIBLE;
		string url = GetTeamURL ();

		mWebView = GetComponent<UniWebView>();
		if (mWebView == null) {
			Debug.Log("Nanoo Creating");
			mWebView = gameObject.AddComponent<UniWebView>();
			mWebView.SetShowSpinnerWhenLoading(true);
			mWebView.autoShowWhenLoadComplete = true;
			mWebView.OnLoadBegin += OnLoadBegin;
			mWebView.OnReceivedMessage += OnReceivedMessage;
			mWebView.OnLoadComplete += OnLoadComplete;
			mWebView.OnWebViewShouldClose += OnWebViewShouldClose;
			mWebView.OnEvalJavaScriptFinished += OnEvalJavaScriptFinished;			
			mWebView.InsetsForScreenOreitation += InsetsForScreenOreitation;
			mWebView.OnReceivedKeyCode += OnReceivedKeyCode;


//			mWebView.SetTransparentBackground(true);
//			mWebView.toolBarShow = true;

		}

		mWebView.url = url;

		mWebView.Load ();
	}

	void OnReceivedKeyCode (UniWebView webView, int keyCode)
	{
		Debug.Log ("OnRecievedKeyCode : " + keyCode);
	}	

	void OnLoadBegin(UniWebView webView, string loadingUrl){
		UtilMgr.ShowLoading (true);
	}

	bool OnWebViewShouldClose(UniWebView webView) {
		Debug.Log ("OnWebViewShouldClose");

		return false;

		if (webView == mWebView) {
			mWebView = null;
			return true;
		}
		return false;
	}

	void OnEvalJavaScriptFinished(UniWebView webView, string result) {
		Debug.Log("js result: " + result);
	}

	void OnReceivedMessage(UniWebView webView, UniWebViewMessage message) {
		Debug.Log ("Received a message from native");
		Debug.Log (message.rawMessage);
	}

	UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation) {
		Debug.Log ("InsetsForScreenOreitation");

		float myRatio = Screen.width / 720f;

//		if(Screen.height > Constants.SCREEN_HEIGHT_ORIGINAL){		
			return new UniWebViewEdgeInsets((int)(125*myRatio),0,0,0);
//		} else {
//			return new UniWebViewEdgeInsets((int)(125*myRatio)+Constants.HEIGHT_STATUS_BAR,0,0,0);
//		}
	}
	
	void OnLoadComplete(UniWebView webView, bool success, string errorMessage) {
		Debug.Log ("OnLoadComplete");

		UtilMgr.DismissLoading ();

		if (success) {
//			webView.Show();
			mStateWebview = STATE_WEBVIEW.VISIBLE;
		}

//		UniWebViewEdgeInsets insets = new UniWebViewEdgeInsets (100, 0, 1130, 720);//top, left, btm, right
//		webView.insets = insets;

//		Debug.Log ("insets top : " + webView.insets.top);
	}

	string GetTeamURL(){
		switch (UserMgr.UserInfo.GetTeamCode()) {
		case "LG":
			return LG_URL;
		case "LT":
			return LOTTE_URL;
		case "HH":
			return HANHWA_URL;
		case "OB":
			return DOOSAN_URL;
		case "HT":
			return KIA_URL;
		case "SS":
			return SAMSUNG_URL;
		case "WO":
			return NEXEN_URL;
		case "SK":
			return SK_URL;
		case "NC":
			return NC_URL;
		case "KT":
			return KT_URL;
		default:
			return SAMSUNG_URL;
		}
	}

	public void HideWebView(){
		if (mStateWebview == STATE_WEBVIEW.VISIBLE) {
			mWebView.Hide ();
			mStateWebview = STATE_WEBVIEW.INVISIBLE;
		}
	}

	public void ShowWebView(){
		if (mStateWebview == STATE_WEBVIEW.INVISIBLE) {
			mWebView.Show ();
			mStateWebview = STATE_WEBVIEW.VISIBLE;
		}
	}
}
