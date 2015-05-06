using UnityEngine;
using System.Collections;

public class Itemcontrol : MonoBehaviour {

	GetItemShopRubyEvent getruby;
	GetItemShopGoldEvent getgold;
	GetItemShopItemEvent getitem;

	GetCardInvenEvent getcard;
	GameObject origin1,origin2,origin3,origin4,temp1,temp2,temp3,temp4;
	public float gap = 288;
	public float category = 4;
	Vector3 originV1,originV2,originV3,originV4;
	// Use this for initialization
	void Start () {
		transform.FindChild ("category 1").gameObject.SetActive (true);
		origin1 = transform.FindChild("category 1").GetChild(0).FindChild("origin").gameObject;
		originV1 = new Vector3(origin1.transform.localPosition.x,235,origin1.transform.localPosition.z);
		origin1.gameObject.SetActive (false);
		getruby = new GetItemShopRubyEvent (new EventDelegate (this, "ruby"));
		NetMgr.GetItemShopRubyList (getruby);

		transform.FindChild ("category 2").gameObject.SetActive (true);
		origin2 = transform.FindChild("category 2").GetChild(0).FindChild("origin").gameObject;
		originV2 = new Vector3(origin2.transform.localPosition.x,235,origin2.transform.localPosition.z);
		origin2.gameObject.SetActive (false);	
		getgold = new GetItemShopGoldEvent (new EventDelegate (this, "gold"));
		NetMgr.GetItemShopGoldList (getgold);

		transform.FindChild ("category 3").gameObject.SetActive (true);
		origin3 = transform.FindChild("category 3").GetChild(0).FindChild("origin").gameObject;
		originV3 = new Vector3(origin3.transform.localPosition.x,235,origin3.transform.localPosition.z);
		origin3.gameObject.SetActive (false);	
		getitem = new GetItemShopItemEvent (new EventDelegate (this, "item"));
		NetMgr.GetItemShopItemList (getitem);
		
		//getcard = new GetCardInvenEvent (new EventDelegate (this, "card"));
		//NetMgr.GetCardInven (getcard);
	
	}


	void ruby(){
	


			

			for (int i = 0; i<getruby.Response.data.Count; i++) {
			
				temp1 = (GameObject)Instantiate (origin1, new Vector3 (0, 0, 0), origin1.transform.localRotation);
				temp1.transform.parent = origin1.transform.parent;
				temp1.transform.localScale = new Vector3 (1, 1, 1);
				temp1.transform.localPosition = new Vector3 (originV1.x, originV1.y - (i * gap), originV1.z);
				temp1.transform.FindChild ("LblBody").GetComponent<UILabel> ().text = getruby.Response.data [i].productName;
				temp1.transform.FindChild ("LblDescription").GetComponent<UILabel> ().text = getruby.Response.data [i].productDesc;
			temp1.transform.FindChild ("LblPrice").GetComponent<UILabel> ().text = "가격 : " + UtilMgr.AddsThousandsSeparator (getruby.Response.data [i].productPrice.ToString ())+"원";
			
			
			temp1.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_ruby_00"+((i%3)+1).ToString();
				temp1.gameObject.SetActive (true);

		
		}

	}

	void gold(){
		
		
		

		
		for (int i = 0; i<getgold.Response.data.Count; i++) {
			
			temp2 = (GameObject)Instantiate (origin2, new Vector3 (0, 0, 0), origin2.transform.localRotation);
			temp2.transform.parent = origin2.transform.parent;
			temp2.transform.localScale = new Vector3 (1, 1, 1);
			temp2.transform.localPosition = new Vector3 (originV2.x, originV2.y - (i * gap), originV2.z);
			temp2.transform.FindChild ("LblBody").GetComponent<UILabel> ().text = getgold.Response.data [i].productName;
			temp2.transform.FindChild ("LblDescription").GetComponent<UILabel> ().text = getgold.Response.data [i].productDesc;
			temp2.transform.FindChild ("LblPrice").GetComponent<UILabel> ().text = "가격 : " + UtilMgr.AddsThousandsSeparator (getgold.Response.data [i].productPrice.ToString ())+"루비";
			
			
			temp2.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_00"+((i%3)+1).ToString();
			temp2.gameObject.SetActive (true);
			
			
		}
		transform.FindChild ("category 2").gameObject.SetActive (false);
	}


	void item(){
		
		
		
		
	//	Debug.Log ("getitem.Response.data.Count : " + getitem.Response.data.Count);
		for (int i = 0; i<getitem.Response.data.Count; i++) {
			
			temp3 = (GameObject)Instantiate (origin3, new Vector3 (0, 0, 0), origin3.transform.localRotation);
			temp3.transform.parent = origin3.transform.parent;
			temp3.transform.localScale = new Vector3 (1, 1, 1);
			temp3.transform.localPosition = new Vector3 (originV3.x, originV3.y - (i * gap), originV3.z);
			temp3.transform.FindChild ("LblBody").GetComponent<UILabel> ().text = getitem.Response.data [i].productName;
			temp3.transform.FindChild ("LblDescription").GetComponent<UILabel> ().text = getitem.Response.data [i].productDesc;
			temp3.transform.FindChild ("LblPrice").GetComponent<UILabel> ().text = "가격 : " + UtilMgr.AddsThousandsSeparator (getitem.Response.data [i].productPrice.ToString ())+"루비";
			Debug.Log (getitem.Response.data [i].productCode);

			if(getitem.Response.data [i].productCode =="ITEM_MULTIPLE_200X"){
				temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_item_2";
			}else if(getitem.Response.data [i].productCode =="ITEM_MULTIPLE_300X"){
				temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_item_3";
			}else{
				temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_item_5";
			}



			//temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_00"+((i%3)+1).ToString();
			temp3.gameObject.SetActive (true);
			
			
		}
		transform.FindChild ("category 3").gameObject.SetActive (false);
	}



}
