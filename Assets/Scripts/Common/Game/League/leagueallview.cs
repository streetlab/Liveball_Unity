using UnityEngine;
using System.Collections;

public class leagueallview : MonoBehaviour {

	char[] strings;
	public void omhit(){
		//Debug.Log (gameObject.transform.parent.parent);
		strings = gameObject.transform.parent.parent.parent.gameObject.ToString ().ToCharArray ();
		//		switch (int.Parse (strings [5].ToString())) {
		//		case 0:
		//			Debug.Log("allview : "+ strings [5].ToString());
		//			break;
		//		case 1:
		//			Debug.Log("allview : "+ strings [5].ToString());
		//			break;
		//		case 2:
		//			Debug.Log("allview : "+ strings [5].ToString());
		//			break;
		//		
		//		}
	//	Debug.Log (gameObject.transform.parent.parent.parent.parent.parent.parent);
		gameObject.transform.parent.parent.parent.parent.parent.parent.parent.GetComponent<fanleaguecontrol> ().allview (int.Parse (strings [5].ToString()));
	}
}
