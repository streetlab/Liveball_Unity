using UnityEngine;
using System.Collections;

public class preSettingItem : MonoBehaviour {

	//게임중 프리셋 수정 클릭시 나오는 팝업데이터
	public void ButtonPlaying(){
		Debug.Log (name);
		GameObject Batting =
			transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").gameObject;
		//Key is this.name
		//		if (!CheckPreset ()) {
		if (int.Parse (transform.parent.name [5].ToString ()) > 4) {
			transform.parent.parent.transform.localPosition = new Vector3 (0, (int.Parse (transform.parent.name [5].ToString ()) - 4) * 95);
		}
		
		transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").GetComponent<BattingCommander> ().SetBatting (this.gameObject);
		
		transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").gameObject.SetActive (true);
		//	}

		for (int i = 0; i<UserMgr.LineUpList[UserMgr.GameSeq.ToString()].Count; i++) {
			if(UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].player == transform.FindChild("Label").GetComponent<UILabel>().text){
				Debug.Log("SetPhoto");
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Photo").FindChild("PhotoPanel").FindChild("Photo")
					.GetComponent<UITexture>().mainTexture = UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].texture;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Top").FindChild("Name").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].player;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("AVG").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].hitAvg;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("HR").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].HR;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("RBI").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].RBI;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("OB").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].BB;




				Batting.transform.FindChild("SprHit").FindChild("BtnHit1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit3").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut3").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";






				Batting.transform.FindChild("SprHit").FindChild("BtnHit1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" + UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].H1B + "］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].H2B+ "］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit3").FindChild("LblGP").GetComponent<UILabel>().text
					="［" + UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].HHR+ "］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].PGO+ "］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].PFO+ "］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut3").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].PK+ "］";
				break;
			}
		}
	}
	//게임 시작 전 프리셋 수정 클릭시 나오는 팝업데이터
	public void Button(){
		//Key is this.name
		GameObject Batting =
			transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Batting").gameObject;

	//	if (!CheckPreset ()) {
			if (int.Parse (transform.parent.name [5].ToString ()) > 4) {
				transform.parent.parent.transform.localPosition = new Vector3 (0, (int.Parse (transform.parent.name [5].ToString ()) - 4) * 95);
			}

			Batting.GetComponent<BattingCommander> ().SetBatting (this.gameObject);

			Batting.SetActive (true);
	//	} else {
	//		transform.parent.parent.parent.parent.parent.FindChild ("Bot").FindChild ("Sumit").gameObject.SetActive (true);
	//	}


		for (int i = 0; i<UserMgr.LineUpList[UserMgr.GameSeq.ToString()].Count; i++) {
			if(UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].player == transform.FindChild("Label").GetComponent<UILabel>().text){
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Photo").FindChild("PhotoPanel").FindChild("Photo")
					.GetComponent<UITexture>().mainTexture = UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].texture;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Top").FindChild("Name").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].player;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("AVG").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].hitAvg;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("HR").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].HR;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("RBI").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].RBI;
				Batting.transform.FindChild("Hiter").FindChild("BG").FindChild("Mid").FindChild("OB").FindChild("Label").GetComponent<UILabel>().text
					= UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].BB;





				Batting.transform.FindChild("SprHit").FindChild("BtnHit1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit3").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut3").FindChild("LblGP").GetComponent<UILabel>().text
					= "［0］";




				Batting.transform.FindChild("SprHit").FindChild("BtnHit1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" + UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].H1B + "］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].H2B+ "］";
				Batting.transform.FindChild("SprHit").FindChild("BtnHit3").FindChild("LblGP").GetComponent<UILabel>().text
					="［" + UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].HHR+ "］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut1").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].PGO+ "］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut2").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].PFO+ "］";
				Batting.transform.FindChild("SprOut").FindChild("BtnOut3").FindChild("LblGP").GetComponent<UILabel>().text
					= "［" +UserMgr.LineUpList[UserMgr.GameSeq.ToString()][i].PK+ "］";
				break;
			}
		}
		
	

	}







	bool CheckPreset(){
		bool b = true;
		GameObject 
			G= 
				transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Mid").FindChild("Scroll View")
				.FindChild("Position").gameObject;
		for (int i = 0; i<G.transform.childCount; i++) {
			if(!
			G.transform.FindChild("Item " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()).FindChild("L_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				b = false;
				break;
			}
			if(!
			   G.transform.FindChild("Item " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()).FindChild("R_name " + (i+1).ToString()+"_pre").
			   FindChild("use").gameObject.activeSelf){
				b = false;
				break;
			}

		}
		return b;
	}
}
