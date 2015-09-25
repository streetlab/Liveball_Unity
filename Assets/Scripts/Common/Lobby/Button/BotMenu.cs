using UnityEngine;
using System.Collections;

public class BotMenu : MonoBehaviour {
	public enum ActivityName{
		None,
		Home,
		MatchInfo,
		Store,
		Post,
		Setting,
		Ranking,
		Inventory,
		Menu,
		etc
	}

//	string mPreSelectedName = "";
//	static string mReservedName = null;
	public static ActivityName mActName = ActivityName.Home;
	static Color COLOR_UNSELECTED = new Color(114f/255f, 107f/255f, 113f/255f);
	static Color COLOR_SELECTED = new Color(1f, 1f, 1f);

	void Start(){
//		if(mReservedName == null){
//			Debug.Log("Launched");
//			mReservedName = "";
//			SetHighlight(ActivityName.Home);
//		} else if(mReservedName.Equals(this.name)){
//			Debug.Log("name is "+this.name);
//			Debug.Log("ActName is "+mActName);
//			mReservedName = "";
//			if(this.name.Equals("Home"))
//				SetHighlight(ActivityName.Home);
//			else
//				Button();
//		}
		if(Application.loadedLevelName.Equals("SceneLobby"))
			SetHighlight(ActivityName.Home);
		else if(Application.loadedLevelName.Equals("SceneMain 1"))
			SetHighlight(ActivityName.None);
	}

