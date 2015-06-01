using UnityEngine;
using System.Collections;

public class top_back_butten : MonoBehaviour {

	public void onhit(){


			transform.parent.GetChild(1).gameObject.SetActive(true);
			transform.parent.GetChild(2).gameObject.SetActive(true);
		transform.parent.parent.GetChild (1).gameObject.SetActive (true);
		transform.parent.parent.parent.GetChild(2).gameObject.SetActive(true);
		transform.parent.parent.parent.GetChild(3).gameObject.SetActive(false);


			transform.parent.GetChild(4).gameObject.SetActive(false);
			transform.parent.GetChild(5).gameObject.SetActive(false);

	}
}
