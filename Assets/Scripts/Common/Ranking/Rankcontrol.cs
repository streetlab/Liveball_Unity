using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rankcontrol : MonoBehaviour {
	public GameObject bars;
	public float gap=122;
	Vector3 posisions;
	float vgaps;
	List<int> rank = new List<int> ();
	List<string> teamname = new List<string> ();
	List<int> v = new List<int> ();
	List<int> l = new List<int> ();
	List<int> d = new List<int> ();
	List<int> behind= new List<int> ();
	List<string> image = new List<string> ();

	GetTeamRankingEvent mRankingEvent;

	// Use this for initialization
	void Start () {
		Init();

		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}

	void Init(){
		mRankingEvent = new GetTeamRankingEvent(new EventDelegate(this, "GotRanking"));
		NetMgr.GetTeamRanking(mRankingEvent);
	}

	public void GotRanking(){
		rank.Clear ();
		teamname.Clear ();
		v.Clear ();
		l.Clear ();
		d.Clear ();
		behind.Clear ();
		image.Clear ();
		Debug.Log (mRankingEvent.Response.data[0].teamName +"'s Ranking is "+mRankingEvent.Response.data[0].ranking);
		for (int i = 0; i<mRankingEvent.Response.data.Count; i++) {
			rank.Add (mRankingEvent.Response.data[i].ranking);
			teamname.Add (mRankingEvent.Response.data[i].teamName);
			v.Add(mRankingEvent.Response.data[i].countWin);
			l.Add(mRankingEvent.Response.data[i].countLose);
			d.Add(mRankingEvent.Response.data[i].countDraw);
			behind.Add(mRankingEvent.Response.data[i].behind);
			image.Add(mRankingEvent.Response.data[i].imageName);
		}
		setposition ();
	}

	public void editng(){
		setposition ();
	}

	void setposition(){
		posisions = bars.transform.GetChild (0).transform.localPosition;
		for (int i =0; i<bars.transform.childCount; i++) {
			bars.transform.GetChild(i).transform.localPosition =new Vector3(posisions.x,posisions.y-(i*gap),posisions.z);
			bars.transform.GetChild(i).GetChild(0).GetComponent<UILabel>().text = rank[i].ToString();
			bars.transform.GetChild(i).GetChild(1).GetComponent<UILabel>().text = teamname[i].ToString();
			bars.transform.GetChild(i).GetChild(2).GetComponent<UILabel>().text = v[i].ToString();
			bars.transform.GetChild(i).GetChild(3).GetComponent<UILabel>().text = l[i].ToString();
			bars.transform.GetChild(i).GetChild(4).GetComponent<UILabel>().text = d[i].ToString();


			bars.transform.GetChild(i).GetChild(5).GetComponent<UILabel>().text = behind[i].ToString();


			string imgName = UtilMgr.GetTeamEmblem(image[i]);
			bars.transform.GetChild(i).GetChild(6).GetComponent<UISprite>().spriteName = imgName;
			rankswitch(i);
		}

	}
	float vgap(int i){
		vgaps = ((float.Parse (bars.transform.GetChild (0).GetChild (2).GetComponent<UILabel> ().text)
			- float.Parse (bars.transform.GetChild (i).GetChild (2).GetComponent<UILabel> ().text))+
			(float.Parse (bars.transform.GetChild (i).GetChild (3).GetComponent<UILabel> ().text)
			 - float.Parse (bars.transform.GetChild (0).GetChild (3).GetComponent<UILabel> ().text)))/2;
		//Debug.Log (vgaps);
		return vgaps;
	}
	void rankswitch(int i){
		//Debug.Log (i + " :: "+rank [i] +" :: "+behind [i]);
		//	Debug.Log (scv.transform.GetChild(0).GetChild(i).GetChild(1));
		if (rank [i] == behind [i]) {
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.855f,0.86f,0.888f,1);
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "bg_circle";
			bars.transform.GetChild(i).GetChild(7).transform.localRotation = Quaternion.Euler(new Vector3 (0,0,0));
		}else if(rank [i] > behind [i]){
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.145f,0.68f,0.88f,1);
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "ic_arrow";
			bars.transform.GetChild(i).GetChild(7).transform.localRotation = Quaternion.Euler(new Vector3 (0,0,0));
		}else if(rank [i] < behind [i]){
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.88f,0.23f,0.255f,1);
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "ic_arrow";
			bars.transform.GetChild(i).GetChild(7).transform.localRotation = Quaternion.Euler(new Vector3 (0,0,180));
		}
//		switch("up"){
//		case "non":
//			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.855f,0.86f,0.888f,1);
//			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "bg_circle";
//			break;
//		case "up":
//			Debug.Log("upupup");
//			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.145f,0.68f,0.88f,1);
//			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "ic_arrow";
//			break;
//		case "down":
//			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.88f,0.23f,0.255f,1);
//			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "ic_arrow";
//			break;
//		}
}
}