	string[] S = {"Home","Ball","Challenge","Post","---"};
	public GameObject mScroll;
	// Use this for initialization
	//하단메뉴 버튼
	public void Button(){
		Debug.Log (this.name);
//		if(Application.loadedLevelName.Equals("SceneMain 1")){
//			mReservedName = this.name;
//			AutoFade.LoadLevel ("SceneLobby", 0f, 1f);
//			return;
//		}

		switch (this.name) {
		case "Home": //홈
			if(mActName.Equals(ActivityName.Home))
				return;

			if(mScroll.transform.FindChild("Main")!=null){
				AllOff();
				Off();
				mScroll.transform.FindChild("GameInfo").gameObject.SetActive(false);
				mScroll.transform.FindChild("Main").gameObject.SetActive(true);
			}else{
				AutoFade.LoadLevel ("SceneLobby", 0f, 1f);
			}

			SetHighlight(ActivityName.Home);
			break;
		case "Ball": //경기경보
			if(mActName.Equals(ActivityName.MatchInfo))
				return;

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
//				AllOff();
//				Off();
				mScroll.transform.FindChild("GameInfo").gameObject.SetActive(false);
				mScroll.transform.FindChild("GameInfo").gameObject.SetActive(true);
				mScroll.transform.FindChild("GameInfo").GetComponent<ScriptGameInfo>().Init();

			}

			SetHighlight(ActivityName.MatchInfo);
			break;
		case "Challenge": //Store
			if(mActName.Equals(ActivityName.Store))
				return;

			AllOff();
			
			transform.root.FindChild ("TF_Items").gameObject.SetActive(true);
//			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);
//			transform.root.FindChild("Scroll").FindChild ("RightMenu").GetComponent<BoxCollider2D> ().enabled = false;
//			transform.root.FindChild("Scroll").FindChild ("RightMenu").FindChild("Shadow").GetComponent<BoxCollider2D> ().enabled = false;
//			mScroll.transform.FindChild("Bot").FindChild("Home").GetComponent<UIButton>().enabled = true;
//			mScroll.transform.FindChild("Bot").FindChild("Ball").GetComponent<UIButton>().enabled = true;
//			mScroll.transform.FindChild("Bot").FindChild("Challenge").GetComponent<UIButton>().enabled = true;
//			mScroll.transform.FindChild("Bot").FindChild("---").GetComponent<UIButton>().enabled = true;
//			mScroll.transform.FindChild("Bot").FindChild("Post").GetComponent<UIButton>().enabled = true;
//			GetComponent<UIButton>().enabled = false;
//			if(transform.FindChild("Scroll View").gameObject.activeSelf){
//				transform.FindChild("Scroll View").gameObject.SetActive(false);
//			}else{
//				transform.FindChild("Scroll View").gameObject.SetActive(true);
//			}

			SetHighlight(ActivityName.Store);
			break;
		case "BtnPost": //Post
			if(mActName.Equals(ActivityName.Post))
				return;

			AllOff();
			GetComponent<PostButton>().on();

			SetHighlight(ActivityName.Post);

			break;
		case "---": //우측메뉴
			//Debug.Log("4");
			PositionCheck();
//			GetComponent<UIButton>().enabled = true;
			break;
//		case 5:
//		//	Debug.Log("NON");
//			ScrollViewOff();
//
//			break;
		}
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

//		mScroll.transform.FindChild("Bot").FindChild("Home").GetComponent<UIButton>().enabled = true;
//		mScroll.transform.FindChild("Bot").FindChild("Ball").GetComponent<UIButton>().enabled = true;
//		mScroll.transform.FindChild("Bot").FindChild("Challenge").GetComponent<UIButton>().enabled = true;
//		mScroll.transform.FindChild("Bot").FindChild("---").GetComponent<UIButton>().enabled = true;
//		mScroll.transform.FindChild("Bot").FindChild("Post").GetComponent<UIButton>().enabled = true;
//		transform.root.FindChild ("Ranking").gameObject.SetActive (false);
//		
//		GetComponent<UIButton>().enabled = false;
	}
	void ScrollViewOff(){
		//transform.root.FindChild("Main").FindChild("Gift").FindChild("Scroll View").gameObject.SetActive(false);
	}

	//카메라 애니메이션 관련
    void PositionCheck(){
		ScrollViewOff ();
		if (transform.root.FindChild ("Camera").localPosition.x == 0) {
			SetHighlightWith(ActivityName.Menu);
			StartCoroutine(RightMoveCamera());
		} else if (transform.root.FindChild ("Camera").localPosition.x == 550) {
			UnsetHighlight(ActivityName.Menu);
			StartCoroutine(LeftMoveCamera());
		}
	}
	int num = 5;
	float WatiTime = 0.02f;
	//카메라 우측으로 이동
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
	//카메라 좌측으로 이동
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

		transform.root.FindChild("TF_Items").gameObject.SetActive(false);
		transform.root.FindChild("Setting").gameObject.SetActive(false);
		transform.root.FindChild("Item").gameObject.SetActive(false);
		transform.root.FindChild("Ranking").gameObject.SetActive(false);
		transform.root.FindChild("RankReward").gameObject.SetActive(false);
	}

	public void UnsetHighlight(ActivityName name){
		switch(name){
		case ActivityName.Home:
			transform.parent.FindChild("Home").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
			transform.parent.FindChild("Home").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;
			break;
		case ActivityName.MatchInfo:
			transform.parent.FindChild("Ball").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
			transform.parent.FindChild("Ball").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;
			break;
		case ActivityName.Store:
			transform.parent.FindChild("Challenge").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
			transform.parent.FindChild("Challenge").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;
			break;
		case ActivityName.Post:
			transform.parent.FindChild("BtnPost").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
			transform.parent.FindChild("BtnPost").FindChild("Background").GetComponent<UISprite>().color = COLOR_UNSELECTED;
			break;
		case ActivityName.Menu:
			transform.parent.FindChild("---").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
			transform.parent.FindChild("---").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;
			break;
		}
	}

	public void SetHighlightWith(ActivityName name){		
		switch(name){
		case ActivityName.Home:
			transform.parent.FindChild("Home").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("Home").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.MatchInfo:
			transform.parent.FindChild("Ball").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("Ball").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.Store:
			transform.parent.FindChild("Challenge").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("Challenge").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.Post:
			transform.parent.FindChild("BtnPost").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("BtnPost").FindChild("Background").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.Menu:
			transform.parent.FindChild("---").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("---").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		}
	}

	public void SetHighlight(ActivityName name){
		mActName = name;
		transform.parent.FindChild("Home").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("Home").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("Ball").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("Ball").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("Challenge").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("Challenge").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("BtnPost").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("BtnPost").FindChild("Background").GetComponent<UISprite>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("---").FindChild("Label").GetComponent<UILabel>().color = COLOR_UNSELECTED;
		transform.parent.FindChild("---").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_UNSELECTED;

		switch(mActName){
		case ActivityName.Home:
			transform.parent.FindChild("Home").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("Home").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.MatchInfo:
			transform.parent.FindChild("Ball").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("Ball").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.Store:
			transform.parent.FindChild("Challenge").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("Challenge").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.Post:
			transform.parent.FindChild("BtnPost").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("BtnPost").FindChild("Background").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		case ActivityName.Menu:
			transform.parent.FindChild("---").FindChild("Label").GetComponent<UILabel>().color = COLOR_SELECTED;
			transform.parent.FindChild("---").FindChild("Sprite").GetComponent<UISprite>().color = COLOR_SELECTED;
			break;
		}
	}
}
