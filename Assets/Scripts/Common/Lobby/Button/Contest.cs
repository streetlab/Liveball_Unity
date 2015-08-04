using UnityEngine;
using System.Collections;

public class Contest : MonoBehaviour {

	public void button(){
		UserMgr.CurrentContestSeq = int.Parse(transform.FindChild ("BG").FindChild ("ContestSeq").GetComponent<UILabel> ().text);
		transform.parent.parent.parent.FindChild ("Nomal Contest").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (true);
		transform.parent.parent.parent.FindChild ("Top").FindChild("Sub").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
		                                                           <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}
}
