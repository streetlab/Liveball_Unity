using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMainMenuRight : MonoBehaviour {
	GameObject BntMenu;
	char [] array;
	public float MaxGame = 5;
	public float gap = 200;
	bool B = false;
	bool w = false;
	bool ING = true;
	bool LEFT = true;
	bool what = false;
	GameObject D;
	GameObject NEW;
	List<string> ALL = new List<string>();
	List<string> ch = new List<string> ();
	// Use this for initialization
	GetScheduleEvent mScheduleEvent;
	void Start () {
		ING = true;
		mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "getdata"));
		NetMgr.GetScheduleMore (null,
		                        UserMgr.UserInfo.teamSeq,
		                        mScheduleEvent);
	

	}
	void getdata(){
		array = mScheduleEvent.Response.data [0].startTime.ToString ().ToCharArray ();

		for (int z = 0; z<8; z++) {
		
			ch.Add (array [z].ToString ());
			if(z ==3||z==5||z==7){
				ch.Add (".");
			}
			
		}
		string aa = string.Join ("", ch.ToArray ());
		transform.GetChild (0).GetChild (0).GetChild (1).GetComponent<UILabel> ().text = aa+ " 경기";
	
		for (int i =0; i<mScheduleEvent.Response.data.Count; i++) {
//			Debug.Log(" I : "+ i + " : " 	+mScheduleEvent.Response.data[i].extend[0].teamName);
		//	Debug.Log(" I : "+ i + " : " 	+	mScheduleEvent.Response.data[i].extend[1].teamName);
		//	Debug.Log(" I : "+ i + " : " 	+	mScheduleEvent.Response.data[i].gameStatus);
		//	Debug.Log(" I : "+ i + " : " 	+	mScheduleEvent.Response.data[i].onairDay);
		
		//	Debug.Log(" I : "+ i + " : " 	+	mScheduleEvent.Response.data[i].startTime);

			ALL.Add(mScheduleEvent.Response.data[i].extend[0].imageName);
			ALL.Add(mScheduleEvent.Response.data[i].extend[1].imageName);
			ALL.Add(mScheduleEvent.Response.data[i].interActive);
			//ALL.Add(mScheduleEvent.Response.data[i].);
		}
		setposition ();
	}
	void setposition(){
		D = transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).gameObject;
		for (int i = 0; i<MaxGame; i++) {
		//	Debug.Log("iiiiiiii");
			NEW = (GameObject)Instantiate (D, new Vector2(0,0), D.transform.localRotation);
			NEW.transform.parent = D.transform.parent;

			NEW.transform.localScale = new Vector3(1,1,1);
			NEW.transform.localPosition = new Vector2(D.transform.localPosition.x,D.transform.localPosition.y-(gap*(i)));

			NEW.name = "Game"+(i+1).ToString();
			string imgName = UtilMgr.GetTeamEmblem(ALL[i*3]);
			NEW.transform.GetChild(0).GetComponent<UISprite>().spriteName = imgName;
			imgName = UtilMgr.GetTeamEmblem(ALL[(i*3)+1]);
			NEW.transform.GetChild(1).GetComponent<UISprite>().spriteName = imgName;
			NEW.transform.GetChild(2).GetComponent<UILabel>().text = ALL[(i*3)+2];
			//NEW.transform.GetChild(2).GetChild(0).GetComponent<UILabel>().text = "19:34";
			NEW.gameObject.SetActive(true);
		}
	}
	public void onhit(){
		BntMenu = transform.parent.parent.parent.FindChild ("Top").GetChild (0).GetChild (1).gameObject;

		BntMenu.GetComponent<PlayMakerFSM> ().SendEvent ("Close Menu");

		if (ING) {
			if (B) {
				B = false;
				transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
		
				LEFT = true;

			} else {
				B = true;
				transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
				LEFT = false;

			}
			if(what){
				what = false;
				StartCoroutine (rolling ());
			}else{
				what = true;
				StartCoroutine (rolling ());
			}
		}
	}
	IEnumerator rolling(){
		ING = false;
		for(int i = 0; i<5;i++){
			if(what){
		transform.GetChild(0).transform.localPosition -= (new Vector3(720/5,0,0));
			}else{
		transform.GetChild(0).transform.localPosition += (new Vector3(720/5,0,0));
			}
			yield return new WaitForSeconds(0.02f);
		
		}
		ING = true;
	}
	public void off(){
		if (B) {
			what = false;
			StartCoroutine (rolling ());
			w = true;
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
			}else{
			
				transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
			
				LEFT = false;
				w = false;
			}
			if (LEFT) {
				LEFT =false;
				transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));
			}else {
				LEFT =true;
				if(what){transform.GetChild (1).transform.localPosition = (new Vector3 (-720, 0, 0));}else{
				transform.GetChild (1).transform.localPosition = (new Vector3 (0, 0, 0));
			}
			}

		}


	}
}
