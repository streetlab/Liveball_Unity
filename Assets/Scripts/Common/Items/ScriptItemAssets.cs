using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class ScriptItemAssets : IStoreAssets {

	/// <summary>
	/// see parent.
	/// </summary>
	public int GetVersion() {
		return 0;
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{};
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualGood[] GetGoods() {
		return new VirtualGood[] {RUBY_50, RUBY_100};
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] {};
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{GENERAL_CATEGORY};
	}
	
	/** Static Final Members **/
	
//	public const string MUFFIN_CURRENCY_ITEM_ID      = "currency_muffin";
//	
//	public const string TENMUFF_PACK_PRODUCT_ID      = "android.test.refunded";
//	
//	public const string FIFTYMUFF_PACK_PRODUCT_ID    = "android.test.canceled";
//	
//	public const string FOURHUNDMUFF_PACK_PRODUCT_ID = "android.test.purchased";
//	
//	public const string THOUSANDMUFF_PACK_PRODUCT_ID = "2500_pack";
//	
//	public const string MUFFINCAKE_ITEM_ID   = "fruit_cake";
//	
//	public const string PAVLOVA_ITEM_ID   = "pavlova";
//	
//	public const string CHOCLATECAKE_ITEM_ID   = "chocolate_cake";
//	
//	public const string CREAMCUP_ITEM_ID   = "cream_cup";
//	
//	public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads";

	public const string RUBY_50_ID = "ruby_50";

	public const string RUBY_100_ID = "ruby_100";
	
	
	/** Virtual Currencies **/
	
//	public static VirtualCurrency MUFFIN_CURRENCY = new VirtualCurrency(
//		"Muffins",										// name
//		"",												// description
//		MUFFIN_CURRENCY_ITEM_ID							// item id
//		);
	
	
	/** Virtual Currency Packs **/
	
//	public static VirtualCurrencyPack TENMUFF_PACK = new VirtualCurrencyPack(
//		"10 Muffins",                                   // name
//		"Test refund of an item",                       // description
//		"muffins_10",                                   // item id
//		10,												// number of currencies in the pack
//		MUFFIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
//		new PurchaseWithMarket(TENMUFF_PACK_PRODUCT_ID, 0.99)
//		);
//	
//	public static VirtualCurrencyPack FIFTYMUFF_PACK = new VirtualCurrencyPack(
//		"50 Muffins",                                   // name
//		"Test cancellation of an item",                 // description
//		"muffins_50",                                   // item id
//		50,                                             // number of currencies in the pack
//		MUFFIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
//		new PurchaseWithMarket(FIFTYMUFF_PACK_PRODUCT_ID, 1.99)
//		);
//	
//	public static VirtualCurrencyPack FOURHUNDMUFF_PACK = new VirtualCurrencyPack(
//		"400 Muffins",                                  // name
//		"Test purchase of an item",                 	// description
//		"muffins_400",                                  // item id
//		400,                                            // number of currencies in the pack
//		MUFFIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
//		new PurchaseWithMarket(FOURHUNDMUFF_PACK_PRODUCT_ID, 4.99)
//		);
//	
//	public static VirtualCurrencyPack THOUSANDMUFF_PACK = new VirtualCurrencyPack(
//		"1000 Muffins",                                 // name
//		"Test item unavailable",                 		// description
//		"muffins_1000",                                 // item id
//		1000,                                           // number of currencies in the pack
//		MUFFIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
//		new PurchaseWithMarket(THOUSANDMUFF_PACK_PRODUCT_ID, 8.99)
//		);
	
	/** Virtual Goods **/

	public static VirtualGood RUBY_50 = new SingleUseVG(
		"RUBY_50",                                       		// name
		"ruby 50...", // description
		"ruby_50",                                       		// item id
		new PurchaseWithVirtualItem("ruby_50", 1)); // the way this virtual good is purchased

	public static VirtualGood RUBY_100 = new SingleUseVG(
		"RUBY_100",                                       		// name
		"ruby 100...", // description
		"ruby_100",                                       		// item id
		new PurchaseWithVirtualItem("ruby_100", 1)); // the way this virtual good is purchased
	
//	public static VirtualGood MUFFINCAKE_GOOD = new SingleUseVG(
//		"Fruit Cake",                                       		// name
//		"Customers buy a double portion on each purchase of this cake", // description
//		"fruit_cake",                                       		// item id
//		new PurchaseWithVirtualItem(MUFFIN_CURRENCY_ITEM_ID, 225)); // the way this virtual good is purchased
//	
//	public static VirtualGood PAVLOVA_GOOD = new SingleUseVG(
//		"Pavlova",                                         			// name
//		"Gives customers a sugar rush and they call their friends", // description
//		"pavlova",                                          		// item id
//		new PurchaseWithVirtualItem(MUFFIN_CURRENCY_ITEM_ID, 175)); // the way this virtual good is purchased
//	
//	public static VirtualGood CHOCLATECAKE_GOOD = new SingleUseVG(
//		"Chocolate Cake",                                   		// name
//		"A classic cake to maximize customer satisfaction",	 		// description
//		"chocolate_cake",                                   		// item id
//		new PurchaseWithVirtualItem(MUFFIN_CURRENCY_ITEM_ID, 250)); // the way this virtual good is purchased
//	
//	
//	public static VirtualGood CREAMCUP_GOOD = new SingleUseVG(
//		"Cream Cup",                                        		// name
//		"Increase bakery reputation with this original pastry",   	// description
//		"cream_cup",                                        		// item id
//		new PurchaseWithVirtualItem(MUFFIN_CURRENCY_ITEM_ID, 50));  // the way this virtual good is purchased
	
	
	/** Virtual Categories **/
	// The muffin rush theme doesn't support categories, so we just put everything under a general category.
	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
		"General",
//		new List<string>(new string[] { MUFFINCAKE_ITEM_ID, PAVLOVA_ITEM_ID, CHOCLATECAKE_ITEM_ID, CREAMCUP_ITEM_ID }
		new List<string>(new string[] {"ruby_50", "ruby_100"}
		));
	
	
	/** LifeTimeVGs **/
	// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
//	public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
//		"No Ads", 														// name
//		"No More Ads!",				 									// description
//		"no_ads",														// item id
//		new PurchaseWithMarket(NO_ADS_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased




}
