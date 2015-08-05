using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {
	public static string Status;
	public void Button(){
		Debug.Log (name);
		if (name == "Button1") {
			if (Status == "Profile") {
				transform.root.FindChild("Scroll").FindChild ("RightMenu").GetComponent<RightMenuCommander> ().SetMemberName (transform.parent.FindChild
			                                                                                                       ("Input").GetComponent<UIInput> ().value);
				transform.parent.parent.gameObject.SetActive(false);
			}
		} else {
			transform.parent.FindChild
				("Input").GetComponent<UIInput> ().value = "";
			transform.parent.parent.gameObject.SetActive(false);
		}
	}
}
