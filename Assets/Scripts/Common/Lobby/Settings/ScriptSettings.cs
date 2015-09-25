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

		string watching = PlayerPrefs.GetString(Constants.PrefSetting_watching_method);
//		Debug.Log("watching is "+watching);
		if(watching == null || watching.Equals("") || watching.Equals("TV")){
			transform.FindChild("Scroll").FindChild("bg_g").FindChild("bg_w").FindChild("bar2")
				.FindChild("Toggle TV").GetComponent<UIToggle>().value = true;
		} else{
			transform.FindChild("Scroll").FindChild("bg_g").FindChild("bg_w").FindChild("bar2")
				.FindChild("Toggle IN").GetComponent<UIToggle>().value = true;
		}
	}
}
