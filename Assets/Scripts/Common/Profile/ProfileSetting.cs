using UnityEngine;
using System.Collections;

public class ProfileSetting : MonoBehaviour {
	GetProfileEvent mProfileEvent;

	// Use this for initialization
	public ProfileManager PM;




	public void settingpage(){
		//SetMemberName ("nonnames");
		//SetMemberTeam ("SK");
		//Debug.Log("settingpage");
	
		transform.parent.FindChild ("Scroll View").gameObject.SetActive (false);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("GroupInfoTop").gameObject.SetActive(false);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnMenu").gameObject.SetActive(false);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnBack").gameObject.SetActive(true);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("ProfileSettings").gameObject.SetActive(true);
		gameObject.SetActive (true);
		PM.SetSettingPage ();
	}

	

	public void back(){
		if (gameObject.activeSelf) {

			if(PM.Sett){
				DialogueMgr.ShowDialogue ("프로필 편집", "변경된 프로필을 저장하시겠습니까?" , DialogueMgr.DIALOGUE_TYPE.YesNo , DialogueHandler);
			}else{
			gameObject.SetActive (false);
			transform.parent.FindChild ("Scroll View").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("GroupInfoTop").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnMenu").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnBack").gameObject.SetActive (false);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("ProfileSettings").gameObject.SetActive (false);
			PM.SetMainPage();
			}


		}
	else{

			if(transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetnamePages").gameObject.activeSelf){
				transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetnamePages").gameObject.SetActive(false);
				transform.parent.FindChild ("SetNamePage").gameObject.SetActive (false);
			}else if(transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetStatePages").gameObject.activeSelf){
				transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetStatePages").gameObject.SetActive(false);
				transform.parent.FindChild ("SetStatePage").gameObject.SetActive (false);
			}
			transform.parent.FindChild ("SetNamePage").FindChild("Input").GetComponent<UIInput>().value = "";
			transform.parent.FindChild ("SetStatePage").FindChild("Input").GetComponent<UIInput>().value = "";
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("GroupInfoTop").gameObject.SetActive(false);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnMenu").gameObject.SetActive(false);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnBack").gameObject.SetActive(true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("ProfileSettings").gameObject.SetActive(true);
			
			gameObject.SetActive (true);

	}


	}
	//DialogueMgr.ShowDialogue ("구매 확인", s , DialogueMgr.DIALOGUE_TYPE.YesNo , DialogueHandler);
	void DialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			PM.SetMemberInfoOut ();
		} else {
			PM.Sett = false;
		}
		Save ();
		
	}

	public void Save(){
		gameObject.SetActive (false);
		transform.parent.FindChild ("Scroll View").gameObject.SetActive (true);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("GroupInfoTop").gameObject.SetActive (true);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnMenu").gameObject.SetActive (true);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnBack").gameObject.SetActive (false);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("ProfileSettings").gameObject.SetActive (false);
		PM.SetMainPage();
		PM.SetSame ();
	}
	
}