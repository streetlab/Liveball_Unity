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
			gameObject.SetActive (false);
			transform.parent.FindChild ("Scroll View").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("GroupInfoTop").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnMenu").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnBack").gameObject.SetActive (false);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("ProfileSettings").gameObject.SetActive (false);

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


}