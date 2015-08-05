using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RankingCommander : MonoBehaviour {
	Dictionary<int,Texture2D> List = new Dictionary<int, Texture2D> ();
	GetRankEvent mGetRankEvent;
	public Texture2D texures;
	void Awake(){
		SetRank ();
	//	mGetRankEvent = new GetRankEvent (new EventDelegate (this, "SetRank1"));
		//NetMgr.GetUserRankingWeeklyGold (UserMgr.UserInfo.memSeq,mGetRankEvent);
	}
	void Set1(){

	}
	void SetRank(){
		

		transform.FindChild("Scroll View"). GetComponent<UIDraggablePanel2>().Init(99, 
		                                                                                              delegate(UIListItem item, int index) {
//			if(mGetRankEvent.Response.data.ranking.Count==index){
//				for(int i = 0; i<item.Target.gameObject.transform.childCount;i++){
//					item.Target.gameObject.transform.GetChild(i).gameObject.SetActive(false);
//					
//				}
//				item.Target.gameObject.GetComponent<UISprite>().color = new Color(1,1,1,1);
//				item.Target.gameObject.SetActive(true);
//			}
//			else{
//				for(int i = 0; i<item.Target.gameObject.transform.childCount;i++){
//					item.Target.gameObject.transform.GetChild(i).gameObject.SetActive(true);
//					
//				}
			item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,134f);
			if(index == 0){
				item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,124f);
			}else if(index == 99){
				item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(false);
			}
				item.Target.gameObject.GetComponent<UISprite>().color = new Color(218f/255,220f/255f,222f/255f,1);
//				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rank.ToString();
				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = index.ToString();
				//item.Target.gameObject.transform.FindChild("Name").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].memberName;
				//item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rankValue.ToString();
				//item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rewardValue.ToString();
				item.Target.gameObject.name = "Player_"+index.ToString();
				item.Target.gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture = texures;
				try{
					Texture2D ex = List[index];
					item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture
						= ex;
				}catch{
					
//					if(mGetRankEvent.Response.data.ranking[index].imageName!=""){
//						WWW www = new WWW (Constants.IMAGE_SERVER_HOST + mGetRankEvent.Response.data.ranking[index].imagePath + mGetRankEvent.Response.data.ranking[index].imageName);
//						StartCoroutine (GetImage (www, item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>(),index));
//					}
				}
				
			//}
			
		});
		transform.FindChild ("Scroll View").GetComponent<UIDraggablePanel2> ().ResetPosition ();
	}
	IEnumerator GetImage(WWW www, UITexture texture,int index)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		try{
			List.Add (index, tmpTex);
		}catch{
			Debug.Log("Same key : " + index.ToString());
		}
		texture.mainTexture = tmpTex;
		
		//Sprite a = Sprite.Create(tmpTex,new Rect(0,0,tmpTex.width,tmpTex.y),new Vector2(0.5f,0.5f));
	}
}
