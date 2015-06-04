using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PostButton : MonoBehaviour {
	GetMailEvent Mail;
	List<Mailinfo> Mails = new List<Mailinfo>();

	public void getitem (int mailseq,int attachseq){
	
	}
	void Start(){
		Mail = new GetMailEvent (new EventDelegate (this, "getdata"));
		NetMgr.GetUserMailBox (UserMgr.UserInfo.memSeq,Mail);
	}
	public void on(){
//		Mail = new GetMailEvent (new EventDelegate (this, "Setdata"));
//		NetMgr.GetUserMailBox (UserMgr.UserInfo.memSeq,Mail);
		transform.FindChild ("TF_Post").gameObject.SetActive (true);
		Setdata();	


	}
	public void off(){
		Mail = new GetMailEvent (new EventDelegate (this, "getdata"));
		NetMgr.GetUserMailBox (UserMgr.UserInfo.memSeq,Mail);
			transform.FindChild ("TF_Post").gameObject.SetActive (false);
	
	}
	void getdata(){
		Mails = Mail.Response.data;
		if (Mail.Response.data != null) {
			transform.FindChild ("Background").FindChild ("on").gameObject.SetActive (true);
		} else {
			transform.FindChild ("Background").FindChild ("on").gameObject.SetActive (false);
		}

	}
	void Setdata(){
		Debug.Log (transform.FindChild ("TF_Post").FindChild ("List").childCount);
		if (transform.FindChild ("TF_Post").FindChild ("List").childCount > 1) {
			for(int i = 1; i < transform.FindChild ("TF_Post").FindChild ("List").childCount;i++){
				Destroy(transform.FindChild ("TF_Post").FindChild ("List").GetChild(i).gameObject);
			}
		}
	//	Mails = Mail.Response.data;
		GameObject origin = transform.FindChild ("TF_Post").FindChild ("List").FindChild ("bar origin").gameObject;
		Vector3 po;
		if (Mails != null) {
			for(int i = 0; i<Mails.Count; i++){
				GameObject temp = (GameObject)Instantiate(origin,new Vector2(1,1),origin.transform.localRotation);
				po = new Vector3(0,i*(110),0);
				temp.transform.parent = origin.transform.parent;
	        	temp.transform.localScale = new Vector3(1,1,1);
				temp.transform.localPosition = origin.transform.localPosition;
				temp.transform.localPosition -= po;
					temp.name = "Mail " + i.ToString();
				temp.transform.FindChild("Name").GetComponent<UILabel>().text = Mails[i].attach[0].attachDesc 
					+ " " + Mails[i].attach[0].attachValue.ToString();
				temp.transform.FindChild("mailseq").GetComponent<UILabel>().text = Mails[i].mailSeq.ToString();
				temp.transform.FindChild("attachseq").GetComponent<UILabel>().text = Mails[i].attach[0].attachSeq.ToString();
						temp.gameObject.SetActive(true);
			}
	}
	
	}

}
