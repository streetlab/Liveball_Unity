using UnityEngine;
using System.Collections;

public class ar_top_back : MonoBehaviour {

	public void onhit(){
		
		
		transform.parent.parent.parent.GetChild (3).GetChild (0).gameObject.SetActive (true);

		transform.parent.parent.parent.GetChild (3).GetChild (2).gameObject.SetActive (false);
		transform.parent.parent.parent.GetChild (3).GetChild (3).gameObject.SetActive (false);
		transform.parent.parent.parent.GetChild (3).GetChild (4).gameObject.SetActive (false);
		transform.parent.parent.parent.GetChild (3).GetChild (5).gameObject.SetActive (false);

		
		transform.parent.parent.GetChild (1).gameObject.SetActive (true);
		transform.parent.GetChild(4).gameObject.SetActive(false);
		transform.parent.GetChild(5).gameObject.SetActive(false);
		
	}
}
