using UnityEngine;
using System.Collections;

public class ScriptMainMenuLeft : MonoBehaviour {

	public GameObject mBtnTeamHome;
	public GameObject mBtnIlovebaseball;
	public GameObject mBtnGameHome;
	public GameObject mBtnCards;
	public GameObject mBtnIamPlayer;
	public GameObject mBtnTeamRanking;
	public GameObject mBtnRecord;
	public GameObject mBtnRanking;
	public GameObject mBtnProfile;
	public GameObject mBtnItem;
	public GameObject mBtnNotice;
	public GameObject mBtnSettings;

	void Start(){
		SetProfile ();
		SetBtnDisable ();
	}
	void SetProfile(){
		GameObject LeftProfile = transform.FindChild("Scroll").FindChild ("LeftMenuProfile").gameObject;
		LeftProfile.transform.FindChild ("Profile").FindChild ("UserName").GetComponent<UILabel> ().text = 
			UserMgr.UserInfo.memberName;
		LeftProfile.transform.FindChild ("Profile").FindChild ("TeamName").GetComponent<UILabel> ().text = 
			UserMgr.UserInfo.GetTeamName();
		LeftProfile.transform.FindChild ("UserImage").FindChild ("img").GetComponent<UITexture> ().mainTexture = 
			UserMgr.UserInfo.Textures;
	}

	void SetBtnsEnable(){
		mBtnTeamHome.GetComponent<UIButton> ().isEnabled = true;
		mBtnIlovebaseball.GetComponent<UIButton> ().isEnabled = true;
		mBtnGameHome.GetComponent<UIButton> ().isEnabled = true;
		mBtnCards.GetComponent<UIButton> ().isEnabled = true;
		mBtnIamPlayer.GetComponent<UIButton> ().isEnabled = true;
		mBtnRanking.GetComponent<UIButton> ().isEnabled = true;
		mBtnProfile.GetComponent<UIButton> ().isEnabled = true;
		mBtnItem.GetComponent<UIButton> ().isEnabled = true;
		mBtnNotice.GetComponent<UIButton> ().isEnabled = true;
		mBtnSettings.GetComponent<UIButton> ().isEnabled = true;
		mBtnTeamRanking.GetComponent<UIButton> ().isEnabled = true;
		mBtnRecord.GetComponent<UIButton> ().isEnabled = true;
	}

	void SetBtnDisable(){
		if (Application.loadedLevelName.Equals ("SceneLoveBaseball")) {
			mBtnIlovebaseball.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnIlovebaseball.GetComponent<UIButton> ().isEnabled = false;
			//mBtnIlovebaseball.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneTeamHome")) {
			mBtnTeamHome.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnTeamHome.GetComponent<UIButton> ().isEnabled = false;
			//mBtnTeamHome.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneMain")) {
			mBtnGameHome.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnGameHome.GetComponent<UIButton> ().isEnabled = false;
			//mBtnGameHome.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneCards")) {
			mBtnCards.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnCards.GetComponent<UIButton> ().isEnabled = false;
			//mBtnCards.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneRanking")) {
			mBtnRanking.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnRanking.GetComponent<UIButton> ().isEnabled = false;
			//mBtnRanking.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneProfile")) {
			mBtnProfile.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnProfile.GetComponent<UIButton> ().isEnabled = false;
			//mBtnProfile.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneItems")) {
			mBtnItem.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
		//	mBtnItem.GetComponent<UIButton> ().isEnabled = false;
			//mBtnItem.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneSettings")) {
			mBtnSettings.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnSettings.GetComponent<UIButton> ().isEnabled = false;
			//mBtnSettings.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		} else if (Application.loadedLevelName.Equals ("SceneTeamRanking")) {
			mBtnTeamRanking.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnTeamRanking.GetComponent<UIButton> ().isEnabled = false;
			//mBtnTeamRanking.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		}else if(Application.loadedLevelName.Equals("SceneRecord")){
			mBtnRecord.GetComponent<UIButton> ().defaultColor = new Color (66f / 255f, 69f / 255f, 76f / 255f);
			//mBtnRecord.GetComponent<UIButton> ().isEnabled = false;
			//mBtnRecord.transform.GetChild (0).GetComponent<UISprite> ().color = new Color (134f / 255f, 220f / 255f, 1, 1);
		}



	}

	public void BtnClicked(string name)
	{
		SetBtnsEnable ();
		Debug.Log (Application.loadedLevelName);
		switch(name)
		{
		case "BtnTeamHome":

			if(!Application.loadedLevelName.Equals("SceneTeamHome")){
//				if(Application.platform == RuntimePlatform.IPhonePlayer){
//					Application.OpenURL("");
//				} else{
					AutoFade.LoadLevel("SceneTeamHome", 0f, 1f);
//				}

			} else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			break;
		case "BtnGameHome":
			//if(!Application.loadedLevelName.Equals("SceneMain"))
	
				ScriptMainTop.LandingState = 4;
			UtilMgr.SelectTeam = "";
		
				AutoFade.LoadLevel("SceneMain", 0f, 1f);
			
			break;
		case "BtnCards":
			if(!Application.loadedLevelName.Equals("SceneCards"))
				AutoFade.LoadLevel("SceneCards", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			//DialogueMgr.ShowDialogue("준비중", "추후 업데이트 됩니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			break;
		case "BtnIloveBaseball":
			if(!Application.loadedLevelName.Equals("SceneLoveBaseball"))
				AutoFade.LoadLevel("SceneLoveBaseball", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			break;
		case "BtnRanking":
			if(!Application.loadedLevelName.Equals("SceneRanking"))
				AutoFade.LoadLevel("SceneRanking", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			break;
		case "BtnProfile":
			if(!Application.loadedLevelName.Equals("SceneProfile"))
				AutoFade.LoadLevel("SceneProfile", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			break;
		case "BtnItem":
			if(!Application.loadedLevelName.Equals("SceneItems"))
				AutoFade.LoadLevel("SceneItems", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			break;
		case "BtnNotice":

			break;
		case "BtnSettings":
			if(!Application.loadedLevelName.Equals("SceneSettings"))
				AutoFade.LoadLevel("SceneSettings", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			
			break;
		case "BtnTeamRanking":
			if(!Application.loadedLevelName.Equals("SceneTeamRanking"))
				AutoFade.LoadLevel("SceneTeamRanking", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			break;
		case "BtnRecord":
			if(!Application.loadedLevelName.Equals("SceneRecord"))
				AutoFade.LoadLevel("SceneRecord", 0f, 1f);
			else
				transform.parent.FindChild("Right").GetComponent<ScriptMainMenuRight>().ALLBack();
			break;
		}
	}


}
