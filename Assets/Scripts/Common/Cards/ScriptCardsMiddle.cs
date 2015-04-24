using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptCardsMiddle : MonoBehaviour {
	//public string grade,maxlv,posi,team,num,name,nowlv,add;

	//public float hp,exp;
	List<string> grade = new List<string> ();
	List<string> maxlv = new List<string> ();
	List<string> posi = new List<string> ();
	List<string> team = new List<string> ();
	List<string> num = new List<string> ();
	List<string> name = new List<string> ();
	List<string> nowlv = new List<string> ();
	List<string> add = new List<string> ();
	List<int> hp = new List<int> ();
	List<int> exp = new List<int> ();
	List<int> maxhp = new List<int> ();
	List<int> maxexp = new List<int> ();
	List<int> cardgrade = new List<int> ();
	List<string> teamimage = new List<string> ();
	List<string> image = new List<string> ();
	List<string> number = new List<string> ();
	GetCardInvenEvent mEvent;

	void GotCardsInven(){
		for (int i = 0; i<mEvent.Response.data.hitter.Count; i++) {
			grade.Add(mEvent.Response.data.hitter [i].className.ToString());
			maxlv.Add(mEvent.Response.data.hitter [i].maxLevel.ToString());
			posi.Add(mEvent.Response.data.hitter [i].className.ToString());
			team.Add(mEvent.Response.data.hitter [i].teamName.ToString());
			num.Add(mEvent.Response.data.hitter [i].cardNo.ToString());
			name.Add(mEvent.Response.data.hitter [i].cardName.ToString());
			nowlv.Add(mEvent.Response.data.hitter [i].cardLevel.ToString());
			add.Add(mEvent.Response.data.hitter [i].rewardRate.ToString());

			hp.Add(nowhp(mEvent.Response.data.hitter [i].classNo,i));
			maxhp.Add(maxhps(mEvent.Response.data.hitter [i].classNo));

			exp.Add(int.Parse(mEvent.Response.data.hitter [i].cardXp.ToString()));
			maxexp.Add(int.Parse(mEvent.Response.data.hitter [i].maxCardXp.ToString()));
			cardgrade.Add(mEvent.Response.data.hitter [i].classNo);
			teamimage.Add(mEvent.Response.data.hitter [i].teamImageName.ToString());
			image.Add(mEvent.Response.data.hitter [i].cardImageName.ToString());
			number.Add(mEvent.Response.data.hitter [i].accrueCardXp.ToString());
				
		}
		transform.GetChild(1). GetComponent<UIDraggablePanel2>().Init(mEvent.Response.data.hitter.Count, 
		                                                              delegate(UIListItem item, int index) {
			
			item.Target.gameObject.SetActive(true);
			
			item.Target.gameObject.transform.GetChild(2).GetChild(0).GetComponent<UILabel>().text = grade[index];//grade;
			item.Target.gameObject.transform.GetChild(2).GetChild(1).GetComponent<UILabel>().text = maxlv[index];//maxlv;
			item.Target.gameObject.transform.GetChild(2).GetChild(2).GetComponent<UILabel>().text = posi[index];//position;
			item.Target.gameObject.transform.GetChild(2).GetChild(3).GetComponent<UILabel>().text = team[index];//team;
			item.Target.gameObject.transform.GetChild(2).GetChild(4).GetComponent<UILabel>().text = index.ToString()+".";//num;
			item.Target.gameObject.transform.GetChild(2).GetChild(5).GetComponent<UILabel>().text = name[index];//name;
			item.Target.gameObject.transform.GetChild(7).GetChild (1).GetComponent<UISprite> ().SetRect (0,0,16+(hp[index]/maxhp[index]*158),20);
			item.Target.gameObject.transform.GetChild(7).GetChild (1).localPosition = new Vector3 (-87,0,0);
			item.Target.gameObject.transform.GetChild(8).GetChild (1).GetComponent<UISprite> ().SetRect (0,0,16+(exp[index]/maxexp[index]*158),20);
			item.Target.gameObject.transform.GetChild(8).GetChild (1).localPosition = new Vector3 (-87,0,0);
			item.Target.gameObject.	transform.GetChild (3).GetChild (1).GetComponent<UILabel> ().text = nowlv[index];
			item.Target.gameObject.	transform.GetChild (4).GetChild (1).GetComponent<UILabel> ().text = add[index];
			item.Target.gameObject.transform.GetChild (1).GetChild (0).GetComponent<UISprite> ().spriteName = grades(cardgrade[index]);
			string imgName = UtilMgr.GetTeamEmblem(teamimage[index]);
			//Debug.Log(imgName);
			item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (0).GetComponent<UISprite> ().spriteName = imgName;
			item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (1).GetChild(1).GetComponent<UILabel> ().text = index.ToString();

			WWW www = new WWW (Constants.IMAGE_SERVER_HOST+mEvent.Response.data.hitter [index].cardImagePath+image[index]);
			//Debug.Log (Constants.IMAGE_SERVER_HOST+mEvent.Response.data.hitter [index].cardImagePath+image[index]);
			StartCoroutine(GetImage (www,item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (2).GetChild(0).gameObject));

			
			
			
		});
	}
	void Start () {

		mEvent = new GetCardInvenEvent(new EventDelegate(this, "GotCardsInven"));
		NetMgr.GetCardInven (mEvent);


	}
	string grades(int gra){
		switch (gra) {
		case 1: 
			return "cd_bg01_normal";
		
		case 2: 
			return "cd_bg02_special";
		
		case 3: 
			return "cd_bg03_rare";
		
		case 4: 
			return "cd_bg04_platinum";

		case 5: 
			return "cd_bg05_elite";
		
		case 6: 
			return "cd_bg06_legend";
		

		}
		return "null";
	}

	int maxhps(int gra){
		switch (gra) {
		case 1: 
			return 1;
			
		case 2: 
			return 1;
			
		case 3: 
			return 20;
			
		case 4: 
			return 10;
			
		case 5: 
			return 6;
			
		case 6: 
			return 3;
			
			
		}
		return 0;
	}
	int nowhp(int gra,int index){
		switch (gra) {
		case 1: 
			return 1;
			
		case 2: 
			return 1;

			
		}
return int.Parse(mEvent.Response.data.hitter [index].availableHp.ToString());
	}

	IEnumerator GetImage(WWW www,GameObject g)
	{

		yield return www;
	
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		g.GetComponent<UITexture> ().mainTexture = temp;
	}
//	public void GotCardsInven()
//	{
//		Debug.Log ("GotCardsInven : "+mEvent.Response.data.cardClass.Count);
//	}
}
