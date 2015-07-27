using UnityEngine;
using System.Collections;

public class Gift : MonoBehaviour {
	bool Check = false;
	public void Button(){
		if (Check) {
			Check = false;
			transform.localPosition = new Vector2(transform.localPosition.x,12f+55f);
			transform.FindChild("Arrow").localPosition = new Vector2(0,5);
			transform.FindChild("Arrow").localScale = new Vector2(1,1);
			transform.parent.FindChild("Scroll View").gameObject.SetActive(false);
			transform.parent.FindChild("Shadow").gameObject.SetActive(false);
		} else {
			Check = true;
			transform.localPosition = new Vector2(transform.localPosition.x,248f+55f);
			transform.FindChild("Arrow").localPosition = new Vector2(0,0);
			transform.FindChild("Arrow").localScale = new Vector2(1,-1);
			transform.parent.FindChild("Scroll View").GetComponent<UIPanel>().depth = 3;
			transform.parent.FindChild("Scroll View").gameObject.SetActive(true);
			transform.parent.FindChild("Shadow").gameObject.SetActive(true);
		}
	}
	public void Off(){
		Check = false;
		transform.localPosition = new Vector2(transform.localPosition.x,12f+55f);
		transform.FindChild("Arrow").localPosition = new Vector2(0,5);
		transform.FindChild("Arrow").localScale = new Vector2(1,1);
		transform.parent.FindChild("Scroll View").GetComponent<UIPanel>().depth = -1;
		transform.parent.FindChild("Scroll View").gameObject.SetActive(true);
		transform.parent.FindChild("Shadow").gameObject.SetActive(false);
	}
}
