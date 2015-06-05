using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

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

	public void purchaseAble(){
		Debug.Log("iOS purchase able : "+IOSMgr.GetMsg());
	}
	
	void Start () {
		#if(UNITY_ANDROID)
		#else
//		SoomlaStore.Initialize(new ScriptItemAssets());
		EventDelegate eventd = new EventDelegate(this, "purchaseAble");
		IOSMgr.InAppInit(eventd);
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


	void ruby(){
	


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
			temp3.transform.FindChild ("buygold").GetComponent<UILabel> ().text = getitem.Response.data [i].productPrice.ToString();
			temp3.transform.FindChild ("id").GetComponent<UILabel> ().text = getitem.Response.data [i].productId.ToString();
		


			temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = getitem.Response.data [i].productImage;
		


			//temp3.transform.FindChild ("SprImgItem").GetComponent<UISprite> ().spriteName = "img_gold_00"+((i%3)+1).ToString();
			temp3.gameObject.SetActive (true);
			
			
		}
		transform.FindChild ("category 3").GetComponent<UIScrollView> ().ResetPosition ();
		transform.FindChild ("category 3").gameObject.SetActive (false);
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
		IOSMgr.ButItem(itemcode);
		//RequestIAP.Response.data.
//		GoogleIAB.init(Constants.GOOGLE_PUBLIC_KEY);
//		GoogleIAB.purchaseProduct(itemcode, RequestIAP.Response.data.purchaseKey );
//		Debug.Log("Goods cnt : "+StoreInfo.Goods.Count);
//		foreach(VirtualGood vg in StoreInfo.Goods){
//			Debug.Log("Goods name : "+vg.Name);
//		}
//
//		StoreInventory.BuyItem(itemcode);
		//}
		//}
		#endif
	}

	void OnEnable()
	{
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
		StoreEvents.OnMarketPurchase += onMarketPurchase;
		StoreEvents.OnMarketRefund += onMarketRefund;
		StoreEvents.OnItemPurchased += onItemPurchased;
		StoreEvents.OnGoodEquipped += onGoodEquipped;
		StoreEvents.OnGoodUnEquipped += onGoodUnequipped;
		StoreEvents.OnGoodUpgrade += onGoodUpgrade;
		StoreEvents.OnBillingSupported += onBillingSupported;
		StoreEvents.OnBillingNotSupported += onBillingNotSupported;
		StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
		StoreEvents.OnItemPurchaseStarted += onItemPurchaseStarted;
		StoreEvents.OnUnexpectedErrorInStore += onUnexpectedErrorInStore;
		StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
		StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
		StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
		StoreEvents.OnRestoreTransactionsStarted += onRestoreTransactionsStarted;
		StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;
		StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
		#endif
	}
	
	
	void OnDisable()
	{
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
		StoreEvents.OnMarketPurchase -= onMarketPurchase;
		StoreEvents.OnMarketRefund -= onMarketRefund;
		StoreEvents.OnItemPurchased -= onItemPurchased;
		StoreEvents.OnGoodEquipped -= onGoodEquipped;
		StoreEvents.OnGoodUnEquipped -= onGoodUnequipped;
		StoreEvents.OnGoodUpgrade -= onGoodUpgrade;
		StoreEvents.OnBillingSupported -= onBillingSupported;
		StoreEvents.OnBillingNotSupported -= onBillingNotSupported;
		StoreEvents.OnMarketPurchaseStarted -= onMarketPurchaseStarted;
		StoreEvents.OnItemPurchaseStarted -= onItemPurchaseStarted;
		StoreEvents.OnUnexpectedErrorInStore -= onUnexpectedErrorInStore;
		StoreEvents.OnCurrencyBalanceChanged -= onCurrencyBalanceChanged;
		StoreEvents.OnGoodBalanceChanged -= onGoodBalanceChanged;
		StoreEvents.OnMarketPurchaseCancelled -= onMarketPurchaseCancelled;
		StoreEvents.OnRestoreTransactionsStarted -= onRestoreTransactionsStarted;
		StoreEvents.OnRestoreTransactionsFinished -= onRestoreTransactionsFinished;
		StoreEvents.OnSoomlaStoreInitialized -= onSoomlaStoreInitialized;
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
		orderNo = ComsumeIAP.Response.data.orderNo;

		Debug.Log( "purchaseSucceededEvent: " + purchase );
	}

	void mComsumeIAP(){
		GoogleIAB.consumeProduct (itemcode);
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
		DoneIAP  = new IAPEvent (new EventDelegate (this, "mDoneIAP"));
		NetMgr.DoneIAP (orderNo,IsTest,DoneIAP);
		Debug.Log( "consumePurchaseSucceededEvent: " + purchase );
	}
	
	
	void consumePurchaseFailedEvent( string error )
	{
		CancelIAP  = new IAPEvent (new EventDelegate (this, "mCancelIAP"));
		NetMgr.CancelIAP (orderNo,IsTest,CancelIAP);
		
		Debug.Log( "consumePurchaseFailedEvent: " + error );
	}

	#else

	public void onSoomlaStoreInitialized() {
		
		// some usage examples for add/remove currency
		// some examples
		if (StoreInfo.Currencies.Count>0) {
			try {
				StoreInventory.GiveItem(StoreInfo.Currencies[0].ItemId,4000);
//				SoomlaUtils.LogDebug("SOOMLA ExampleEventHandler", "Currency balance:" + StoreInventory.GetItemBalance(StoreInfo.Currencies[0].ItemId));
				Debug.Log("Initialize succeed");
			} catch (VirtualItemNotFoundException ex){
//				SoomlaUtils.LogError("SOOMLA ExampleEventHandler", ex.Message);
				Debug.Log("Initialize failed");
			}
		}
		Debug.Log("Initialize end");
//		setupItemsTextures();
//		
//		setupItemsAffordability ();
	}

	public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
		Debug.Log("onCurrencyBalanceChanged");
