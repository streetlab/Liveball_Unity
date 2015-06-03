using UnityEngine;
using System.Collections;

public class ScriptEvents : MonoBehaviour {
		
	private UniWebView mWebView;
	enum STATE_WEBVIEW{
		VISIBLE,
		INVISIBLE
	}
	STATE_WEBVIEW mStateWebview;

	public GameObject mLblTitle;
	bool StatusBarIsHidden;

	GetEventsEvent mEvent;
	public int Page;
	public int MAX_PAGE;
	
	// Use this for initialization
	void Start () {
		Debug.Log("Events Start!");
		InitNanoo ();
		mEvent = new GetEventsEvent(new EventDelegate(this, "GotEvents"));
		NetMgr.GetEvents(mEvent);
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

	public void GotEvents(){
//		Debug.Log("GotEvents");
		Page = 1;
		MAX_PAGE = mEvent.Response.result.count;
		GoToNext();
	}

	public void GoToNext(){
//		Debug.Log("GoToNext");
		mLblTitle.GetComponent<UILabel>().text = mEvent.Response.result.data[Page-1].title;

		mWebView.url = mEvent.Response.result.data[Page-1].url;
		mWebView.Load();
	}
	
	public string GetUrl(){
		return mWebView.url;
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
		//		string menuStatus = mMainMenu.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("StatusAnimation").Value;
		//		
		//		if (menuStatus.Equals ("Closed")) {
		//			ShowWebView();
		//		} else {
		//			HideWebView();
		//		}
	}
	
	void InitNanoo(){
		mStateWebview = STATE_WEBVIEW.INVISIBLE;
		
		mWebView = GetComponent<UniWebView>();
		if (mWebView == null) {
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
//		
//		mWebView.url = LOVE_URL;
//		
//		mWebView.Load ();
//		mWebView.Stop();
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
		return new UniWebViewEdgeInsets((int)(96*myRatio),0,0,0);
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
