using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMainMenuRight : MonoBehaviour {
	public GameObject BntMenu;
	
	public float MaxGame = 5;
	public float gap = 200;
	bool B = false;
	bool w = false;
	bool ING = true;
	bool LEFT = true;
	bool what = false;
	bool nonoff = true;
	bool Starts = true;
	bool whens = true;
	
	int when = 0;
	GameObject D;
	GameObject NEW;
	
	List<string> LImage = new List<string>();
	List<string> RImage = new List<string>();
	List<string> interActive = new List<string> ();
	List<string> Score = new List<string> ();
	List<string> Code = new List<string>();
	List<string> Day = new List<string>();
	List<int> Count = new List<int>();
	List<string> ch = new List<string> ();
	List<GameObject> News = new List<GameObject> ();
	// Use this for initialization
	GetScheduleEvent mScheduleEvent;

	List<int> dayandday = new List<int>();
	List<int> When = new List<int>();
	List<int> daycount = new List<int>();
	int c;
	void chacktoday(){
		dayandday.Clear ();
		When.Clear ();
		char [] array;
		string result;
		for (int i = 0; i<mScheduleEvent.Response.data.Count; i+=1) {
			array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
			for (int z = 6; z<array.Length; z++) {
				ch.Add (array [z].ToString ());
			}
			result = string.Join ("", ch.ToArray ());
			dayandday.Add(int.Parse(result));
			if(i>0){
				if(
					dayandday[i]!=dayandday[i-1]){
					When.Add(i);
				}
			}else{
				When.Add(i);
			}
			ch.Clear ();
		}
		daycount.Clear ();
		for (int i =0; i<When.Count; i++) {
			daycount.Add(0);
		}
		for (int i =0; i<dayandday.Count; i++) {
			for(int z = 0; z < When.Count;z++){
				if(dayandday[i] == dayandday[When[z]] ){
					daycount[z]+=1;
					
				}
			}	
		}

	}
	void Start () {
		ING = true;
		//mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "getdata"));
		//    NetMgr.GetScheduleMore (null,
		//                        UserMgr.UserInfo.teamSeq,
		//                     mScheduleEvent);
		
		
	}
	public void P(){
		if (c != 0) {
			nums += 1;
		}
		c = 2;
		Button ();
	}
	public void M(){
		c = 1;
		nums -=1;
		Button ();
	}
	void Button(){
		mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "Setdata"));
		    NetMgr.GetScheduleAll (
		                     mScheduleEvent);
	}
	int nums = 0;
	void numset(){
		if (nums > 4) {
			nums = 4;
		} else if (nums < -4) {
			nums = -4;
		}
	}

	void Setdata(){
		numset ();
		int days = 0;
		int a;
		for (a = 0; a < 7; a++) {
			for (int i = 0; i<When.Count; i++) {
				int day = System.DateTime.Now.Day+a;
				//Debug.Log("search for day ... " + day);
				if(day>31){
					day = day-31;
				}
				if (day == dayandday [When [i]]) {
					int v = i + nums;
					if (i + nums > When.Count - 1) {
						v = When.Count - 1;
						nums -= 1;
					} else if (i + nums < 0) {
						v = 0;
						nums += 1;
					}
					days = dayandday [When [v]];
					a = 100;
					break;
				}
			}
		}
		Debug.Log ("a is " + a);
		Debug.Log ("nums is " + nums);
		if (a < 100&&nums<0) {
		//	Debug.Log ("m m m m");
			for (a = 0; a > -7; a--) {
				for (int i = 0; i<When.Count; i++) {
					int day = System.DateTime.Now.Day+a;

					if(day>31){
						day = day-31;
					}
					Debug.Log("search for day ... " + day);
					if (day == dayandday [When [i]]) {
						int v = i + nums;
						if (i + nums > When.Count - 1) {
							v = When.Count - 1;
							nums -= 1;
						} else if (i + nums < 0) {
							v = 0;
							nums += 1;
						}
						days = dayandday [When [v+1]];
						a = -100;
						break;
					}
				}
			}
		}
		numset ();
		Starts = true;
		whens = true;
		char [] array;
		string aa;
		Count.Clear ();
		transform.FindChild ("BtnRight").FindChild("Panel").FindChild("Label").GetComponent<UILabel> ().text = "경기 없음";
		for (int i =0; i<mScheduleEvent.Response.data.Count; i++) {
			array = mScheduleEvent.Response.data [i].startTime.ToCharArray ();
			ch.Clear ();
			for (int z = 0; z<8; z++) {
			
				ch.Add (array [z].ToString ());
				if (z == 3 || z == 5) {
					ch.Add (".");
				}
			
			}
			aa = string.Join ("", ch.ToArray ());
		
			//Debug.Log (aa);
			ch.Clear ();
			for (int z = 6; z<8; z++) {
			
				ch.Add (array [z].ToString ());
			
			
			}
			string bb = string.Join ("", ch.ToArray ());
		
		
			if (days == int.Parse (bb)) {
				if(whens){
					when = i;
					whens = false;
				}

				Count.Add (i);

				transform.FindChild ("BtnRight").FindChild("Panel").FindChild("Label").GetComponent<UILabel> ().text = aa + " 경기";
			}
		}
		D = transform.GetChild (0).GetChild (0).GetChild (0).gameObject;
		for (int i = 1; i < D.transform.childCount; i++) {
			Destroy(D.transform.GetChild(i).gameObject);
		}
		if (0 < Count.Count) {
			if (Starts) {
				setposition ();
			}
		}
	}
	void getdata(){
		c = 1;
		D = transform.GetChild (0).GetChild (0).GetChild (0).gameObject;
		for (int i = 1; i < D.transform.childCount; i++) {
			Destroy(D.transform.GetChild(i).gameObject);
		}
		chacktoday ();
		whens = true;
		
		char [] array;
		//    Debug.Log(mScheduleEvent.Response.data [q].startTime);
		LImage.Clear ();
		RImage.Clear ();
		interActive.Clear ();
		Score.Clear ();
		Code.Clear ();
		Day.Clear ();
		
		string aa;
		
		
		
		transform.FindChild ("BtnRight").FindChild("Panel").FindChild("Label").GetComponent<UILabel> ().text = "경기 없음";
		
		for (int i =0; i<mScheduleEvent.Response.data.Count; i++) {
			LImage.Add (mScheduleEvent.Response.data [i].extend [0].imageName);
			RImage.Add (mScheduleEvent.Response.data [i].extend [1].imageName);
			interActive.Add (mScheduleEvent.Response.data [i].interActive);
			Score.Add ((mScheduleEvent.Response.data [i].extend [0].score).ToString () + " : " + (mScheduleEvent.Response.data [i].extend [1].score).ToString ());
			Code.Add ((mScheduleEvent.Response.data [i].extend [0].teamCode));
			array = mScheduleEvent.Response.data [i].startTime.ToCharArray ();
			ch.Clear ();
			for (int z = 0; z<8; z++) {
				
				ch.Add (array [z].ToString ());
				if (z == 3 || z == 5) {
					ch.Add (".");
				}
				
			}
			aa = string.Join ("", ch.ToArray ());
		
		//	Debug.Log(aa);
			ch.Clear ();
			for (int z = 6; z<8; z++) {
				
				ch.Add (array [z].ToString ());
				
				
			}
			string bb = string.Join ("", ch.ToArray ());
			//Day.Add (aa);

			if (System.DateTime.Now.Day == int.Parse (bb)) {
	
				if(whens){
					when = i;
					whens = false;
				}
				Count.Add (i);
				//    Debug.Log((mScheduleEvent.Response.data[i].extend[0].score)+ " : " + (mScheduleEvent.Response.data[i].extend[1].score));
				//ALL.Clear ();
				
				transform.FindChild ("BtnRight").FindChild("Panel").FindChild("Label").GetComponent<UILabel> ().text = aa + " 경기";
			}
		}
		//Debug.Log(" ALL.Count! :  " + ALL.Count);
		
		
		D = transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).gameObject;
		if (0<Count.Count) {
			if(Starts){
				setposition ();
			}
			
//			for (int i = 0; i<Count.Count; i++) {    
//				//Debug.Log(((i * 4) + 2)+" : " + ALL.Count);
//				
//				D.transform.parent.GetChild (i + 1).transform.GetChild (2).GetComponent<UILabel> ().text = interActive [Count[i]];
//				D.transform.parent.GetChild (i + 1).transform.GetChild (4).GetComponent<UILabel> ().text = Score [Count[i]];
//				D.transform.parent.GetChild (i + 1).transform.GetChild (5).GetComponent<UILabel> ().text = Code [Count[i]];
//				
//			}
		}
		
		
		if (transform.FindChild ("BtnRight").FindChild ("Panel").FindChild ("Label").GetComponent<UILabel> ().text == "경기 없음") {
			c = 0;
		}
	}
	
	void setposition(){
		News.Clear ();
		D = transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).gameObject;
		
		for (int i = 0; i<Count.Count; i++) {
			//Debug.Log("iiiiiiii");
		
				NEW = (GameObject)Instantiate (D, new Vector2 (0, 0), D.transform.localRotation);
				
				NEW.transform.parent = D.transform.parent;
				
				NEW.transform.localScale = new Vector3 (1, 1, 1);
				NEW.transform.localPosition = new Vector2 (D.transform.localPosition.x, D.transform.localPosition.y - (gap * (i)));
				
				NEW.name = "Game" + (i + 1).ToString ();
				string imgName = UtilMgr.GetTeamEmblem (LImage[Count[i]]);
				NEW.transform.GetChild (0).GetComponent<UISprite> ().spriteName = imgName;
			imgName = UtilMgr.GetTeamEmblem (RImage[Count[i]]);
				NEW.transform.GetChild (1).GetComponent<UISprite> ().spriteName = imgName;
			NEW.transform.GetChild (2).GetComponent<UILabel> ().text = interActive [Count[i]];
			NEW.transform.GetChild (4).GetComponent<UILabel> ().text = Score [Count[i]];
			NEW.transform.GetChild (5).GetComponent<UILabel> ().text = Code [Count[i]];
				//NEW.transform.GetChild(2).GetChild(0).GetComponent<UILabel>().text = "19:34";
				NEW.gameObject.SetActive (true);
				//Debug.Log(NEW);
				//Debug.Log(NEW.transform.parent);
			News.Add(NEW);
				transform.FindChild("BtnRight").FindChild("Scroll").GetComponent<UIScrollView>().ResetPosition();

		}
		Starts = false;
	}
	public void onhit(){
		UtilMgr.AddBackEvent(new EventDelegate(this, "BackPressed"));
//		if(transform.parent.parent.parent.name=="UI Root"){
//			BntMenu = transform.parent.parent.parent.FindChild ("Top").GetChild (0).GetChild (1).gameObject;
//		}else if(transform.parent.parent.parent.parent.name=="UI Root"){
//			BntMenu = transform.parent.parent.parent.parent.FindChild ("Top").GetChild (0).GetChild (1).gameObject;
//		}
		BntMenu.GetComponent<PlayMakerFSM> ().SendEvent ("Close Menu");
		
		if (ING) {
			if (B) {
				B = false;
				transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
				transform.FindChild ("SprBack").GetComponent<UIButton>().enabled =false;
				w = false;
				LEFT = true;
				
			} else {
				B = true;
				transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
				LEFT = false;
				
			}
			if(what){
				what = false;
				ING = false;
				StartCoroutine (rolling ());
			}else{
				what = true;
				ING = false;
				D = transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).gameObject;
				
				//    for (int i = 0; i<MaxGame; i++) {
				//Debug.Log("iiiiiiii");
				
				mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "getdata"));
				NetMgr.GetScheduleAll (
					mScheduleEvent);
				
				//NEW.transform.GetChild(2).GetChild(0).GetComponent<UILabel>().text = "19:34";
				
				//Debug.Log(NEW);
				//Debug.Log(NEW.transform.parent);
				//}
				
				
				
				StartCoroutine (rolling ());
			}
		}
	}
	IEnumerator rolling(){
		if (!ING) {
			for (int i = 0; i<5; i++) {
				if (what) {
					transform.GetChild (0).transform.localPosition -= (new Vector3 (720 / 5, 0, 0));
				} else {
					transform.GetChild (0).transform.localPosition += (new Vector3 (720 / 5, 0, 0));
				}
				yield return new WaitForSeconds (0.02f);
				
			}
			if(what){
				transform.FindChild ("SprBack").GetComponent<UIButton>().enabled =true;
			}
			ING = true;
		}
	}

	public void BackPressed(){
		UtilMgr.RemoveAllBackEvents();
		Debug.Log ("Button!!!!");
		ALLBack ();
	}

	public void off(){

		UtilMgr.AddBackEvent(new EventDelegate(this, "BackPressed"));

		if (nonoff) {
			
			if (B) {
				what = false;
				ING  = false;
				StartCoroutine (rolling ());
				w = false;
				B = false;
			}
			Debug.Log ("off");
			if (ING) {
				
				Debug.Log ("ing");
				if (!w) {
					Debug.Log ("!B");
					w = true;
					Debug.Log (transform.GetChild (1));
					transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
					
					LEFT = true;
				} else {
					
					transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
					
					LEFT = false;
					w = false;
				}
				if (LEFT) {
					LEFT = false;
					transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
				} else {
					
					if (what) {
						transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
					} else {
						if (B) {
							transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
						} else {
							transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
						}
					}
					LEFT = true;
				}
				
			}
		}
		
	}
	public void ALLBack(){
		//        BntMenu = transform.parent.parent.parent.FindChild ("Top").FindChild("Panel").FindChild ("BtnMenu").gameObject;
		Debug.Log (B);
		BntMenu.GetComponent<PlayMakerFSM> ().SendEvent ("Close Menu");
		if (B) {
			what = false;
			ING  = false;
			StartCoroutine (rolling ());
			w = true;
			B = false;
		}
		transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
		transform.FindChild ("SprBack").GetComponent<UIButton>().enabled =false;
	}
	public void mm(){
		nonoff = false;
		transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
	}
	public void pp(){
		nonoff = true;
		transform.FindChild ("SprBack").GetComponent<UIButton>().enabled =true;
	}
	public void ss(){
		transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
		transform.FindChild ("SprBack").GetComponent<UIButton>().enabled =false;
	}
	
	public bool IsOpen{
		get{return what;}
	}
	public void buttening(int i){
		
		//}
		//Debug.Log (i+" and "+a);
		
		UserMgr.Schedule = mScheduleEvent.Response.data [when+(i-1)];
		Debug.Log ("UserMgr.Schedule.gameStatus : " + UserMgr.Schedule.gameStatus);
		Debug.Log ("ScheduleInfo.GAME_READY : " + ScheduleInfo.GAME_READY);
		if (UserMgr.Schedule.gameStatus == ScheduleInfo.GAME_READY) {
			//non
			ScriptMainTop.LandingState =1;
			AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);    
		} else {
			//Startgame();
			ScriptMainTop.LandingState =2;
			AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);    
		}
		
	}
}