//		if (itemsAffordability != null)
//		{
//			List<string> keys = new List<string> (itemsAffordability.Keys);
//			foreach(string key in keys)
//				itemsAffordability[key] = StoreInventory.CanAfford(key);
//		}
	}

	/// <summary>
	/// Handles a market purchase event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	/// <param name="purchaseToken">Purchase token.</param>
	public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra) {
		Debug.Log("onMarketPurchase");
	}
	
	/// <summary>
	/// Handles a market refund event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketRefund(PurchasableVirtualItem pvi) {
		Debug.Log("onMarketRefund");
	}
	
	/// <summary>
	/// Handles an item purchase event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onItemPurchased(PurchasableVirtualItem pvi, string payload) {
		Debug.Log("onItemPurchased");
	}
	
	/// <summary>
	/// Handles a good equipped event.
	/// </summary>
	/// <param name="good">Equippable virtual good.</param>
	public void onGoodEquipped(EquippableVG good) {
		Debug.Log("onGoodEquipped");
	}
	
	/// <summary>
	/// Handles a good unequipped event.
	/// </summary>
	/// <param name="good">Equippable virtual good.</param>
	public void onGoodUnequipped(EquippableVG good) {
		Debug.Log("onGoodUnequipped");
	}
	
	/// <summary>
	/// Handles a good upgraded event.
	/// </summary>
	/// <param name="good">Virtual good that is being upgraded.</param>
	/// <param name="currentUpgrade">The current upgrade that the given virtual
	/// good is being upgraded to.</param>
	public void onGoodUpgrade(VirtualGood good, UpgradeVG currentUpgrade) {
		Debug.Log("onGoodUpgrade");
	}
	
	/// <summary>
	/// Handles a billing supported event.
	/// </summary>
	public void onBillingSupported() {
		Debug.Log("BillingSupported!");
	}
	
	/// <summary>
	/// Handles a billing NOT supported event.
	/// </summary>
	public void onBillingNotSupported() {
		Debug.Log("BillingNotSupported!");
	}
	
	/// <summary>
	/// Handles a market purchase started event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
		Debug.Log("onMarketPurchaseStarted");
	}
	
	/// <summary>
	/// Handles an item purchase started event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onItemPurchaseStarted(PurchasableVirtualItem pvi) {
		Debug.Log("onItemPurchaseStarted");
	}
	
	/// <summary>
	/// Handles an item purchase cancelled event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
		Debug.Log("onMarketPurchaseCancelled");
	}
	
	/// <summary>
	/// Handles an unexpected error in store event.
	/// </summary>
	/// <param name="message">Error message.</param>
	public void onUnexpectedErrorInStore(string message) {
		Debug.Log("onUnexpectedErrorInStore");
	}
	
	/// <summary>
	/// Handles a currency balance changed event.
	/// </summary>
	/// <param name="virtualCurrency">Virtual currency whose balance has changed.</param>
	/// <param name="balance">Balance of the given virtual currency.</param>
	/// <param name="amountAdded">Amount added to the balance.</param>
//	public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
//		
//	}
	
	/// <summary>
	/// Handles a good balance changed event.
	/// </summary>
	/// <param name="good">Virtual good whose balance has changed.</param>
	/// <param name="balance">Balance.</param>
	/// <param name="amountAdded">Amount added.</param>
	public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
		Debug.Log("onGoodBalanceChanged");
	}
	
	/// <summary>
	/// Handles a restore Transactions process started event.
	/// </summary>
	public void onRestoreTransactionsStarted() {
		Debug.Log("onRestoreTransactionsStarted");
	}
	
	/// <summary>
	/// Handles a restore transactions process finished event.
	/// </summary>
	/// <param name="success">If set to <c>true</c> success.</param>
	public void onRestoreTransactionsFinished(bool success) {
		Debug.Log("onRestoreTransactionsFinished");
	}
	
	/// <summary>
	/// Handles a store controller initialized event.
	/// </summary>
//	public void onSoomlaStoreInitialized() {
//		
//	}
	
	#endif

}
