using UnityEngine;
using System.Collections;

public class GameParticipantRank : MonoBehaviour {
	GetGameParticipantRankingEvent Rank;
	public Texture2D texures;
	public void Start(){
		Vector3 pos = transform.localPosition;
		pos.y += UtilMgr.GetScaledPositionY();
		transform.localPosition = pos;
	
		Rank = new GetGameParticipantRankingEvent (new EventDelegate (this, "SetRank"));
		NetMgr.GetGameParticipantRanking (Rank);
	}
	void SetRank(){

		Debug.Log ("SetRank");
		transform.FindChild ("BG_W").FindChild("BG_BOT").FindChild ("MyRank").FindChild ("Name").GetComponent<UILabel> ().text = 
			UserMgr.UserInfo.memberName;
		transform.FindChild ("BG_W").FindChild("BG_BOT").FindChild ("MyRank").FindChild ("Score").GetComponent<UILabel> ().text = 
			Rank.Response.data.rankValue.ToString();
		transform.FindChild ("BG_W").FindChild("BG_BOT").FindChild ("MyRank").FindChild ("Reward").GetComponent<UILabel> ().text = 
			Rank.Response.data.rewardValue.ToString();
		transform.FindChild ("BG_W").FindChild("BG_BOT").FindChild ("MyRank").FindChild ("Bule Left").FindChild("GameObject").FindChild("Rank").GetComponent<UILabel> ().text = 
			Rank.Response.data.myRank.ToString();
		if (UserMgr.UserInfo.Textures != null) {
			transform.FindChild ("BG_W").FindChild ("BG_BOT").FindChild ("MyRank").FindChild ("photo").FindChild ("Sprite").FindChild ("Texture").GetComponent<UITexture> ().mainTexture = 
			UserMgr.UserInfo.Textures;
		}
		transform.FindChild ("BG_W").FindChild("Scroll View"). GetComponent<UIDraggablePanel2>().Init(Rank.Response.data.rank.Count, 
		                                                                         delegate(UIListItem item, int index) {

			item.Target.gameObject.transform.FindChild("Rank").GetComponent<UILabel>().text = Rank.Response.data.rank[index].rank.ToString();
			item.Target.gameObject.transform.FindChild("Name").GetComponent<UILabel>().text = Rank.Response.data.rank[index].memberName;
			item.Target.gameObject.transform.FindChild("Score").GetComponent<UILabel>().text = Rank.Response.data.rank[index].rankValue.ToString();
			item.Target.gameObject.transform.FindChild("Reward").GetComponent<UILabel>().text = Rank.Response.data.rank[index].rewardValue.ToString();
			item.Target.gameObject.name = "Player_"+index.ToString();
			item.Target.gameObject.SetActive(true);
			item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>().mainTexture = texures;
			if(Rank.Response.data.rank[index].imageName!=""){
			WWW www = new WWW (Constants.IMAGE_SERVER_HOST + Rank.Response.data.rank[index].imagePath + Rank.Response.data.rank[index].imageName);
			StartCoroutine (GetImage (www, item.Target.gameObject.transform.FindChild("photo").FindChild("Sprite").FindChild("Texture").GetComponent<UITexture>()));
			}
			});
		transform.FindChild ("BG_W").FindChild ("Scroll View").GetComponent<UIDraggablePanel2> ().ResetPosition ();
}
	IEnumerator GetImage(WWW www, UITexture texture)
	{
		yield return www;
		Texture2D tmpTex = new Texture2D (0, 0);
		www.LoadImageIntoTexture (tmpTex);
		texture.mainTexture = tmpTex;
		//Sprite a = Sprite.Create(tmpTex,new Rect(0,0,tmpTex.width,tmpTex.y),new Vector2(0.5f,0.5f));
	}
	public void close(){
		gameObject.SetActive (false);
	}
}