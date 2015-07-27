using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class LobbyGiftCommander : MonoBehaviour {
	static GiftListResponse mGift;
	public UITexture T;
	void Getdata(){
		Debug.Log ("mGift.imageurl : " + mGift.imageurl);
	//	WWW www = new WWW (mGift.imageurl+"/"+mGift.gift[0].imagename);
		//StartCoroutine (SaveImage (www));

		
	}
		public void Save(Texture2D t) {
			
			
			byte[] bytes = t.EncodeToPNG();
		Debug.Log ("Start Save : " + Application.persistentDataPath + "/image.png");
			
			
		File.WriteAllBytes(Application.persistentDataPath + "/image.png", bytes);

			Debug.Log ("End Save");
			//Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
	//	WWW www = new WWW ("file://"+Application.persistentDataPath + "/image.png");

		//StartCoroutine (
		//GetImage (www);
		//	);
			
		}    
	IEnumerator SaveImage(WWW www)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		Save (tmpTex);

	}
	IEnumerator GetImage(WWW www)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		T.mainTexture = tmpTex;
	}
//	void save(){
//		Debug.Log (Application.persistentDataPath +"/image.txt");
//		StreamWriter swRBell = new StreamWriter(Application.persistentDataPath +"/image.txt"); 
//		swRBell.WriteLine(Application.loadedLevelName );
//		swRBell.Close();
//	}

	void Start(){
	WWW www2 = new WWW ("file://"+Application.persistentDataPath + "/image.png");
		StartCoroutine (GetImage (www2));
		EventDelegate eventd = new EventDelegate(this, "Getdata");
		//WWW www = new WWW("http://appif.liveball.kr:4002/tuby_file/gift/gift.json");
		//StartCoroutine(webProcess(www, eventd));
	}
	
	IEnumerator webProcess(WWW www, EventDelegate eventd){
		float timeSum = 0f;
		while(!www.isDone && 
		      string.IsNullOrEmpty(www.error) && 
		      timeSum < 10f) { 
			timeSum += Time.deltaTime; 
			yield return 0; 
		} 
		
		
		if(www.error == null && www.isDone)
		{
			Debug.Log(www.text);
			mGift = Newtonsoft.Json.JsonConvert.DeserializeObject<GiftListResponse>(www.text);
		}
		
		eventd.Execute();
	}
	class GiftListResponse{

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

	}
	
	class gift{
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
