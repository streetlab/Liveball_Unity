using UnityEngine;
using System.Collections;

public class ScriptCertification : MonoBehaviour {

	UniWebView mWebView;

	public void BtnClicked(){
		string url = Constants.URL_CERT + "?mem=" + UserMgr.UserInfo.memSeq;
		
		mWebView = GetComponent<UniWebView>();
		if (mWebView == null) {
			mWebView = gameObject.AddComponent<UniWebView>();
			mWebView.SetShowSpinnerWhenLoading(true);
			mWebView.autoShowWhenLoadComplete = true;
			mWebView.OnLoadBegin += OnLoadBegin;
//			mWebView.OnReceivedMessage += OnReceivedMessage;
//			mWebView.OnLoadComplete += OnLoadComplete;
//			mWebView.OnWebViewShouldClose += OnWebViewShouldClose;
//			mWebView.OnEvalJavaScriptFinished += OnEvalJavaScriptFinished;			
//			mWebView.InsetsForScreenOreitation += InsetsForScreenOreitation;
//			mWebView.OnReceivedKeyCode += OnReceivedKeyCode;
			
			mWebView.backButtonEnable = false;
			
		}
		
		mWebView.url = url;
		
		mWebView.Load ();
	}

	void OnLoadBegin(UniWebView webView, string loadingUrl){
		Debug.Log("OnLoadBegin to "+loadingUrl);
	}

	void OnLoadComplete(UniWebView webView, bool success, string errorMessage) {
		Debug.Log ("OnLoadComplete : "+webView.currentUrl);
		
		UtilMgr.DismissLoading ();
		
		if (success) {
//			webView.Show();
//			mStateWebview = STATE_WEBVIEW.VISIBLE;
		}
		
		//		UniWebViewEdgeInsets insets = new UniWebViewEdgeInsets (100, 0, 1130, 720);//top, left, btm, right
		//		webView.insets = insets;
		
		//		Debug.Log ("insets top : " + webView.insets.top);
	}

	public void CompleteCert(){

	}
}
