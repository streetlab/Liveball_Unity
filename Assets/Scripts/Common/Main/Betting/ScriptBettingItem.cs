using UnityEngine;
using System.Collections;

public class ScriptBettingItem : MonoBehaviour {
	public enum TYPE
	{
		Batter,
		Loaded
	}

	public TYPE mType;

	public GameObject mBetting;
	public GameObject mSprBetting;

	public static bool ClickCheck = false;

	GameObject mSprSelected;
	UISprite mSprSilhouette;
	UISprite[] mSprCombos;

	ScriptTF_Betting mSb;
	bool _isSelected;

	CardInfo mCardInfo;
	ItemStrategyInfo mStrategyInfo;

	static Color ColorSilhouetteDisable = new Color(78f/255f, 89f/255f, 104f/255f);
	static Color ColorSilhouetteEnable = new Color(67f/255f, 75f/255f, 89f/255f);
	static Color ColorComboDisable = new Color(141f/255f, 150f/255f, 166f/255f, 100f/255f);
	static Color ColorComboEnable = new Color(1f, 1f, 0f, 100f/255f);

	// Use this for initialization
	void Start () {
		Init ();
	}

	void Init(){
		mSb = mBetting.GetComponent<ScriptTF_Betting> ();
//		mSprSelected = transform.FindChild ("SprSelected").gameObject;
		
//		if(mType == TYPE.Batter)
//		{
//			mSprSilhouette = transform.FindChild ("SprSilhouette").GetComponent<UISprite>();
//			mSprCombos = new UISprite[3];
//			mSprCombos [0] = transform.FindChild ("SprCombo1").GetComponent<UISprite> ();
//			mSprCombos [1] = transform.FindChild ("SprCombo2").GetComponent<UISprite> ();
//			mSprCombos [2] = transform.FindChild ("SprCombo3").GetComponent<UISprite> ();
//			mSprCombos [0].color = ColorComboDisable;
//			mSprCombos [1].color = ColorComboDisable;
//			mSprCombos [2].color = ColorComboDisable;
//		}

		SetUnselected ();
	}

	public void Reset()
	{
		Init ();

//		int index = mSprBetting.GetComponent<ScriptBetting> ().GetIndex (transform.name);
//		if (QuizMgr.QuizInfo.order.Count > index) {
//			transform.FindChild ("LblBody").GetComponent<UILabel> ().text = QuizMgr.QuizInfo.order [index].description;
//		}



	}

	public bool IsSelected{
		get{return _isSelected;}
		set{_isSelected = value;}
	}

	public void SetCardInfo(CardInfo cardInfo){
		mCardInfo = cardInfo;
	}

	public void SetStrategyInfo(ItemStrategyInfo strategy){
		mStrategyInfo = strategy;
	}

	public void SetSelectedWithCard(CardInfo cardInfo){
		mCardInfo = cardInfo;
		SetSelected();
	}

	public void SetSelectedWithStrategy(ItemStrategyInfo strategy){
		mStrategyInfo = strategy;
		SetSelected();
	}

	public void SetSelected()
	{
		IsSelected = true;
		//mSprSelected.SetActive (true);

		if(mType == TYPE.Loaded)
		{
			return;
		}

		//mSprSilhouette.color = ColorSilhouetteEnable;
		//SetCombo (3);
	}

	public void SetUnselected()
	{
		IsSelected = false;
		//mSprSelected.SetActive (false);

		if(mCardInfo != null){
			Debug.Log ("mCardInfo.position : "+mCardInfo.position);
			if(mCardInfo.position.Equals("H")){
				mSprBetting.transform.parent.FindChild("BtnBatter").GetComponent<ScriptBettingCard>().ReturnCard();
			} else{
				mSprBetting.transform.parent.FindChild("BtnPitcher").GetComponent<ScriptBettingCard>().ReturnCard();
			}
			mCardInfo = null;
		} else if(mStrategyInfo != null){
			Debug.Log (mStrategyInfo);
			mSprBetting.transform.parent.FindChild("BtnStrategy").GetComponent<ScriptBettingCard>().ReturnCard();
			mStrategyInfo = null;
		}

		if(mType == TYPE.Loaded)
		{
			return;
		}

		//mSprSilhouette.color = ColorSilhouetteDisable;
	}

