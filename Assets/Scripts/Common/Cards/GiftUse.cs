using UnityEngine;
using System.Collections;

public class GiftUse : MonoBehaviour {
	DoneInvenItemInfoEvent mEvent;
	public GameObject Certification;
	public void onhit(){
//		mEvent = new DeleteInvenItemInfoEvent(new EventDelegate(this, "D"));
//		NetMgr.DeleteInvenItem (int.Parse(transform.parent.FindChild("itemNo").GetComponent<UILabel>().text)
		                //        ,int.Parse(transform.parent.FindChild("itemid").GetComponent<UILabel>().text),mEvent);
		DialogueMgr.ShowDialogue ("사용 확인", "["+transform.parent.FindChild("name").GetComponent<UILabel>().text+"] 을"
		                          +"\n사용하시겠습니까?", DialogueMgr.DIALOGUE_TYPE.YesNo , MileageDialogueHandler);
	
	} 
	void D(){
		ScriptItemMiddle.UseItem = true;
		ScriptItemMiddle.name = transform.parent.FindChild ("name").GetComponent<UILabel> ().text;
	
		transform.root.FindChild ("Item").GetComponent<ScriptItemMiddle> ().Reset ();
	
	}
	public void temp(){
	try{
		mEvent = new DoneInvenItemInfoEvent(new EventDelegate(this, "D"));
		NetMgr.DoneInvenItem (long.Parse(transform.parent.FindChild("itemNo").GetComponent<UILabel>().text)
		                      ,long.Parse(transform.parent.FindChild("itemid").GetComponent<UILabel>().text),mEvent);
		}catch{
			//AutoFade.LoadLevel("SceneCards", 0f, 1f);
		}
	}

	void MileageDialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			if(UserMgr.UserInfo.activeAuth>0){
				//Use Item
				temp ();
			}else{
				DialogueMgr.ShowDialogue ("사용 확인","[본인인증]을 진행하시겠습니까?", DialogueMgr.DIALOGUE_TYPE.YesNo , OpenCertification);
			}
				}
		
	}
	void OpenCertification(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1){ 
			Certification.transform.GetChild(0).GetComponent<ScriptCertification>().GetItemObj(this.gameObject);
			Certification.SetActive(true);
		}
	}

}
