using UnityEngine;
using System.Collections;

public class SubInSub : MonoBehaviour {

	public void Button(){


		Debug.Log ("MenuStatus " + LobbyMainCommander.MenuStatus + " : " + transform.parent.name + " : " + name);

		if (transform.FindChild ("Arrow") != null) {
			if (transform.FindChild ("Arrow").GetComponent<UISprite> ().color == Color.yellow) {
				if (transform.FindChild ("Arrow").localScale.y == 1) {
					transform.FindChild ("Arrow").localScale = new Vector3 (1, -1, 1);
					transform.parent.parent.FindChild ("Arrow").localScale = new Vector3 (1, -1, 1);
				} else {
					transform.FindChild ("Arrow").localScale = new Vector3 (1, 1, 1);
					transform.parent.parent.FindChild ("Arrow").localScale = new Vector3 (1, 1, 1);
				}
			} else {
				transform.FindChild ("Arrow").localScale = new Vector3 (1, 1, 1);
				transform.parent.parent.FindChild ("Arrow").localScale = new Vector3 (1, 1, 1);
			}
		}
		
		bool Check = true;
		int i = 0;
		while (Check) {
			if(transform.parent.FindChild("Menu " + i.ToString())!=null){
				//transform.parent.FindChild("Menu" + i.ToString()).GetComponent<UIButton> ().defaultColor = Color.white;

				if(transform.parent.name == "PeopleBox"){
				//	int a = 0;
				//	bool Check2 = true;
			//	while(Check2){
						if(transform.parent.FindChild("Menu " + i.ToString())!=null){
							transform.parent.FindChild("Menu " + i.ToString()).FindChild("white").gameObject.SetActive(true);
							transform.parent.FindChild("Menu " + i.ToString()).FindChild("yellow").gameObject.SetActive(false);
						//	}else{
						//	Check2 = false;
					//	}
						//a++;
				}

					if(transform.parent.FindChild("Menu " + i.ToString()).FindChild("Arrow")!=null){

						if("Menu " + i.ToString()!=this.name){
						transform.parent.FindChild("Menu " + i.ToString()).FindChild("Arrow").GetComponent<UISprite>().color = Color.white;
							transform.parent.FindChild("Menu " + i.ToString()).FindChild("Arrow").localScale = new Vector3(1,1,1);
					}
					
					}

				}else{
				if(transform.parent.FindChild("Menu " + i.ToString()).GetComponent<UIButton>()!=null){
					transform.parent.FindChild("Menu " + i.ToString()).GetComponent<UIButton> ().isEnabled = true;
				}

				transform.parent.FindChild("Menu " + i.ToString()).gameObject.SetActive(false);
				transform.parent.FindChild("Menu " + i.ToString()).gameObject.SetActive(true);
				
				}
			}else{
				Check = false;
			}
			i++;
		}
	
		if (transform.parent.name == "PeopleBox") {
			transform.FindChild("white").gameObject.SetActive(false);
			transform.FindChild("yellow").gameObject.SetActive(true);
			transform.parent.parent.FindChild("Label").GetComponent<UILabel>().text = transform.FindChild("white").GetComponent<UILabel>().text;
			if (transform.FindChild ("Arrow") != null) {
				transform.FindChild ("Arrow").GetComponent<UISprite> ().color = Color.yellow;
			}
		} else {
			GetComponent<UIButton> ().isEnabled = false;
//			gameObject.SetActive (false);
//			gameObject.SetActive (true);
		
			transform.parent.parent.FindChild("Label").GetComponent<UILabel>().text = GetComponent<UILabel>().text;
		}

	
		//GetComponent<UIButton> ().disabledColor = Color.yellow;
	

	}
}
