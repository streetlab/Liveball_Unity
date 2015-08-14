using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contest : MonoBehaviour {
	GetGamePresetLineupEvent GGPL;
	List<GamePresetLineupInfo> List ;
	public void button(){
		int presetCount = 0;
		for (int a = 0; a<UserMgr.PresetList.Count; a++) {
			if (UserMgr.PresetList [a].contestSeq == int.Parse (transform.FindChild ("BG").FindChild ("ContestSeq").GetComponent<UILabel> ().text)) {
				Debug.Log ("UserMgr.PresetList[a].contestSeq : " + UserMgr.PresetList [a].contestSeq);
				presetCount += 1;
				
			}
		}
		if (int.Parse (transform.FindChild ("BG").FindChild ("MultiEntry").GetComponent<UILabel> ().text) == presetCount) {
			DialogueMgr.ShowDialogue ("멀티 엔트리 등록취소", "멀티 엔트리 최대 가능 개수 :"+transform.FindChild ("BG").FindChild ("MultiEntry").GetComponent<UILabel> ().text+" 개\n이 컨테스트에 등록가능한 멀티엔트리 개수를 초과하였습니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);
		
		
		} else {
			try{
		List= UserMgr.LineUpList[transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text];
				SetPresettingSetting();
			
			}catch{

			GGPL = new GetGamePresetLineupEvent(new EventDelegate(this,"getlineup"));
			NetMgr.GetGamePresetLineup(int.Parse (transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text),GGPL);
		
			}

			}
	}

	void getlineup(){
	
		try{
			UserMgr.LineUpList.Add (transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text,
			                        GGPL.Response.data);
		}	catch{
				Debug.Log("Same Line Up");
			}
		List= UserMgr.LineUpList[transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text];
		SetPresettingSetting ();
		}

		void SetPresettingSetting(){
		string Ateam = UtilMgr.GetTeamCode (transform.FindChild ("Team").FindChild("LT").GetComponent<UILabel>().text.Replace("[b]",""));
		string Hteam = UtilMgr.GetTeamCode (transform.FindChild ("Team").FindChild("RT").GetComponent<UILabel>().text.Replace("[b]",""));
		Debug.Log ("List.Count : " + List.Count);

		for (int i = 0; i<List.Count; i++) {
			if(List[i].team == Ateam){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid")
					.FindChild("Scroll View").FindChild("Position").FindChild("Item " +List[i].lineup.ToString())
						.FindChild("L_name "+ List[i].lineup.ToString()).FindChild("Label")
						.GetComponent<UILabel>().text = List[i].player;

			}else if(List[i].team == Hteam){
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid")
					.FindChild("Scroll View").FindChild("Position").FindChild("Item " +List[i].lineup.ToString())
						.FindChild("R_name "+ List[i].lineup.ToString()).FindChild("Label")
						.GetComponent<UILabel>().text = List[i].player;
			}
		}


		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Gift").gameObject.SetActive (false);
		UserMgr.CurrentContestSeq = int.Parse (transform.FindChild ("BG").FindChild ("ContestSeq").GetComponent<UILabel> ().text);
		UserMgr.CurrentContestMultiEntry = int.Parse (transform.FindChild ("BG").FindChild ("MultiEntry").GetComponent<UILabel> ().text);
		UserMgr.CurrentContestTotalEntry = int.Parse (transform.FindChild ("BG").FindChild ("TotalEntry").GetComponent<UILabel> ().text);
		transform.parent.parent.parent.FindChild ("Nomal Contest").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("PreSetting").gameObject.SetActive (true);
		transform.parent.parent.parent.FindChild ("PreSetting").FindChild ("Bot").FindChild ("Batting").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("PreSetting").FindChild ("Bot").FindChild ("Sumit").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("PreSetting").FindChild ("Mid").FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
		transform.parent.parent.parent.FindChild ("PreSetting").GetComponent<PreSettingCommander> ().Mode = "Add";
		transform.parent.parent.parent.FindChild ("PreSetting").GetComponent<PreSettingCommander> ().SetTeamName (transform.
		                                                                                                          FindChild ("Team").FindChild ("LT").GetComponent<UILabel> ().text,
		                                                                                                          transform.
		                                                                                                          FindChild ("Team").FindChild ("RT").GetComponent<UILabel> ().text, "");
		transform.parent.parent.parent.FindChild ("PreSetting").GetComponent<PreSettingCommander> ().Ruby (transform.
		                                                                                                   FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text,
		                                                                                                   transform.
		                                                                                                   FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text,
		                                                                                                   transform.
		                                                                                                   FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text
		                                                                                                   );
		transform.parent.parent.parent.FindChild ("Top").FindChild ("Sub").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
		                                                            <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		

		}
}
