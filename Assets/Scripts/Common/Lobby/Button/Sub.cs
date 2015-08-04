using UnityEngine;
using System.Collections;

public class Sub : MonoBehaviour {

	public void button(){
		Debug.Log ("MenuStatus " + LobbyMainCommander.MenuStatus +" : " + name);
		bool Check = false;
		if (transform.FindChild (name + "Box").gameObject.activeSelf) {
			Check = true;
		}
		for(int i = 0; i < transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyAddSub> ().SubMenuName.Length; i++){
		
			transform.parent.FindChild(transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyAddSub> ().SubMenuName [i]).FindChild(transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyAddSub> ().SubMenuName [i] + "Box").gameObject.SetActive(false);
		}
		if (Check) {
			transform.FindChild (name + "Box").gameObject.SetActive (false);
			transform.parent.FindChild ("BG_B").gameObject.SetActive (false);
		} else {
			transform.FindChild (name + "Box").gameObject.SetActive (true);
			transform.parent.FindChild ("BG_B").gameObject.SetActive (true);
		}

	}
}
