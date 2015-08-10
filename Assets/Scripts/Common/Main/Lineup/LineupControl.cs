using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LineupControl : MonoBehaviour {
	public GameObject T1,T2,T11,T22;
	
	GameObject ScrollView,S,P,H,temp;
	GetLineupEvent mlineupEvent;
	bool buttens = true;
	bool fist = true;
	
	bool onepit = true;
	List<string> lineup = new List<string>();
	List<string> pit = new List<string>();
	List<string> hit = new List<string>();
	
	
	List<GameObject> Glineup = new List<GameObject>();
	List<GameObject> Gpit = new List<GameObject>();
	List<GameObject> Ghit = new List<GameObject>();
	
	
	List<GameObject> Gpit2 = new List<GameObject>();
	List<GameObject> Ghit2 = new List<GameObject>();
	public float gap = 122;
	float GsetrectY = 116;
	
	Vector3 pits,hits,Cpits,Chits;
	
	WWW www;
	void Start(){
//		Reset ();
	}
	public void Reset(){
		T1.GetComponent<UIButton> ().isEnabled = false;
//		T2.GetComponent<UIButton> ().isEnabled = true;
		
//		T11.GetComponent<UIButton> ().isEnabled = false;
//		T22.GetComponent<UIButton> ().isEnabled = true;
		T1.SetActive(true);
		T2.SetActive(false);
	}
	// Use this for initialization
	public void view (string teamCode) {
		transform.FindChild ("Scroll View").gameObject.SetActive(false);
		transform.FindChild ("Scroll View 1").gameObject.SetActive(false);
		//if (!transform.FindChild ("Scroll View").gameObject.activeSelf && !transform.FindChild ("Scroll View 1").gameObject.activeSelf) {
		
		//}

		ScrollView = transform.FindChild ("Scroll View").gameObject;
		//ScrollView.GetComponent<UIScrollView> ().ResetPosition ();
		Reset ();
		if (fist) {
			fist = false;
			pits = ScrollView.transform.GetChild (1).GetChild (0).GetChild(1).transform.localPosition;
			hits = ScrollView.transform.GetChild (2).GetChild (0).GetChild(1).transform.localPosition;
			Cpits = ScrollView.transform.GetChild (1).GetChild (0).GetChild(0).transform.localPosition;
			Chits = ScrollView.transform.GetChild (2).GetChild (0).GetChild(0).transform.localPosition;
			//Debug.Log("Cpits : " + ScrollView.transform.GetChild (2).GetChild (0).GetChild(1).GetChild(0).transform.localPosition);
			//Debug.Log("Cpits : " + Cpits);
			
		}
		lineup.Clear ();
		pit.Clear ();
		hit.Clear ();
		buttens = true;
		for (int i = 0; i<Gpit.Count; i++) {
			Destroy(Gpit[i].gameObject);
		}
		for (int i = 0; i<Ghit.Count; i++) {
			Destroy(Ghit[i].gameObject);
		}
		for (int i = 0; i<Gpit2.Count; i++) {
			Destroy(Gpit2[i].gameObject);
		}
		for (int i = 0; i<Ghit2.Count; i++) {
			Destroy(Ghit2[i].gameObject);
		}
		Gpit.Clear ();
		Ghit.Clear ();
		Gpit2.Clear ();
		Ghit2.Clear ();
		//Debug.Log ("what teamcode : " + UserMgr.Schedule.extend[0].teamCode);
//		if (UserMgr.Schedule != null) {
			mlineupEvent = new GetLineupEvent (new EventDelegate (this, "setarrray"));
			NetMgr.GetLineup (teamCode, mlineupEvent);

			T1.GetComponent<UILabel> ().text = UtilMgr.GetTeamName(teamCode);
//			T2.GetComponent<UILabel> ().text = UserMgr.Schedule.extend [1].teamName;
//			T11.GetComponent<UILabel> ().text = UserMgr.Schedule.extend [0].teamName;
//			T22.GetComponent<UILabel> ().text = UserMgr.Schedule.extend [1].teamName;
//		}
		
	}
	
	void setarrray(){
		gameObject.SetActive(true);
		
		UserMgr.AwayLineup = mlineupEvent.Response.data;
		
		getdata ();
		
	}
	void getdata(){
		S = ScrollView.transform.GetChild (0).GetChild (0).FindChild ("S").gameObject;
		onepit = true;
		if (UserMgr.AwayLineup.lineup.Count > 0) {
//			transform.FindChild ("non").gameObject.SetActive(false);
			transform.FindChild ("Scroll View").gameObject.SetActive(true);
			transform.FindChild ("Scroll View 1").gameObject.SetActive(true);
//			for (int i = 0; i<S.transform.childCount; i++) {
//				S.transform.GetChild (i).gameObject.SetActive (false);
//			}
			
			for (int i =0; i<UserMgr.AwayLineup.lineup.Count; i++) {
				//Debug.Log("Image pach! : " + UserMgr.AwayLineup.lineup [i].imagePath+UserMgr.AwayLineup.lineup [i].imageName);
				//Debug.Log(UserMgr.AwayLineup.lineup [i].playerName + " : " + UserMgr.AwayLineup.lineup [i].lineup );
				switch (UserMgr.AwayLineup.lineup [i].lineup) {
					
				case 1:
					if(onepit){
						onepit = false;
						S.transform.GetChild (0).gameObject.SetActive (true);
						//Debug.Log(Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
						www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
						StartCoroutine (GetImage (www, S.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
						//Debug.Log(UserMgr.AwayLineup.lineup [i].playerName);
						S.transform.GetChild (0).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					}
					break;
				case 2:
					S.transform.GetChild (1).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (1).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (1).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					break;
				case 3:
					S.transform.GetChild (2).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (2).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (2).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					break;
				case 4:
					S.transform.GetChild (3).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (3).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (3).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					break;
				case 6:
					S.transform.GetChild (4).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (4).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (4).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					
					break;
				case 7:
					S.transform.GetChild (5).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (5).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (5).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					
					break;
				case 5:
					S.transform.GetChild (6).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (6).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (6).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					
					break;
				case 8:
					S.transform.GetChild (7).gameObject.SetActive (true);
					Debug.Log ("what log : " + UserMgr.AwayLineup.lineup [i].playerName);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (7).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (7).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					
					break;
				case 9:
					S.transform.GetChild (8).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (8).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (8).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					
					break;
				case 10:
					Debug.Log ("???");
					S.transform.GetChild (9).gameObject.SetActive (true);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.lineup [i].imagePath + UserMgr.AwayLineup.lineup [i].imageName);
					StartCoroutine (GetImage (www, S.transform.GetChild (9).GetChild (0).GetChild (0).gameObject));
					S.transform.GetChild (9).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.lineup [i].playerName;
					
					break;
					
				}
			}
			
			S = ScrollView.transform.GetChild (1).GetChild (0).gameObject;
			P = S.transform.GetChild (1).gameObject;
			
			
			P.transform.localPosition = pits;
			P.transform.parent.GetChild (0).transform.localPosition = Cpits;
			P.transform.localPosition += new Vector3 (0, (122 * (UserMgr.AwayLineup.pit.Count - 1)) / 2, 0);
			P.transform.parent.GetChild (0).transform.localPosition += new Vector3 (0, (122 * (UserMgr.AwayLineup.pit.Count - 1)) / 2, 0);
			
			
			P.gameObject.SetActive (true);
			//Debug.Log("UserMgr.AwayLineup.UserMgr.AwayLineup.pit.Count : "+ UserMgr.AwayLineup.UserMgr.AwayLineup.pit.Count);
			setv();
			
			GsetrectY = 116;
			if (UserMgr.AwayLineup.pit.Count > 0) {
				P.transform.parent.parent.GetComponent<UISprite> ().SetRect (-330, -509 - 104 - ((122 * (UserMgr.AwayLineup.pit.Count - 1))), 660, 208 + (122 * (UserMgr.AwayLineup.pit.Count - 1)));
				GsetrectY = ((122 * (UserMgr.AwayLineup.pit.Count - 1)) / 2);
				P.transform.parent.GetComponent<UISprite> ().SetRect (-328, -102 - ((122 * (UserMgr.AwayLineup.pit.Count - 1)) / 2), 656, 204 + (122 * (UserMgr.AwayLineup.pit.Count - 1)));
				//Debug.Log ("GsetrectY" + GsetrectY);
			}
			
			S.transform.parent.GetComponent<BoxCollider2D> ().size = new Vector2 (720, 208 + (122 * (UserMgr.AwayLineup.pit.Count - 1)) + 40);
			//Debug.Log("P.gameObject" + P.gameObject);
			P.gameObject.SetActive (false);
			
			S = ScrollView.transform.GetChild (2).GetChild (0).gameObject;
			//Debug.Log (S.transform.GetChild (1).gameObject);
			P = S.transform.GetChild (1).gameObject;
			
			P.transform.localPosition = hits;
			//Debug.Log (S);
			P.transform.parent.GetChild (0).transform.localPosition = Chits;
			
			P.transform.localPosition += new Vector3 (0, ((122 * (UserMgr.AwayLineup.hit.Count - 1)) / 2), 0);
			P.transform.parent.GetChild (0).transform.localPosition += new Vector3 (0, ((122 * (UserMgr.AwayLineup.hit.Count - 1)) / 2), 0);
			P.gameObject.SetActive (true);
			setv2 ();
			
			
			
			
			
			P.transform.parent.parent.GetComponent<UISprite> ().SetRect (-330, -740 - 104 - ((122 * (UserMgr.AwayLineup.hit.Count - 1))) - (GsetrectY * 2), 660, 208 + (122 * (UserMgr.AwayLineup.hit.Count - 1)));
			P.transform.parent.GetComponent<UISprite> ().SetRect (-328, -102 - ((122 * (UserMgr.AwayLineup.hit.Count - 1)) / 2), 656, 204 + (122 * (UserMgr.AwayLineup.hit.Count - 1)));
			
			S.transform.parent.GetComponent<BoxCollider2D> ().size = new Vector2 (720, 208 + (122 * (UserMgr.AwayLineup.hit.Count - 1)) + 40);
			
			
			
//			if (buttens) {
//				buttens = false;
//				
//				ScrollView = transform.FindChild ("Scroll View 1").gameObject;
//				Debug.Log ("what teamcode : " + UserMgr.Schedule.extend [1].teamCode);
//				mlineupEvent = new GetLineupEvent (new EventDelegate (this, "setarrray"));
//				NetMgr.GetLineup (UserMgr.Schedule.extend [1].teamCode, mlineupEvent);
//				
//			}
			transform.FindChild ("Scroll View 1").gameObject.SetActive (false);
			P.gameObject.SetActive (false);
		} else {
//			transform.FindChild ("non").gameObject.SetActive(true);
			transform.FindChild ("Scroll View").gameObject.SetActive(false);
			transform.FindChild ("Scroll View 1").gameObject.SetActive(false);
		}
		
	}
	
	IEnumerator GetImage(WWW www,GameObject g)
	{
		
		yield return www;
		
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		g.GetComponent<UITexture> ().mainTexture = temp;
	}
	public void ChangesA(){
		
		//transform.FindChild ("Scroll View").transform.localPosition = transform.FindChild ("Scroll View 1").transform.localPosition;
		//transform.FindChild ("Scroll View").GetComponent<UIPanel> ().clipOffset = transform.FindChild ("Scroll View 1").GetComponent<UIPanel> ().clipOffset;
		transform.FindChild ("Scroll View").gameObject.SetActive (true);
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition();
		transform.FindChild ("Scroll View 1").gameObject.SetActive (false);

		T1.GetComponent<UIButton> ().isEnabled = false;
		T2.GetComponent<UIButton> ().isEnabled = true;

		T11.GetComponent<UIButton> ().isEnabled = false;
		T22.GetComponent<UIButton> ().isEnabled = true;

//		T1.GetComponent<UILabel>().color = new Color(147f/255f,147f/255f,147f/255f);
//		T2.GetComponent<UILabel>().color = new Color(37f/255f,170f/255f,225f/255f);
//		T11.GetComponent<UILabel>().color = new Color(147f/255f,147f/255f,147f/255f);
//		T22.GetComponent<UILabel>().color = new Color(37f/255f,170f/255f,225f/255f);
//		
	}
	public void ChangesH(){
		
		//transform.FindChild ("Scroll View 1").transform.localPosition = transform.FindChild ("Scroll View").transform.localPosition;
		//transform.FindChild ("Scroll View 1").GetComponent<UIPanel> ().clipOffset = transform.FindChild ("Scroll View").GetComponent<UIPanel> ().clipOffset;
		
		transform.FindChild ("Scroll View 1").gameObject.SetActive (true);
		transform.FindChild ("Scroll View 1").GetComponent<UIScrollView> ().ResetPosition();
		transform.FindChild ("Scroll View").gameObject.SetActive (false);
		
		T1.GetComponent<UIButton> ().isEnabled = true;
		T2.GetComponent<UIButton> ().isEnabled = false;
		
		T11.GetComponent<UIButton> ().isEnabled = true;
		T22.GetComponent<UIButton> ().isEnabled = false;
		
	}
	
	
	void setv(){
		if(buttens){
			if(Gpit.Count>0){
				for (int i =0; i<UserMgr.AwayLineup.pit.Count; i++) {
					//Debug.Log(Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.UserMgr.AwayLineup.pit [0].imagePath + UserMgr.AwayLineup.UserMgr.AwayLineup.pit [0].imageName);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.pit [i].imagePath + UserMgr.AwayLineup.pit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					
					
					Gpit[i].transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerName;
					Gpit[i].transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					Gpit[i].transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerNumber.ToString ();
				}
				
			}else{
				
				
				
				for (int i =0; i<UserMgr.AwayLineup.pit.Count; i++) {
					temp = (GameObject)Instantiate (P, new Vector3 (0, 0, 0), P.transform.localRotation);
					temp.transform.parent = P.transform.parent;
					temp.transform.localScale = new Vector3 (1, 1, 1);
					temp.transform.localPosition = new Vector3 (P.transform.localPosition.x, P.transform.localPosition.y - (gap * i), P.transform.localPosition.z);
					//Debug.Log(Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.pit [i].imagePath + UserMgr.AwayLineup.pit [i].imageName);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.pit [i].imagePath + UserMgr.AwayLineup.pit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					temp.transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerName;
					temp.transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					temp.transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerNumber.ToString ();
					temp.name = "bar" + (i + 1).ToString ();
					Gpit.Add(temp);
					
					
				}
			}
		}else{
			
			if(Gpit2.Count>0){
				for (int i =0; i<UserMgr.AwayLineup.pit.Count; i++) {
					//Debug.Log(Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.UserMgr.AwayLineup.pit [0].imagePath + UserMgr.AwayLineup.UserMgr.AwayLineup.pit [0].imageName);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.pit [i].imagePath + UserMgr.AwayLineup.pit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					
					
					Gpit2[i].transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerName;
					Gpit2[i].transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					Gpit2[i].transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerNumber.ToString ();
				}
				
			}else{
				
				
				
				
				
				for (int i =0; i<UserMgr.AwayLineup.pit.Count; i++) {
					temp = (GameObject)Instantiate (P, new Vector3 (0, 0, 0), P.transform.localRotation);
					temp.transform.parent = P.transform.parent;
					temp.transform.localScale = new Vector3 (1, 1, 1);
					temp.transform.localPosition = new Vector3 (P.transform.localPosition.x, P.transform.localPosition.y - (gap * i), P.transform.localPosition.z);
					Debug.Log(Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.pit [i].imagePath + UserMgr.AwayLineup.pit [i].imageName);
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.pit [i].imagePath + UserMgr.AwayLineup.pit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					temp.transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerName;
					temp.transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					temp.transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.pit [i].playerNumber.ToString ();
					temp.name = "bar" + (i + 1).ToString ();
					Gpit2.Add(temp);
					
					
				}
			}
			
			
			
			
			
			
		}
	}
	
	void setv2(){
		if (buttens) {
			
			if (Ghit.Count > 0) {
				
				
				
				for (int i =0; i<UserMgr.AwayLineup.hit.Count; i++) {
					
					
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.hit [i].imagePath + UserMgr.AwayLineup.hit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					
					
					Ghit [i].transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerName;
					Ghit [i].transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					Ghit [i].transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerNumber.ToString ();
					Ghit [i].transform.GetChild (3).GetComponent<UILabel> ().text = (i + 1).ToString ();
					
					
				}
				
				
				
			} else {
				
				
				for (int i =0; i<UserMgr.AwayLineup.hit.Count; i++) {
					temp = (GameObject)Instantiate (P, new Vector3 (0, 0, 0), P.transform.localRotation);
					temp.transform.parent = P.transform.parent;
					temp.transform.localScale = new Vector3 (1, 1, 1);
					temp.transform.localPosition = new Vector3 (P.transform.localPosition.x, P.transform.localPosition.y - (gap * i), P.transform.localPosition.z);
					
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.hit [i].imagePath + UserMgr.AwayLineup.hit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					
					
					temp.transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerName;
					temp.transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					temp.transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerNumber.ToString ();
					temp.transform.GetChild (3).GetComponent<UILabel> ().text = (i + 1).ToString ();
					temp.name = "bar" + (i + 1).ToString ();
					Ghit.Add (temp);
				}
				
				
			}
		} else {
			if (Ghit2.Count > 0) {
				
				
				
				for (int i =0; i<UserMgr.AwayLineup.hit.Count; i++) {
					
					
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.hit [i].imagePath + UserMgr.AwayLineup.hit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					
					
					Ghit2 [i].transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerName;
					Ghit2 [i].transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					Ghit2 [i].transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerNumber.ToString ();
					Ghit2 [i].transform.GetChild (3).GetComponent<UILabel> ().text = (i + 1).ToString ();
					
					
				}
				
				
				
			} else {
				
				
				for (int i =0; i<UserMgr.AwayLineup.hit.Count; i++) {
					temp = (GameObject)Instantiate (P, new Vector3 (0, 0, 0), P.transform.localRotation);
					temp.transform.parent = P.transform.parent;
					temp.transform.localScale = new Vector3 (1, 1, 1);
					temp.transform.localPosition = new Vector3 (P.transform.localPosition.x, P.transform.localPosition.y - (gap * i), P.transform.localPosition.z);
					
					www = new WWW (Constants.IMAGE_SERVER_HOST + UserMgr.AwayLineup.hit [i].imagePath + UserMgr.AwayLineup.hit [i].imageName);
					StartCoroutine (GetImage (www, temp.transform.GetChild (0).GetChild (0).GetChild (0).gameObject));
					
					
					
					temp.transform.GetChild (1).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerName;
					temp.transform.GetChild (1).GetChild (0).GetComponent<UILabel> ().text = "";
					temp.transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = UserMgr.AwayLineup.hit [i].playerNumber.ToString ();
					temp.transform.GetChild (3).GetComponent<UILabel> ().text = (i + 1).ToString ();
					temp.name = "bar" + (i + 1).ToString ();
					Ghit2.Add (temp);
				}
				
				
			}
			
			
			
			
			
			
		}
		if (UserMgr.AwayLineup.hit.Count < 9) {
			ScrollView.transform.FindChild ("Bg_g 1").gameObject.SetActive (false);
			ScrollView.transform.FindChild ("Bg_g 2").gameObject.SetActive (false);
		} else {
			ScrollView.transform.FindChild ("Bg_g 1").gameObject.SetActive (true);
			ScrollView.transform.FindChild ("Bg_g 2").gameObject.SetActive (true);
		}


	}
}