using UnityEngine;
using System.Collections;

public class sh : MonoBehaviour {
	public bool onoff;

	void Start(){
		Debug.Log("PlayerPrefs.GetString (Constants.PrefSetting_vibrate_on_off) : " + PlayerPrefs.GetString (Constants.PrefSetting_vibrate_on_off));
		if (PlayerPrefs.GetString (Constants.PrefSetting_vibrate_on_off) == null) {
			PlayerPrefs.SetString (Constants.PrefSetting_vibrate_on_off, "on");	
			transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value =true;
			Debug.Log("NULL");

		}
		if (PlayerPrefs.GetString (Constants.PrefSetting_vibrate_on_off) == "on") {
			Debug.Log (transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value );
			Debug.Log("ON");
			transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value = true;
		} else {
			Debug.Log (transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value );
			Debug.Log("OFF");
			transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value =false;
		}


	}
	public void onoffbutten(){
		if (PlayerPrefs.GetString (Constants.PrefSetting_vibrate_on_off) == "off") {
			Debug.Log (transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value );
			Debug.Log("ON");
			PlayerPrefs.SetString(Constants.PrefSetting_vibrate_on_off,"on");
			transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value =true;
		} else {
		
		
			Debug.Log (transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value );
			Debug.Log("OFF");
			PlayerPrefs.SetString(Constants.PrefSetting_vibrate_on_off,"off");
			transform.FindChild ("Toggle SH").GetComponent<UIToggle> ().value =false;
		}
	}
}
