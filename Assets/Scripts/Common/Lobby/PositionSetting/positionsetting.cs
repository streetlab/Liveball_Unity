using UnityEngine;
using System.Collections;

public class positionsetting : MonoBehaviour {

	// Use this for initialization
	void Awake () {

	//	Debug.Log(UtilMgr.GetScaledPositionY ());

		transform.localPosition += new Vector3 (0,(UtilMgr.GetScaledPositionY ())*2);
		if (name == "Bot") {
			
		
		transform.FindChild ("BtnPost").FindChild ("TF_Post").localPosition -= new Vector3 (0,(UtilMgr.GetScaledPositionY ())*2);
		transform.FindChild ("Challenge").FindChild ("Scroll View").localPosition -= new Vector3 (0,(UtilMgr.GetScaledPositionY ())*2);
		}

	}

}
