using UnityEngine;
using System.Collections;

public class positionsetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//기기별 사이즈 대응
		if (name == "Batting") {
			transform.localPosition += new Vector3 (0, (UtilMgr.GetScaledPositionY ()) * 2f);
			if (GetComponent<BattingCommander>() == null) {
				transform.localPosition -= new Vector3 (0, (UtilMgr.GetScaledPositionY ()));
			}
		}else if (name == "Bot") {
			transform.localPosition += new Vector3 (0, (UtilMgr.GetScaledPositionY ()) * 2f);
			transform.FindChild ("BtnPost").FindChild ("TF_Post").localPosition -= new Vector3 (0, (UtilMgr.GetScaledPositionY ()) * 2);
			transform.FindChild ("Challenge").FindChild ("Scroll View").localPosition -= new Vector3 (0, (UtilMgr.GetScaledPositionY ()) * 2);
		} else if(name == "BtnClose"){
			transform.localPosition += new Vector3 (0, (UtilMgr.GetScaledPositionY ()) * 2f);
		} else if(name == "RankReward"){
			transform.localPosition += new Vector3 (0, (UtilMgr.GetScaledPositionY ()));
		} else if(name == "Gift"){
			transform.localPosition += new Vector3 (0, (UtilMgr.GetScaledPositionY ()) * 2f);
		} else{
			transform.localPosition += new Vector3 (0, (UtilMgr.GetScaledPositionY ()) * 2f);
		}
	}

}
