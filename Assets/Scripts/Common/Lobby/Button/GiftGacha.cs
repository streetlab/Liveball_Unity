using UnityEngine;
using System.Collections;

public class GiftGacha : MonoBehaviour {
	public GameObject GachaAnim;
	procStoreGachaEvent PSGE;
	GetItemShopRubyEvent getruby;
	//경품 가챠
	public void GachaButton(){

		string S = transform.FindChild("Label").GetComponent<UILabel>().text;
		S = S.Replace(" 참여하기 ","");

	
		DialogueMgr.ShowDialogue ("참여하기", S+"로 이벤트에 응모하시겠습니까?\n이벤트 참여 시 차감된 마일리지는 환불되지 않습니다.", DialogueMgr.DIALOGUE_TYPE.YesNo, JoinGacha);

	
		Debug.Log ("BUtton");

//	
	

	}
	void JoinGacha(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			string S = transform.FindChild("Label").GetComponent<UILabel>().text;
			//S.Replace(" ","");
		
	S =	S.Replace(",","");
			S = S.Replace(" M 참여하기 ","");
			Debug.Log("S : "  + S);
			//마일리지 체크
			if(long.Parse(S)>long.Parse(UserMgr.UserInfo.userDiamond)){
				DialogueMgr.ShowDialogue ("마일리지 부족", "마일리지가 부족합니다.\n마일리지를 얻기위해 게임에 참여하시겠습니까?", DialogueMgr.DIALOGUE_TYPE.YesNo, NoneM);
			}else{
				UserMgr.UserInfo.userDiamond = (int.Parse(UserMgr.UserInfo.userDiamond)-int.Parse(S)).ToString();
				Debug.Log(S);
				PSGE = new procStoreGachaEvent (new EventDelegate(this,"Gachas"));
				NetMgr.ProcStoreGacha (long.Parse(transform.FindChild("product").GetComponent<UILabel>().text),PSGE);
			}


			
		}
		}
	void NoneM(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.transform.localPosition = new Vector3(-720,0,0);
			transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.SetActive (false);
		}
			

	}

	void Gachas(){
		//가챠 애니메이션 생성
		if (transform.root.FindChild ("Gacha") != null) {
			Destroy(transform.root.FindChild ("Gacha").gameObject);
		}
		GameObject Temp = (GameObject)Instantiate (GachaAnim);
		Temp.transform.parent = transform.root;
		Temp.transform.localScale = new Vector3 (1,1,1);
		Temp.transform.localPosition = new Vector3 (25f, 0, 0);
		Temp.transform.localScale = new Vector3 (100f,100f,1f);
		Temp.gameObject.SetActive (true);
		Temp.name ="Gacha";
	}

	public void Gacha(){
		UserMgr.UserInfo.userRuby = PSGE.Response.data.userRuby;
		UserMgr.UserInfo.userDiamond = PSGE.Response.data.userDiamond;
		if (PSGE.Response.data.gacha.itemType >= 6) {
			DialogueMgr.ShowDialogue ("경품 확인", PSGE.Response.data.gacha.itemName + " 지급 완료 되었습니다.\n인벤토리를 확인해주세요.", DialogueMgr.DIALOGUE_TYPE.EventAlert_NonBg, DialogueHandler);
		} else {
			DialogueMgr.ShowDialogue ("경품 확인", PSGE.Response.data.gacha.itemName + " 지급 완료 되었습니다.", DialogueMgr.DIALOGUE_TYPE.EventAlert_NonBg, DialogueHandler);
		}

}


	
void DialogueHandler(DialogueMgr.BTNS btn){
	if (btn == DialogueMgr.BTNS.Cancel) {
			Destroy(transform.root.FindChild("Gacha").gameObject);
	}
	
}

}
