using UnityEngine;
using System.Collections;

public class InvenD : MonoBehaviour {
	DeleteInvenItemInfoEvent mEvent;
	public void onhit(){
//		mEvent = new DeleteInvenItemInfoEvent(new EventDelegate(this, "D"));
//		NetMgr.DeleteInvenItem (int.Parse(transform.parent.FindChild("itemNo").GetComponent<UILabel>().text)
		                //        ,int.Parse(transform.parent.FindChild("itemid").GetComponent<UILabel>().text),mEvent);
		DialogueMgr.ShowDialogue ("삭제 확인", transform.parent.FindChild("name").GetComponent<UILabel>().text
		                          +"\n정말 삭제하시겠습니까?", DialogueMgr.DIALOGUE_TYPE.YesNo , MileageDialogueHandler);
	
	} 
	void D(){
		DialogueMgr.ShowDialogue ("삭제 성공", transform.parent.FindChild("name").GetComponent<UILabel>().text
		                          +"\n삭제되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		GetComponent<UIButton> ().isEnabled = false;
		transform.FindChild ("Label").GetComponent<UILabel> ().text = "삭제됨";
		//AutoFade.LoadLevel("SceneCards", 0f, 1f);
	}

	void MileageDialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			mEvent = new DeleteInvenItemInfoEvent(new EventDelegate(this, "D"));
			NetMgr.DeleteInvenItem (int.Parse(transform.parent.FindChild("itemNo").GetComponent<UILabel>().text)
			                        ,int.Parse(transform.parent.FindChild("itemid").GetComponent<UILabel>().text),mEvent);
		}
		
	}
}
