using UnityEngine;
using System.Collections;

public class ScriptTF_Love : MonoBehaviour {	
	
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
}
