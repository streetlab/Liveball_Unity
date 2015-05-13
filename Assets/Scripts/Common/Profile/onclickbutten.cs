using UnityEngine;
using System.Collections;

public class onclickbutten : MonoBehaviour {

	public void setImgae(){

		for(int i = 0; i<19;i+=2){
			//Debug.Log(transform.parent.parent.GetChild(i)+" I : " + i);
			transform.parent.parent.GetChild(i).GetChild(3).GetComponent<UIButton> ().defaultColor  = new Color(0.9f,0.9f,0.9f,1);
			transform.parent.parent.GetChild(i).GetChild(3).GetComponent<UIButton> ().hover  = new Color(0.88f,0.78f,0.39f,1);
			transform.parent.parent.GetChild(i).GetChild(3).GetComponent<UIButton> ().disabledColor  = new Color(0.9f,0.9f,0.9f,1);
			transform.parent.parent.GetChild(i).GetChild(3).GetComponent<UIButton> ().normalSprite = "ic_plus";
		}
		GetComponent<UIButton> ().defaultColor= new Color(1,1,1,1);
		GetComponent<UIButton> ().hover  = new Color(1,1,1,1);
		GetComponent<UIButton> ().disabledColor  = new Color(1,1,1,1);
		GetComponent<UIButton> ().normalSprite = "btn_team_check_on";
		transform.parent.parent.parent.parent.parent.parent.parent.GetComponent<ScriptSelectTeam> ().ChangeTeam (transform.parent.name);

	}
}
