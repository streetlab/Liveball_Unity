using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RankingCommander : MonoBehaviour {
	Dictionary<string,Texture2D> List1 = new Dictionary<string, Texture2D> ();
	Dictionary<string,Texture2D> List2 = new Dictionary<string, Texture2D> ();
	Dictionary<string,Texture2D> List3 = new Dictionary<string, Texture2D> ();
	Dictionary<string,Texture2D> List4 = new Dictionary<string, Texture2D> ();
	bool Rank1 = true;
	bool Rank2 = true;
	bool Rank3 = true;
	bool Rank4 = true;
	GetRankEvent mGetRankEvent;
	public Texture2D texures;
	public void List01(){
		Alloff ();
		transform.FindChild ("Scroll View1").gameObject.SetActive (true);
		if (Rank1) {
			mGetRankEvent = new GetRankEvent (new EventDelegate (this, "SetRank1"));
			NetMgr.GetUserRankingSeasonPoint (UserMgr.UserInfo.memSeq, mGetRankEvent);
		}
	}
	public void List02(){
		Alloff ();
		transform.FindChild ("Scroll View2").gameObject.SetActive (true);
		if (Rank2) {
			mGetRankEvent = new GetRankEvent (new EventDelegate (this, "SetRank2"));
			NetMgr.GetUserRankingSeasonForecast (UserMgr.UserInfo.memSeq, mGetRankEvent);
		}
	}
	public void List03(){
		Alloff ();
		transform.FindChild ("Scroll View3").gameObject.SetActive (true);
		if (Rank3) {
			mGetRankEvent = new GetRankEvent (new EventDelegate (this, "SetRank3"));
			NetMgr.GetUserRankingWeeklyPoint (UserMgr.UserInfo.memSeq, mGetRankEvent);
		}
	}
	public void List04(){
		Alloff ();
		transform.FindChild ("Scroll View4").gameObject.SetActive (true);
		if (Rank4) {
			mGetRankEvent = new GetRankEvent (new EventDelegate (this, "SetRank4"));
			NetMgr.GetUserRankingWeeklyForecast (UserMgr.UserInfo.memSeq, mGetRankEvent);
		}
	}
	void Alloff(){
		transform.FindChild ("Scroll View1").gameObject.SetActive (false);
		transform.FindChild ("Scroll View2").gameObject.SetActive (false);
		transform.FindChild ("Scroll View3").gameObject.SetActive (false);
		transform.FindChild ("Scroll View4").gameObject.SetActive (false);
	}
	void SetRank1(){
		
		transform.FindChild ("Scroll View1").gameObject.SetActive (true);
		transform.FindChild("Scroll View1"). GetComponent<UIDraggablePanel2>().Init(mGetRankEvent.Response.data.Count, 
		                                                                           delegate(UIListItem item, int index) {
			
			item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,134f);
			if(index == 0){
				item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,124f);
			}else if(index == mGetRankEvent.Response.data.Count){
				item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(false);
			}
			item.Target.gameObject.GetComponent<UISprite>().color = new Color(218f/255,220f/255f,222f/255f,1);
			//				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rank.ToString();
			item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(false);
			item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(false);
			if(UserMgr.UserInfo.memSeq == mGetRankEvent.Response.data[index].memSeq){
				item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("MyRank").FindChild("Label").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}else{
				item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}

			item.Target.gameObject.transform.FindChild("Name").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].memberName;
			item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text =  mGetRankEvent.Response.data[index].score.ToString();
			//item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rewardValue.ToString();
			item.Target.gameObject.name = "Player_"+index.ToString();
			item.Target.gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture = texures;
		
			try{
					Texture2D ex = List1[mGetRankEvent.Response.data[index].imageName];
				item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture
					= ex;
			}catch{
				
				if(mGetRankEvent.Response.data[index].imageName!=""){
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data[index].imagePath + mGetRankEvent.Response.data[index].imageName);
					StartCoroutine (GetImage1 (www, item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>(),mGetRankEvent.Response.data[index].imageName));
				}
			}

			//}
			
		});
		transform.FindChild ("Scroll View1").GetComponent<UIDraggablePanel2> ().ResetPosition ();
		Rank1 = false;
	}
	void SetRank2(){
		
		transform.FindChild ("Scroll View2").gameObject.SetActive (true);
		transform.FindChild("Scroll View2"). GetComponent<UIDraggablePanel2>().Init(mGetRankEvent.Response.data.Count, 
		                                                                           delegate(UIListItem item, int index) {
			
			item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,134f);
			if(index == 0){
				item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,124f);
			}else if(index == mGetRankEvent.Response.data.Count){
				item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(false);
			}
			item.Target.gameObject.GetComponent<UISprite>().color = new Color(218f/255,220f/255f,222f/255f,1);
			//				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rank.ToString();
			item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(false);
			item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(false);
			if(UserMgr.UserInfo.memSeq == mGetRankEvent.Response.data[index].memSeq){
				item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("MyRank").FindChild("Label").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}else{
				item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}
			item.Target.gameObject.transform.FindChild("Name").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].memberName;
			item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text =  mGetRankEvent.Response.data[index].score.ToString();
			//item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rewardValue.ToString();
			item.Target.gameObject.name = "Player_"+index.ToString();
			item.Target.gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture = texures;
			try{
				Texture2D ex = List2[mGetRankEvent.Response.data[index].imageName];
				item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture
					= ex;
			}catch{
				
				if(mGetRankEvent.Response.data[index].imageName!=""){
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data[index].imagePath + mGetRankEvent.Response.data[index].imageName);
					StartCoroutine (GetImage2 (www, item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>(),mGetRankEvent.Response.data[index].imageName));
				}
			}
			
			//}
			
		});
		transform.FindChild ("Scroll View2").GetComponent<UIDraggablePanel2> ().ResetPosition ();
		Rank2 = false;
	}
	void SetRank3(){
		transform.FindChild ("Scroll View3").gameObject.SetActive (true);
		
		transform.FindChild("Scroll View3"). GetComponent<UIDraggablePanel2>().Init(mGetRankEvent.Response.data.Count, 
		                                                                           delegate(UIListItem item, int index) {
			
			item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,134f);
			if(index == 0){
				item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,124f);
			}else if(index == mGetRankEvent.Response.data.Count){
				item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(false);
			}
			item.Target.gameObject.GetComponent<UISprite>().color = new Color(218f/255,220f/255f,222f/255f,1);
			//				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rank.ToString();

			item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(false);
			item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(false);
			if(UserMgr.UserInfo.memSeq == mGetRankEvent.Response.data[index].memSeq){
				item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("MyRank").FindChild("Label").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}else{
				item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}
			item.Target.gameObject.transform.FindChild("Name").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].memberName;
			item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text =  mGetRankEvent.Response.data[index].score.ToString();
			//item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rewardValue.ToString();
			item.Target.gameObject.name = "Player_"+index.ToString();
			item.Target.gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture = texures;
			try{
				Texture2D ex = List3[mGetRankEvent.Response.data[index].imageName];
				item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture
					= ex;
			}catch{
				
				if(mGetRankEvent.Response.data[index].imageName!=""){
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data[index].imagePath + mGetRankEvent.Response.data[index].imageName);
					StartCoroutine (GetImage3 (www, item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>(),mGetRankEvent.Response.data[index].imageName));
				}
			}
			
			//}
			
		});
		transform.FindChild ("Scroll View3").GetComponent<UIDraggablePanel2> ().ResetPosition ();
		Rank3 = false;
	}
	void SetRank4(){
		transform.FindChild ("Scroll View4").gameObject.SetActive (true);
		
		transform.FindChild("Scroll View4"). GetComponent<UIDraggablePanel2>().Init(mGetRankEvent.Response.data.Count, 
		                                                                           delegate(UIListItem item, int index) {
			
			item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,134f);
			if(index == 0){
				item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,124f);
			}else if(index == mGetRankEvent.Response.data.Count){
				item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(false);
			}
			item.Target.gameObject.GetComponent<UISprite>().color = new Color(218f/255,220f/255f,222f/255f,1);
			//				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rank.ToString();

			item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(false);
			item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(false);
			if(UserMgr.UserInfo.memSeq == mGetRankEvent.Response.data[index].memSeq){
				item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("MyRank").FindChild("Label").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}else{
				item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].rank.ToString();
			}
			item.Target.gameObject.transform.FindChild("Name").GetComponent<UILabel>().text = mGetRankEvent.Response.data[index].memberName;
			item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text =  mGetRankEvent.Response.data[index].score.ToString();
			//item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rewardValue.ToString();
			item.Target.gameObject.name = "Player_"+index.ToString();
			item.Target.gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture = texures;
			try{
				Texture2D ex = List4[mGetRankEvent.Response.data[index].imageName];
				item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture
					= ex;
			}catch{
				
				if(mGetRankEvent.Response.data[index].imageName!=""){
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data[index].imagePath + mGetRankEvent.Response.data[index].imageName);
				
					StartCoroutine (GetImage4 (www, item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>(),mGetRankEvent.Response.data[index].imageName));
				
					}
			}
			
			//}
			
		});
		transform.FindChild ("Scroll View4").GetComponent<UIDraggablePanel2> ().ResetPosition ();
		Rank4 = false;
	}
	IEnumerator GetImage1(WWW www, UITexture texture,string index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		
		try{
			www.LoadImageIntoTexture (tmpTex);
			List1.Add (index, tmpTex);
		}catch{
			Debug.Log("Same key : " + index.ToString());
		}
		texture.mainTexture = tmpTex;
		
	}
	IEnumerator GetImage2(WWW www, UITexture texture,string index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		
		try{
			www.LoadImageIntoTexture (tmpTex);
			List2.Add (index, tmpTex);
		}catch{
			Debug.Log("Same key : " + index.ToString());
		}
		texture.mainTexture = tmpTex;
		
	}
	IEnumerator GetImage3(WWW www, UITexture texture,string index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		
		try{
			www.LoadImageIntoTexture (tmpTex);
			List3.Add (index, tmpTex);
		}catch{
			Debug.Log("Same key : " + index.ToString());
		}
		texture.mainTexture = tmpTex;
		
	}
	IEnumerator GetImage4(WWW www, UITexture texture,string index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		
		try{
			www.LoadImageIntoTexture (tmpTex);
			List4.Add (index, tmpTex);
		}catch{
			Debug.Log("Same key : " + index.ToString());
		}
		texture.mainTexture = tmpTex;
		
	}
}
