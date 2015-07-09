using UnityEngine;
using System.Collections;

public class GameParticipantRank : MonoBehaviour {
	GetGameParticipantRankingEvent Rank;
	public void ViewRank(){
		Rank = new GetGameParticipantRankingEvent (new EventDelegate (this, "SetRank"));
		NetMgr.GetGameParticipantRanking (Rank);
	}
	void SetRank(){
		//transform.FindChild("BG_W").FindChild("MyRank").FindChild("Name").GetComponent<UILabel>().text = 
	}
}
