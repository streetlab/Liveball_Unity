using UnityEngine;
using System.Collections;

public class GachaAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Effeter ());
	}
	IEnumerator Effeter(){
		float i = 1;
		while (i<11) {
			i++;
			transform.FindChild("BG").GetComponent<UISprite>().color = new Color(1,1,1,((i/10)*166)/255);
			yield return new WaitForSeconds(0.02f);
		}
		transform.FindChild ("Swing").gameObject.SetActive (true);
	}


}
