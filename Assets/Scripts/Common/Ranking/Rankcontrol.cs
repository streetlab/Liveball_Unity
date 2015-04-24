using UnityEngine;
using System.Collections;

public class Rankcontrol : MonoBehaviour {
	public GameObject bars;
	public float gap=122;
	Vector3 posisions;
	float vgaps;

	GetTeamRankingEvent mRankingEvent;

	// Use this for initialization
	void Start () {
		Init();
		setposition ();
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}

	void Init(){
		mRankingEvent = new GetTeamRankingEvent(new EventDelegate(this, "GotRanking"));
		NetMgr.GetTeamRanking(mRankingEvent);
	}

	public void GotRanking(){
		Debug.Log (mRankingEvent.Response.data[0].teamName +"'s Ranking is "+mRankingEvent.Response.data[0].ranking);
	}

	public void editng(){
		setposition ();
	}

	void setposition(){
		posisions = bars.transform.GetChild (0).transform.localPosition;
		for (int i =0; i<bars.transform.childCount; i++) {
			bars.transform.GetChild(i).transform.localPosition =new Vector3(posisions.x,posisions.y-(i*gap),posisions.z);
			bars.transform.GetChild(i).GetChild(0).GetComponent<UILabel>().text = (i+1).ToString();
			bars.transform.GetChild(i).GetChild(1).GetComponent<UILabel>().text = "T";
			bars.transform.GetChild(i).GetChild(2).GetComponent<UILabel>().text = "50";
			bars.transform.GetChild(i).GetChild(3).GetComponent<UILabel>().text = "20";
			bars.transform.GetChild(i).GetChild(4).GetComponent<UILabel>().text = "5";
			bars.transform.GetChild(i).GetChild(5).GetComponent<UILabel>().text = vgap(i).ToString();
			bars.transform.GetChild(i).GetChild(6).GetComponent<UISprite>().spriteName = "ic_doosan";
			rankswitch(i);
		}

	}
	float vgap(int i){
		vgaps = ((float.Parse (bars.transform.GetChild (0).GetChild (2).GetComponent<UILabel> ().text)
			- float.Parse (bars.transform.GetChild (i).GetChild (2).GetComponent<UILabel> ().text))+
			(float.Parse (bars.transform.GetChild (0).GetChild (3).GetComponent<UILabel> ().text)
			 - float.Parse (bars.transform.GetChild (i).GetChild (3).GetComponent<UILabel> ().text)))/2;
		//Debug.Log (vgaps);
		return vgaps;
	}
	void rankswitch(int i){
		//	Debug.Log (scv.transform.GetChild(0).GetChild(i).GetChild(1));
		switch("up"){
		case "non":
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.855f,0.86f,0.888f,1);
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "bg_circle";
			break;
		case "up":
			Debug.Log("upupup");
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.145f,0.68f,0.88f,1);
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "ic_arrow";
			break;
		case "down":
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().color = new Color(0.88f,0.23f,0.255f,1);
			bars.transform.GetChild(i).GetChild(7).GetComponent<UISprite>().spriteName = "ic_arrow";
			break;
		}
}
}