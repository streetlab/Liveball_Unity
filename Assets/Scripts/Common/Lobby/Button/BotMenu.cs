using UnityEngine;
using System.Collections;

public class BotMenu : MonoBehaviour {
	string[] S = {"Home","Ball","Challenge","Post","---"};
	public GameObject mScroll;
	// Use this for initialization
	public void Button(){


		Debug.Log (GetIndex (this.name));
		switch (GetIndex (this.name)) {
		case 0:
			Off();
			Debug.Log("Main On");
			mScroll.transform.FindChild("Main").gameObject.SetActive(true);
			break;
		case 1:
			Off();
			mScroll.transform.FindChild("GameInfo").gameObject.SetActive(true);
			mScroll.transform.FindChild("GameInfo").GetComponent<ScriptGameInfo>().Init();
			break;
		case 2:
			//AllOff();
			if(transform.FindChild("Scroll View").gameObject.activeSelf){
				transform.FindChild("Scroll View").gameObject.SetActive(false);
			}else{
				transform.FindChild("Scroll View").gameObject.SetActive(true);
			}

			break;
		case 3:
			break;
		case 4:

			//Debug.Log("4");
			PositionCheck();
			GetComponent<UIButton>().enabled = true;
			break;
		case 5:
		//	Debug.Log("NON");
			ScrollViewOff();

			break;
		}
	}
	void Off(){
		mScroll.transform.FindChild("Main").gameObject.SetActive(false);
		mScroll.transform.FindChild("GameInfo").gameObject.SetActive(false);
		mScroll.transform.FindChild("Bot").FindChild("Home").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("Ball").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("Challenge").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("---").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("Post").GetComponent<UIButton>().enabled = true;
		
		GetComponent<UIButton>().enabled = false;
	}
	public void ChallengeClose(){
		transform.FindChild("Scroll View").gameObject.SetActive(false);
	}
	int GetIndex(string name){
		int i;
		for (i = 0; i<S.Length; i++) {
			if(name == S[i]){
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
		float Y = transform.root.FindChild ("Camera").localPosition.y;
		transform.root.FindChild("Scroll").FindChild ("RightMenu").GetComponent<BoxCollider2D> ().enabled = true;
		for (int i = 0; i<num; i++) {
			transform.root.FindChild("Camera").localPosition+= new Vector3(550f/num,0);
			yield return new WaitForSeconds(WatiTime);
			if(transform.root.FindChild("Camera").localPosition.x>=550){


				break;
			}
		}
		transform.root.FindChild("Camera").localPosition = new Vector3(550,Y);
	}
	IEnumerator LeftMoveCamera(){ 
		float Y = transform.root.FindChild ("Camera").localPosition.y;
		transform.root.FindChild("Scroll").FindChild ("RightMenu").GetComponent<BoxCollider2D> ().enabled = false;
		for (int i = 0; i<num; i++) {
			transform.root.FindChild("Camera").localPosition-= new Vector3(550f/num,0);
			yield return new WaitForSeconds(WatiTime);
			if(transform.root.FindChild("Camera").localPosition.x<=0){

				break;
			}
		}
		transform.root.FindChild("Camera").localPosition = new Vector3(0,Y);
	}
	void AllOff(){
		transform.root.FindChild ("Scroll").FindChild ("Bot").FindChild ("Challenge").FindChild ("Scroll View").gameObject.SetActive (false);
		transform.root.FindChild ("Scroll").FindChild ("Bot").FindChild ("BtnPost").FindChild ("TF_Post").gameObject.SetActive (false);
	}
}
