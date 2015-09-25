using UnityEngine;
using System.Collections;

public class HistoryItem1 : MonoBehaviour {
	RemoveContestHistoryEvent mRemoveHisEvent;

	public void BtnTop(){
		transform.GetComponentInChildren<HistoryItem>().Button();
	}
	
	public void Delete(){
		mRemoveHisEvent = new RemoveContestHistoryEvent(new EventDelegate(this, "DeleteComplete"));
		int presetSeq = int.Parse(transform.GetComponentInChildren<HistoryItem>()
			.transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text);
		Debug.Log("Delete num is "+presetSeq);
		NetMgr.RemoveContestHistory(presetSeq, mRemoveHisEvent);
	}
	
	void DeleteComplete(){
		transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("History")
			.GetComponent<TopMenu>().Button();
	}
}
