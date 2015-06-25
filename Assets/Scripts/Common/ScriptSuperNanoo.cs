using UnityEngine;
using System.Collections;

public class ScriptSuperNanoo : MonoBehaviour {
		
	protected UniWebView mWebView;

	public GameObject mBtnMenu;
	public GameObject mBtnBack;
	public GameObject mBtnAccusation;
	public GameObject mBtnNotice;
	
	protected string mBoardNum;
	protected string mContentNum;
	protected AccuseContentEvent mAccuEvent;

	protected void OnLoadBegin(UniWebView webView, string loadingUrl){
		Debug.Log("OnLoadBegin : "+loadingUrl);

		if(loadingUrl.Contains("youtube.com/embed")
		   || loadingUrl.Contains("about:blank")){

		} else
		if(loadingUrl.Contains("liveball/board?cd=")
		   || loadingUrl.Equals("https://game.nanoo.so/liveball/board")){//in board

			if(mBtnMenu != null) mBtnMenu.SetActive(true);
			mBtnNotice.SetActive(true);
			
			mBtnAccusation.SetActive(false);
			mBtnBack.SetActive(false);
			
			try{
				if(loadingUrl.Equals("https://game.nanoo.so/liveball/board")){
					mBoardNum = "0";
				} else{
					int startNum = loadingUrl.IndexOf("/board?")+10;
					mBoardNum = loadingUrl.Substring(startNum);
				}
			} catch{
				mBoardNum = "0";
			}
			Debug.Log("mBoardNum : "+mBoardNum);
		} else
		if(loadingUrl.Contains("liveball/board/")){//in content
			//			Debug.Log("index : " + loadingUrl.IndexOf("/board/"));
			// switch menu btn to back btn
			mBtnBack.SetActive(true);
			mBtnAccusation.SetActive(true);
			
			if(mBtnMenu != null) mBtnMenu.SetActive(false);
			mBtnNotice.SetActive(false);
			// show accusation btn
			try{
				int startNum = loadingUrl.IndexOf("/board/")+7;
				if(loadingUrl.Contains("?cd=")){
					int endNum = loadingUrl.IndexOf("?cd=");
					mContentNum = loadingUrl.Substring(startNum, endNum-startNum);
				} else{
					mContentNum = loadingUrl.Substring(startNum);
				}
			} catch{
				mContentNum = "0";
			}
			Debug.Log("mContentNum : "+mContentNum);
		} else{
			//turn off accusation
			mBtnBack.SetActive(true);
			mBtnNotice.SetActive(true);
			
			if(mBtnMenu != null) mBtnMenu.SetActive(false);
			mBtnAccusation.SetActive(false);
			
		}
		
		UtilMgr.ShowLoading (true);
	}

	public void AccuseClicked(){
		AccusationInfo accuInfo = new AccusationInfo();
		accuInfo.BoardNum = mBoardNum;
		accuInfo.ContentNum = mContentNum;
		mAccuEvent = new AccuseContentEvent(new EventDelegate(this, "DoneAccusation"));
		DialogueMgr.ShowAccusationDialog(accuInfo, mAccuEvent);
	}
	
	public void DoneAccusation(){
		DialogueMgr.DismissAccusationDialog();
		DialogueMgr.ShowDialogue("신고 완료", mAccuEvent.Response.data.outMessage, DialogueMgr.DIALOGUE_TYPE.Alert, null);
		
	}
	
	public void NoticeClicked(){
		mBtnBack.SetActive(true);
		
		mBtnNotice.SetActive(false);
		if(mBtnMenu != null) mBtnMenu.SetActive(false);
		mBtnAccusation.SetActive(false);
		
		if(mWebView != null){
			mWebView.url = Constants.EULA_URL;
			mWebView.Load();
		}
	}
}
