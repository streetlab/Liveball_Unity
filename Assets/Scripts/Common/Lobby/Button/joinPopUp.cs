using UnityEngine;
using System.Collections;

public class joinPopUp : MonoBehaviour {
	public GameObject Register;

	//컨테스트 참여버튼
	public void Button(){
		Debug.Log (name);
		if (name == "Button1") {
			Register.GetComponent<PresettingRC>().JoinButton();
			transform.parent.parent.gameObject.SetActive(false);
		} else {
			transform.parent.parent.gameObject.SetActive(false);
		}
	}
}
