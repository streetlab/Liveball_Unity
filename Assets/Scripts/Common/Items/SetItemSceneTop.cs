using UnityEngine;
using System.Collections;

public class SetItemSceneTop : MonoBehaviour {
	public GameObject mLblDia,mLblRuby;
	public GameObject Ruby,Gold,Item,Card;
	public GameObject btRuby,btGold,btItem,btCard;
	void Start(){
		//rubyon ();
		Ruby.SetActive (true);
		btRuby.GetComponent<UIButton> ().isEnabled = false;
	}
	public void rubyon(){
		Alloff ();
		Ruby.SetActive (true);
		btRuby.GetComponent<UIButton> ().isEnabled = false;
	}
	public void goldon(){
		Alloff ();
		Gold.SetActive (true);
		btGold.GetComponent<UIButton> ().isEnabled = false;
	}
	public void itemon(){
		Alloff ();
		Item.SetActive (true);
		btItem.GetComponent<UIButton> ().isEnabled = false;
	}
	public void cardon(){
		Alloff ();
		Card.SetActive (true);
		btCard.GetComponent<UIButton> ().isEnabled = false;
	}

	void Alloff(){

		Ruby.gameObject.SetActive (false);
		Gold.gameObject.SetActive (false);
		Item.gameObject.SetActive (false);
		Card.gameObject.SetActive (false);
		btRuby.GetComponent<UIButton> ().isEnabled = true;
		btGold.GetComponent<UIButton> ().isEnabled = true;
		btItem.GetComponent<UIButton> ().isEnabled = true;
		btCard.GetComponent<UIButton> ().isEnabled = true;



}

//	void Update(){
//		SetTopInfo ();
//	}
	
	void SetTopInfo()
	{
		mLblDia.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
		//mLblGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userGoldenBall);
		mLblRuby.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);
	}
}
