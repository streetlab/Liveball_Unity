using UnityEngine;
using System.Collections;

public class ScriptCertification : MonoBehaviour {

	public GameObject UniWebViewObject;
	UniWebView mWebView;
	GameObject UseItem;

	public string mSucceedTitle;
	public string mSucceedBody;
	public string mFailedTitle;
	public string mFailedBody;

	public void BtnClicked(){
		gameObject.SetActive (true);
		UniWebViewObject.SetActive(true);
		string url = Constants.URL_CERT + "?mem=" + UserMgr.UserInfo.memSeq;
		
		mWebView = UniWebViewObject.GetComponent<UniWebView>();
		if (mWebView == null) {
			mWebView = UniWebViewObject.AddComponent<UniWebView>();
			mWebView.SetShowSpinnerWhenLoading(false);
			mWebView.autoShowWhenLoadComplete = true;
			mWebView.OnLoadBegin += OnLoadBegin;
//			mWebView.OnReceivedMessage += OnReceivedMessage;
//			mWebView.OnLoadComplete += OnLoadComplete;
//			mWebView.OnWebViewShouldClose += OnWebViewShouldClose;
//			mWebView.OnEvalJavaScriptFinished += OnEvalJavaScriptFinished;			
			mWebView.InsetsForScreenOreitation += InsetsForScreenOreitation;
//			mWebView.OnReceivedKeyCode += OnReceivedKeyCode;
			
			mWebView.backButtonEnable = false;
			
		}
		
		mWebView.url = url;
		
		mWebView.Load ();
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

	void OnLoadBegin(UniWebView webView, string loadingUrl){
		Debug.Log("OnLoadBegin to "+loadingUrl);
		if(loadingUrl.Contains("user_auth_success")){
			Debug.Log("Succeed");
			CompleteCert();
		} else if(loadingUrl.Contains("user_auth_fail")){
			Debug.Log("Failed");
			FailedCert();
		} 
//		else if(loadingUrl.Equals("http://auth.friize.com/m/user_auth_success.php")){
//			Debug.Log("Equals");
//		} else {
//			Debug.Log("Shit");
//		}
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
		mWebView.Stop();
		mWebView.Hide();
		mWebView = null;
		UniWebViewObject.SetActive(false);
		DialogueMgr.ShowDialogue(mSucceedTitle, mSucceedBody, DialogueMgr.DIALOGUE_TYPE.Alert, OnDialogClicked);
		if (UseItem != null && (Application.loadedLevelName.Equals ("SecenCaards"))) {
			UseItem.GetComponent<GiftUse>().temp();
		}

	}

	public void FailedCert(){
		mWebView.Stop ();
		mWebView.Hide();
		mWebView = null;
		UniWebViewObject.SetActive(false);
		DialogueMgr.ShowDialogue(mFailedTitle, mFailedBody, DialogueMgr.DIALOGUE_TYPE.Alert, OnDialogClicked);
		//gameObject.SetActive (false);

//		DialogueMgr.SetEvent(OnClicked);
	}
	public void OnActive(){
		gameObject.transform.parent.gameObject.SetActive (true);
	}
	public void OffActive(){
		if (mWebView != null) {
			FailedCert ();
		}
		gameObject.transform.parent.gameObject.SetActive (false);
	}
	public void OnDialogClicked(DialogueMgr.BTNS type){
		//string email = PlayerPrefs.GetString (Constants.PrefEmail);
		//string pwd = PlayerPrefs.GetString (Constants.PrefPwd);
		//transform.parent.GetComponent<ScriptTitle>().Login(email, pwd);
	}
	public void GetItemObj(GameObject obj){
		UseItem = obj;
	}
}
