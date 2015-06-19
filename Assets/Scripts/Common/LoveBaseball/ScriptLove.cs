﻿using UnityEngine;
using System.Collections;

public class ScriptLove : MonoBehaviour {
	
	
	const string LOVE_URL = "https://game.nanoo.so/liveball/board?cd=221";
	
	private UniWebView mWebView;
	enum STATE_WEBVIEW{
		VISIBLE,
		INVISIBLE
	}
	STATE_WEBVIEW mStateWebview;
	
	public GameObject mMainMenu;
	public GameObject mTop;
	public GameObject TF_Post;
	public GameObject mRight;

	public GameObject mBtnMenu;
	public GameObject mBtnBack;
	public GameObject mBtnAccusation;
	public GameObject mBtnNotice;

	string mBoardNum;
	string mContentNum;

	bool StatusBarIsHidden;
	
	// Use this for initialization
	void Start () {
		Debug.Log("Love Start!");
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
//
//

	}
	
	void CheckVisible(){
		string menuStatus = mMainMenu.GetComponent<PlayMakerFSM>().
			FsmVariables.FindFsmString("StatusAnimation").Value;
//		Debug.Log ("menuStatus : " + menuStatus + " ?? " + mMainMenu);
		bool isOpen = mRight.GetComponent<ScriptMainMenuRight>().IsOpen;
		
		if (menuStatus.Equals ("Closed") 
		    && !isOpen
		    && !DialogueMgr.IsShown
		    && !TF_Post.activeSelf) {
			ShowWebView();
		} else {
			HideWebView();
		}
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
		
		mWebView.url = LOVE_URL;
		
		mWebView.Load ();
	}
	
	void OnReceivedKeyCode (UniWebView webView, int keyCode)
	{
		Debug.Log ("OnRecievedKeyCode : " + keyCode);
	}	
	
	void OnLoadBegin(UniWebView webView, string loadingUrl){
		Debug.Log("OnLoadBegin : "+loadingUrl);
		if(loadingUrl.Contains("liveball/board?cd=")){//in board
			mBtnMenu.SetActive(true);
			//			mBtnNotice.SetActive(true);
			
			//			mBtnAccusation.SetActive(false);
			mBtnBack.SetActive(false);
			
			int startNum = loadingUrl.IndexOf("/board?")+10;
			mBoardNum = loadingUrl.Substring(startNum);
			Debug.Log("mBoardNum : "+mBoardNum);
		} else
		if(loadingUrl.Contains("liveball/board/")){//in content
			//			Debug.Log("index : " + loadingUrl.IndexOf("/board/"));
			// switch menu btn to back btn
			mBtnBack.SetActive(true);
			//			mBtnAccusation.SetActive(true);
			
			mBtnMenu.SetActive(false);
			//			mBtnNotice.SetActive(false);
			// show accusation btn
			int startNum = loadingUrl.IndexOf("/board/")+7;
			int endNum = loadingUrl.IndexOf("?cd=");
			mContentNum = loadingUrl.Substring(startNum, endNum-startNum);
			Debug.Log("mContentNum : "+mContentNum);
		} else{
			//turn off accusation
			mBtnBack.SetActive(true);
			
			//			mBtnNotice.SetActive(false);
			mBtnMenu.SetActive(false);
			//			mBtnAccusation.setActive(false);
			
		}
	}

	public void BackClicked(){
		if(mWebView != null){
			mWebView.url = LOVE_URL;
			mWebView.Load();
		}
	}
	
	bool OnWebViewShouldClose(UniWebView webView) {
		Debug.Log ("OnWebViewShouldClose");
		UtilMgr.OnBackPressed();
		
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
		return new UniWebViewEdgeInsets((int)(Constants.WEBVIEW_GAB_TOP*myRatio),0,0,0);
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
