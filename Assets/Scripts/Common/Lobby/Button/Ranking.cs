using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour {
	static int Tstatus=1;
	static int Bstatus=1;

	//랭킹페이지 
	public void Button(){
		GameObject Ranking = transform.root.FindChild ("Ranking").gameObject;
		bool Check = true;
		switch (name) {
		case "Sranking":
//			if(Tstatus ==1){
//				Check = false;
//			}
			Tstatus = 1;
			transform.FindChild("Bar").gameObject.SetActive(true);
			transform.FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
			transform.parent.FindChild("Wranking").FindChild("Bar").gameObject.SetActive(false);
			transform.parent.FindChild("Wranking").FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
			break;
		case "Wranking":
//			if(Tstatus ==2){
//				Check = false;
//			}
			Tstatus = 2;
			transform.FindChild("Bar").gameObject.SetActive(true);
			transform.FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
			transform.parent.FindChild("Sranking").FindChild("Bar").gameObject.SetActive(false);
			transform.parent.FindChild("Sranking").FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
			break;
		case "getpoint":
			transform.FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
			transform.parent.FindChild("getchose").FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
			Bstatus = 1;
			break;
		case "getchose":
			transform.FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
			transform.parent.FindChild("getpoint").FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
			Bstatus = 2;
			break;
		}
	//	Debug.Log("Check : " + Check);
		if (Check) {
			if (Tstatus == 1) {

				if (Bstatus == 1) {
					// T = 1 , B = 1
					Ranking.GetComponent<RankingCommander> ().List01 ();
				} else {
					// T = 1 , B = 2
					Ranking.GetComponent<RankingCommander> ().List02 ();
				}

			} else {
		
				if (Bstatus == 1) {
					// T = 2 , B = 1
					Ranking.GetComponent<RankingCommander> ().List03 ();
				} else {
					// T = 2 , B = 2
					Ranking.GetComponent<RankingCommander> ().List04 ();
				}

			}
		}
		Check = true;
	}
}
