using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Itemcontrol : MonoBehaviour {

	GetItemShopRubyEvent getruby;
	GetItemShopGoldEvent getgold;
	GetItemShopItemEvent getitem;

	GetProfileEvent mProfileEvent;

	IAPEvent RequestIAP,ComsumeIAP,DoneIAP,CancelIAP,golds,items;
	int itemid,orderNo;
	float Bruby,Aruby,Agold,Bgold;
	string itemcode,itemproduct;
	GetCardInvenEvent getcard;
	GameObject origin1,origin2,origin3,origin4,temp1,temp2,temp3,temp4,imageC1,imageC2;
	public float gap = 288;
	public float category = 4;
	Vector3 originV1,originV2,originV3,originV4;
	string Sgold;
	bool IsTest = false;

	// Use this for initialization

	void Awake(){
	//	GoogleIAB.init(Constants.GOOGLE_PUBLIC_KEY);
	//	GoogleIAB.purchaseProduct( "ruby_50", "payload that gets stored and returned" );
		IsTest = UtilMgr.IsTestServer();
	}

	void OnDestroy(){
		ClearDelegates();
	}
	
	void Start () {
		SetDelegates();

		#if(UNITY_ANDROID || UNITY_EDITOR)
		#else
		#endif
		
		transform.FindChild ("category 1").gameObject.SetActive (true);
		origin1 = transform.FindChild("category 1").GetChild(0).FindChild("origin").gameObject;
		imageC1 = transform.FindChild("category 1").GetChild(0).FindChild("C").gameObject;
		imageC2 = transform.FindChild("category 1").GetChild(0).FindChild("C2").gameObject;
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

	void SetDelegates(){
		#if(UNITY_ANDROID)
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		#else
		IOSMgr.PurchaseSucceededEvent += purchaseSucceededEvent;
		IOSMgr.PurchaseFailedEvent += purchaseFailedEvent;
		#endif
	}

	void ClearDelegates(){
		#if(UNITY_ANDROID)
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent -= purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		#else
		IOSMgr.PurchaseSucceededEvent -= purchaseSucceededEvent;
		IOSMgr.PurchaseFailedEvent -= purchaseFailedEvent;
		#endif
	}


	void ruby(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			EventDelegate eventd = new EventDelegate(this, "purchaseInit");
			string prodList = "";
			foreach(ItemShopRubyInfo rubyInfo in getruby.Response.data){
				prodList += rubyInfo.productCode + ";";
			}
			IOSMgr.InAppInit(prodList, eventd);
		} else{
			InitRubyList();
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
			temp2.transform.FindChild ("buygold").GetComponent<UILabel> ().text = getgold.Response.data [i].productPrice.ToString();
			temp2.transform.FindChild ("id").GetComponent<UILabel> ().text = getgold.Response.data [i].productId.ToString();
			temp2.transform.FindChild ("Value").GetComponent<UILabel> ().text = getgold.Response.data [i].productValue.ToString();
			Debug.Log("gold code : " + getgold.Response.data [i].productCode);
			float nums = 0;
			nums = i+1;
			if(nums>2){
				nums = 3;
			}
			temp2.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = getgold.Response.data [i].productImage;
			temp2.gameObject.SetActive (true);
			
			
		}
		transform.FindChild ("category 2").GetComponent<UIScrollView> ().ResetPosition ();
		transform.FindChild ("category 2").gameObject.SetActive (false);
	}
	int Gid,Gcost;
	string Gs;
	bool GI = true;
	public void setusergold(int id,int cost,string s){
		GI = true;
		Gid = id;
		Gcost = cost;
		Gs = s;
		Debug.Log (s);
		//Debug.Log("DialogueMgr.DialogClickHandler 1 : " + DialogueMgr.DialogClickHandler);
		DialogueMgr.ShowDialogue ("구매 확인", s , DialogueMgr.DIALOGUE_TYPE.YesNo , DialogueHandler);
		//Debug.Log("DialogueMgr.DialogClickHandler 2 : " + DialogueMgr.IsShown);

	}

	void DialogueHandler(DialogueMgr.BTNS btn){
		if (btn == DialogueMgr.BTNS.Btn1) {
			if (int.Parse (UserMgr.UserInfo.userRuby) < Gcost) {
				DialogueMgr.ShowDialogue ("구매 실패", "루비가 부족합니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			} else {
				Sgold = Gs;
				if(GI){
				golds = new IAPEvent (new EventDelegate (this, "mGrequestIAP"));
				NetMgr.PurchaseGold (Gid, golds);
				}else{
					items = new IAPEvent (new EventDelegate (this, "mIrequestIAP"));
					NetMgr.PurchaseItem (Gid, items);
				}
			}
		}

	}

	void mGrequestIAP(){

		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "addgold"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);

	}

	void addgold(){
		UserMgr.UserInfo.userGoldenBall = mProfileEvent.Response.data.userGoldenBall;
		UserMgr.UserInfo.userRuby = mProfileEvent.Response.data.userRuby;
		DialogueMgr.ShowDialogue ("구매 성공", Sgold+" 완료.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
	}

	void item(){		
		for (int i = 0; i<getitem.Response.data.Count; i++) {
			
			temp3 = (GameObject)Instantiate (origin3, new Vector3 (0, 0, 0), origin3.transform.localRotation);
			temp3.transform.parent = origin3.transform.parent;
			temp3.transform.localScale = new Vector3 (1, 1, 1);
			temp3.transform.localPosition = new Vector3 (originV3.x, originV3.y - (i * gap), originV3.z);
			temp3.transform.FindChild ("LblBody").GetComponent<UILabel> ().text = getitem.Response.data [i].productName;
			temp3.transform.FindChild ("LblDescription").GetComponent<UILabel> ().text = getitem.Response.data [i].productDesc;
			temp3.transform.FindChild ("LblPrice").GetComponent<UILabel> ().text = "가격 : " + UtilMgr.AddsThousandsSeparator (getitem.Response.data [i].productPrice.ToString ())+"루비";
			//Debug.Log (getitem.Response.data [i].productCode);
			temp3.transform.FindChild ("buygold").GetComponent<UILabel> ().text = getitem.Response.data [i].productPrice.ToString();
			temp3.transform.FindChild ("id").GetComponent<UILabel> ().text = getitem.Response.data [i].productId.ToString();
			
			
			
			temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = getitem.Response.data [i].productImage;
			
			
			
			//temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_00"+((i%3)+1).ToString();
			temp3.gameObject.SetActive (true);
			
			
		}
		transform.FindChild ("category 3").GetComponent<UIScrollView> ().ResetPosition ();
		transform.FindChild ("category 3").gameObject.SetActive (false);
	//	Debug.Log ("getitem.Response.data.Count : " + getitem.Response.data.Count);

	}

	void InitRubyList(){
		temp1 = (GameObject)Instantiate (imageC2, new Vector3 (0, 0, 0), origin1.transform.localRotation);
		temp1.transform.parent = origin1.transform.parent;
		temp1.transform.localScale = new Vector3 (1, 1, 1);
		temp1.transform.localPosition = new Vector3 (originV1.x, originV1.y - ((0) * gap), originV1.z);
		temp1.gameObject.SetActive (true);
		for (int i = 1; i<getruby.Response.data.Count+1; i++) {
			
			temp1 = (GameObject)Instantiate (origin1, new Vector3 (0, 0, 0), origin1.transform.localRotation);
			temp1.transform.parent = origin1.transform.parent;
			temp1.transform.localScale = new Vector3 (1, 1, 1);
			temp1.transform.localPosition = new Vector3 (originV1.x, originV1.y - ((i) * gap), originV1.z);
			temp1.transform.FindChild ("LblBody").GetComponent<UILabel> ().text = "루비 " + getruby.Response.data [i-1].productValue+"개";
			temp1.transform.FindChild ("LblDescription").GetComponent<UILabel> ().text = getruby.Response.data [i-1].productDesc;
			if(Application.platform == RuntimePlatform.IPhonePlayer)
				temp1.transform.FindChild ("LblPrice").GetComponent<UILabel> ().text = "가격 : $ " + getruby.Response.data [i-1].productPriceIOS;
			else
				temp1.transform.FindChild ("LblPrice").GetComponent<UILabel> ().text = "가격 : " + UtilMgr.AddsThousandsSeparator (getruby.Response.data [i-1].productPrice.ToString ())+"원";
			temp1.transform.FindChild ("code").GetComponent<UILabel> ().text = getruby.Response.data [i-1].productCode;
			temp1.transform.FindChild ("id").GetComponent<UILabel> ().text = getruby.Response.data [i-1].productId.ToString();
			temp1.transform.FindChild ("add").FindChild ("buyruby").GetComponent<UILabel> ().text = getruby.Response.data [i-1].productValue.ToString();
			temp1.transform.FindChild ("add").FindChild ("addruby").GetComponent<UILabel> ().text = getruby.Response.data [i-1].bonusRuby.ToString();
			temp1.transform.FindChild ("add").FindChild ("addgold").GetComponent<UILabel> ().text = getruby.Response.data [i-1].bonusGoldenball.ToString();
			//	Debug.Log("ruby code : " + getruby.Response.data [i].productCode);
			
			temp1.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = getruby.Response.data [i-1].rubyImage;
			temp1.gameObject.SetActive (true);
			
			
		}
		transform.FindChild ("category 1").GetComponent<UIScrollView> ().ResetPosition ();
	}

	public void setuseritem(int id,int cost,string s){
		GI = false;
		Gid = id;
		Gcost = cost;
		Gs = s;

		//Debug.Log("DialogueMgr.DialogClickHandler 1 : " + DialogueMgr.DialogClickHandler);
		DialogueMgr.ShowDialogue ("구매 확인", s , DialogueMgr.DIALOGUE_TYPE.YesNo , DialogueHandler);
		//Debug.Log("DialogueMgr.DialogClickHandler 2 : " + DialogueMgr.IsShown);
	

	
	}


	void mIrequestIAP(){
		
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "additem"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);
		
	}
	void additem(){
		UserMgr.UserInfo.item = mProfileEvent.Response.data.item;
		UserMgr.UserInfo.userRuby = mProfileEvent.Response.data.userRuby;
		DialogueMgr.ShowDialogue ("구매 성공", Sgold+" 구매 완료.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
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
		NetMgr.RequestIAP (itemid,itemcode,IsTest,RequestIAP);
	}

	void mRequestIAP(){
		#if(UNITY_ANDROID)
		//if (RequestIAP.Response.data != null) {
			//if(RequestIAP.Response.data.productId==itemid&&RequestIAP.Response.data.productCode==itemcode){
				orderNo = RequestIAP.Response.data.orderNo;
				//RequestIAP.Response.data.
				GoogleIAB.init(Constants.GOOGLE_PUBLIC_KEY);
		GoogleIAB.purchaseProduct(itemcode, RequestIAP.Response.data.purchaseKey );
			//}
		//}
		#else
		orderNo = RequestIAP.Response.data.orderNo;
		IOSMgr.BuyItem(itemcode);
		#endif
	}

	void addruby(){
		//mProfileEvent.Response.data.userRuby = (float.Parse(mProfileEvent.Response.data.userRuby)+Bruby+Aruby).ToString();
		//mProfileEvent.Response.data.userGoldenBall = (float.Parse(mProfileEvent.Response.data.userGoldenBall)+Agold).ToString();
		UserMgr.UserInfo.userRuby = mProfileEvent.Response.data.userRuby;
		UserMgr.UserInfo.userGoldenBall = mProfileEvent.Response.data.userGoldenBall;
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
	
	void mComsumeIAP(){
		if(ComsumeIAP.Response.message.Equals("200")){
			DialogueMgr.ShowDialogue("구매 실패", itemproduct + " 영수증 정보에 오류가 있습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else{
			#if(UNITY_ANDROID)
			GoogleIAB.consumeProduct (itemcode);
			#else
			RequestDone();
			#endif
		}
	}

	void RequestDone(){
		DoneIAP  = new IAPEvent (new EventDelegate (this, "mDoneIAP"));
		NetMgr.DoneIAP (orderNo,IsTest,DoneIAP);
	}

	#if(UNITY_ANDROID)
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
		NetMgr.ComsumeIAP (orderNo,purchase.purchaseToken,IsTest,ComsumeIAP);
//		orderNo = ComsumeIAP.Response.data.orderNo;

		Debug.Log( "purchaseSucceededEvent: " + purchase );
	}
	
	void purchaseFailedEvent( string error, int response )
	{

		CancelIAP  = new IAPEvent (new EventDelegate (this, "mCancelIAP"));
		NetMgr.CancelIAP (orderNo,IsTest,CancelIAP);

		Debug.Log( "purchaseFailedEvent: " + error + ", response: " + response );
	}

	void mDoneIAP(){
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "addruby"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);

		DialogueMgr.ShowDialogue("구매 성공", itemproduct + " 구매가 완료 되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);

		Debug.Log ("All PurchaseSucceeded");

	}

	void mCancelIAP(){
		DialogueMgr.ShowDialogue("구매 실패", itemproduct + " 구매를 실패 했습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		Debug.Log ("FailedEvent");
		
		
	}

	void consumePurchaseSucceededEvent( GooglePurchase purchase )
	{
		RequestDone();
	}
	
	
	void consumePurchaseFailedEvent( string error )
	{
		DialogueMgr.ShowDialogue("컨슘 실패", itemproduct + " 컨슘을 실패 했습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		Debug.Log ("FailedConsume");
		//		CancelIAP  = new IAPEvent (new EventDelegate (this, "mCancelIAP"));
		//		NetMgr.CancelIAP (orderNo,IsTest,CancelIAP);
		//		
		//		Debug.Log( "consumePurchaseFailedEvent: " + error );
	}
	
	#else

	public void purchaseInit(){
		string msg = IOSMgr.GetMsg();
		if(msg.Equals("NO")){
			billingNotSupportedEvent("");
		} else{
			billingSupportedEvent();
			IOSProducts products = Newtonsoft.Json.JsonConvert.DeserializeObject<IOSProducts>(msg);
			foreach(ItemShopRubyInfo rubyInfo in getruby.Response.data){
				switch(rubyInfo.productCode){
				case "ruby_50" : rubyInfo.productPriceIOS = products.ruby_50;
					break;
				case "ruby_100" : rubyInfo.productPriceIOS = products.ruby_100;
					break;
				case "ruby_200" : rubyInfo.productPriceIOS = products.ruby_200;
					break;
				case "ruby_300" : rubyInfo.productPriceIOS = products.ruby_300;
					break;
				case "ruby_500" : rubyInfo.productPriceIOS = products.ruby_500;
					break;
				}
			}
		}
		InitRubyList();
	}

	void purchaseSucceededEvent(string receipt)
	{		
		ComsumeIAP  = new IAPEvent (new EventDelegate (this, "mComsumeIAP"));
		NetMgr.ComsumeIAP (orderNo,receipt,IsTest,ComsumeIAP);
//		orderNo = ComsumeIAP.Response.data.orderNo;
	}
	
	void purchaseFailedEvent(string receipt)
	{		
		CancelIAP  = new IAPEvent (new EventDelegate (this, "mCancelIAP"));
		NetMgr.CancelIAP (orderNo,IsTest,CancelIAP);
	}
	
	void mDoneIAP(){
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "addruby"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);
		
		DialogueMgr.ShowDialogue("구매 성공", itemproduct + " 구매가 완료 되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		
		Debug.Log ("All PurchaseSucceeded");
		
	}
	
	void mCancelIAP(){
		DialogueMgr.ShowDialogue("구매 실패", itemproduct + " 구매를 실패 했습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		Debug.Log ("FailedEvent");
		
		
	}

	#endif

	class IOSProducts{
		string _ruby_50;
		
		public string ruby_50 {
			get {
				return _ruby_50;
			}
			set {
				_ruby_50 = value;
			}
		}
		
		string _ruby_100;
		
		public string ruby_100 {
			get {
				return _ruby_100;
			}
			set {
				_ruby_100 = value;
			}
		}
		
		string _ruby_200;
		
		public string ruby_200 {
			get {
				return _ruby_200;
			}
			set {
				_ruby_200 = value;
			}
		}
		
		string _ruby_300;
		
		public string ruby_300 {
			get {
				return _ruby_300;
			}
			set {
				_ruby_300 = value;
			}
		}
		
		string _ruby_500;
		
		public string ruby_500 {
			get {
				return _ruby_500;
			}
			set {
				_ruby_500 = value;
			}
		}
	}

}
