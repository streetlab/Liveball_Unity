using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Itemcontrol : MonoBehaviour {

	GetItemShopRubyEvent getruby;
	GetItemShopGoldEvent getgold;
	GetItemShopItemEvent getitem;
	GetProfileEvent mProfileEvent;
	IAPEvent RequestIAP,ComsumeIAP,DoneIAP,CancelIAP;
	int itemid,orderNo;
	float Bruby,Aruby,Agold,Bgold;
	string itemcode,itemproduct;
	GetCardInvenEvent getcard;
	GameObject origin1,origin2,origin3,origin4,temp1,temp2,temp3,temp4;
	public float gap = 288;
	public float category = 4;
	Vector3 originV1,originV2,originV3,originV4;

	// Use this for initialization

	void Awake(){
	//	GoogleIAB.init(Constants.GOOGLE_PUBLIC_KEY);
	//	GoogleIAB.purchaseProduct( "ruby_50", "payload that gets stored and returned" );
	}

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
			temp1.transform.FindChild ("LblBody").GetComponent<UILabel> ().text = "루비 " + getruby.Response.data [i].productValue+"개";
			temp1.transform.FindChild ("LblDescription").GetComponent<UILabel> ().text = getruby.Response.data [i].productDesc;
			temp1.transform.FindChild ("LblPrice").GetComponent<UILabel> ().text = "가격 : " + UtilMgr.AddsThousandsSeparator (getruby.Response.data [i].productPrice.ToString ())+"원";
			temp1.transform.FindChild ("code").GetComponent<UILabel> ().text = getruby.Response.data [i].productCode;
			temp1.transform.FindChild ("id").GetComponent<UILabel> ().text = getruby.Response.data [i].productId.ToString();
			temp1.transform.FindChild ("add").FindChild ("buyruby").GetComponent<UILabel> ().text = getruby.Response.data [i].productValue.ToString();
			temp1.transform.FindChild ("add").FindChild ("addruby").GetComponent<UILabel> ().text = getruby.Response.data [i].bonusRuby.ToString();
			temp1.transform.FindChild ("add").FindChild ("addgold").GetComponent<UILabel> ().text = getruby.Response.data [i].bonusGoldenball.ToString();
			temp1.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_ruby_00"+((i%3)+1).ToString();
				temp1.gameObject.SetActive (true);

		
		}
		transform.FindChild ("category 1").GetComponent<UIScrollView> ().ResetPosition ();
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
			temp2.transform.FindChild ("buygold").GetComponent<UILabel> ().text = getgold.Response.data [i].productValue;
			
			temp2.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_00"+((i%3)+1).ToString();
			temp2.gameObject.SetActive (true);
			
			
		}
		transform.FindChild ("category 2").GetComponent<UIScrollView> ().ResetPosition ();
		transform.FindChild ("category 2").gameObject.SetActive (false);
	}

	void setusergold(){


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
			//Debug.Log (getitem.Response.data [i].productCode);

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
		transform.FindChild ("category 3").GetComponent<UIScrollView> ().ResetPosition ();
		transform.FindChild ("category 3").gameObject.SetActive (false);
	}

	public void prime31(string id,string code,string product,string buyruby,string addruby,string addgold){
		Debug.Log ("id : " + id + " code : " + code);
		itemid = int.Parse(id);
		itemcode = code;
		itemproduct = product;
		Bruby = float.Parse(buyruby);
		Aruby = float.Parse(addruby);
		Agold = float.Parse(addgold);
		RequestIAP  = new IAPEvent (new EventDelegate (this, "mRequestIAP"));
		NetMgr.RequestIAP (itemid,itemcode,RequestIAP);
	
//		 NetMgr.RequestIAP
//		NetMgr.ComsumeIAP
//		GoogleIAB.consumeProduct
	//	NetMgr.DoneIAP
	
	
		//Debug.Log ("prime31");
//			GoogleIAB.init(Constants.GOOGLE_PUBLIC_KEY);
//		GoogleIAB.purchaseProduct(code, "payload that gets stored and returned" );
	
		//GoogleIAB.consumeProduct(code);
		
	}
	void mRequestIAP(){

		//if (RequestIAP.Response.data != null) {
			if(RequestIAP.Response.data.productId==itemid&&RequestIAP.Response.data.productCode==itemcode){
				orderNo = RequestIAP.Response.data.orderNo;
				//RequestIAP.Response.data.
				GoogleIAB.init(Constants.GOOGLE_PUBLIC_KEY);
				GoogleIAB.purchaseProduct(itemcode, "payload that gets stored and returned" );
			}
		//}
	}
	void OnEnable()
	{
		// Listen to all events for illustration purposes
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}
	
	
	void OnDisable()
	{
		// Remove all event handlers
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent -= purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}
	
	
	
	void billingSupportedEvent()
	{
		//DialogueMgr.ShowDialogue("billing", "this device can be purchasement", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		Debug.Log( "billingSupportedEvent" );
	}
	
	
	void billingNotSupportedEvent( string error )
	{
		//DialogueMgr.ShowDialogue("billing", error, DialogueMgr.DIALOGUE_TYPE.Alert, null);
		Debug.Log( "billingNotSupportedEvent: " + error );
	}
	
	
	void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
	{
		Debug.Log( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
		Prime31.Utils.logObject( purchases );
		Prime31.Utils.logObject( skus );
	}
	
	
	void queryInventoryFailedEvent( string error )
	{
		Debug.Log( "queryInventoryFailedEvent: " + error );
	}
	
	
	void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
	{
		Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
	}
	

	void purchaseSucceededEvent( GooglePurchase purchase )
	{

		ComsumeIAP  = new IAPEvent (new EventDelegate (this, "mComsumeIAP"));
		NetMgr.ComsumeIAP (orderNo,purchase.purchaseToken,ComsumeIAP);
		orderNo = ComsumeIAP.Response.data.orderNo;

		Debug.Log( "purchaseSucceededEvent: " + purchase );
	}
	void mComsumeIAP(){
		GoogleIAB.consumeProduct (itemcode);

	}
	
	void purchaseFailedEvent( string error, int response )
	{

		CancelIAP  = new IAPEvent (new EventDelegate (this, "mCancelIAP"));
		NetMgr.CancelIAP (orderNo,CancelIAP);

		Debug.Log( "purchaseFailedEvent: " + error + ", response: " + response );
	}
	void mDoneIAP(){
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "addruby"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);

		DialogueMgr.ShowDialogue("구매 성공", itemproduct + " 구매가 완료 되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);

		Debug.Log ("All PurchaseSucceeded");

	}
	void addruby(){
		mProfileEvent.Response.data.userRuby = (float.Parse(mProfileEvent.Response.data.userRuby)+Bruby+Aruby).ToString();
		mProfileEvent.Response.data.userGoldenBall = (float.Parse(mProfileEvent.Response.data.userGoldenBall)+Agold).ToString();
		UserMgr.UserInfo.userRuby = mProfileEvent.Response.data.userRuby;
		UserMgr.UserInfo.userGoldenBall = mProfileEvent.Response.data.userGoldenBall;
	}
	void mCancelIAP(){
		DialogueMgr.ShowDialogue("구매 실패", itemproduct + " 구매를 실패 했습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		Debug.Log ("FailedEvent");

	
	}
	void consumePurchaseSucceededEvent( GooglePurchase purchase )
	{
		DoneIAP  = new IAPEvent (new EventDelegate (this, "mDoneIAP"));
		NetMgr.DoneIAP (orderNo,DoneIAP);
		Debug.Log( "consumePurchaseSucceededEvent: " + purchase );
	}
	
	
	void consumePurchaseFailedEvent( string error )
	{
		CancelIAP  = new IAPEvent (new EventDelegate (this, "mCancelIAP"));
		NetMgr.CancelIAP (orderNo,CancelIAP);

		Debug.Log( "consumePurchaseFailedEvent: " + error );
	}

}
