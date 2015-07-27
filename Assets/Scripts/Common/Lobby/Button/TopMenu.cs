using UnityEngine;
using System.Collections;

public class TopMenu : MonoBehaviour {

	public void Button(){
		int MenuStatus = LobbyMainCommander.MenuStatus;
		AllBarDisable ();
		for (int i = 0; i<transform.root.FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName.Length; i++) {

			if(name == transform.root.FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]){
				LobbyMainCommander.MenuStatus = i+1;
				transform.FindChild("Bar").gameObject.SetActive(true);
				transform.FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
				transform.FindChild("Num").GetComponent<UILabel>().color = new Color(1,1,1,1);
				break;
			}
		}
		Debug.Log (name + " : MenuStatus " + LobbyMainCommander.MenuStatus);

		if (MenuStatus == LobbyMainCommander.MenuStatus) {
			if (transform.parent.FindChild ("Sub").gameObject.activeSelf) {
				transform.root.FindChild ("Main").GetComponent<LobbyAddSub> ().DisableSub ();
			} else {
				transform.parent.FindChild ("Sub").gameObject.SetActive (true);
			}
		} else {
			transform.parent.FindChild ("Sub").gameObject.SetActive (true);
			transform.root.FindChild ("Main").GetComponent<LobbyAddSubInSub> ().DisableSub ();
		}
	}
	void AllBarDisable(){
		for (int i = 0; i<transform.root.FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName.Length; i++) {
			transform.parent.FindChild(transform.root.FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Bar").
				gameObject.SetActive(false);
			transform.parent.FindChild(transform.root.FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Label").
				GetComponent<UILabel>().color = new Color(1f,1f,1f,0.5f);
			transform.parent.FindChild(transform.root.FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Num").
				GetComponent<UILabel>().color = new Color(1f,1f,1f,0.5f);
		} 
	}
}