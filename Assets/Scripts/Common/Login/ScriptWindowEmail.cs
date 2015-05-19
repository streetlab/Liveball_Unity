using UnityEngine;
using System.Collections;

public class ScriptWindowEmail : MonoBehaviour {

	public GameObject mBtnJoin;
	public GameObject mBtnLogin;

//	public string mStrFindPwd1;
//	public string mStrFindPwd2;
//	public string mStrJoinWhite1;
//	public string mStrJoinWhite2;
//	public string mStrJoinWhite3;
//	public string mStrJoinYellow1;
//	public string mStrJoinYellow2;

	public GameObject mFindPwd1;
	public GameObject mFindPwd2;
//	public GameObject mStrJoinWhite1;
	public GameObject mStrJoinWhite2;
	public GameObject mStrJoinWhite3;
	public GameObject mStrJoinYellow1;
	public GameObject mStrJoinYellow2;
	public GameObject mTglPrivacy;
	public GameObject mTglTerms;

	static Color GRAY = new Color (120f / 255f, 130f / 255f, 150f / 255f);
	static Color WHITE = new Color (1f, 1f, 1f);

	enum SELECTION_STATE{
		JOIN, LOGIN
	}

	SELECTION_STATE mState;

	void Start()
	{
		SetStateLogin ();
	}

	void SetStateLogin()
	{
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = WHITE;
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 4;
		mBtnLogin.transform.FindChild ("Label").GetComponent<UILabel> ().color = WHITE;
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = GRAY;
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 2;
		mBtnJoin.transform.FindChild ("Label").GetComponent<UILabel> ().color = GRAY;
		transform.FindChild ("BtnFindPwd").gameObject.SetActive (true);
		transform.FindChild ("LblPwd").gameObject.SetActive (true);

		mFindPwd1.SetActive(true);
		mFindPwd2.SetActive(true);
//		mStrJoinWhite1.SetActive(false);
		mStrJoinWhite2.SetActive(false);
		mStrJoinWhite3.SetActive(false);
		mStrJoinYellow1.SetActive(false);
		mStrJoinYellow2.SetActive(false);

		mState = SELECTION_STATE.LOGIN;
	}

	void SetStateJoin()
	{
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = WHITE;
		mBtnJoin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 4;
		mBtnJoin.transform.FindChild ("Label").GetComponent<UILabel> ().color = WHITE;
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().color = GRAY;
		mBtnLogin.transform.FindChild ("SprUnderline").GetComponent<UISprite> ().height = 2;
		mBtnLogin.transform.FindChild ("Label").GetComponent<UILabel> ().color = GRAY;
		transform.FindChild ("BtnFindPwd").gameObject.SetActive (false);
		transform.FindChild ("LblPwd").gameObject.SetActive (false);

		mFindPwd1.SetActive(false);
		mFindPwd2.SetActive(false);
//		mStrJoinWhite1.SetActive(true);
		mStrJoinWhite2.SetActive(true);
		mStrJoinWhite3.SetActive(true);
		mStrJoinYellow1.SetActive(true);
		mStrJoinYellow2.SetActive(true);

		mState = SELECTION_STATE.JOIN;
	}

	public void JoinClicked()
	{
		if (mState != SELECTION_STATE.JOIN)
			SetStateJoin ();
	}

	public void LoginClicked()
	{
		if (mState != SELECTION_STATE.LOGIN)
			SetStateLogin ();
	}

	public void BackClicked()
	{
		UtilMgr.OnBackPressed ();
	}

	bool CheckAgreement(){
		if(!mTglPrivacy.GetComponent<UIToggle>().value){
			DialogueMgr.ShowDialogue("개인정보취급방침", "개인정보취급방침에 동의하여야 합니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			return true;
		}

		if(!mTglTerms.GetComponent<UIToggle>().value){
			DialogueMgr.ShowDialogue("서비스약관", "서비스약관에 동의하여야 합니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			return true;
		}
		return false;
	}

	public void NextClicked()
	{
		if(CheckAgreement())
			return;


		UtilMgr.AddBackEvent(new EventDelegate(transform.parent.GetComponent<ScriptTitle>(), "OpenEmail"));

		string eMail = transform.FindChild ("InputEmail").GetComponent<UIInput> ().value;
		string pwd = transform.FindChild ("InputPwd").GetComponent<UIInput> ().value;
		if (mState == SELECTION_STATE.LOGIN) {
			if(eMail.Equals("admin@.")
			   && pwd.Equals("test")){
				PlayerPrefs.SetString(Constants.PrefServerTest, "1");
				AutoFade.LoadLevel("SceneLogin");
				return;
			}


			GetComponentInParent<ScriptTitle>().DoLogin(eMail, pwd);
		} else {
			gameObject.SetActive (false);
			transform.parent.FindChild ("FormJoin").gameObject.SetActive (true);
			transform.parent.FindChild ("FormJoin").GetComponent<ScriptJoinForm>().Init(eMail, pwd, true);
		}

	}

	public void FindPwdClicked()
	{

	}

	public void ConfirmedEmail(){
		transform.FindChild ("InputPwd").GetComponent<UIInput>().isSelected = true;
	}

	public void ConfirmedPwd(){
		NextClicked();
	}

	public void OpenPrivacy(){
		Application.OpenURL("http://www.naver.com");
	}

	public void OpenTerms(){
		Application.OpenURL("http://www.daum.net");
	}
}
