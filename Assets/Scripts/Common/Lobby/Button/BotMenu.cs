using UnityEngine;
using System.Collections;

public class BotMenu : MonoBehaviour {
	string[] S = {"Home","Ball","Challenge","Post","---"};
	public GameObject mScroll;
	// Use this for initialization
	//하단메뉴 버튼
	public void Button(){


		Debug.Log (GetIndex (this.name));
		switch (GetIndex (this.name)) {
		case 0:
		
			Debug.Log("Main On");
			if(mScroll.transform.FindChild("Main")!=null){
				AllOff();
				Off();
				mScroll.transform.FindChild("GameInfo").gameObject.SetActive(false);
			mScroll.transform.FindChild("Main").gameObject.SetActive(true);
			}else{
				AutoFade.LoadLevel ("SceneLobby", 0f, 1f);
			}
			break;
		case 1:
			//ScriptMainTop.OpenBettingCheck = false;
			AllOff();
			Off();
			if(Application.loadedLevelName.Equals ("SceneMain 1")){
			if(mScroll.transform.FindChild("GameInfo").gameObject.activeSelf){
				mScroll.transform.FindChild("GameInfo").gameObject.SetActive(false);
				if(Application.loadedLevelName.Equals ("SceneMain 1")){
					transform.root.FindChild("Scroll").FindChild("ContestIn").localPosition = new Vector3(0,0,0);
				}
			}else{

				if(Application.loadedLevelName.Equals ("SceneMain 1")){
					transform.root.FindChild("Scroll").FindChild("ContestIn").localPosition = new Vector3(-720,0,0);
				}
			mScroll.transform.FindChild("GameInfo").gameObject.SetActive(true);
			mScroll.transform.FindChild("GameInfo").GetComponent<ScriptGameInfo>().Init();
				}
			}else{
				AllOff();
				Off();
				mScroll.transform.FindChild("GameInfo").gameObject.SetActive(false);
				mScroll.transform.FindChild("GameInfo").gameObject.SetActive(true);
				mScroll.transform.FindChild("GameInfo").GetComponent<ScriptGameInfo>().Init();

			}

			GetComponent<UIButton>().enabled = true;
			break;
		case 2:
			AllOff();
			//Off();

			mScroll.transform.FindChild("Bot").FindChild("Home").GetComponent<UIButton>().enabled = true;
			mScroll.transform.FindChild("Bot").FindChild("Ball").GetComponent<UIButton>().enabled = true;
			mScroll.transform.FindChild("Bot").FindChild("Challenge").GetComponent<UIButton>().enabled = true;
			mScroll.transform.FindChild("Bot").FindChild("---").GetComponent<UIButton>().enabled = true;
			mScroll.transform.FindChild("Bot").FindChild("Post").GetComponent<UIButton>().enabled = true;
			GetComponent<UIButton>().enabled = false;
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
	public void ChallengeClose(){
		transform.FindChild("Scroll View").gameObject.SetActive(false);
	}
	//오브젝트 이름을 인티저로 변환
	int GetIndex(string name){
		int i;
		for (i = 0; i<S.Length; i++) {
			if(name == S[i]){
				break;
			}
		}

		return i;
	}

	void Off(){
		if (mScroll.transform.FindChild ("Main") != null) {
			mScroll.transform.FindChild ("Main").gameObject.SetActive (false);
		}

		mScroll.transform.FindChild("Bot").FindChild("Home").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("Ball").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("Challenge").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("---").GetComponent<UIButton>().enabled = true;
		mScroll.transform.FindChild("Bot").FindChild("Post").GetComponent<UIButton>().enabled = true;
		transform.root.FindChild ("Ranking").gameObject.SetActive (false);
		
		GetComponent<UIButton>().enabled = false;
	}
	void ScrollViewOff(){
		//transform.root.FindChild("Main").FindChild("Gift").FindChild("Scroll View").gameObject.SetActive(false);
	}

	//카메라 애니메이션 관련
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
		transform.root.FindChild("Scroll").FindChild ("RightMenu").FindChild("Shadow").GetComponent<BoxCollider2D> ().enabled = true;
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
		transform.root.FindChild("Scroll").FindChild ("RightMenu").FindChild("Shadow").GetComponent<BoxCollider2D> ().enabled = false;
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
