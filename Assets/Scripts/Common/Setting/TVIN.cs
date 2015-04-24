using UnityEngine;
using System.Collections;

public class TVIN : MonoBehaviour {

	public bool tvin = false;
	void Start(){
		tvin = false;
	}
	public void tvinbutten(){
		if (tvin) {
			tvin = false;
			Debug.Log("INTERNET");
			PlayerPrefs.SetString(Constants.PrefSetting_watching_method,"INTERNET");
			Debug.Log(PlayerPrefs.GetString(Constants.PrefSetting_watching_method));

		} else {
			
			tvin = true;
		
			Debug.Log("TV");
			PlayerPrefs.SetString(Constants.PrefSetting_watching_method,"TV");
			Debug.Log(PlayerPrefs.GetString(Constants.PrefSetting_watching_method));
		}
	}
}
