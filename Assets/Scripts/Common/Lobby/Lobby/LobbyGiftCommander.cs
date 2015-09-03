using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class LobbyGiftCommander : MonoBehaviour {

	//ScriptTitle에서 mGift에 데이터 저장.
	public static GiftListResponse mGift ;

	void Start(){

		//시작시 저장된 데이터를 집어넣음
		//아이템 생성은 UIScrollView.cs에서 관리함
		if (mGift != null) {

			for(int i = 0; i<mGift.gift.Count;i++){

				transform.FindChild ("Gift").FindChild ("Scroll View").GetChild(i+1).FindChild("CoverFlowItem").GetComponent<UITexture>().mainTexture = 
					mGift.Textures[mGift.gift[i].image];
				transform.FindChild ("Gift").FindChild ("Scroll View").GetChild(i+1).FindChild("CoverFlowItem").FindChild("openurl").GetComponent<UILabel>().text = 
					mGift.gift[i].image;
				
				transform.FindChild ("Gift").FindChild ("Scroll View").GetChild(i+1).FindChild("CoverFlowItem").FindChild("product").GetComponent<UILabel>().text = 
					mGift.gift[i].product;

			}

		}

	}

	public class GiftListResponse{

		string _imageurl;
		
		public string imageurl {
			get {
				return _imageurl;
			}
			set {
				_imageurl = value;
			}
		}
		
		List<gift> _gift;
		
		public List<gift> gift {
			get {
				return _gift;
			}
			set {
				_gift = value;
			}
		}

//		Dictionary<int,Texture2D> _image = new Dictionary<int,Texture2D>();
//		public Dictionary<int,Texture2D> image{
//			get {
//				return _image;
//			}
//			set {
//				_image = value;
//			}
//		}

		Dictionary<string,Texture2D> _Textures = new Dictionary<string,Texture2D>();
		public Dictionary<string,Texture2D> Textures{
			get {
				return _Textures;
			}
			set {
				_Textures = value;
			}
		}
		
	}
	
	public class gift{
		string _image;
		
		public string image {
			get {
				return _image;
			}
			set {
				_image = value;
			}
		}
		string _mileage;
		
		public string mileage {
			get {
				return _mileage;
			}
			set {
				_mileage = value;
			}
		}

		string _product;
		
		public string product {
			get {
				return _product;
			}
			set {
				_product = value;
			}
		}
		string _onoff;
		
		public string onoff {
			get {
				return _onoff;
			}
			set {
				_onoff = value;
			}
		}
		List<Detail> _detail;
		
		public List<Detail> detail {
			get {
				return _detail;
			}
			set {
				_detail = value;
			}
		}

	}
	public class Detail{
		string _image;
		
		public string image {
			get {
				return _image;
			}
			set {
				_image = value;
			}
		}
		string _text;
		
		public string text {
			get {
				return _text;
			}
			set {
				_text = value;
			}
		}
	
	}
}
