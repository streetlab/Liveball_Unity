using UnityEngine;
using System.Collections;

public class YellowLight : MonoBehaviour {

	// Use this for initialization
	bool check = false;
	void Start(){
		if (!check) {
			check = true;
			StartCoroutine ("Lighten");
		}
	}
	void OnEnable(){
		if (!check) {
			check = true;
			StartCoroutine ("Lighten");
		}
	}
	public void off(){
		check = false;
		StopCoroutine ("Lighten");
	}
	IEnumerator Lighten(){
		Color origin = transform.GetComponent<UISprite> ().color;
		while(check){
		for (int i = 20; i>0; i--) {
				transform.GetComponent<UISprite> ().color = new Color (origin.r, origin.g, origin.b, ((float)i)/20f);
				yield return new WaitForSeconds (0.05f);
		}
		for (int i = 0; i<5; i++) {
				transform.GetComponent<UISprite> ().color = new Color (origin.r, origin.g, origin.b, ((float)i)/5f);
				yield return new WaitForSeconds (0.05f);
			}
		
		}

	}
}
