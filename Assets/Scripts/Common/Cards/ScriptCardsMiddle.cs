using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptCardsMiddle : MonoBehaviour {
	//public string grade,maxlv,posi,team,num,name,nowlv,add;
	public GameObject maincard;
	//public float hp,exp;
	List<List<string>> useCard = new List<List<string>> ();
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
	List<string> needuppoint = new List<string> ();
	List<string> backnum = new List<string> ();
	List<GameObject> cards = new List<GameObject> ();
	GetCardInvenEvent mEvent;
	public float countnum = 0;
	void GotCardsInven(){
		needuppoint.Clear ();
		grade.Clear ();
		maxlv.Clear ();
		posi.Clear ();
		team.Clear ();
		//num.Clear ();
		name.Clear ();
		nowlv.Clear ();
		add.Clear ();
		hp.Clear ();
		maxhp.Clear ();
		exp.Clear ();
		maxexp.Clear ();
		cardgrade.Clear ();
		teamimage.Clear ();
		image.Clear ();
		number.Clear ();
		cards.Clear ();
		backnum.Clear ();
		for (int i = 0; i<mEvent.Response.data.hitter.Count; i++) {
			grade.Add(mEvent.Response.data.hitter [i].className);
			maxlv.Add(mEvent.Response.data.hitter [i].maxLevel.ToString());
			posi.Add(mEvent.Response.data.hitter [i].className.ToString());
			team.Add(mEvent.Response.data.hitter [i].teamName);
			//num.Add(mEvent.Response.data.hitter [i].cardNo.ToString());
			name.Add(mEvent.Response.data.hitter [i].cardName);
			nowlv.Add(mEvent.Response.data.hitter [i].cardLevel.ToString());
			add.Add(mEvent.Response.data.hitter [i].rewardRate.ToString());
			backnum.Add(mEvent.Response.data.hitter [i].backNum.ToString());
			hp.Add(nowhp(mEvent.Response.data.hitter [i].classNo,i));
			maxhp.Add(maxhps(mEvent.Response.data.hitter [i].classNo));

			exp.Add(mEvent.Response.data.hitter [i].cardXp);
			maxexp.Add(mEvent.Response.data.hitter [i].maxCardXp);
			//Debug.Log(mEvent.Response.data.hitter [i].cardXp +" : " + mEvent.Response.data.hitter [i].maxCardXp);
			cardgrade.Add(mEvent.Response.data.hitter [i].classNo);
			teamimage.Add(mEvent.Response.data.hitter [i].teamImageName.ToString());
			image.Add(mEvent.Response.data.hitter [i].cardImageName.ToString());
			number.Add(mEvent.Response.data.hitter [i].accrueCardXp.ToString());
			needuppoint.Add(mEvent.Response.data.hitter[i].needUpgradePoint.ToString());
				
		}
		transform.GetChild(1). GetComponent<UIDraggablePanel2>().Init(mEvent.Response.data.hitter.Count, 
		                                                              delegate(UIListItem item, int index) {
			
			item.Target.gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("needexp").GetComponent<UILabel>().text = needuppoint[index];//grade;
			item.Target.gameObject.transform.GetChild(2).GetChild(0).GetComponent<UILabel>().text = grade[index];//grade;
			item.Target.gameObject.transform.GetChild(2).GetChild(1).GetComponent<UILabel>().text = maxlv[index];//maxlv;
			item.Target.gameObject.transform.GetChild(2).GetChild(2).GetComponent<UILabel>().text = posi[index];//position;
			item.Target.gameObject.transform.GetChild(2).GetChild(3).GetComponent<UILabel>().text = team[index];//team;
			item.Target.gameObject.transform.GetChild(2).GetChild(4).GetComponent<UILabel>().text = backnum[index]+".";//num;
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
			item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (1).GetChild(1).GetComponent<UILabel> ().text = backnum[index];
			item.Target.gameObject.transform.localPosition = new Vector3(maincard.transform.localPosition.x,item.Target.gameObject.transform.localPosition.y,item.Target.gameObject.transform.localPosition.z);
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST+mEvent.Response.data.hitter [index].cardImagePath+image[index]);
			//Debug.Log (Constants.IMAGE_SERVER_HOST+mEvent.Response.data.hitter [index].cardImagePath+image[index]);
			StartCoroutine(GetImage (www,item.Target.gameObject.transform.GetChild (1).GetChild (0).GetChild (2).GetChild(0).gameObject));
			//item.Target.gameObject.transform.localPosition = new Vector3(999,999,999);
			cards.Add(item.Target.gameObject);
			
			
		});

		transform.FindChild ("ListCards").GetComponent<UIDraggablePanel2> ().ResetPosition ();
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

	public bool cardcountP(string name, string needxp,string nowlv,string maxlv,string cardclass){
	
		if (countnum < 2) {
			useCard.Add(new List<string>());
			useCard [useCard.Count - 1].Add (name);
			useCard [useCard.Count - 1].Add (needxp);
			useCard [useCard.Count - 1].Add (nowlv);
			useCard [useCard.Count - 1].Add (maxlv);
			useCard [useCard.Count - 1].Add (cardclass);
			if (countnum == 0) {
				
				sliedon(name,needxp);
			}else if(countnum == 1){
				string max1,max2;
				max1 = useCard [useCard.Count - 2][2];
				max2 = useCard [useCard.Count - 1][2];
				if(max1 == useCard [useCard.Count - 2][3]){
					max1 = "MAX LV";
				}else{
					max1 = max1 + " LV";
				}
				if(max2 == useCard [useCard.Count - 1][3]){
					max2 = "MAX LV";
				}else{
					max2 = max2 + " LV";
				}
				UpgraedMenuOn(useCard [useCard.Count - 2][0],max1,useCard [useCard.Count - 2][4],
				              useCard [useCard.Count - 1][0],max2,useCard [useCard.Count - 1][4]);
			}

			countnum += 1;
		
			return true;
		}
		return false;
	}
	public bool cardcountN(string name, string needxp,string nowlv,string maxlv,string cardclass){
		for (int i = 0; i<useCard.Count; i++) {
			if(useCard[i][0]==name){
				useCard.RemoveAt(i);
			}
		}
		if (countnum == 2) {
			PowerUpMenuOn(name,needxp);
		}else if(countnum == 1){
			useCard.Clear ();
			sliedoff();
		}


		if (countnum > 0) {
			countnum -= 1;
			return true;
		}
		return false;
	}

	public void Allcolse(){
		useCard.Clear ();
		countnum = 0;
		sliedoff ();

		for (int i = 0; i <cards.Count; i++) {
		
			cards[i].transform.FindChild("BtnSelect").GetComponent<onhitchose>().off();
		}

	}

	void PowerUpMenuOn(string name, string needxp){
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("PowerUp").FindChild("UserPoint").FindChild("Label").GetComponent<UILabel>().text = "0";
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("PowerUp").FindChild("NeedPoint").FindChild("Label").GetComponent<UILabel>().text = needxp;
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("PowerUp").gameObject.SetActive (true);
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("Upgraed").gameObject.SetActive (false);
	
	}
	void UpgraedMenuOn(string name1,string LV1 ,string cardclass1,string name2,string LV2 ,string cardclass2){
		transform.parent.FindChild ("Bottom").FindChild ("Panel").FindChild ("Upgraed").FindChild ("Card1").GetComponent<UILabel> ().text = name1;
		transform.parent.FindChild ("Bottom").FindChild ("Panel").FindChild ("Upgraed").FindChild ("Card1").FindChild("Label").GetComponent<UILabel> ().text = cardclass1 + " " + LV1;
		transform.parent.FindChild ("Bottom").FindChild ("Panel").FindChild ("Upgraed").FindChild ("Card2").GetComponent<UILabel> ().text = name2;
		transform.parent.FindChild ("Bottom").FindChild ("Panel").FindChild ("Upgraed").FindChild ("Card2").FindChild("Label").GetComponent<UILabel> ().text = cardclass2 + " " + LV2;
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("PowerUp").gameObject.SetActive (false);
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("Upgraed").gameObject.SetActive (true);
	
	}
	void AllmenuOff(){
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("PowerUp").gameObject.SetActive (false);
		transform.parent.FindChild ("Bottom").FindChild("Panel").FindChild("Upgraed").gameObject.SetActive (false);
	}
	void sliedon(string name, string needxp){
		StartCoroutine (moveup(name,needxp));
	}
	void sliedoff(){
		StartCoroutine (movedown());
	}
	public float sliedspeed = 5;
	IEnumerator moveup(string name, string needxp){
		PowerUpMenuOn(name,needxp);
		for (int i = 0; i<sliedspeed; i++) {
			transform.parent.FindChild ("Bottom").transform.localPosition += new Vector3(0,212f/sliedspeed,0);
			yield return new WaitForSeconds(0.04f);

	}

	}
	IEnumerator movedown(){
		for (int i = 0; i<sliedspeed; i++) {
			transform.parent.FindChild ("Bottom").transform.localPosition -= new Vector3(0,212f/sliedspeed,0);
			yield return new WaitForSeconds(0.04f);
			
		}
		AllmenuOff ();
	}

	public void PowerUp(){
		if (0 < int.Parse (useCard [useCard.Count - 1] [1])) {
			DialogueMgr.ShowDialogue ("강화 실패", "강화 포인트가 부족합니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else {
		
		}
	}
	public void Upgraed(){
		if (int.Parse (useCard [useCard.Count - 1] [2]) < int.Parse (useCard [useCard.Count - 1] [3]) ||
			int.Parse (useCard [useCard.Count - 2] [2]) < int.Parse (useCard [useCard.Count - 2] [3])) {
			DialogueMgr.ShowDialogue ("진화 실패", "동일한 등급의 MAX LV 카드 2장이 필요합니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else {
		
		}
}
}