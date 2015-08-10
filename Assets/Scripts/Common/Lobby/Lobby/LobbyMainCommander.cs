using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LobbyMainCommander : MonoBehaviour {
	public static int MenuStatus = 1; 
	public float RatioTop;
	public float RatioFC;
	public float RatioNC;
	public float RatioBot;
	Dictionary<string,float> HightList = new Dictionary<string, float> ();

	// Use this for initialization
	void Awake(){
		if (UserMgr.ContestStatus == 2) {
			transform.FindChild("Top").FindChild("Preset").FindChild("Label").GetComponent<UILabel>().text = "라이브";
		}
		MenuStatus = 1; 
		for (int i = 0; i<GetComponent<LobbyTopCommander>().mTopMenuName.Length; i++) {
			transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Bar").gameObject.SetActive(false);
			transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
			transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[i]).FindChild("Num").GetComponent<UILabel>().color = new Color(1,1,1,0.5f);
		}
		transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[0]).FindChild("Bar").gameObject.SetActive(true);
		transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[0]).FindChild("Label").GetComponent<UILabel>().color = new Color(1,1,1,1);
		transform.FindChild("Top").FindChild(GetComponent<LobbyTopCommander>().mTopMenuName[0]).FindChild("Num").GetComponent<UILabel>().color = new Color(1,1,1,1);
		for (int i = 0; i<GetComponent<LobbyAddSub>().SubMenuName.Length; i++) {
			//                        transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild("Label")
			//                            .FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").FindChild("Menu 0").
			//                                gameObject.SetActive(true);
			//            transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild("Label")
			//                .FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").FindChild("Menu 0").
			//                    gameObject.SetActive(false);
			
			transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild(
				GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").gameObject.SetActive(false);
		}
		if (transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("Arrow") != null) {
			transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("Arrow").
				GetComponent<UISprite>().color = Color.yellow;
			transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("white").gameObject
				.SetActive(true);
			transform.FindChild ("Top").FindChild ("Sub").FindChild ("People").FindChild ("PeopleBox").FindChild ("Menu 0").FindChild("yellow").gameObject
				.SetActive(true);
		}
		transform.FindChild ("Top").FindChild ("Sub").FindChild("BG_B").gameObject.SetActive (false);
		transform.FindChild ("Top").FindChild ("Sub").gameObject.SetActive (false);
		transform.FindChild ("Gift").gameObject.SetActive (true);
		transform.FindChild ("Gift").FindChild ("GiftButton").GetComponent<Gift> ().Off ();
		transform.FindChild ("Gift").FindChild ("GiftButton").GetComponent<Gift> ().Button ();
		GetComponent<LobbyNCCommander> ().CreatCItem ();
		transform.FindChild ("Nomal Contest").gameObject.SetActive (true);
		transform.FindChild ("PreSet Contest").GetComponent<PresetContestCommander> ().CreatItem ();
		transform.FindChild ("PreSet Contest").gameObject.SetActive (false);

		LobbyBotCommander.mBtnState = LobbyBotCommander.BtmState.Main;
	}
	void CSComplete(){
		Debug.Log("CSComplete");
	}
	public void Ratio () {
		try{
			HightList.Add ("Top",GetComponent<LobbyTopCommander>().TopHight);
			HightList.Add ("FC",GetComponent<LobbyFCCommander>().FCHight);
			HightList.Add ("NC",GetComponent<LobbyNCCommander>().NCHight);
			HightList.Add ("Bot",GetComponent<LobbyBotCommander>().BotHeight);
		}catch{
			Debug.Log("The \"HightList\" already exists.");
		}
		float Sum = RatioTop + RatioFC + RatioNC + RatioBot;
		GetComponent<LobbyTopCommander>().TopHight = (RatioTop / Sum)*1280f;
		GetComponent<LobbyFCCommander>().FCHight = (RatioFC / Sum)*1280f;
		GetComponent<LobbyNCCommander>().NCHight = (RatioNC / Sum)*1280f;
		GetComponent<LobbyBotCommander>().BotHeight = (RatioBot / Sum)*1280f;
		Debug.Log ("Ratio Setting complete");
		
	}
	public void HightListClear(){
		HightList.Clear ();
		Debug.Log ("HightList Clear");
	}
	public void GetHightList(){
		if (HightList.Count > 0) {
			GetComponent<LobbyTopCommander>().TopHight = HightList["Top"];
			GetComponent<LobbyFCCommander>().FCHight = HightList["FC"];
			GetComponent<LobbyNCCommander>().NCHight = HightList["NC"];
			GetComponent<LobbyBotCommander>().BotHeight = HightList["Bot"];
			Debug.Log("HightList Load");
		} else {
			Debug.Log("HightList is empty");
		}
	}
	
	
	public void AllCreate(){
		GetComponent<LobbyTopCommander> ().CreateTop ();
		GetComponent<LobbyFCCommander> ().CreateFC ();
		GetComponent<LobbyNCCommander> ().CreateNC ();
		GetComponent<LobbyBotCommander> ().CreateBot ();
		Debug.Log ("ALL Create Complete");
	}
	
	public void AllReset(){
		Init ();
		
	}
	// Update is called once per frame
	
	void Init() {
		#if UNITY_EDITOR_OSX 
		bool option = UnityEditor.EditorUtility.DisplayDialog(
			"Warning!",
			"The traditional ALL GameObject is deleted when you reset.",
			"ReSet",
			"Cancle");
		if (option) {
			
			if(transform.FindChild("Top")!=null){
				DestroyImmediate(transform.FindChild("Top").gameObject);
				Debug.Log("Destroy Top");
			}
			if(transform.FindChild("Featured Contest")!=null){
				DestroyImmediate(transform.FindChild("Featured Contest").gameObject);    
				Debug.Log("Destroy Featured Contest");
			}
			if(transform.FindChild("Nomal Contest")!=null){
				DestroyImmediate(transform.FindChild("Nomal Contest").gameObject);    
				Debug.Log("Destroy Nomal Contest");
			}
			if(transform.FindChild("Bot")!=null){
				DestroyImmediate(transform.FindChild("Bot").gameObject);
				Debug.Log("Destroy Bot");
			}
			AllCreate();
			Debug.Log ("ALL Reset Complete");
		} else {
			Debug.Log("Cancle");
		}
		#endif 
	}
	
}
