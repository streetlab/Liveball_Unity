using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TopMenu : MonoBehaviour {
	public GameObject BlackBuleBar;
	PresetListEvent presetListEvent;
	HistoryListEvent HistoryEvent;
	public void Button(){
		int MenuStatus = LobbyMainCommander.MenuStatus;
		AllBarDisable ();
		for (int i = 0; i<transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName.Length; i++) {

			if(name == transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]){
				LobbyMainCommander.MenuStatus = i+1;
				transform.FindChild("Bar").gameObject.SetActive(true);
				transform.FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
				transform.FindChild("Num").GetComponent<UILabel>().color = new Color(1,1,1,1);
				break;
			}
		}
		Debug.Log (name + " : MenuStatus " + LobbyMainCommander.MenuStatus);

		if (MenuStatus == LobbyMainCommander.MenuStatus) {
			if (transform.parent.FindChild ("Sub").gameObject.activeSelf) {
				transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyAddSub> ().DisableSub ();
				transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Up");
			} else {
				transform.parent.FindChild ("Sub").gameObject.SetActive (true);
				transform.root.FindChild("Scroll").FindChild ("Main").FindChild ("Gift").FindChild ("GiftButton").GetComponent<Gift> ().Off ();
				transform.root.FindChild("Scroll").FindChild ("Main").FindChild ("Gift").gameObject.SetActive (false);
				transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Down");
			}
		} else {
	
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild ("Gift").FindChild ("GiftButton").GetComponent<Gift> ().Off ();
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild ("Gift").gameObject.SetActive (false);

			transform.parent.FindChild ("Sub").gameObject.SetActive (true);
			transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyAddSubInSub> ().DisableSub ();
			transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyAddSub> ().ResetAddSub ();
			transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyAddSubInSub> ().ResetSubInSub ();
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Top").FindChild("Sub").FindChild("BG_B").gameObject.SetActive(false);
			transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Down");
		}
		if (LobbyMainCommander.MenuStatus > 1) {
			transform.parent.FindChild ("Sub").gameObject.SetActive (false);
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild ("Gift").FindChild ("GiftButton").GetComponent<Gift> ().Off ();
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild ("Gift").gameObject.SetActive (false);
			transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Up");
			if(LobbyMainCommander.MenuStatus == 2){

				if(UserMgr.UserInfo.memSeq!=null){
				presetListEvent = new PresetListEvent(new EventDelegate(this, "GetPresetList"));
				NetMgr.GetPresetList(presetListEvent);
				}else{
					transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Nomal Contest").gameObject.SetActive(false);
					transform.root.FindChild("Scroll").FindChild ("Main").FindChild("PreSet Contest").gameObject.SetActive(true);
					transform.root.FindChild("Scroll").FindChild ("Main").FindChild("History Contest").gameObject.SetActive(false);
				}
				//transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Nomal Contest").gameObject.SetActive(false);
				//transform.root.FindChild("Scroll").FindChild ("Main").FindChild("PreSet Contest").gameObject.SetActive(true);

			}else if(LobbyMainCommander.MenuStatus == 3){


				if(UserMgr.UserInfo.memSeq!=null){
					HistoryEvent = new HistoryListEvent(new EventDelegate(this, "GetHistoryList"));
					NetMgr.GetHistoryList(HistoryEvent);
				}else{
					transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Gift").gameObject.SetActive(false);
					transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Nomal Contest").gameObject.SetActive(false);
					transform.root.FindChild("Scroll").FindChild ("Main").FindChild("PreSet Contest").gameObject.SetActive(false);
					transform.root.FindChild("Scroll").FindChild ("Main").FindChild("History Contest").gameObject.SetActive(true);
				}




			}
		} else {
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Gift").gameObject.SetActive(true);
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Nomal Contest").gameObject.SetActive(true);
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild("PreSet Contest").gameObject.SetActive(false);
			transform.root.FindChild("Scroll").FindChild ("Main").FindChild("History Contest").gameObject.SetActive(false);
		}
	}



	void AllBarDisable(){
		for (int i = 0; i<transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName.Length; i++) {
			transform.parent.FindChild(transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Bar").
				gameObject.SetActive(false);
			transform.parent.FindChild(transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Label").
				GetComponent<UILabel>().color = new Color(1f,1f,1f,0.5f);
			transform.parent.FindChild(transform.root.FindChild("Scroll").FindChild("Main").GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Num").
				GetComponent<UILabel>().color = new Color(1f,1f,1f,0.5f);
		} 
		if (BlackBuleBar != null) {
			BlackBuleBar.GetComponent<UISlider>().value = 0;
			BlackBuleBar.transform.FindChild("InSlider").GetComponent<UISlider>().value = 0;
			BlackBuleBar.transform.parent.FindChild("Menu 0").GetComponent<UILabel>().text = "모든 입장료";
				 
		}
		transform.root.FindChild("Scroll").FindChild ("Main").GetComponent<LobbyNCCommander> ().NCUpDown ("Up");
	}

	void GetHistoryList(){
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("History Contest").FindChild ("None").gameObject.SetActive (true);
		List<PresetListInfo> historylist;
		historylist = HistoryEvent.Response.data;
		if (historylist.Count != 0) {
			transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("History Contest").FindChild ("None").gameObject.SetActive (false);
		}
		//Load PresetList
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild("History Contest").GetComponent<HistoryContestCommander> ().CreatItem (historylist);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Nomal Contest").gameObject.SetActive(false);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("PreSet Contest").gameObject.SetActive(false);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("History Contest").gameObject.SetActive(true);
	}




	void GetPresetList(){
		List<PresetListInfo> presetlist;
		presetlist = presetListEvent.Response.data;
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("PreSet Contest").FindChild ("None").gameObject.SetActive (true);
		for (int i = 0; i<presetlist.Count; i++) {
			if(presetlist [i].contestStatus==2){
				UserMgr.ContestStatus =  presetlist [i].contestStatus;
				break;
			}
		}
		if (presetlist.Count != 0) {
			transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("PreSet Contest").FindChild ("None").gameObject.SetActive (false);
		}		//UserMgr.ContestStatus = presetlist [0].contestStatus;
		if (UserMgr.ContestStatus == 2) {
			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").FindChild("Label").GetComponent<UILabel>().text = "라이브";
		}
		//Load PresetList
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild("PreSet Contest").GetComponent<PresetContestCommander> ().CreatItem (presetlist);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("Nomal Contest").gameObject.SetActive(false);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("PreSet Contest").gameObject.SetActive(true);
		transform.root.FindChild("Scroll").FindChild ("Main").FindChild("History Contest").gameObject.SetActive(false);
	}
}