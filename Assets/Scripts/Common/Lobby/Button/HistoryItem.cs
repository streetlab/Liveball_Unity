using UnityEngine;
using System.Collections;

public class HistoryItem : MonoBehaviour {
	public void Button(){
		string Title = transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text;
		string MyRank = transform.FindChild ("BG").FindChild ("myrank").GetComponent<UILabel> ().text;
		string Tpreset = transform.FindChild ("BG").FindChild ("totalpreset").GetComponent<UILabel> ().text;
		string Tentry = transform.FindChild ("BG").FindChild ("totalentry").GetComponent<UILabel> ().text;
		string Rscore = transform.FindChild ("BG").FindChild ("rewordscore").GetComponent<UILabel> ().text;
		string Rcount = transform.FindChild ("BG").FindChild ("rewordcount").GetComponent<UILabel> ().text;
		string gameSeq =  transform.parent.FindChild ("BG").FindChild ("GameSeq").GetComponent<UILabel> ().text;
		string contestSeq =  transform.FindChild ("BG").FindChild ("contestSeq").GetComponent<UILabel> ().text;
		UserMgr.CurrentContestSeq = int.Parse (contestSeq);
		Debug.Log ("Title : " + Title);
		Debug.Log ("MyRank : " + MyRank);
		Debug.Log ("Tpreset : " + Tpreset);
		Debug.Log ("Tentry : " + Tentry);
		Debug.Log ("Rscore : " + Rscore);
		Debug.Log ("Rcount : " + Rcount);
		Debug.Log ("gameSeq : " + gameSeq);
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("History Contest").
			FindChild ("HistoryRanking").GetComponent<HistoryRankingClose> ().RankingOn (
				Title,MyRank,Tpreset,Tentry,Rscore,Rcount,gameSeq);
	}
}
