using UnityEngine;
using System.Collections;

public class sh : MonoBehaviour {
	public bool onoff = false;

	void Start(){
		onoff = false;
	}
	public void onoffbutten(){
		if (onoff) {
			onoff = false;
			Debug.Log("ON");
			PlayerPrefs.SetString(Constants.PrefSetting_vibrate_on_off,"on");
		} else {
		
			onoff = true;
		
			Debug.Log("OFF");
			PlayerPrefs.SetString(Constants.PrefSetting_vibrate_on_off,"off");
		}
	}
}
