using UnityEngine;
using System.Collections;

public class Gift : MonoBehaviour {
	//메인 상품 OnOff버튼
	bool Check = false;
	public void Button(){
		if (Check) {
			//최초 클릭시 경품이 닫힘
			Check = false;
			transform.localPosition = new Vector2(transform.localPosition.x,12f+55f);
			GetComponent<UISprite>().spriteName = "btn_gift_03";
			transform.FindChild("Arrow").gameObject.SetActive(false);
			transform.FindChild("Arrow").localPosition = new Vector2(0,5);
			transform.FindChild("Arrow").localScale = new Vector2(1,1);
			transform.parent.FindChild("Scroll View").gameObject.SetActive(false);
			transform.parent.FindChild("Shadow").gameObject.SetActive(false);
			SubOnoff(true);
			transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Down");
		} else {
			//경품이 열림
			Check = true;
			transform.localPosition = new Vector2(transform.localPosition.x,248f+55f);
			GetComponent<UISprite>().spriteName = "btn_gift_03";
			transform.FindChild("Arrow").gameObject.SetActive(true);
			transform.FindChild("Arrow").localPosition = new Vector2(0,0);
			transform.FindChild("Arrow").localScale = new Vector2(1,-1);
			//transform.parent.FindChild("Scroll View").GetComponent<UIPanel>().depth = 9;
			transform.parent.FindChild("Scroll View").gameObject.SetActive(true);
			transform.parent.FindChild("Shadow").gameObject.SetActive(true);
			SubOnoff(false);
			transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Up");
				}
	}
	public void Off(){
		//선물상자 이미지 관련
		Check = false;
		transform.localPosition = new Vector2(transform.localPosition.x,12f+55f);
		GetComponent<UISprite>().spriteName = "btn_gift_03";
		transform.FindChild("Arrow").gameObject.SetActive(false);
		transform.FindChild("Arrow").localPosition = new Vector2(0,5);
		transform.FindChild("Arrow").localScale = new Vector2(1,1);
		transform.parent.FindChild("Scroll View").gameObject.SetActive(false);
		transform.parent.FindChild("Shadow").gameObject.SetActive(false);
	}
	public void SubOnoff(bool b){
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Sub").gameObject.SetActive (b);
		transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyAddSubInSub> ().DisableSub ();
		transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyAddSub> ().ResetAddSub ();
		transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyAddSubInSub> ().ResetSubInSub ();
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Top").FindChild("Sub").FindChild("BG_B").gameObject.SetActive(false);

		
	}
}
