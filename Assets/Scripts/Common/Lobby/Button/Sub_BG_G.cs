using UnityEngine;
using System.Collections;

public class Sub_BG_G : MonoBehaviour {

	// Use this for initialization
	public void Button(){
		for(int i = 0; i < transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyAddSub> ().SubMenuName.Length; i++){
			
			transform.parent.FindChild(transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyAddSub> ().SubMenuName [i]).FindChild(transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyAddSub> ().SubMenuName [i] + "Box").gameObject.SetActive(false);
		}
		this.gameObject.SetActive (false);
	}
}