	public void OnClicked()
	{
		ClickCheck = true;
		switch (name) {
		case "BtnHit1":
			QuizMgr.QuizValue =1;
			break;
		case "BtnHit2":
			QuizMgr.QuizValue =2;
			break;
		case "BtnHit3":
			QuizMgr.QuizValue =3;
			break;
		case "BtnOut1":
			QuizMgr.QuizValue =4;
			break;
		case "BtnOut2":
			QuizMgr.QuizValue =5;
			break;
		case "BtnOut3":
			QuizMgr.QuizValue =6;
			break;
		};
		transform.FindChild ("LblGP").GetComponent<UILabel> ().color = new Color (1,1,1,1);
		ClickButton ();
		GetComponent<UIButton> ().isEnabled = false;
		OpenBetWindow (this.name);
	}

	void OpenBetWindow(string name)
	{

		if(mSprBetting.GetComponent<ScriptBetting> ().InitWithoutCard (name)){
			return;
		}

//		mSprBetting.SetActive (true);
//
//		UtilMgr.AddBackEvent(
//			new EventDelegate (mSprBetting.GetComponent<ScriptBetting> (),
//		                   "CloseWindow"));
	}
	void ClickButton(){
		if (transform.parent.name == "SprHit") {
			transform.parent.FindChild ("BtnHit1").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.FindChild ("BtnHit2").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.FindChild ("BtnHit3").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.parent.FindChild ("SprOut").FindChild ("BtnOut1").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.parent.FindChild ("SprOut").FindChild ("BtnOut2").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.parent.FindChild ("SprOut").FindChild ("BtnOut3").GetComponent<UIButton> ().isEnabled = true;
		} else {
			transform.parent.FindChild ("BtnOut1").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.FindChild ("BtnOut2").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.FindChild ("BtnOut3").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.parent.FindChild ("SprHit").FindChild ("BtnHit1").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.parent.FindChild ("SprHit").FindChild ("BtnHit2").GetComponent<UIButton> ().isEnabled = true;
			transform.parent.parent.FindChild ("SprHit").FindChild ("BtnHit3").GetComponent<UIButton> ().isEnabled = true;

		}
	}
	public void ClearCombos()
	{
		transform.FindChild("SprCombo3").GetComponent<UISprite>().color = ColorComboDisable;
		transform.FindChild("SprCombo2").GetComponent<UISprite>().color = ColorComboDisable;
		transform.FindChild("SprCombo1").GetComponent<UISprite>().color = ColorComboDisable;
	}

	public void SetCombo(int count)
	{
		Vector3 pos;
		switch(count){
		case 3:
			pos = transform.FindChild("SprCombo3").localPosition;
			pos.x -= 46f;
			pos.y += 20f;
			transform.FindChild("SprCombo3").GetComponent<UISprite>().color = ColorComboEnable;
			iTween.MoveFrom(transform.FindChild("SprCombo3").gameObject, iTween.Hash(
				"easetype", "easeOutCirc",
				"islocal", true,
				"time", 0.5f,
				"position", pos
				));
			iTween.ScaleFrom(transform.FindChild("SprCombo3").gameObject, new Vector3(2f, 2f, 2f), 0.5f);

			goto case 2;
		case 2:
			pos = transform.FindChild("SprCombo2").localPosition;
			pos.y -= 46f;
			transform.FindChild("SprCombo2").GetComponent<UISprite>().color = ColorComboEnable;
			iTween.MoveFrom(transform.FindChild("SprCombo2").gameObject, iTween.Hash(
				"easetype", "easeOutCirc",
				"islocal", true,
				"time", 0.5f,
				"position", pos
				));
			iTween.ScaleFrom(transform.FindChild("SprCombo2").gameObject, new Vector3(2f, 2f, 2f), 0.5f);

			goto case 1;
		case 1:
			pos = transform.FindChild("SprCombo1").localPosition;
			pos.x += 46f;
			pos.y += 20f;
			transform.FindChild("SprCombo1").GetComponent<UISprite>().color = ColorComboEnable;
			iTween.MoveFrom(transform.FindChild("SprCombo1").gameObject, iTween.Hash(
				"easetype", "easeOutCirc",
				"islocal", true,
				"time", 0.5f,
				"position", pos
				));
			iTween.ScaleFrom(transform.FindChild("SprCombo1").gameObject, new Vector3(2f, 2f, 2f), 0.5f);

			break;
		}
	}

}
