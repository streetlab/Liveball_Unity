using UnityEngine;
using System.Collections;

public class OnclickItem : MonoBehaviour {

	public void onhit(){
		transform.parent.parent.parent.parent.
			GetComponent<Itemcontrol> ().setuseritem (int.Parse(transform.parent.FindChild("id").GetComponent<UILabel>().text),int.Parse(transform.parent.FindChild("buygold").GetComponent<UILabel>().text),transform.parent.FindChild("LblBody").GetComponent<UILabel>().text);
	}
}
