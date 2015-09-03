using UnityEngine;
using System.Collections;

public class ScriptSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(){
		transform.FindChild("Scroll").FindChild("bg_g").gameObject.SetActive(true);
		transform.FindChild("Scroll").FindChild("bg_g 1").gameObject.SetActive(true);
		transform.FindChild("Scroll").FindChild("bg_leave").gameObject.SetActive(false);
	}
}
