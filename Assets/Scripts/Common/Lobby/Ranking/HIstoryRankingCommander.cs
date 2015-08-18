using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HIstoryRankingCommander : MonoBehaviour {
	Dictionary<int,Texture2D> List = new Dictionary<int, Texture2D> ();
	GetRankEvent mGetRankEvent;
	public Texture2D texures;
	ContestRankingEvent CRE;
	void Awake(){
		//SetRank ();
	//	mGetRankEvent = new GetRankEvent (new EventDelegate (this, "SetRank1"));
		//NetMgr.GetUserRankingWeeklyGold (UserMgr.UserInfo.memSeq,mGetRankEvent);
	}
	public void Button(int gameSeq){
		//Debug.Log ("gameSeq : " + gameSeq);
		transform.FindChild("Scroll View"). GetComponent<UIDraggablePanel2>().RemoveAll();
		CRE = new ContestRankingEvent (new EventDelegate (this,"SetRank"));
		NetMgr.GetHistoryContestRanking (gameSeq,CRE);
	}
	void SetRank(){
		transform.gameObject.SetActive (true);

		transform.FindChild("Scroll View"). GetComponent<UIDraggablePanel2>().Init(CRE.Response.data.Count, 
		                                                                                              delegate(UIListItem item, int index) {

			item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,134f);
			if(index == 0){
				item.Target.gameObject.transform.FindChild("Sprite").GetComponent<UISprite>().SetRect(720f,124f);
			}else if(index == CRE.Response.data.Count-1){
				item.Target.gameObject.transform.FindChild("Bar").gameObject.SetActive(false);
			}
				item.Target.gameObject.GetComponent<UISprite>().color = new Color(218f/255,220f/255f,222f/255f,1);
//				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rank.ToString();
			item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(false);
			item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(false);

			if(UserMgr.UserInfo.memSeq ==  CRE.Response.data[index].memSeq){
				item.Target.gameObject.transform.FindChild("MyRank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("MyRank").FindChild("Label").GetComponent<UILabel>().text = CRE.Response.data[index].rank.ToString();
			}else{
				item.Target.gameObject.transform.FindChild("Rank").gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = CRE.Response.data[index].rank.ToString();
			}
			item.Target.gameObject.transform.FindChild("Name").GetComponent<UILabel>().text = CRE.Response.data[index].memberName;
//			if(int.Parse(CRE.Response.data[index].itemValue)>=100){
//				item.Target.gameObject.transform.FindChild("Item").GetComponent<UILabel>().text = CRE.Response.data[index].memberName;
//			}else{
			item.Target.gameObject.transform.FindChild("Item").GetComponent<UILabel>().text = CRE.Response.data[index].rewardItem+" "+CRE.Response.data[index].itemValue;
		//	}
			item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = CRE.Response.data[index].score.ToString();
				//item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = mGetRankEvent.Response.data.ranking[index].rewardValue.ToString();
				item.Target.gameObject.name = "Player_"+index.ToString();
				item.Target.gameObject.SetActive(true);
				item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture = texures;
				try{
					Texture2D ex = List[index];
					item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture
						= ex;
				}catch{
					
				if(CRE.Response.data[index].imageName!=""){
					WWW www = new WWW (Constants.IMAGE_SERVER_HOST + CRE.Response.data[index].imagePath + CRE.Response.data[index].imageName);
						StartCoroutine (GetImage (www, item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>(),index));
					}
				}
				
			//}
			
		});
		transform.FindChild ("Scroll View").GetComponent<UIDraggablePanel2> ().ResetPosition ();

		transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild ("GameInfo").localPosition = new Vector3 (-720,0,0);
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
