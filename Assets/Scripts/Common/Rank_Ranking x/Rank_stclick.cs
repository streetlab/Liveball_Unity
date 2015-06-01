using UnityEngine;
using System.Collections;

public class Rank_stclick : MonoBehaviour {
	char[] strings;


	public void stclicks(){
		//Debug.Log (gameObject.transform.parent.parent);
		strings = gameObject.transform.parent.parent.parent.gameObject.ToString ().ToCharArray ();
		switch (int.Parse (strings [5].ToString())) {
		case 0:
			Debug.Log("0");
			transform.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(0).GetChild(4).gameObject.SetActive(true);
			transform.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(0).GetChild(5).gameObject.SetActive(true);
			transform.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
			transform.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
			transform.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(1).gameObject.SetActive(false);
			transform.parent.parent.parent.parent.parent.parent.parent.GetChild(3).gameObject.SetActive(true);
			transform.parent.parent.parent.parent.parent.parent.parent.GetChild(2).gameObject.SetActive(false);
			break;
		case 1:
			Debug.Log("1");
			break;
		case 2:
			Debug.Log("2");
			break;
		
		}
	}
}
