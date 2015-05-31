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
	int count = 0;
	int when = 0;
	GameObject D;
	GameObject NEW;
	List<string> ALL = new List<string>();
	List<string> Code = new List<string>();
	List<string> ch = new List<string> ();
	// Use this for initialization
	GetScheduleEvent mScheduleEvent;
	void Start () {
		ING = true;
		//mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "getdata"));
	//	NetMgr.GetScheduleMore (null,
		//                        UserMgr.UserInfo.teamSeq,
	   //                     mScheduleEvent);
	

	}
	void getdata(){
		whens = true;
		count = 0;
		char [] array;
			//	Debug.Log(mScheduleEvent.Response.data [q].startTime);
			ALL.Clear ();
		Code.Clear ();
			

		string aa;



				transform.GetChild (0).GetChild (1).GetComponent<UILabel> ().text = "경기 없음";
		
			for (int i =0; i<mScheduleEvent.Response.data.Count; i++) {
			array = mScheduleEvent.Response.data [i].startTime.ToCharArray ();
			ch.Clear ();
			
			for (int z = 6; z<8; z++) {
				
				ch.Add (array [z].ToString ());
				
				
			}
			aa = string.Join ("", ch.ToArray ());
				if (System.DateTime.Now.Day == int.Parse (aa)) {
				if(whens){
				when = i;
					whens = false;
				}
					count+=1;
					ALL.Add (mScheduleEvent.Response.data [i].extend [0].imageName);
					ALL.Add (mScheduleEvent.Response.data [i].extend [1].imageName);
					ALL.Add (mScheduleEvent.Response.data [i].interActive);
					ALL.Add ((mScheduleEvent.Response.data [i].extend [0].score).ToString () + " : " + (mScheduleEvent.Response.data [i].extend [1].score).ToString ());
				Code.Add ((mScheduleEvent.Response.data [i].extend [0].teamCode));
					//	Debug.Log((mScheduleEvent.Response.data[i].extend[0].score)+ " : " + (mScheduleEvent.Response.data[i].extend[1].score));
				//ALL.Clear ();
				
				ch.Clear ();
				
				
				for (int z = 0; z<8; z++) {
					
					ch.Add (array [z].ToString ());
					if (z == 3 || z == 5) {
						ch.Add (".");
					}
					
				}
				aa = string.Join ("", ch.ToArray ());
				Debug.Log(aa);
				
				
				transform.GetChild (0).GetChild (1).GetComponent<UILabel> ().text = aa + " 경기";
			}
			}
			//Debug.Log(" ALL.Count! :  " + ALL.Count);
		Debug.Log(count);
			
			D = transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).gameObject;
			if (0<ALL.Count) {
			if(Starts){
			setposition ();
			}
		
				for (int i = 0; i<count; i++) {	
				//Debug.Log(((i * 4) + 2)+" : " + ALL.Count);
					if((i * 4) + 2<ALL.Count){
					D.transform.parent.GetChild (i + 1).transform.GetChild (2).GetComponent<UILabel> ().text = ALL [(i * 4) + 2];
					D.transform.parent.GetChild (i + 1).transform.GetChild (4).GetComponent<UILabel> ().text = ALL [(i * 4) + 3];
					D.transform.parent.GetChild (i + 1).transform.GetChild (5).GetComponent<UILabel> ().text = Code [(i)];
				}
				}
			}



	}

	void setposition(){
		D = transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).gameObject;
	
		for (int i = 0; i<count; i++) {
			//Debug.Log("iiiiiiii");
			if ((i * 4) + 2 < ALL.Count) {
				NEW = (GameObject)Instantiate (D, new Vector2 (0, 0), D.transform.localRotation);
		
				NEW.transform.parent = D.transform.parent;

				NEW.transform.localScale = new Vector3 (1, 1, 1);
				NEW.transform.localPosition = new Vector2 (D.transform.localPosition.x, D.transform.localPosition.y - (gap * (i)));

				NEW.name = "Game" + (i + 1).ToString ();
				string imgName = UtilMgr.GetTeamEmblem (ALL [i * 4]);
				NEW.transform.GetChild (0).GetComponent<UISprite> ().spriteName = imgName;
				imgName = UtilMgr.GetTeamEmblem (ALL [(i * 4) + 1]);
				NEW.transform.GetChild (1).GetComponent<UISprite> ().spriteName = imgName;
				NEW.transform.GetChild (2).GetComponent<UILabel> ().text = ALL [(i * 4) + 2];
				NEW.transform.GetChild (4).GetComponent<UILabel> ().text = ALL [(i * 4) + 3];
				NEW.transform.GetChild (5).GetComponent<UILabel> ().text = Code [(i)];
				//NEW.transform.GetChild(2).GetChild(0).GetComponent<UILabel>().text = "19:34";
				NEW.gameObject.SetActive (true);
				//Debug.Log(NEW);
				//Debug.Log(NEW.transform.parent);

				transform.FindChild("BtnRight").FindChild("Scroll").GetComponent<UIScrollView>().ResetPosition();
			}
		}
		Starts = false;
	}
	public void onhit(){


		BntMenu = transform.parent.parent.parent.FindChild ("Top").GetChild (0).GetChild (1).gameObject;

		BntMenu.GetComponent<PlayMakerFSM> ().SendEvent ("Close Menu");
	
		if (ING) {
			if (B) {
				B = false;
				transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
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

			//	for (int i = 0; i<MaxGame; i++) {
					//Debug.Log("iiiiiiii");
			
				mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "getdata"));
					NetMgr.GetScheduleMore (null,
					                        UserMgr.UserInfo.teamSeq,
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
			ING = true;
		}
	}
	public void off(){
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
//		BntMenu = transform.parent.parent.parent.FindChild ("Top").FindChild("Panel").FindChild ("BtnMenu").gameObject;
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
	}
	public void mm(){
		nonoff = false;
		transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
	}
	public void pp(){
		nonoff = true;
	}
	public void ss(){
		transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
	}

	public bool IsOpen{
		get{return what;}
	}
	public void buttening(int i){

		//}
		//Debug.Log (i+" and "+a);

		UserMgr.Schedule = mScheduleEvent.Response.data [when+(i-1)];
		AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);
		
	}
}
