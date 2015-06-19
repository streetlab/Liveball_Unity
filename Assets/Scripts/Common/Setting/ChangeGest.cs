using UnityEngine;
using System.Collections;

public class ChangeGest : MonoBehaviour {
	public UIInput Email;
	public UIInput Pwd1;
	public UIInput Pwd2;


	public void ChangeGestButton(){
		//Debug.Log ("Email : " + Email.value);
		//Debug.Log ("Pwd1 : " + Pwd1.value);
		//Debug.Log ("Pwd2 : " + Pwd2.value);
		if (Pwd1.value.Length < 4 || Pwd2.value.Length < 4) {
			DialogueMgr.ShowDialogue ("가입정보오류", "비밀번호는 4자리 이상 입려해주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}else if(Pwd1.value.Length != Pwd2.value.Length){
			DialogueMgr.ShowDialogue ("가입정보오류", "비밀번호가 서로 다릅니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}



			
		LoginInfo loginInfo = new LoginInfo ();
			UpdateMemberInfoEvent event1 = null;
		loginInfo.memSeq = UserMgr.UserInfo.memSeq;
		loginInfo.memberEmail = Email.value;
		loginInfo.memberPwd = Pwd2.value;

		loginInfo.memUID = "";
		loginInfo.DeviceID = "";


			event1 = new UpdateMemberInfoEvent (new EventDelegate (this, "Set"));
		NetMgr.ChangGestInfo (loginInfo, event1, UtilMgr.IsTestServer (), false);

	}
	void Set(){
		DialogueMgr.ShowDialogue ("전환 성공", "회원 전환되셨습니다.", DialogueMgr.DIALOGUE_TYPE.EventAlert, MileageDialogueHandler);
	}

	void MileageDialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			PlayerPrefs.SetString (Constants.PrefGuest,"1");
			AutoFade.LoadLevel("SceneSettings", 0f, 1f);
		}
		
	}
}
