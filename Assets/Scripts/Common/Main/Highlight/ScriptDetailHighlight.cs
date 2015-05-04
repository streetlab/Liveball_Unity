using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptDetailHighlight : MonoBehaviour {

	public GameObject[] Items;
	public GameObject mListFriends;
	public GameObject mItemFriend;
	public GameObject mSprJoinBar;

	GetQuizResultResponse mResponse;
	float mPosGuide;
	float mPosJoinBar;
	static List<GameObject> mListFriendItems = new List<GameObject>();

	public void Init(GetQuizResultResponse response)
	{
		mPosJoinBar = 85;
		mPosGuide = 175f;
		mResponse = response;
		SetResultGraph ();
		SetResultFriends ();
	}

	public void ClearList(){
		foreach (GameObject go in mListFriendItems)
			NGUITools.DestroyImmediate (go);
	}

	void SetResultGraph(){
		int i = 0;

//		List<QuizResultGlobal> globalList = new List<QuizResultGlobal>();
//		List<QuizResultGlobal> friendsList = new List<QuizResultGlobal>();
//
//		QuizResultGlobal global = new QuizResultGlobal();
		QuizInfo myQuiz = null;
		int resp1 = 0;
		int resp2 = 0;

		foreach(QuizInfo quiz in QuizMgr.QuizList){
			if(quiz.quizListSeq == mResponse.data.global[0].quizListSeq)
				myQuiz = quiz;
		}

		if(myQuiz != null
		   && myQuiz.resp != null
		   && myQuiz.resp.Count > 0){
			resp1 = int.Parse(myQuiz.resp[0].respValue);
			if(myQuiz.resp.Count > 1){
				resp2 = int.Parse(myQuiz.resp[1].respValue);
			}
		}
		

		for (; i < mResponse.data.global.Count; i++) {
			bool isMyChoice = false;
			if(mResponse.data.global[i].orderSeq == resp1
			   || mResponse.data.global[i].orderSeq == resp2)
				isMyChoice = true;

			Items[i].GetComponent<ScriptItemDetailGraph>().Init(
				mResponse.data.global[i], mResponse.data.friend[i], isMyChoice);
		}

		for(; i < 8; i++){
			Items[i].GetComponent<ScriptItemDetailGraph>().gameObject.SetActive(false);
			mPosJoinBar -= 30;
			mPosGuide -= 30;
		}

		mSprJoinBar.transform.localPosition = new Vector3 (0, -mPosJoinBar, 0);
	}

	void SetResultFriends(){

		mListFriendItems.Clear ();

		for (int i = 0; i < mResponse.data.result.Count; i++) {
			QuizResultResults friend = mResponse.data.result[i];
			List<QuizResultGlobal> orders = mResponse.data.global;
			GameObject go = Instantiate(mItemFriend, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
			go.transform.parent = mListFriends.transform;
			go.transform.localScale = new Vector3(1f, 1f, 1f);	
			go.GetComponent<ScriptItemListFriends>().Init(friend, orders);			
			go.transform.localPosition = new Vector3(0f, -mPosGuide, 0f);
			mPosGuide += 140f;
			mListFriendItems.Add (go);
		}
		mListFriends.GetComponent<UIScrollView> ().ResetPosition ();
	}
}
