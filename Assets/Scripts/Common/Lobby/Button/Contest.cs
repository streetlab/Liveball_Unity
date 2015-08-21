using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contest : MonoBehaviour {
	GetGamePresetLineupEvent GGPL;
	List<GamePresetLineupInfo> List ;
	float speed = 200;
	bool One = true;
	float  X ;
	void Start(){
		if (One) {
			//Debug.Log(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").GetComponent<UILabel>().localSize.x);
		//	Debug.Log(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").GetComponent<UILabel>().printedSize.x);
		//	Debug.Log(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").GetComponent<UILabel>().text);
			//Debug.Log(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").GetComponent<UILabel>().x.x);
			X = transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").GetComponent<UILabel>().localSize.x+30f;
			if (X < 650f) {
				X = 650;
			}
			Debug.Log ("X : " + X);
			transform.FindChild ("NewTop").FindChild("TitlePanel").FindChild ("Title1").localPosition = new Vector3 (0,0,0);
			transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").localPosition=new Vector3(0,0,0); 
			transform.FindChild ("NewTop").FindChild("TitlePanel").FindChild ("Title2").localPosition = new Vector3 (X,0,0);
			transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title2").localPosition=new Vector3(X,0,0); 
			One =false;
		}
		Debug.Log ("ON!");
		StartCoroutine("RollingTitle");
	}
	int EnableCount  = 0;
	void OnEnable(){

		if (EnableCount != 0) {
			StartCoroutine("RollingTitle");
		}
		EnableCount++;
	}
	IEnumerator RollingTitle(){

		//Debug.Log ("RollingTitle");

		//Debug.Log ("X : " + X);

		while (true) {
			transform.FindChild("NewTop").FindChild("TitlePanel").FindChild("Title1").localPosition+=new Vector3(-X/speed,0,0);
			transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").localPosition+=new Vector3(-X/speed,0,0); 
			transform.FindChild("NewTop").FindChild("TitlePanel").FindChild("Title2").localPosition+=new Vector3(-X/speed,0,0);
			transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title2").localPosition+=new Vector3(-X/speed,0,0); 
			if(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").localPosition.x <=-X){
				transform.FindChild ("NewTop").FindChild("TitlePanel").FindChild ("Title1").localPosition = new Vector3 (X,0,0);
				transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").localPosition=new Vector3(X,0,0); 
			}
			if(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title2").localPosition.x <=-X){
				transform.FindChild ("NewTop").FindChild("TitlePanel").FindChild ("Title2").localPosition = new Vector3 (X,0,0);
				transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title2").localPosition=new Vector3(X,0,0); 
			}
			yield return new WaitForSeconds(0.04f);
			//Debug.Log(transform.FindChild("ChangeTopBot").FindChild("OldTop").FindChild("TitlePanel").FindChild("Title1").GetComponent<UILabel>().text);
		}
	}
	public void button(){

	//	ex ();
		UserMgr.GameSeq = int.Parse (transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text);
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
	int count= 0;
	void getlineup(){
		count = 0;
		Debug.Log ("GGPL.Response.data.Count : " + GGPL.Response.data.Count);

		if (GGPL.Response.data.Count != 0) {
			for (int i = 0; i<GGPL.Response.data.Count; i++) {
				Debug.Log ("i : " + i);
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST + GGPL.Response.data [i].imagePath + GGPL.Response.data [i].imageName);
				StartCoroutine (GetImage (www, GGPL.Response.data [i]));

			}
		} else {
		

			try{
				UserMgr.LineUpList.Add (transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text,
				                        GGPL.Response.data);
			}	catch{
				Debug.Log("Same Line Up");
			}
			List= UserMgr.LineUpList[transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text];
			SetPresettingSetting ();
			




		}





		}

		void SetPresettingSetting(){
		string Ateam = UtilMgr.GetTeamCode (transform.FindChild ("BG").FindChild("Lteam").GetComponent<UILabel>().text.Replace("[b]",""));
		string Hteam = UtilMgr.GetTeamCode (transform.FindChild ("BG").FindChild("Rteam").GetComponent<UILabel>().text.Replace("[b]",""));
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
		                                                                                                          FindChild ("BG").FindChild ("Lteam").GetComponent<UILabel> ().text,
		                                                                                                          transform.
		                                                                                                          FindChild ("BG").FindChild ("Rteam").GetComponent<UILabel> ().text, "");
		transform.parent.parent.parent.FindChild ("PreSetting").GetComponent<PreSettingCommander> ().Ruby (transform.FindChild("OldBot").
		                                                                                                   FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text,
		                                                                                                   transform.FindChild("OldBot").
		                                                                                                   FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text,
		                                                                                                   transform.FindChild("OldBot").
		                                                                                                   FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text
		                                                                                                   );
		transform.parent.parent.parent.FindChild ("Top").FindChild ("Sub").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
		                                                            <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		

		}

	IEnumerator GetImage(WWW www, GamePresetLineupInfo index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);

		www.LoadImageIntoTexture (tmpTex);
		index.texture = tmpTex;
		count += 1;
		if (count == GGPL.Response.data.Count) {
			try{
				UserMgr.LineUpList.Add (transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text,
				                        GGPL.Response.data);
			}	catch{
				Debug.Log("Same Line Up");
			}
			List= UserMgr.LineUpList[transform.FindChild ("BG").FindChild ("gameSeq").GetComponent<UILabel> ().text];
			SetPresettingSetting ();

		
		}
	
	}


	void ex(){
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
		                                                                                                          FindChild ("BG").FindChild ("Lteam").GetComponent<UILabel> ().text,
		                                                                                                          transform.
		                                                                                                          FindChild ("BG").FindChild ("Rteam").GetComponent<UILabel> ().text, "");
		transform.parent.parent.parent.FindChild ("PreSetting").GetComponent<PreSettingCommander> ().Ruby (transform.FindChild("OldBot").
		                                                                                                   FindChild ("Ruby").FindChild ("Label").GetComponent<UILabel> ().text,
		                                                                                                   transform.FindChild("OldBot").
		                                                                                                   FindChild ("Mileage").FindChild ("Label1").GetComponent<UILabel> ().text,
		                                                                                                   transform.FindChild("OldBot").
		                                                                                                   FindChild ("Mileage").FindChild ("Label2").GetComponent<UILabel> ().text
		                                                                                                   );
		transform.parent.parent.parent.FindChild ("Top").FindChild ("Sub").gameObject.SetActive (false);
		transform.parent.parent.parent.FindChild ("Top").FindChild (transform.root.FindChild ("Scroll").FindChild ("Main").GetComponent
		                                                            <LobbyTopCommander> ().mTopMenuName [0]).gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		

	}
}
