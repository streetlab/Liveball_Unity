using UnityEngine;
using System.Collections;

public class stbarclick : MonoBehaviour {

	char[] strings1;
	char[] strings2;
	public void onhit(){
		strings1 = gameObject.transform.parent.parent.parent.gameObject.ToString().ToCharArray();
		strings2 = gameObject.ToString ().ToCharArray ();
		switch(int.Parse(strings1[5].ToString())){
		case 0:
			switch(int.Parse(strings2[3].ToString())){
			case 1:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
			case 2:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
			case 3:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
			
			}
			break;
		case 1:
			switch(int.Parse(strings2[3].ToString())){
			case 1:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
			case 2:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
			case 3:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
				
			}
			break;
		case 2:
			switch(int.Parse(strings2[3].ToString())){
			case 1:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
			case 2:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
			case 3:
				Debug.Log("top : "+strings1[5].ToString()+" bar : " + strings2[3].ToString());
				break;
				
			}
			break;

	}
}
}
