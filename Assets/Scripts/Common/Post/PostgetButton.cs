using UnityEngine;
using System.Collections;

public class PostgetButton : MonoBehaviour {
	GetDoneMailEvent get;
	public void Getit(){
		Debug.Log ("ms : " + transform.parent.FindChild("mailseq").GetComponent<UILabel>().text);
		Debug.Log ("at : " + transform.parent.FindChild("attachseq").GetComponent<UILabel>().text);
		get = new GetDoneMailEvent (new EventDelegate (this, "getdata"));

		NetMgr.GetUserDoneMailBox (UserMgr.UserInfo.memSeq,int.Parse(transform.parent.FindChild("mailseq").GetComponent<UILabel>().text)
		                           ,int.Parse(transform.parent.FindChild("attachseq").GetComponent<UILabel>().text),get);
	}

	void getdata(){
	
		transform.parent.FindChild ("get").gameObject.SetActive (false);
		transform.parent.FindChild ("com").gameObject.SetActive (true);

		//getprofile


		DialogueMgr.ShowDialogue ("지급 완료", get.Response.data.outMessage, DialogueMgr.DIALOGUE_TYPE.Alert, null);
		

	
	}
}
