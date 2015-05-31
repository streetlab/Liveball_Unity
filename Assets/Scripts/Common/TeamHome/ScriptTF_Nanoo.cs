using UnityEngine;
using System.Collections;

public class ScriptTF_Nanoo : MonoBehaviour {

	public GameObject mMainMenu;
	public GameObject mUniWebView;

	public void MenuBtnClicked(){
		//Debug.Log ("cbcbvcbcvbcv ");
		string menuStatus = mMainMenu.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("StatusAnimation").Value;
		Debug.Log ("menuStatus : " + menuStatus);
		if (menuStatus.Equals ("Closed")) {
			mUniWebView.GetComponent<ScriptLove>().HideWebView();

		} else {
			mUniWebView.GetComponent<ScriptLove>().ShowWebView();
			//StartCoroutine(ColsedWait());
		}
	}
	IEnumerator ColsedWait(){
		yield return new WaitForSeconds(0.2f);
		transform.gameObject.SetActive(true);

	}

//	void Update(){
//		string menuStatus = mMainMenu.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("StatusAnimation").Value;
//		Debug.Log ("menuStatus : " + menuStatus);
//		if (menuStatus.Equals ("Closed")) {
//			transform.FindChild ("Panel").gameObject.SetActive (true);
//		} else {
//			transform.FindChild ("Panel").gameObject.SetActive (false);
//		}
//	}
}
