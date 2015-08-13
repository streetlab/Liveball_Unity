using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RankingManager : MonoBehaviour {
//	GameObject W,D,M,S;
//
//	bool Wcheck = false;
//	bool Dcheck = false;
//	string Dstring;
//	Color B = new Color(37f/255f,170f/255f,225f/255f);
//	Color G = new Color(147f/255f,147f/255f,147f/255f);
//	// Use this for initialization
//	GetRankEvent mGetRankEvent;
//	void Start () {
//		W = transform.FindChild("Scroll View").FindChild("bgs").FindChild("Weekly").gameObject;
//		D = transform.FindChild("Scroll View").FindChild("bgs").FindChild("Daily").gameObject;
//		M = transform.FindChild("Scroll View").FindChild("bgs").FindChild("My").gameObject;
//		mGetRankEvent = new GetRankEvent (new EventDelegate (this, "Set1"));
//		NetMgr.GetUserRankingWeeklyGold (UserMgr.UserInfo.memSeq,mGetRankEvent);
//	}
//
//	void Set1(){
//	
//			M.transform.FindChild ("List").FindChild ("BG").FindChild ("GetGold").FindChild ("BallW").FindChild ("W").FindChild ("Rank").GetComponent<UILabel> ().text = 
//			mGetRankEvent.Response.data.rank + "위";
//			M.transform.FindChild ("List").FindChild ("BG").FindChild ("GetGold").FindChild ("BallW").FindChild ("W").FindChild ("Gold").GetComponent<UILabel> ().text = 
//			"(" + UtilMgr.AddsThousandsSeparator (mGetRankEvent.Response.data.rankValue) + "점)";
//			if (mGetRankEvent.Response.data.ranking.Count > 0 && mGetRankEvent.Response.data.ranking != null) {
//			W.transform.FindChild ("BG_W").gameObject.SetActive(false);
//			W.transform.FindChild ("List").gameObject.SetActive(true);
//				for (int i = 0; i < mGetRankEvent.Response.data.ranking.Count; i++) {
//					W.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("rank").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].rank.ToString ();
//					W.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("Name").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].memberName;
//					W.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("Gold").GetComponent<UILabel> ().text = 
//					UtilMgr.AddsThousandsSeparator (mGetRankEvent.Response.data.ranking [i].rankValue) + " 점";
//					if (mGetRankEvent.Response.data != null) {
//			
//						if (mGetRankEvent.Response.data.ranking [i].imageName != "") {
//							string Imagewww;
//							if (mGetRankEvent.Response.data.ranking [i].imageName [0] == 'h' &&
//								mGetRankEvent.Response.data.ranking [i].imageName [1] == 't' &&
//								mGetRankEvent.Response.data.ranking [i].imageName [2] == 't' &&
//								mGetRankEvent.Response.data.ranking [i].imageName [3] == 'p') {
//								Imagewww = mGetRankEvent.Response.data.ranking [i].imageName;
//					
//							} else {
//								Imagewww = Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data.ranking [i].imagePath + mGetRankEvent.Response.data.ranking [i].imageName;
//					
//							}
//							//Debug.Log("Imgae URL : " + Imagewww);
//							WWW www = new WWW (Imagewww);
//							StartCoroutine (GetImage (www, W.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("BG").FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
//						}
//					}
//				}
//			
//		}else {
//			W.transform.FindChild ("BG_W").gameObject.SetActive(false);
//			Wcheck = true;
//			W.transform.FindChild("NoGame").FindChild("todaynogame").GetComponent<UILabel>().text = 
//				mGetRankEvent.Response.data.rankDesc;
//			W.transform.FindChild("NoGame").gameObject.SetActive(true);
//			W.transform.FindChild ("List").gameObject.SetActive(false);
//			W.transform.FindChild ("BG").FindChild("BG 1").GetComponent<BoxCollider2D>().enabled = false;
//		}
//		mGetRankEvent = new GetRankEvent (new EventDelegate (this, "Set2"));
//		NetMgr.GetUserRankingWeeklyForecast (UserMgr.UserInfo.memSeq,mGetRankEvent);
//	}
//
//	void Set2(){
//		M.transform.FindChild ("List").FindChild ("BG").FindChild ("Estimated hits").FindChild ("BallW").FindChild ("W").FindChild ("Rank").GetComponent<UILabel> ().text = 
//			mGetRankEvent.Response.data.rank + "위";
//		M.transform.FindChild ("List").FindChild ("BG").FindChild ("Estimated hits").FindChild ("BallW").FindChild ("W").FindChild ("Gold").GetComponent<UILabel> ().text = 
//			"("+mGetRankEvent.Response.data.rankValue +")";
//		if (mGetRankEvent.Response.data.ranking.Count > 0 && mGetRankEvent.Response.data.ranking != null) {
//			for (int i = 0; i < mGetRankEvent.Response.data.ranking.Count; i++) {
//				W.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("rank").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].rank.ToString ();
//				W.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("Name").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].memberName;
//				W.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("Gold").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].rankValue.ToString () + " 회";
//				if (mGetRankEvent.Response.data != null) {
//				
//					if (mGetRankEvent.Response.data.ranking [i].imageName != "") {
//						string Imagewww;
//						if (mGetRankEvent.Response.data.ranking [i].imageName [0] == 'h' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [1] == 't' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [2] == 't' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [3] == 'p') {
//							Imagewww = mGetRankEvent.Response.data.ranking [i].imageName;
//						} else {
//							Imagewww = Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data.ranking [i].imagePath + mGetRankEvent.Response.data.ranking [i].imageName;
//							
//						}
//						WWW www = new WWW (Imagewww);
//						StartCoroutine (GetImage (www, W.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("BG").FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
//					}
//				}
//			}
//		} else {
//		
//		}
//		mGetRankEvent = new GetRankEvent (new EventDelegate (this, "Set3"));
//		NetMgr.GetUserRankingDailyGold (UserMgr.UserInfo.memSeq,mGetRankEvent);
//	}
//
//	void Set3(){
//		M.transform.FindChild ("List").FindChild ("BG").FindChild ("GetGold").FindChild ("BallD").FindChild ("D").FindChild ("Rank").GetComponent<UILabel> ().text = 
//			mGetRankEvent.Response.data.rank + "위";
//		M.transform.FindChild ("List").FindChild ("BG").FindChild ("GetGold").FindChild ("BallD").FindChild ("D").FindChild ("Gold").GetComponent<UILabel> ().text = 
//			"("+UtilMgr.AddsThousandsSeparator(mGetRankEvent.Response.data.rankValue) + "점)";
//		Debug.Log (mGetRankEvent.Response.data.ranking.Count);
//		D.transform.FindChild("List").gameObject.SetActive (true);
//		if (mGetRankEvent.Response.data.ranking.Count > 0 && mGetRankEvent.Response.data.ranking != null) {
//			for (int i = 0; i < mGetRankEvent.Response.data.ranking.Count; i++) {
//				D.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("rank").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].rank.ToString ();
//				D.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("Name").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].memberName;
//				D.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("Gold").GetComponent<UILabel> ().text = 
//					UtilMgr.AddsThousandsSeparator (mGetRankEvent.Response.data.ranking [i].rankValue) + " 점";
//				if (mGetRankEvent.Response.data != null) {
//				
//					if (mGetRankEvent.Response.data.ranking [i].imageName != "") {
//						string Imagewww;
//						if (mGetRankEvent.Response.data.ranking [i].imageName [0] == 'h' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [1] == 't' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [2] == 't' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [3] == 'p') {
//							Imagewww = mGetRankEvent.Response.data.ranking [i].imageName;
//						} else {
//							Imagewww = Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data.ranking [i].imagePath + mGetRankEvent.Response.data.ranking [i].imageName;
//							
//						}
//						WWW www = new WWW (Imagewww);
//						StartCoroutine (GetImage (www, D.transform.FindChild ("List").FindChild ("BG").GetChild (i + 1).FindChild ("BG").FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
//					}
//				}
//			}
//		} else {
//			Dcheck = true;
//			Dstring = mGetRankEvent.Response.data.rankDesc;
//			D.transform.FindChild ("List").gameObject.SetActive(false);
//			D.transform.FindChild ("BG").FindChild("BG 1").GetComponent<BoxCollider2D>().enabled = false;
//		}
//		mGetRankEvent = new GetRankEvent (new EventDelegate (this, "Set4"));
//		NetMgr.GetUserRankingDailyForecast (UserMgr.UserInfo.memSeq,mGetRankEvent);
//	}
//
//	void Set4(){
//		M.transform.FindChild ("List").FindChild ("BG").FindChild ("Estimated hits").FindChild ("BallD").FindChild ("D").FindChild ("Rank").GetComponent<UILabel> ().text = 
//			mGetRankEvent.Response.data.rank + "위";
//		M.transform.FindChild ("List").FindChild ("BG").FindChild ("Estimated hits").FindChild ("BallD").FindChild ("D").FindChild ("Gold").GetComponent<UILabel> ().text = 
//			"("+mGetRankEvent.Response.data.rankValue +")";
//		if (mGetRankEvent.Response.data.ranking.Count > 0 && mGetRankEvent.Response.data.ranking != null) {
//			for (int i = 0; i < mGetRankEvent.Response.data.ranking.Count; i++) {
//				D.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("rank").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].rank.ToString ();
//				D.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("Name").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].memberName;
//				D.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("Gold").GetComponent<UILabel> ().text = 
//				mGetRankEvent.Response.data.ranking [i].rankValue.ToString () + " 회";
//				if (mGetRankEvent.Response.data != null) {
//				
//					if (mGetRankEvent.Response.data.ranking [i].imageName != "") {
//						string Imagewww;
//						if (mGetRankEvent.Response.data.ranking [i].imageName [0] == 'h' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [1] == 't' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [2] == 't' &&
//							mGetRankEvent.Response.data.ranking [i].imageName [3] == 'p') {
//							Imagewww = mGetRankEvent.Response.data.ranking [i].imageName;
//						} else {
//							Imagewww = Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data.ranking [i].imagePath + mGetRankEvent.Response.data.ranking [i].imageName;
//							
//						}
//						WWW www = new WWW (Imagewww);
//						StartCoroutine (GetImage (www, D.transform.FindChild ("List").FindChild ("BG 1").GetChild (i + 1).FindChild ("BG").FindChild ("Panel").FindChild ("Texture").GetComponent<UITexture> ()));
//					}
//				}
//			}
//
//		} else {
//		
//		}
//	}
//
//
//
//	IEnumerator GetImage(WWW www, UITexture texture)
//	{
//		Debug.Log ("ININ");
//		yield return www;
//		Texture2D tmpTex = new Texture2D (0, 0);
//		if (tmpTex != null) {
//			www.LoadImageIntoTexture (tmpTex);
//			texture.mainTexture = tmpTex;
//		}
//		Debug.Log ("ououou");
//	}
//
//	// Update is called once per frame
//	public void Weekly(){
//		if (!Wcheck) {
//			W.transform.FindChild ("BG").gameObject.SetActive (true);
//			D.transform.FindChild ("BG").gameObject.SetActive (false);
//			M.transform.FindChild ("BG").gameObject.SetActive (false);
//			W.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
//			D.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			M.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			W.transform.FindChild ("List").transform.localPosition = new Vector2 (212, -31);
//			W.transform.FindChild ("List").gameObject.SetActive (false);
//			W.transform.FindChild ("List").gameObject.SetActive (true);
//			D.transform.FindChild ("List").transform.localPosition = new Vector2 (720, -31);
//			M.transform.FindChild ("List").gameObject.SetActive (false);
//			W.transform.FindChild("NoGame").gameObject.SetActive (false);
//			D.transform.FindChild("NoGame").gameObject.SetActive (false);
//		} else {
//			W.transform.FindChild ("BG").gameObject.SetActive (true);
//			D.transform.FindChild ("BG").gameObject.SetActive (false);
//			M.transform.FindChild ("BG").gameObject.SetActive (false);
//			W.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
//			D.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			M.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			W.transform.FindChild ("List").transform.localPosition = new Vector2 (212, -31);
//			W.transform.FindChild ("List").gameObject.SetActive (false);
//		
//			D.transform.FindChild ("List").transform.localPosition = new Vector2 (720, -31);
//			M.transform.FindChild ("List").gameObject.SetActive (false);
//			W.transform.FindChild("NoGame").gameObject.SetActive (true);
//			D.transform.FindChild("NoGame").gameObject.SetActive (false);
//		}
//	}
//	public void Daily(){
//		if (!Dcheck) {
//			W.transform.FindChild ("BG").gameObject.SetActive (false);
//			D.transform.FindChild ("BG").gameObject.SetActive (true);
//			M.transform.FindChild ("BG").gameObject.SetActive (false);
//			W.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			D.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
//			M.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			W.transform.FindChild ("List").transform.localPosition = new Vector2 (932, -31);
//			D.transform.FindChild ("List").transform.localPosition = new Vector2 (0, -31);
//			D.transform.FindChild ("List").gameObject.SetActive (false);
//			D.transform.FindChild ("List").gameObject.SetActive (true);
//			M.transform.FindChild ("List").gameObject.SetActive (false);
//			W.transform.FindChild("NoGame").gameObject.SetActive (false);
//			D.transform.FindChild("NoGame").gameObject.SetActive (false);
//		} else {
//			W.transform.FindChild ("BG").gameObject.SetActive (false);
//			D.transform.FindChild ("BG").gameObject.SetActive (true);
//			M.transform.FindChild ("BG").gameObject.SetActive (false);
//			W.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			D.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
//			M.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//			W.transform.FindChild ("List").transform.localPosition = new Vector2 (932, -31);
//			D.transform.FindChild ("List").transform.localPosition = new Vector2 (0, -31);
//			D.transform.FindChild ("List").gameObject.SetActive (false);
//	
//			M.transform.FindChild ("List").gameObject.SetActive (false);
//			W.transform.FindChild("NoGame").gameObject.SetActive (false);
//			D.transform.FindChild("NoGame").gameObject.SetActive (true);
//			D.transform.FindChild("NoGame").FindChild("todaynogame").GetComponent<UILabel>().text = Dstring;
//		}
//	}
//	public void My(){
//		W.transform.FindChild ("BG").gameObject.SetActive (false);
//		D.transform.FindChild ("BG").gameObject.SetActive (false);
//		M.transform.FindChild ("BG").gameObject.SetActive (true);
//		W.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//		D.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
//		M.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
//		W.transform.FindChild ("List").transform.localPosition = new Vector2 (932,-31);
//		D.transform.FindChild ("List").transform.localPosition = new Vector2 (720,-31);
//		M.transform.FindChild ("List").gameObject.SetActive (true);
//		W.transform.FindChild("NoGame").gameObject.SetActive (false);
//		D.transform.FindChild("NoGame").gameObject.SetActive (false);
//	}
}
