using UnityEngine;
using System.Collections;

public class OnclickRuby : MonoBehaviour {

	public void onhit(){
		transform.parent.parent.parent.parent.
			GetComponent<Itemcontrol> ().prime31 (
				transform.parent.FindChild("id").GetComponent<UILabel>().text,
				transform.parent.FindChild("code").GetComponent<UILabel>().text,
		        transform.parent.FindChild("LblBody").GetComponent<UILabel>().text,
				transform.parent.FindChild("add").FindChild("buyruby").GetComponent<UILabel>().text,
				transform.parent.FindChild("add").FindChild("addruby").GetComponent<UILabel>().text,
				transform.parent.FindChild("add").FindChild("addgold").GetComponent<UILabel>().text);
	
	}
}
