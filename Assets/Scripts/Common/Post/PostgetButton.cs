using UnityEngine;
using System.Collections;

public class PostgetButton : MonoBehaviour {
	GetDoneMailEvent getdone;
	GetCheckMailEvent getCheck;
	public void Getit(){
		Debug.Log ("ms : " + transform.parent.FindChild ("mailseq").GetComponent<UILabel> ().text);
		Debug.Log ("at : " + transform.parent.FindChild ("attachseq").GetComponent<UILabel> ().text);
		if (transform.parent.FindChild ("Code").GetComponent<UILabel> ().text == "ATTACH_GACHA") {

			getCheck = new GetCheckMailEvent (new EventDelegate (this, "getcheckdata"));
			NetMgr.GetUserCheckMailBox (UserMgr.UserInfo.memSeq, int.Parse (transform.parent.FindChild ("mailseq").GetComponent<UILabel> ().text)
			                            , getCheck);

		} else {
			getdone = new GetDoneMailEvent (new EventDelegate (this, "getdonedata"));
			NetMgr.GetUserDoneMailBox (UserMgr.UserInfo.memSeq, int.Parse (transform.parent.FindChild ("mailseq").GetComponent<UILabel> ().text)
		                           , int.Parse (transform.parent.FindChild ("attachseq").GetComponent<UILabel> ().text), getdone);
		}
	}

	void getdonedata(){
	
		transform.parent.FindChild ("get").gameObject.SetActive (false);
		transform.parent.FindChild ("com").gameObject.SetActive (true);

		if (getdone.Response.data.userGoldenBall != null) {
			UserMgr.UserInfo.userGoldenBall = getdone.Response.data.userGoldenBall;}
		if (getdone.Response.data.userDiamond != null) {
			UserMgr.UserInfo.userDiamond = getdone.Response.data.userDiamond;}
		if (getdone.Response.data.useActiveDiamond != null) {
			UserMgr.UserInfo.useActiveDiamond = getdone.Response.data.useActiveDiamond;}
		if (getdone.Response.data.userRuby != null) {
			UserMgr.UserInfo.userRuby = getdone.Response.data.userRuby;}

		//getprofile


		DialogueMgr.ShowDialogue ("지급 완료", transform.parent.FindChild("Name").GetComponent<UILabel>().text+" 지급 완료", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		

	
	}
	public static GameObject anim ;
	void getcheckdata(){

		Debug.Log("gat");
		transform.parent.FindChild ("get").gameObject.SetActive (false);
		transform.parent.FindChild ("com").gameObject.SetActive (true);

		//getprofile
		
		//anim
		GameObject Anim = transform.parent.parent.parent.parent.GetComponent<PostButton> ().GachaAnim;
		anim = (GameObject)Instantiate (Anim,new Vector3 (0.0390625f,-0.0078125f,0),Anim.transform.localRotation);
		anim.transform.parent = transform.parent.parent.parent.parent;
		anim.transform.localPosition = new Vector3 (-195f, -595f, 0);
		anim.transform.localScale = new Vector3 (100f,100f,1f);
	//	transform.parent.parent.parent.parent.FindChild("PostDialogue").FindChild("Panel").FindChild("Sprite")
		anim.SetActive (true);

		//DialogueMgr.ShowDialogue ("지급 완료", transform.parent.FindChild("Name").GetComponent<UILabel>().text+" 지급 완료", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		
		
		
	}


}
