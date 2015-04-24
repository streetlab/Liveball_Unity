using UnityEngine;
using System.Collections;

public class ScriptCardsMiddle : MonoBehaviour {
	public string grade,maxlv,posi,team,num,name,nowlv,add;
	string lod;
	public float hp,exp;
	GetCardInvenEvent mEvent;

	void getdata (){
		lod =Constants.IMAGE_SERVER_HOST;//card/player/name
	}
	void Start () {

		mEvent = new GetCardInvenEvent(new EventDelegate(this, "GotCardsInven"));
		NetMgr.GetCardInven (mEvent);

		transform.GetChild(1). GetComponent<UIDraggablePanel2>().Init(20, 
		                                       delegate(UIListItem item, int index) {

			item.Target.gameObject.SetActive(true);

			item.Target.gameObject.transform.GetChild(2).GetChild(0).GetComponent<UILabel>().text = grade;//grade;
			item.Target.gameObject.transform.GetChild(2).GetChild(1).GetComponent<UILabel>().text = maxlv;//maxlv;
			item.Target.gameObject.transform.GetChild(2).GetChild(2).GetComponent<UILabel>().text = posi;//position;
			item.Target.gameObject.transform.GetChild(2).GetChild(3).GetComponent<UILabel>().text = team;//team;
			item.Target.gameObject.transform.GetChild(2).GetChild(4).GetComponent<UILabel>().text = num;//num;
			item.Target.gameObject.transform.GetChild(2).GetChild(5).GetComponent<UILabel>().text = name;//name;
			item.Target.gameObject.transform.GetChild(7).GetChild (1).GetComponent<UISprite> ().SetRect (0,0,16+(hp/100*158),20);
			item.Target.gameObject.transform.GetChild(7).GetChild (1).localPosition = new Vector3 (-87,0,0);
			item.Target.gameObject.transform.GetChild(8).GetChild (1).GetComponent<UISprite> ().SetRect (0,0,16+(exp/100*158),20);
			item.Target.gameObject.transform.GetChild(8).GetChild (1).localPosition = new Vector3 (-87,0,0);
			item.Target.gameObject.	transform.GetChild (3).GetChild (1).GetComponent<UILabel> ().text = nowlv;
			item.Target.gameObject.	transform.GetChild (4).GetChild (1).GetComponent<UILabel> ().text = add;
			
			//item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (0).GetComponent<UISprite> ().spriteName = "";
			item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (1).GetChild(1).GetComponent<UILabel> ().text = index.ToString();
			//item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (2).GetChild(0).GetComponent<UITexture> ().mainTexture = "";
			
			
			
		});
	}

	public void GotCardsInven()
	{
		Debug.Log ("GotCardsInven : "+mEvent.Response.data.cardClass.Count);
	}
}
