using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LineupControl : MonoBehaviour {
	public GameObject T1,T2,T11,T22;
	
	GameObject ScrollView,S,P,H,temp;
	GetLineupEvent mlineupEvent;
	bool buttens = true;
	List<string> lineup = new List<string>();
	List<string> pit = new List<string>();
	List<string> hit = new List<string>();
	public float gap = 122;
	// Use this for initialization
	public void view () {
		ScrollView = transform.FindChild ("Scroll View").gameObject;
		Debug.Log ("what teamcode : " + UserMgr.Schedule.extend[0].teamCode);
		mlineupEvent = new GetLineupEvent (new EventDelegate (this, "setarrray"));
		NetMgr.GetLineup (UserMgr.Schedule.extend[0].teamCode,mlineupEvent);
		
		T1.GetComponent<UILabel> ().text = UserMgr.Schedule.extend [0].teamName;
		T2.GetComponent<UILabel> ().text = UserMgr.Schedule.extend [1].teamName;
		T11.GetComponent<UILabel> ().text = UserMgr.Schedule.extend [0].teamName;
		T22.GetComponent<UILabel> ().text = UserMgr.Schedule.extend [1].teamName;
		
	}
	
	void setarrray(){
		
		UserMgr.AwayLineup = mlineupEvent.Response.data;
		
		getdata ();
		
		
	}
	void getdata(){
		S = ScrollView.transform.GetChild (0).GetChild (0).FindChild ("S").gameObject;
		WWW www;
		
		for (int i = 0; i<UserMgr.AwayLineup.lineup.Count; i++) {
			S.transform.GetChild (i).gameObject.SetActive(false);
		}
		for (int i =0; i<UserMgr.AwayLineup.lineup.Count; i++) {
			//Debug.Log("Image pach! : " + mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
			
			switch (mlineupEvent.Response.data.lineup [i].lineup) {
				
			case 1:
				S.transform.GetChild (0).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (0).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (0).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				
				break;
			case 2:
				S.transform.GetChild (1).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (1).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (1).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				break;
			case 3:
				S.transform.GetChild (2).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (2).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (2).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				break;
			case 4:
				S.transform.GetChild (3).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (3).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (3).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				break;
			case 5:
				S.transform.GetChild (4).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (4).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (4).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				
				break;
			case 6:
				S.transform.GetChild (5).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (5).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (5).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				
				break;
			case 7:
				S.transform.GetChild (6).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (6).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (6).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				
				break;
			case 8:
				S.transform.GetChild (7).gameObject.SetActive(true);
				Debug.Log("what log : " + mlineupEvent.Response.data.lineup [i].playerName);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (7).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (7).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				
				break;
			case 9:
				S.transform.GetChild (8).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (8).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (8).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				
				break;
			case 10:
				Debug.Log("???");
				S.transform.GetChild (9).gameObject.SetActive(true);
				www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.lineup [i].imagePath+mlineupEvent.Response.data.lineup [i].imageName);
				StartCoroutine(GetImage (www,S.transform.GetChild (9).GetChild (0).GetChild(0).gameObject));
				S.transform.GetChild (9).GetChild (1).GetChild (0).GetChild (0).GetComponent<UILabel> ().text = mlineupEvent.Response.data.lineup [i].playerName;
				
				break;
				
			}
		}
		
		
		
		S = ScrollView.transform.GetChild (1).GetChild (0).gameObject;
		P = S.transform.GetChild (1).gameObject;
		P.transform.localPosition += new Vector3 (0,(122 * (mlineupEvent.Response.data.pit.Count-1 ))/2,0);
		P.transform.parent.GetChild(0).transform.localPosition += new Vector3 (0,(122 * (mlineupEvent.Response.data.pit.Count -1))/2,0);
		for (int i =0; i<mlineupEvent.Response.data.pit.Count; i++) {
			temp = (GameObject)Instantiate(P,new Vector3(0,0,0),P.transform.localRotation);
			temp.transform.parent = P.transform.parent;
			temp.transform.localScale = new Vector3(1,1,1);
			temp.transform.localPosition  = new Vector3(P.transform.localPosition.x,P.transform.localPosition.y-(gap*i),P.transform.localPosition.z);
			
			www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.pit[0].imagePath+mlineupEvent.Response.data.pit[0].imageName);
			StartCoroutine(GetImage (www,temp.transform.GetChild(0).GetChild(0).GetChild(0).gameObject));
			
			
			
			temp.transform.GetChild(1).GetComponent<UILabel>().text = mlineupEvent.Response.data.pit[0].playerName;
			temp.transform.GetChild(1).GetChild (0).GetComponent<UILabel>().text = "";
			temp.transform.GetChild(2).GetChild (0).GetComponent<UILabel>().text = mlineupEvent.Response.data.pit[0].playerNumber.ToString();
			temp.name = "bar"+(i+1).ToString();
		}
		float GsetrectY = 116;
		if (mlineupEvent.Response.data.pit.Count > 0) {
			P.transform.parent.parent.GetComponent<UISprite> ().SetRect (-330, -509-104-((122 * (mlineupEvent.Response.data.pit.Count-1 ))), 660, 208 + (122 * (mlineupEvent.Response.data.pit.Count -1)));
			GsetrectY = ((122 * (mlineupEvent.Response.data.pit.Count-1))/2);
			P.transform.parent.GetComponent<UISprite> ().SetRect (-328, -102-((122 * (mlineupEvent.Response.data.pit.Count -1))/2), 656, 204 + (122 * (mlineupEvent.Response.data.pit.Count -1)));
			Debug.Log("GsetrectY" + GsetrectY);
		}
		
		
		
		
		S.transform.parent.GetComponent<BoxCollider2D> ().size = new Vector2 (720,208 + (122 * (mlineupEvent.Response.data.pit.Count -1))+40);
		
		
		
		P.gameObject.SetActive (false);
		
		S = ScrollView.transform.GetChild (2).GetChild (0).gameObject;
		Debug.Log (S.transform.GetChild (1).gameObject);
		P = S.transform.GetChild (1).gameObject;
		P.transform.localPosition += new Vector3 (0,((122 * (mlineupEvent.Response.data.hit.Count -1))/2),0);
		P.transform.parent.GetChild(0).transform.localPosition += new Vector3 (0,((122 * (mlineupEvent.Response.data.hit.Count -1))/2),0);
		//Debug.Log (S);
		for (int i =0; i<mlineupEvent.Response.data.hit.Count; i++) {
			temp = (GameObject)Instantiate(P,new Vector3(0,0,0),P.transform.localRotation);
			temp.transform.parent = P.transform.parent;
			temp.transform.localScale = new Vector3(1,1,1);
			temp.transform.localPosition  = new Vector3(P.transform.localPosition.x,P.transform.localPosition.y-(gap*i),P.transform.localPosition.z);
			
			www = new WWW (Constants.IMAGE_SERVER_HOST+mlineupEvent.Response.data.hit[i].imagePath+mlineupEvent.Response.data.hit[i].imageName);
			StartCoroutine(GetImage (www,temp.transform.GetChild(0).GetChild(0).GetChild(0).gameObject));
			
			
			
			temp.transform.GetChild(1).GetComponent<UILabel>().text = mlineupEvent.Response.data.hit[i].playerName;
			temp.transform.GetChild(1).GetChild (0).GetComponent<UILabel>().text = "";
			temp.transform.GetChild(2).GetChild (0).GetComponent<UILabel>().text = mlineupEvent.Response.data.hit[i].playerNumber.ToString();
			temp.transform.GetChild(3).GetComponent<UILabel>().text = (i+1).ToString();
			temp.name = "bar"+(i+1).ToString();
		}
		
		
		P.transform.parent.parent.GetComponent<UISprite> ().SetRect (-330, -740-104-((122 * (mlineupEvent.Response.data.hit.Count -1)))-(GsetrectY*2), 660, 208 + (122 * (mlineupEvent.Response.data.hit.Count -1)));
		P.transform.parent.GetComponent<UISprite> ().SetRect (-328, -102-((122 * (mlineupEvent.Response.data.hit.Count -1))/2), 656, 204 + (122 * (mlineupEvent.Response.data.hit.Count -1)));
		
		S.transform.parent.GetComponent<BoxCollider2D> ().size = new Vector2 (720,208 + (122 * (mlineupEvent.Response.data.hit.Count -1))+40);
		
		
		
		if(buttens){
			buttens = false;
			ScrollView = transform.FindChild ("Scroll View 1").gameObject;
			Debug.Log ("what teamcode : " + UserMgr.Schedule.extend[1].teamCode);
			mlineupEvent = new GetLineupEvent (new EventDelegate (this, "setarrray"));
			NetMgr.GetLineup (UserMgr.Schedule.extend[1].teamCode,mlineupEvent);
			
		}
		transform.FindChild ("Scroll View 1").gameObject.SetActive (false);
		P.gameObject.SetActive (false);
		
	}
	
	IEnumerator GetImage(WWW www,GameObject g)
	{
		
		yield return www;
		
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		g.GetComponent<UITexture> ().mainTexture = temp;
	}
	public void ChangesA(){
		
		transform.FindChild ("Scroll View").gameObject.SetActive (true);
		transform.FindChild ("Scroll View 1").gameObject.SetActive (false);
		
		//T1.GetComponent<UILabel>().color = new Color(147f/255f,147f/255f,147f/255f);
		//T2.GetComponent<UILabel>().color = new Color(37f/255f,170f/255f,225f/255f);
		
	}
	public void ChangesH(){
		transform.FindChild ("Scroll View 1").gameObject.SetActive (true);
		transform.FindChild ("Scroll View").gameObject.SetActive (false);
		
		//T2.GetComponent<UILabel>().color = new Color(147f/255f,147f/255f,147f/255f);
		//T1.GetComponent<UILabel>().color = new Color(37f/255f,170f/255f,225f/255f);
		
	}
	
	
	
	
}
