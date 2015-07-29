using UnityEngine;
using System.Collections;

public class GiftShadow : MonoBehaviour {

	public void Button(){
		transform.parent.FindChild ("GiftButton").GetComponent<Gift> ().Off ();
		transform.parent.FindChild ("GiftButton").GetComponent<Gift> ().SubOnoff (true);
		transform.root.FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Down");

	}
}
