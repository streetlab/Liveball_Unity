using UnityEngine;
using System.Collections;

public class TVIN : MonoBehaviour {

//	public bool tvin = false;
//	void Start(){
//		tvin = false;
//	}

	public void TvButton(){
		PlayerPrefs.SetString(Constants.PrefSetting_watching_method,"TV");
		Debug.Log(PlayerPrefs.GetString(Constants.PrefSetting_watching_method));
	}

	public void InButton(){
		PlayerPrefs.SetString(Constants.PrefSetting_watching_method,"INTERNET");
		Debug.Log(PlayerPrefs.GetString(Constants.PrefSetting_watching_method));
	}
}
