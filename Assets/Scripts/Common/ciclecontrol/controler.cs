using UnityEngine;
using System.Collections;

public class controler : MonoBehaviour {
	public float speed = 1;
	public float pm = 1;
	public float percent = 70;
	float percent1;
	void Start(){
		StartCoroutine (roll ());
	}

	IEnumerator roll(){
		if (percent > 50) {
			percent1 = 50; 
		} else {
			percent1 = percent;
		}
		for (int i =0; i<(180/speed)*(percent1/50); i++) {
			//gameObject.transform.Rotate (new Vector3 (0, 0, -speed*pm));
			gameObject.transform.GetChild (0).transform.GetChild (0).transform.Rotate (new Vector3 (0, 0, -speed*pm));
			yield return new WaitForSeconds (0.02f);
		}
		if(percent>50){
			percent1 =percent-50;
		gameObject.transform.GetChild (1).gameObject.SetActive (true);
			for (int i =0; i<180/speed*(percent1/50); i++) {
			//gameObject.transform.Rotate (new Vector3 (0, 0, -speed*pm));
			gameObject.transform.GetChild (1).transform.GetChild (0).transform.Rotate (new Vector3 (0, 0, -speed*pm));
			yield return new WaitForSeconds (0.02f);
		}
		}
	}
}
