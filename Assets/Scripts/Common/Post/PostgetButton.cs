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

		if (get.Response.data.userGoldenBall != null) {
			UserMgr.UserInfo.userGoldenBall = get.Response.data.userGoldenBall;}
		if (get.Response.data.userDiamond != null) {
			UserMgr.UserInfo.userDiamond = get.Response.data.userDiamond;}
		if (get.Response.data.useActiveDiamond != null) {
			UserMgr.UserInfo.useActiveDiamond = get.Response.data.useActiveDiamond;}
		if (get.Response.data.userRuby != null) {
			UserMgr.UserInfo.userRuby = get.Response.data.userRuby;}

		//getprofile


		DialogueMgr.ShowDialogue ("지급 완료", transform.parent.FindChild("Name").GetComponent<UILabel>().text+" 지급 완료", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		

	
	}
}
