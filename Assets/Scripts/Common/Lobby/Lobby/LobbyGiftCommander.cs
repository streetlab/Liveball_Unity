using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class LobbyGiftCommander : MonoBehaviour {
	//static GiftListResponse _mGift;

	public static GiftListResponse mGift ;

//	void Awake(){
//		if (mGift != null) {
//			transform.FindChild ("Gift").FindChild ("Scroll View").GetComponent<UIScrollView> ().CoverFlowCount =
//				mGift.gift.Count;
//			Debug.Log(mGift.gift.Count);
//		} else {
//			transform.FindChild ("Gift").FindChild ("Scroll View").GetComponent<UIScrollView> ().CoverFlowCount = 7;
//		}
//	}
	void Start(){

		//	Debug.Log (transform.FindChild ("Gift").FindChild ("Scroll View").GetComponent<UIScrollView> ()==null);
		if (mGift != null) {

			for(int i = 0; i<mGift.gift.Count;i++){
				transform.FindChild ("Gift").FindChild ("Scroll View").FindChild("Item " + i.ToString()).FindChild("CoverFlowItem").GetComponent<UITexture>().mainTexture = 
					mGift.image[i];
				transform.FindChild ("Gift").FindChild ("Scroll View").FindChild("Item " + i.ToString()).FindChild("CoverFlowItem").FindChild("openurl").GetComponent<UILabel>().text = 
					mGift.gift[i].openurl;

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

		Dictionary<int,Texture2D> _image = new Dictionary<int,Texture2D>();
		public Dictionary<int,Texture2D> image{
			get {
				return _image;
			}
			set {
				_image = value;
			}
		}

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
		string _start;
		
		public string start {
			get {
				return _start;
			}
			set {
				_start = value;
			}
		}
		string _end;
		
		public string end {
			get {
				return _end;
			}
			set {
				_end = value;
			}
		}
		string _openurl;
		
		public string openurl {
			get {
				return _openurl;
			}
			set {
				_openurl = value;
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
