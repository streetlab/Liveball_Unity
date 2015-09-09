using UnityEngine;
using System.Collections;

public class ScriptLobbyRoot : ScriptSuperRoot {

	// Use this for initialization
	void Start () {
		transform.FindChild("Setting").gameObject.SetActive(false);
		transform.FindChild("SetNamePage").gameObject.SetActive(false);
		transform.FindChild("Certification").gameObject.SetActive(false);
		transform.FindChild("Item").gameObject.SetActive(false);
		transform.FindChild("Ranking").gameObject.SetActive(false);
		transform.FindChild("TF_Items").gameObject.SetActive(false);
	}
	
	// Update is called once per frame

}
