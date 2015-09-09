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
		mStateWebview = STATE_WEBVIEW.INVISIBLE;
		mWebView = UniWebViewObject.GetComponent<UniWebView>();
		if (mWebView == null) {
			mWebView = UniWebViewObject.AddComponent<UniWebView>();
			mWebView.SetShowSpinnerWhenLoading(false);
			mWebView.autoShowWhenLoadComplete = true;
			mWebView.OnLoadBegin += OnLoadBegin;
			//mWebView.OnReceivedMessage += OnReceivedMessage;
			mWebView.OnLoadComplete += OnLoadComplete;
			mWebView.OnWebViewShouldClose += OnWebViewShouldClose;
//			mWebView.OnEvalJavaScriptFinished += OnEvalJavaScriptFinished;			
			mWebView.InsetsForScreenOreitation += InsetsForScreenOreitation;
//			mWebView.OnReceivedKeyCode += OnReceivedKeyCode;
			
			mWebView.backButtonEnable = false;
			
		}
		
		mWebView.url = url;
		
		mWebView.Load ();
	}

//	void OnReceivedMessage(UniWebView webView, UniWebViewMessage message) {
//		Debug.Log("rawMessage : " + message.rawMessage);
//		Debug.Log("path : " + message.path);
//	
//		if (string.Equals(message.path, "http://auth.friize.com/m/user_auth_fail.php")) {
//			// It is time to move!
//			
//			// In this example:
//			Debug.Log(message.args["message"]);
//			Debug.Log("message : " + ConvertHexToString(message.args["message"]));
//			// message.args["distance"] = "1"
//		}
//	}
	UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation) {
		Debug.Log ("InsetsForScreenOreitation");
		
		float myRatio = Screen.width / 720f;
		
		//		if(Screen.height > Constants.SCREEN_HEIGHT_ORIGINAL){		
		return new UniWebViewEdgeInsets((int)(Constants.WEBVIEW_GAB_TOP*myRatio),0,0,0);
		//		} else {
		//			return new UniWebViewEdgeInsets((int)(125*myRatio)+Constants.HEIGHT_STATUS_BAR,0,0,0);
		//		}
	}
	public string Base64Decode(string base64EncodedData) {
		var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}

	void OnLoadBegin(UniWebView webView, string loadingUrl){
		Debug.Log("OnLoadBegin to "+loadingUrl);
		if(loadingUrl.Contains("user_auth_success")){
			Debug.Log("Succeed");
			CompleteCert();
		} else if(loadingUrl.Contains("user_auth_fail")){
			Debug.Log("Failed");

	
				string replace = loadingUrl.Replace(Constants.URL_CERT.Replace("user_auth.php","")+"user_auth_fail.php","");
			Debug.Log("replace : " + replace);
			replace =replace.Replace("?message=","");
			FailedCert(replace);
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
			mStateWebview = STATE_WEBVIEW.VISIBLE;
		}
		
		//		UniWebViewEdgeInsets insets = new UniWebViewEdgeInsets (100, 0, 1130, 720);//top, left, btm, right
		//		webView.insets = insets;
		
		//		Debug.Log ("insets top : " + webView.insets.top);
	}

	public void CompleteCert(){


		Debug.Log ("UseItem name : " + UseItem.name);
		mWebView.Stop();
		mWebView.Hide();
		mWebView = null;
		UniWebViewObject.SetActive(false);
		DialogueMgr.ShowDialogue(mSucceedTitle, mSucceedBody, DialogueMgr.DIALOGUE_TYPE.EventAlert, OnDialogClicked);

		transform.parent.gameObject.SetActive (false);

	}

	public void FailedCert(string message){
		mWebView.Stop ();
		mWebView.Hide();
		mWebView = null;
		UniWebViewObject.SetActive(false);
	
		if (message != "") {
			DialogueMgr.ShowDialogue (mFailedTitle, Base64Decode(message), DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else {
			DialogueMgr.ShowDialogue (mFailedTitle, mFailedBody, DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}
		transform.parent.gameObject.SetActive (false);
	
//		DialogueMgr.SetEvent(OnClicked);
	}
	public void OnActive(){
		gameObject.transform.parent.gameObject.SetActive (true);
	}
	public void OffActive(){
		if (mWebView != null) {
			FailedCert ("");
		}
		gameObject.transform.parent.gameObject.SetActive (false);
	}
	public void OnDialogClicked(DialogueMgr.BTNS type){
		if (type == DialogueMgr.BTNS.Cancel){ 
			UserMgr.UserInfo.activeAuth+=1;
				gameObject.transform.parent.gameObject.SetActive (false);
			transform.root.FindChild("Item").GetComponent<ScriptItemMiddle>().Reset();
			//	AutoFade.LoadLevel("SceneCards", 0f, 1f);
			//ResetItemList
			
		}
		//string email = PlayerPrefs.GetString (Constants.PrefEmail);
		//string pwd = PlayerPrefs.GetString (Constants.PrefPwd);
		//transform.parent.GetComponent<ScriptTitle>().Login(email, pwd);
	}
	public void GetItemObj(GameObject obj){
		UseItem = obj;
	}
	enum STATE_WEBVIEW{
		VISIBLE,
		INVISIBLE
	}

	STATE_WEBVIEW mStateWebview;
	void OnApplicationPause(bool pause){
				Debug.Log("pause is "+pause);
		if(pause){
			HideWebView();
		} else{
			ShowWebView();
		}
	}

	public void HideWebView(){
		Debug.Log ("mStateWebview : " + mStateWebview);
		Debug.Log ("STATE_WEBVIEW.VISIBLE : " + STATE_WEBVIEW.VISIBLE);
		if (mStateWebview == STATE_WEBVIEW.VISIBLE) {
			Debug.Log("Hide");
			mWebView.Hide ();
			mStateWebview = STATE_WEBVIEW.INVISIBLE;
		}
	}
	
	public void ShowWebView(){
		if (mStateWebview == STATE_WEBVIEW.INVISIBLE) {
			Debug.Log("Show");
			mWebView.Show ();
			mStateWebview = STATE_WEBVIEW.VISIBLE;
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
}
