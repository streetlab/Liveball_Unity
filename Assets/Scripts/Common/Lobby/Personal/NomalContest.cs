using UnityEngine;
using System.Collections;

public class NomalContest : MonoBehaviour {

	void OnEnable (){
		StartCoroutine ("Reset");
	}

	void OnDisable (){
		StopCoroutine ("Reset");
	}

	IEnumerator Reset(){
		while (true) {
			yield return new WaitForSeconds (60f);
			transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().ResetInBackground ();
		}
	}
}
