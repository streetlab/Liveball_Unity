using UnityEngine;
using System.Collections;

public class PostgetButton : MonoBehaviour {
	GetDoneMailEvent get;
	public void Getit(){
		//get = new GetDoneMailEvent (new EventDelegate (this, "getdata"));
		//NetMgr.GetUserDoneMailBox (UserMgr.UserInfo.memSeq,int.Parse(transform.FindChild("mailseq").GetComponent<UILabel>().text)
		//                       ,int.Parse(transform.FindChild("attachseq").GetComponent<UILabel>().text),get);
	}

	void getdata(){
		transform.FindChild ("get").gameObject.SetActive (false);
		transform.FindChild ("com").gameObject.SetActive (true);
		Debug.Log ("outMessage : " + get.Response.data.outMessage);
	}
}
