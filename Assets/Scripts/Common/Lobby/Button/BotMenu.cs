using UnityEngine;
using System.Collections;

public class BotMenu : MonoBehaviour {

	// Use this for initialization
	public void Button(){
		switch (GetIndex (this.name)) {
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:

			//Debug.Log("4");
			PositionCheck();
			break;
		case 5:
		//	Debug.Log("NON");
			ScrollViewOff();

			break;
		}
	}
	int GetIndex(string name){
		int i;
		for (i = 0; i<transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyBotCommander>().mBotMenuName.Length; i++) {
			if(name == transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyBotCommander>().mBotMenuName[i]){
				break;
			}
		}

		return i;
	}
	void ScrollViewOff(){
		//transform.root.FindChild("Main").FindChild("Gift").FindChild("Scroll View").gameObject.SetActive(false);
	}

    void PositionCheck(){
		ScrollViewOff ();
		if (transform.root.FindChild ("Camera").localPosition.x == 0) {
			StartCoroutine(RightMoveCamera());
		} else if (transform.root.FindChild ("Camera").localPosition.x == 550) {
			StartCoroutine(LeftMoveCamera());
		}
	}
	int num = 5;
	float WatiTime = 0.02f;
	IEnumerator RightMoveCamera(){
		transform.root.FindChild("Scroll").FindChild ("RightMenu").GetComponent<BoxCollider2D> ().enabled = true;
		for (int i = 0; i<num; i++) {
			transform.root.FindChild("Camera").localPosition+= new Vector3(550f/num,0);
			yield return new WaitForSeconds(WatiTime);
			if(transform.root.FindChild("Camera").localPosition.x>=550){

				break;
			}
		}
		transform.root.FindChild("Camera").localPosition = new Vector3(550,0);
	}
	IEnumerator LeftMoveCamera(){ 
		transform.root.FindChild("Scroll").FindChild ("RightMenu").GetComponent<BoxCollider2D> ().enabled = false;
		for (int i = 0; i<num; i++) {
			transform.root.FindChild("Camera").localPosition-= new Vector3(550f/num,0);
			yield return new WaitForSeconds(WatiTime);
			if(transform.root.FindChild("Camera").localPosition.x<=0){

				break;
			}
		}
		transform.root.FindChild("Camera").localPosition = new Vector3(0,0);
	}
}
