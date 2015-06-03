using UnityEngine;
using System.Collections;

public class rankbutten : MonoBehaviour {
	int num;
	char[] strings;
	public void onhit(){
//		strings = gameObject.ToString ().ToCharArray ();
//		num = int.Parse(strings [3].ToString ());
//		Debug.Log ("rankbutten : "+num);
		transform.parent.parent.parent.parent.parent.parent.GetComponent<Rankcontrol> ().GoMainScens (transform.FindChild("name").GetComponent<UILabel>().text,transform.FindChild("Seq").GetComponent<UILabel>().text);
	}
}
