using UnityEngine;
using System.Collections;

public class RightMenu : MonoBehaviour {
	public GameObject mPincode;

	// Use this for initialization
	public void Button(){
		if(name.Equals("burger_menu_001")){
			if(mPincode != null)
				mPincode.GetComponent<ScriptPincode>().OpenToCheckPincode();
		}else 

		if (name == "burger_menu_006") {
			float Y = transform.root.FindChild ("Camera").localPosition.y;
			
			transform.root.FindChild ("Setting").gameObject.SetActive(true);
			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);
			
			
		}else if(name == "burger_menu_002"){
			float Y = transform.root.FindChild ("Camera").localPosition.y;
			
			transform.root.FindChild ("TF_Items").gameObject.SetActive(true);
			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);
		}
	}
}