using UnityEngine;
using System.Collections;

public class GetLength : MonoBehaviour {

	 public void get(){
if(transform.parent.name == "SetNamePage"){
			transform.FindChild("Label").GetComponent<UILabel>().text = transform.parent.FindChild("Input").FindChild("Label").GetComponent<UILabel>().text.Length+"/10";
	
	}else{

			transform.FindChild("Label").GetComponent<UILabel>().text = transform.parent.FindChild("Input").FindChild("Label").GetComponent<UILabel>().text.Length+"/20";
	}

}
}