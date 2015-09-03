using UnityEngine;
using System.Collections;

public class HistoryRankingClose : MonoBehaviour {
	public void Button(){
		gameObject.SetActive (false);
	}


	public void RankingOn(string Title,string MyRank,string Tpreset,string Tentry,string Rscore,string Rcount,string gameSeq){
		transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = Title;




		//히스토리 랭킹 관련
		transform.FindChild ("Ranking").GetComponent<HIstoryRankingCommander> ().Button (int.Parse(gameSeq));




		//히스토리 게이지 관련
		GameObject RankGage = transform.FindChild("RankGagePanel").FindChild ("RankGage").FindChild("BGIn").gameObject;
		
		
		if (MyRank == "0") {
			RankGage.transform.FindChild ("BG").FindChild ("Maker").localPosition = new Vector3 (
				-316 + ((float.Parse (MyRank) / float.Parse (Tentry)) * 632), 23);

		} else {
		
			RankGage.transform.FindChild ("BG").FindChild ("Maker").localPosition = new Vector3 (
				-316f+((float.Parse (Tentry)-(float.Parse (MyRank)-1))/float.Parse (Tentry) * 632f), 23);


//			if(float.Parse (Rcount)>float.Parse(Tpreset)){
//				RankGage.transform.FindChild ("BG").FindChild ("rewordScore").localPosition = new Vector3 (
//					-316f+(632f), -35);
//			}else{
//			RankGage.transform.FindChild ("BG").FindChild ("rewordScore").localPosition = new Vector3 (
//				-316f+((float.Parse (Tpreset)-(float.Parse (Rcount)-1)) /float.Parse(Tpreset)*632), -35);
//			}
		
		}
		RankGage.transform.FindChild ("BG").FindChild ("rewordScore").localPosition = new Vector3 (
			-316f+((float.Parse (Tentry)-(float.Parse (Rcount)-1)) /float.Parse(Tentry)*632), -35);
		
		RankGage.transform.FindChild("BG").FindChild("rewordScore").GetComponent<UILabel>().text = Rcount;
		//Debug.Log ("((float.Parse (Tpreset)-(float.Parse (Rcount)-1)) /float.Parse(Tpreset)*632) : " + ((float.Parse (Tpreset)-(float.Parse (Rcount)-1)) /float.Parse(Tpreset)*632));
		RankGage.transform.FindChild("BG").FindChild("R_bar").GetComponent<UIPanel>().clipOffset = new Vector2(((float.Parse (Tentry)-(float.Parse (Rcount)-1)) /float.Parse(Tentry)*632),0);

//		if (float.Parse (Rcount) >= float.Parse (Tpreset)) {
//			RankGage.transform.FindChild ("BG").FindChild ("R_bar").GetComponent<UIPanel> ().clipOffset = new Vector2 (0, 0);
//			RankGage.transform.FindChild ("BG").FindChild ("rewordScore").localPosition = new Vector3 (-316f, -35);
//		}

		gameObject.SetActive (true);
	} 



}
