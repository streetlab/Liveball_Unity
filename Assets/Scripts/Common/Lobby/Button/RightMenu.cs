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
			AllOff();
			float Y = transform.root.FindChild ("Camera").localPosition.y;
			
			transform.root.FindChild ("Setting").gameObject.SetActive(true);
			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);
			
			
		}else if(name == "burger_menu_002"){
			AllOff();
			float Y = transform.root.FindChild ("Camera").localPosition.y;
			
			transform.root.FindChild ("TF_Items").gameObject.SetActive(true);
			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);
		}else if(name == "burger_menu_005"){
			AllOff();
			transform.root.FindChild("Item").GetComponent<ScriptItemMiddle>().Starts();
			float Y = transform.root.FindChild ("Camera").localPosition.y;
			
			transform.root.FindChild ("Item").gameObject.SetActive(true);
			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);

		} else if(name == "burger_menu_004"){
			Application.OpenURL("https://game.nanoo.so/liveball");
		}
	}
	void AllOff(){
		transform.root.FindChild ("Setting").gameObject.SetActive(false);
		transform.root.FindChild ("TF_Items").gameObject.SetActive(false);
		transform.root.FindChild ("Item").gameObject.SetActive(false);
		transform.root.FindChild ("Scroll").FindChild ("Bot").FindChild ("Challenge").FindChild ("Scroll View").gameObject.SetActive (false);
		transform.root.FindChild ("Scroll").FindChild ("Bot").FindChild ("BtnPost").FindChild ("TF_Post").gameObject.SetActive (false);
	}
}