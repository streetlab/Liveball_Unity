using UnityEngine;
using System.Collections;

public class ProfileSettingButten : MonoBehaviour {
	GameObject TopPanel,TF_Profile;
	public void onhit(){
		TopPanel = transform.parent.parent.parent.parent.parent.parent.FindChild ("Top").FindChild("Panel").gameObject;
		TF_Profile = transform.parent.parent.parent.parent.parent.gameObject;
		TopPanel.transform.FindChild("BtnBack").gameObject.SetActive(true);
		TopPanel.transform.FindChild("BtnMenu").gameObject.SetActive(false);
		TopPanel.transform.FindChild("GroupInfoTop").gameObject.SetActive(false);
		TopPanel.transform.FindChild("ProfileSettings").gameObject.SetActive(false);
		TF_Profile.transform.FindChild ("ProfileSetting").gameObject.SetActive(false);
	
		if(name == "N>"){
			TopPanel.transform.FindChild("SetnamePages").gameObject.SetActive(true);
			TF_Profile.transform.FindChild ("SetNamePage").gameObject.SetActive(true);
			TF_Profile.transform.FindChild ("SetNamePage").FindChild("length").FindChild("Label").GetComponent<UILabel>().text = "0/10";
		}else if(name == "S>"){
			TopPanel.transform.FindChild("SetStatePages").gameObject.SetActive(true);
			TF_Profile.transform.FindChild ("SetStatePage").gameObject.SetActive(true);
			TF_Profile.transform.FindChild ("SetStatePage").FindChild("length").FindChild("Label").GetComponent<UILabel>().text = "0/20";
		}else if(name == "T>"){
			TopPanel.transform.parent.gameObject.SetActive (false);
			TopPanel.transform.parent.parent.FindChild("SelectTeam").gameObject.SetActive(true);

		}

	//	TF_Profile.transform.FindChild ("ProfileSetting").gameObject.SetActive(false);

	}
}
