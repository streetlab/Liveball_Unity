﻿using UnityEngine;
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

			UtilMgr.AddBackEvent(new EventDelegate(
				transform.parent.parent.parent.parent.GetComponent<PostButton> (), "CloseGachaAnim"));
			//DialogueMgr.DismissDialogue();

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

		UserMgr.UserMailCount -= 1;
		DialogueMgr.ShowDialogue ("지급 완료", transform.parent.FindChild("Name").GetComponent<UILabel>().text+" 지급 완료", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		

	
	}
	public static GameObject anim ;
	void getcheckdata(){
		transform.parent.parent.parent.parent.GetComponent<PostButton> ().CheckMail = getCheck.Response.data;
		Debug.Log("gat");


		//getprofile
		if (getCheck.Response.data.userGoldenBall != null) {
			UserMgr.UserInfo.userGoldenBall = getCheck.Response.data.userGoldenBall;}
		if (getCheck.Response.data.userDiamond != null) {
			UserMgr.UserInfo.userDiamond = getCheck.Response.data.userDiamond;}
		if (getCheck.Response.data.useActiveDiamond != null) {
			UserMgr.UserInfo.useActiveDiamond = getCheck.Response.data.useActiveDiamond;}
		if (getCheck.Response.data.userRuby != null) {
			UserMgr.UserInfo.userRuby = getCheck.Response.data.userRuby;}
		//anim
		if (transform.parent.parent.parent.parent.FindChild ("Gacha") != null) {
			Destroy(transform.parent.parent.parent.parent.FindChild ("Gacha").gameObject);
		}
		GameObject Anim = transform.parent.parent.parent.parent.GetComponent<PostButton> ().GachaAnim;
		anim = (GameObject)Instantiate (Anim,new Vector3 (0.0390625f,-0.0078125f,0),Anim.transform.localRotation);
		anim.transform.parent = transform.parent.parent.parent.parent;
		anim.transform.localPosition = new Vector3 (-119f,  588f, 0);
		anim.transform.localScale = new Vector3 (100f,100f,1f);
		anim.name = "Gacha";
		Debug.Log ("getCheck.Response.data.gacha.itemCode" + getCheck.Response.data.gacha.itemCode);
		switch (getCheck.Response.data.gacha.itemCode) {
		case "ITEM_RUBY":
			transform.parent.parent.parent.parent.FindChild("PostDialogue").FindChild("Panel").FindChild("Sprite").GetComponent<UISprite>().
				spriteName = "item_ruby_300.png";
			break;
		case "ITEM_GOLD":
			transform.parent.parent.parent.parent.FindChild("PostDialogue").FindChild("Panel").FindChild("Sprite").GetComponent<UISprite>().
				spriteName = "item_goldenball_30k.png";

			break;

		case "ITEM_MILEAGE":
			transform.parent.parent.parent.parent.FindChild("PostDialogue").FindChild("Panel").FindChild("Sprite").GetComponent<UISprite>().
				spriteName = "gift_m";
			break;

		case "ITEM_ITEM":

			break;

		case "ITEM_CARD":
	
			break;

		case "ITEM_GIFT":
			transform.parent.parent.parent.parent.FindChild("PostDialogue").FindChild("Panel").FindChild("Sprite").GetComponent<UISprite>().
				spriteName = "gift_c";
			break;

		case "ITEM_PPOINT":
			transform.parent.parent.parent.parent.FindChild("PostDialogue").FindChild("Panel").FindChild("Sprite").GetComponent<UISprite>().
				spriteName = "gift_p";
			break;


		}
	
	
		StartCoroutine ("AnimStart");
		transform.parent.parent.parent.parent.GetComponent<PostButton> ().GachaCount++;
		Debug.Log ("gachacount : " + transform.parent.parent.parent.parent.GetComponent<PostButton> ().GachaCount);
		//DialogueMgr.ShowDialogue ("지급 완료", transform.parent.FindChild("Name").GetComponent<UILabel>().text+" 지급 완료", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		
		
		
	}
	IEnumerator AnimStart(){
		UserMgr.UserMailCount -= 1;
		anim.SetActive (true);
		transform.parent.FindChild ("get").gameObject.SetActive (false);
		transform.parent.FindChild ("com").gameObject.SetActive (true);
		yield return new WaitForSeconds (0f);

	
	}


}
