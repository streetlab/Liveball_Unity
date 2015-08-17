using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptItemHitterHighlight : cUIScrollListBase {
	
	public GameObject mBG;
	public GameObject mPhoto;
	public GameObject mLblName;
	public GameObject mLblNumber;
	public GameObject mLblResult;
	public GameObject mLblReward;
	public GameObject mLblSelect1;
	public GameObject mLblSelect2_1;
	public GameObject mLblSelect2_2;
	
	public QuizInfo mQuizInfo;
	GetQuizResultEvent mEvent;
	GameObject mDetailView;
	ScriptMatchPlaying mMatchPlaying;
	
	public float mPositionY;
	//    bool isOpened;
	bool isImgLoaded;
	//    public bool needSimpleResult;
	//    GetSimpleResultEvent mSimpleEvent;
	
	void Update()
	{
		//        if (needSimpleResult) {
		//            needSimpleResult = false;
		//            mSimpleEvent = new GetSimpleResultEvent(new EventDelegate(this, "GotSimpleResult"));
		////            Debug.Log("mQuizInfo.quizListSeq : "+mQuizInfo.quizListSeq);
		//            NetMgr.GetSimpleResult (mQuizInfo.quizListSeq, mSimpleEvent);
		//        }
	}
	
	public void Init(ScriptMatchPlaying matchPlaying, QuizInfo quizInfo, GameObject detailView)
	{
		isImgLoaded = false;
		mDetailView = detailView;
		mMatchPlaying = matchPlaying;
		mQuizInfo = quizInfo;
		
		if (mQuizInfo.typeCode.Contains ("_QZC_")) {
			mLblName.transform.GetComponent<UILabel> ().text = mQuizInfo.subTitle;
			mLblNumber.SetActive(false);
		} else {
			mLblName.transform.GetComponent<UILabel> ().text = mQuizInfo.playerName;
			//            int width = mLblName.transform.GetComponent<UILabel> ().width;
			float width = mQuizInfo.playerName.Length * 25;
			mLblNumber.transform.GetComponent<UILabel> ().text = "No."+mQuizInfo.playerNumber;
			//            Vector3 pos = new Vector3(mLblName.transform.localPosition.x,
			//                                      mLblName.transform.localPosition.y,
			//                                      mLblName.transform.localPosition.z);
			//            pos.x += width;
			Vector3 pos = mLblNumber.transform.localPosition;
			pos.x = -80f+width;
			mLblNumber.transform.localPosition = pos;
		}
		
		mLblReward.transform.GetComponent<UILabel> ().text = mQuizInfo.rewardDividend;
		string strImage = mQuizInfo.imageName;
		if (mQuizInfo.imagePath != null && mQuizInfo.imagePath.Length > 0)
			strImage = mQuizInfo.imagePath + mQuizInfo.imageName;
		WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
		gameObject.SetActive (true);
		StartCoroutine(GetImage (www));
		SetQuizResult (mQuizInfo);
		//        isOpened = false;
	}
	
	//    public void RefreshDatas()
	//    {
	//        SetQuizResult (mQuizInfo);
	//    }
	
	public void SetQuizResult(QuizInfo quizInfo)
	{
		mQuizInfo = quizInfo;
		
		mLblReward.SetActive (false);
		
		if(quizInfo.quizValue.Length > 0){
			Debug.Log("quizValue : "+quizInfo.quizValue);
			int idx = int.Parse(quizInfo.quizValue) -1;
			mLblResult.GetComponent<UILabel>().text = quizInfo.order[idx].description;
			
			bool isCorrect = false;
			QuizRespInfo resp = null;
			if(quizInfo.resp != null){
				for(int i = 0; i < quizInfo.resp.Count; i++){
					resp = quizInfo.resp[i];
					if(resp.respValue.Equals(quizInfo.quizValue)){
						isCorrect = true;
						break;
					}
				}
			}
			
			if(isCorrect){
				mLblReward.SetActive(true);
				
				mLblSelect1.SetActive (false);
				mLblSelect2_1.SetActive (false);
				mLblSelect2_2.SetActive (false);
				
				mLblReward.GetComponent<UILabel>().text = UtilMgr.AddsThousandsSeparator(resp.expectRewardPoint);
				
				return;
			}
		} else if(quizInfo.resultMsg.Length > 0){
			Debug.Log("quiz cancel");
			mLblResult.GetComponent<UILabel>().text = quizInfo.resultMsg;
			//            mLblSelect1.SetActive(true);
			//            mLblSelect1.GetComponent<UILabel> ().text = "X";
		}
		try{
		if (quizInfo.resp == null
		    || quizInfo.resp.Count == 0) {    
			Debug.Log("quiz1");
			mLblSelect1.SetActive (false);
			mLblSelect2_1.SetActive (false);
			mLblSelect2_2.SetActive (false);
		} else if(quizInfo.resp.Count == 1
		          && quizInfo.resp[0].respValue.Length > 0){
			Debug.Log("quiz2");
			mLblSelect1.SetActive (true);
			
			mLblSelect2_1.SetActive (false);
			mLblSelect2_2.SetActive (false);
			
			Debug.Log("quizInfo.resp[0].respValue : "+quizInfo.resp[0].respValue);
			int respValue = int.Parse(quizInfo.resp[0].respValue) -1;
			mLblSelect1.GetComponent<UILabel>().text = quizInfo.order[respValue].description;
		} else if(quizInfo.resp.Count == 2){
			Debug.Log("quiz3");
			mLblSelect1.SetActive (false);
			
			mLblSelect2_1.SetActive (true);
			int respValue = int.Parse(quizInfo.resp[0].respValue) -1;
			mLblSelect2_1.GetComponent<UILabel>().text = quizInfo.order[respValue].description;
			mLblSelect2_2.SetActive (true);
			
			Debug.Log("quizInfo.resp[1].respValue : "+quizInfo.resp[1].respValue);
			respValue = int.Parse(quizInfo.resp[1].respValue) -1;
			mLblSelect2_2.GetComponent<UILabel>().text = quizInfo.order[respValue].description;
		}
		}
		catch{
			Debug.Log("Out of range");
		}
	}
	
	IEnumerator GetImage(WWW www)
	{
		yield return www;
		isImgLoaded = true;
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		mPhoto.transform.FindChild("Panel").FindChild("Texture").GetComponent<UITexture>().mainTexture = temp;
		
	}
	
	void OnEnable()
	{
		if(!isImgLoaded && mQuizInfo != null){
			string strImage = mQuizInfo.imageName;
			if (mQuizInfo.imagePath != null && mQuizInfo.imagePath.Length > 0)
				strImage = mQuizInfo.imagePath + mQuizInfo.imageName;
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + strImage);
			StartCoroutine(GetImage (www));
		}
		
		//        if (mQuizInfo != null)
		//            SetQuizResult (mQuizInfo);
		
	}
	
	//    public void GotSimpleResult(){
	//        if (mSimpleEvent.Response.data == null
	//            || mSimpleEvent.Response.data.Count < 1)
	//                        return;
	//
	//        mQuizInfo.quizValue = mSimpleEvent.Response.data [0].quizValue;
	//
	//        mQuizInfo.resp = new List<QuizRespInfo> ();
	//        QuizRespInfo tmpInfo;
	//        if (mSimpleEvent.Response.data.Count > 1) {
	//            //got 2 answers
	//            tmpInfo = new QuizRespInfo();
	//            tmpInfo.respValue = mSimpleEvent.Response.data[1].respValue;
	//            mQuizInfo.resp.Add(tmpInfo);
	//        } 
	//
	//        tmpInfo = new QuizRespInfo();
	//        tmpInfo.respValue = mSimpleEvent.Response.data[0].respValue;
	//        mQuizInfo.resp.Insert(0, tmpInfo);
	//
	//
	//
	//        SetQuizResult (mQuizInfo);
	//    }
	
	public void OnClicked()
	{
		if (mMatchPlaying.mDetailOpened) {
			UtilMgr.RemoveAllBackEvents();
			mMatchPlaying.mDetailOpened = false;
			
			//            mDetailView.GetComponent<UIPanel> ().depth = 0;
			mDetailView.SetActive(false);
			mDetailView.transform.FindChild("ListDetail").gameObject.SetActive(false);//GetComponent<UIPanel>().depth = 0;
			transform.GetComponent<UIDragScrollView>().enabled = true;
			//            if(transform.parent.GetComponent<SpringPanel> () != null)
			//                transform.parent.GetComponent<SpringPanel> ().enabled = true;
			mDetailView.GetComponent<ScriptDetailHighlight> ().ClearList();
			//            NGUITools.FindInParents<UIPanel> (gameObject).clipOffset = mClipOffsetPanel;
			
			Debug.Log("mLocalPosList 2 : "+mMatchPlaying.mLocalPosList.y);
			//            mMatchPlaying.ResetList(mMatchPlaying.mLocalPosList, mMatchPlaying.mClipOffsetPanel);
			//            transform.parent.localPosition = mLocalPosList;
			mMatchPlaying.mList.SetActive(true);
			mMatchPlaying.mItemHitter.SetActive(false);
		} else{
			mMatchPlaying.mDetailOpened = true;
			
			UtilMgr.AddBackEvent(new EventDelegate(this, "OnClicked"));
			mEvent = new GetQuizResultEvent (new EventDelegate (this, "GotResult"));
			NetMgr.GetQuizResult (mQuizInfo.quizListSeq, mEvent);
			transform.GetComponent<UIDragScrollView>().enabled = false;
			
			mMatchPlaying.mLocalPosList = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y);
			Debug.Log("mLocalPosList 1 : "+mMatchPlaying.mLocalPosList.y);
			mMatchPlaying.mClipOffsetPanel = new Vector2(NGUITools.FindInParents<UIPanel> (gameObject).clipOffset.x
			                                             ,NGUITools.FindInParents<UIPanel> (gameObject).clipOffset.y);
			//            mMatchPlaying.SetList(mQuizInfo);
		}
	}
	
	public void GotResult()
	{
		Debug.Log("GotResult");
		mMatchPlaying.mList.SetActive(false);
		mMatchPlaying.mItemHitter.SetActive(true);
		mMatchPlaying.mItemHitter.GetComponent<ScriptItemHitterHighlight>()
			.Init (mMatchPlaying, mQuizInfo, mDetailView);
		mMatchPlaying.mItemHitter.transform.FindChild("Round").gameObject.SetActive(false);
		
		
		//        mDetailView.GetComponent<UIPanel> ().depth = 2;
		mDetailView.SetActive(true);
		mDetailView.transform.FindChild("ListDetail").gameObject.SetActive(true);//GetComponent<UIPanel>().depth = 3;
		mDetailView.GetComponent<ScriptDetailHighlight> ().Init (mEvent.Response);
		
		//        if(transform.parent.GetComponent<SpringPanel> () != null)
		//            transform.parent.GetComponent<SpringPanel> ().enabled = false;
		//        mLocalPosList = transform.parent.localPosition;
		//        transform.parent.localPosition = new Vector3 (0f, 52f+mPositionY, 0f);
		//        TweenPosition.Begin(transform.parent.gameObject, 1f, new Vector3(0f, 54f+mPositionY, 0f));
		
		//move after 1f
		//        mClipOffsetPanel = NGUITools.FindInParents<UIPanel> (gameObject).clipOffset;
		
		//        float result = -326f+UtilMgr.GetScaledPositionY();
		
		//        NGUITools.FindInParents<UIPanel>(gameObject).clipOffset = new Vector2(0f, result-mPositionY);//191
	}
	
	//    IEnumerator moveOffset(){
	//    }
}
