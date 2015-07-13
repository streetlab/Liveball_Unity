using UnityEngine;
using System.Collections;

public class MainRightButten : MonoBehaviour {
GameObject Top;
	//char [] array;
	public void Onhit(){
		if (UserMgr.Schedule!=null) {
			if(UserMgr.Schedule.myEntryFee!="0"){
				if(ScriptMainTop.LandingState!=3){
			DialogueMgr.ShowDialogue ("게임 종료", "현 랭킹전을 나간 동안에는\n[ff0000]문제를 받을 수 없습니다.[-] 동의하십니까?", DialogueMgr.DIALOGUE_TYPE.YesNo , MileageDialogueHandler);
				}else{
					click();
				}
				}else{
				click();
			}
		} else {
			click();
		}
	}
	void MileageDialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			if (transform.parent.parent.parent.parent.parent.parent.parent.name == "UI Root") {
				Top = transform.parent.parent.parent.parent.parent.parent.parent.transform.FindChild ("Top").gameObject;
			} else
			if (transform.parent.parent.parent.parent.parent.parent.parent.parent.name == "UI Root") {
				Top = transform.parent.parent.parent.parent.parent.parent.parent.parent.transform.FindChild ("Top").gameObject;
			} 
			//transform.parent.parent.parent.parent.GetComponent<ScriptMainMenuRight> ().buttening (int.Parse( array[4].ToString()));
			Debug.Log (transform.FindChild ("Code").GetComponent<UILabel> ().text + " is teamcode");
			string result = transform.parent.parent.parent.FindChild ("Panel").FindChild ("Label").GetComponent<UILabel> ().text;
			result = result [8].ToString () + result [9].ToString ();
			Top.GetComponent<ScriptMainTop> ().GoGame (transform.FindChild ("Code").GetComponent<UILabel> ().text,
			                                           transform.FindChild ("Statue").GetComponent<UILabel> ().text, result);
		}
		
	}
	void click(){
		
		
		//array = gameObject.ToString ().ToCharArray ();
		if (transform.parent.parent.parent.parent.parent.parent.parent.name == "UI Root") {
			Top = transform.parent.parent.parent.parent.parent.parent.parent.transform.FindChild ("Top").gameObject;
		} else
		if (transform.parent.parent.parent.parent.parent.parent.parent.parent.name == "UI Root") {
			Top = transform.parent.parent.parent.parent.parent.parent.parent.parent.transform.FindChild ("Top").gameObject;
		} 
		//transform.parent.parent.parent.parent.GetComponent<ScriptMainMenuRight> ().buttening (int.Parse( array[4].ToString()));
		Debug.Log (transform.FindChild ("Code").GetComponent<UILabel> ().text + " is teamcode");
		string result = transform.parent.parent.parent.FindChild ("Panel").FindChild ("Label").GetComponent<UILabel> ().text;
		result = result [8].ToString () + result [9].ToString ();
		if(Top==null){
			Top = transform.root.FindChild("Top").gameObject;
		}
		Top.GetComponent<ScriptMainTop> ().GoGame (transform.FindChild ("Code").GetComponent<UILabel> ().text,
		                                           transform.FindChild ("Statue").GetComponent<UILabel> ().text, result);

	}
}
