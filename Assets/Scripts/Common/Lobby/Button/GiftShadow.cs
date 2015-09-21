using UnityEngine;
using System.Collections;

public class GiftShadow : MonoBehaviour {
	public void Button(){
		//상품 상단 그림자 클릭 시 경품이 사라짐
		transform.parent.FindChild ("GiftButton").GetComponent<Gift> ().Off ();
		transform.parent.FindChild ("GiftButton").GetComponent<Gift> ().SubOnoff (false);
		transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Up");

	}
}
