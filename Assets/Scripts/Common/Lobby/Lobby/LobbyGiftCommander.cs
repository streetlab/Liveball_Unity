using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class LobbyGiftCommander : MonoBehaviour {
	//static GiftListResponse _mGift;

	public static GiftListResponse mGift ;

	void Awake(){
		if (mGift != null) {
			transform.FindChild ("Gift").FindChild ("Scroll View").GetComponent<UIScrollView> ().CoverFlowCount =
				mGift.gift.Count;
		} else {
			transform.FindChild ("Gift").FindChild ("Scroll View").GetComponent<UIScrollView> ().CoverFlowCount = 7;
		}
	}
	void Start(){

		//	Debug.Log (transform.FindChild ("Gift").FindChild ("Scroll View").GetComponent<UIScrollView> ()==null);
		if (mGift != null) {

			for(int i = 0; i<mGift.gift.Count;i++){
				transform.FindChild ("Gift").FindChild ("Scroll View").FindChild("Item " + i.ToString()).GetComponent<UITexture>().mainTexture = 
					mGift.image[i];
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
	}
	
	public class gift{
		string _imagename;
		
		public string imagename {
			get {
				return _imagename;
			}
			set {
				_imagename = value;
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
	
	}
}
