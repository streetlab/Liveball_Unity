using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {
	public bool IsLeaving = false;

	public void Button(){
		gameObject.SetActive(false);

		float Y = transform.root.FindChild ("Camera").localPosition.y;

		if(IsLeaving){
			transform.parent.parent.FindChild("Scroll").FindChild("bg_g").gameObject.SetActive(true);
			transform.parent.parent.FindChild("Scroll").FindChild("bg_g 1").gameObject.SetActive(true);
			transform.parent.parent.FindChild("Scroll").FindChild("bg_leave").gameObject.SetActive(false);
			transform.parent.FindChild("Label").GetComponent<UILabel>().text = "설정";
			IsLeaving = false;
			return;
		}

		if (transform.root.FindChild ("Setting").gameObject.activeSelf) {
			ScriptMainTop.OpenBettingCheck = true;
			transform.root.FindChild ("Setting").gameObject.SetActive (false);
		

		} else {
			transform.root.FindChild ("Setting").gameObject.SetActive(true);
			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);

		}
	}
}
