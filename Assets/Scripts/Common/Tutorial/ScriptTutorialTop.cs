using UnityEngine;
using System.Collections;

public class ScriptTutorialTop : MonoBehaviour {

	public GameObject mBtnClose;
	public GameObject mBtnNext;
	public GameObject mBtnPrev;
	public GameObject mWebView;

//	string[] URLS = {"http://service.liveball.kr/lb_tutorial_001.html",
//		"http://service.liveball.kr/lb_tutorial_002.html",
//		"http://service.liveball.kr/lb_tutorial_003.html"};
	string URL = "http://service.liveball.kr/lb_tutorial.html";

	int mPage;
	const int PAGE_MAX = 3;

	void Start(){

		PlayerPrefs.SetString (Constants.PrefTutorial, "1");
		mPage = 0;
//		mBtnPrev.SetActive(false);
//		mBtnClose.SetActive(false);
		mBtnClose.SetActive(true);
		mBtnPrev.SetActive(false);
		mBtnNext.SetActive(false);
	}

	public void CloseClicked(){
		string value = PlayerPrefs.GetString(Constants.PrefNotice);
		if(value != null && value.Equals(UtilMgr.GetDateTime("yyyyMMdd"))){
			AutoFade.LoadLevel("SceneMain");
		} else{
			AutoFade.LoadLevel("SceneNotice");
		}
	}

	public void NextClicked(){
		mPage += 1;
		if(mPage > PAGE_MAX-1)
			mPage = PAGE_MAX-1;

		mBtnPrev.SetActive(true);
		if(mPage == PAGE_MAX-1){
			mBtnClose.SetActive(true);
			mBtnNext.SetActive(false);
		}
//		mWebView.GetComponent<ScriptTutorial>().GoTo(URLS[mPage]);
	}

	public void PrevClicked(){
		mPage -= 1;
		if(mPage < 0)
			mPage = 0;

		mBtnClose.SetActive(false);
		mBtnNext.SetActive(true);
		if(mPage == 0){
			mBtnPrev.SetActive(false);
		}
//		mWebView.GetComponent<ScriptTutorial>().GoTo(URLS[mPage]);
	}
}
