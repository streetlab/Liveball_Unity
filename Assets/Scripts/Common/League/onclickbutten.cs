using UnityEngine;
using System.Collections;

public class onclickbutten : MonoBehaviour {
	char[] strings;
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
		strings = transform.parent.gameObject.ToString ().ToCharArray ();
		if(strings [0].ToString ()=="T"){
			Debug.Log("10");
		}else{
		switch (int.Parse (strings [0].ToString ())) {
		case 0:
			Debug.Log("0");
			break;
		case 1:
			Debug.Log("1");
			break;
		case 2:
			Debug.Log("2");
			break;
		case 3:
			Debug.Log("3");
			break;
		case 4:
			Debug.Log("4");
			break;
		case 5:
			Debug.Log("5");
			break;
		case 6:
			Debug.Log("6");
			break;
		case 7:
			Debug.Log("7");
			break;
		case 8:
			Debug.Log("8");
			break;
		case 9:
			Debug.Log("9");
			break;
		

		}
		}

	}
}
