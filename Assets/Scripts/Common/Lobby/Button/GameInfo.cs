using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

	public void Button(){
		Debug.Log (name);
		if (name == "SAVG") {
			transform.FindChild("Bar").gameObject.SetActive(true);
			transform.parent.FindChild("PlayersInfo").FindChild("Bar").gameObject.SetActive(false);
			transform.parent.parent.FindChild("Info").FindChild("BG1").gameObject.SetActive(true);
			transform.parent.parent.FindChild("Info").FindChild("BG2").gameObject.SetActive(false);
		} else if (name == "PlayersInfo") {
			transform.FindChild("Bar").gameObject.SetActive(true);
			transform.parent.FindChild("SAVG").FindChild("Bar").gameObject.SetActive(false);
			transform.parent.parent.FindChild("Info").FindChild("BG2").gameObject.SetActive(true);
			transform.parent.parent.FindChild("Info").FindChild("BG1").gameObject.SetActive(false);
		}
	}
}
