using UnityEngine;
using System.Collections;

public class SubInSub : MonoBehaviour {

	public void Button(){
		Debug.Log ("MenuStatus " + LobbyMainCommander.MenuStatus + " : " + transform.parent.name + " : " + name);
		bool Check = true;
		int i = 0;
		while (Check) {
			if(transform.parent.FindChild("Menu " + i.ToString())!=null){
				//transform.parent.FindChild("Menu" + i.ToString()).GetComponent<UIButton> ().defaultColor = Color.white;
				transform.parent.FindChild("Menu " + i.ToString()).GetComponent<BoxCollider2D>().enabled = true; 

				transform.parent.FindChild("Menu " + i.ToString()).gameObject.SetActive(false);
				transform.parent.FindChild("Menu " + i.ToString()).gameObject.SetActive(true);

			}else{
				Check = false;
			}
			i++;
		}
		GetComponent<BoxCollider2D> ().enabled = false;
		gameObject.SetActive(false);
		gameObject.SetActive(true);
		//GetComponent<UIButton> ().disabledColor = Color.yellow;
		transform.parent.parent.FindChild("Label").GetComponent<UILabel>().text = GetComponent<UILabel>().text;

	}
}
