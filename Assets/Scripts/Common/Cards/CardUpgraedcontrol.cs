using UnityEngine;
using System.Collections;

public class CardUpgraedcontrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log ("Screen.height : " + (float)Screen.width/(float)Screen.height);

		this.transform.localPosition += new Vector3 (0,UtilMgr.GetScaledPositionY (),0);
		UtilMgr.ResizeList(gameObject);
	}

	public void cardon(){
		transform.parent.FindChild ("Card").gameObject.SetActive (true);
		transform.parent.FindChild ("Item").gameObject.SetActive (false);
	}
	public void itemon(){
		transform.parent.FindChild ("Card").gameObject.SetActive (false);
		transform.parent.FindChild ("Item").gameObject.SetActive (true);
	}

}
