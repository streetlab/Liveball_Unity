using UnityEngine;
using System.Collections;

public class RightMenu : MonoBehaviour {

	public GameObject mPincode;

	// Use this for initialization
	public void Button(){
		Debug.Log(name);
		if(name.Equals("burger_menu_001")){
			if(mPincode != null)
				mPincode.GetComponent<ScriptPincode>().OpenToCheckPincode();
		}
	}
}
