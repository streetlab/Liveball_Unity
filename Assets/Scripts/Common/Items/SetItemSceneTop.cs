using UnityEngine;
using System.Collections;

public class SetItemSceneTop : MonoBehaviour {
	public GameObject mLblDia,mLblRuby;
	public GameObject Ruby,Gold,Item,Card;
	public GameObject btRuby,btGold,btItem,btCard;
	void Start(){
		//rubyon ();
		Ruby.SetActive (true);
//		btRuby.GetComponent<UIButton> ().isEnabled = false;btRuby
		btRuby.transform.FindChild ("bar").gameObject.SetActive (true);
		btRuby.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (1,1,1,1);
	}
	public void rubyon(){
		Alloff ();
		Ruby.SetActive (true);
//		btRuby.GetComponent<UIButton> ().isEnabled = false;
		btRuby.transform.FindChild ("bar").gameObject.SetActive (true);
		btRuby.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (1,1,1,1);
	}
	public void goldon(){
		Alloff ();
		Gold.SetActive (true);
//		btGold.GetComponent<UIButton> ().isEnabled = false;
		btGold.transform.FindChild ("bar").gameObject.SetActive (true);
		btGold.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (1,1,1,1);
	}
	public void itemon(){
		Alloff ();
		Item.SetActive (true);
		//btItem.GetComponent<UIButton> ().isEnabled = false;
		btItem.transform.FindChild ("bar").gameObject.SetActive (true);
		btItem.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (1,1,1,1);
	}
	public void cardon(){
		Alloff ();
		Card.SetActive (true);
		//btCard.GetComponent<UIButton> ().isEnabled = false;
		btCard.transform.FindChild ("bar").gameObject.SetActive (true);
		btCard.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (1,1,1,1);
	}

	void Alloff(){

		Ruby.gameObject.SetActive (false);
		Gold.gameObject.SetActive (false);
		Item.gameObject.SetActive (false);
		Card.gameObject.SetActive (false);
//		btRuby.GetComponent<UIButton> ().isEnabled = true;
//		btGold.GetComponent<UIButton> ().isEnabled = true;
//		btItem.GetComponent<UIButton> ().isEnabled = true;
//		btCard.GetComponent<UIButton> ().isEnabled = true;
		btRuby.transform.FindChild ("bar").gameObject.SetActive (false);
		//btGold.transform.FindChild ("bar").gameObject.SetActive (false);
		btItem.transform.FindChild ("bar").gameObject.SetActive (false);
		//btCard.transform.FindChild ("bar").gameObject.SetActive (false);

		btRuby.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (156f/255f,151f/255f,155f/255f,1);
		btGold.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (156f/255f,151f/255f,155f/255f,1);
		btItem.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (156f/255f,151f/255f,155f/255f,1);
		btCard.transform.FindChild ("Label").GetComponent<UILabel> ().color = new Color (156f/255f,151f/255f,155f/255f,1);


}

	void Update(){
		SetTopInfo ();
	}
	
	void SetTopInfo()
	{
		mLblDia.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
		//mLblGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userGoldenBall);
		mLblRuby.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);
	}

	public void Close(){
		ScriptMainTop.OpenBettingCheck = true;
		transform.parent.parent.gameObject.SetActive (false);
	}
}
