using UnityEngine;
using System.Collections;

public class HistoryRankingClose : MonoBehaviour {
	public void Button(){
		gameObject.SetActive (false);
	}
	public void RankingOn(string Title,string MyRank,string Tpreset,string Tentry,string Rscore,string Rcount,string gameSeq){
		transform.FindChild ("Title").FindChild ("Label").GetComponent<UILabel> ().text = Title;





		transform.FindChild ("Ranking").GetComponent<HIstoryRankingCommander> ().Button (int.Parse(gameSeq));





		GameObject RankGage = transform.FindChild ("RankGage").FindChild("BGIn").gameObject;
		
		
		
		RankGage.transform.FindChild("BG").FindChild("Maker").localPosition = new Vector3(
			-316+(float.Parse(MyRank)/ float.Parse(Tpreset))*632,23);
		RankGage.transform.FindChild("BG").FindChild("rewordScore").localPosition = new Vector3(
			-316+(float.Parse(MyRank)/ float.Parse(Tpreset))*632,-35);
		
		RankGage.transform.FindChild("BG").FindChild("rewordScore").GetComponent<UILabel>().text = Rscore;
		RankGage.transform.FindChild("BG").FindChild("R_bar").GetComponent<UIPanel>().clipOffset = new Vector2((float.Parse(Rcount)/float.Parse(Tpreset))*632,0);

		gameObject.SetActive (true);
	} 



}
