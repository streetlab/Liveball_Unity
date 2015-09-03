using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {
	public static string Status;
	//닉네임 변경
	public void Button(){
		Debug.Log (name);
		if (name == "Button1") {
			if (Status == "Profile") {



				CheckDuplication();


			






			}
		} else {
			transform.parent.FindChild
				("Input").GetComponent<UIInput> ().value = "";
			transform.parent.parent.gameObject.SetActive(false);
		}
	}
	string CheckValidation()
	{

		string value = transform.parent.FindChild("Input").GetComponent<UIInput> ().value;
		if(value.Length < 2)
			return "2자 이상 입력해 주세요.";
		
		return null;
		
	}


	CheckNickEvent mNickEvent;

	public void CheckDuplication(){
		string value = CheckValidation ();
		if (value == null) {
			mNickEvent = new CheckNickEvent(new EventDelegate(this, "CheckComplete"));
			string name = transform.parent.FindChild("Input").GetComponent<UIInput> ().value;
			NetMgr.CheckNickname(name, mNickEvent);
		} else
		{
			transform.parent.parent.gameObject.SetActive(false);
			DialogueMgr.ShowDialogue("변경정보오류", value, DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null, null);
		}
	}

	void CheckComplete(){
		if(mNickEvent.Response.code == 0){//duplicated
			transform.parent.parent.gameObject.SetActive(false);
			DialogueMgr.ShowDialogue("닉네임 중복 확인", "이미 등록된 닉네임 입니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null, null, null, null);
		} else{
			transform.root.FindChild("Scroll").FindChild ("RightMenu").GetComponent<RightMenuCommander> ().
				SetMemberName (transform.parent.FindChild("Input").GetComponent<UIInput> ().value);
			transform.parent.parent.gameObject.SetActive(false);
		}
	}


}
