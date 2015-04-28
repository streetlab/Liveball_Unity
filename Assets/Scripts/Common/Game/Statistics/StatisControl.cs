using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StatisControl : MonoBehaviour {

	public GameObject bgs;
	public float gap = 536;
	public float bargap = 122;
	public List<string> labals = new List<string>();
	Vector3 positions;
	Vector3 barposition;
	List<List<List<string>>> ALL = new List<List<List<string>>> ();
	List<List<string>> AVG = new List<List<string>>();
	List<List<string>> ERA = new List<List<string>>();
	List<List<string>> HR = new List<List<string>>();
	List<List<string>> WIN = new List<List<string>>();


	GetPlayerStatisticsEvent mStatisticsEvent;


	public void editng(){
		setposition ();
	}
	void Start(){

		Init();

		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}

	void Init(){
		mStatisticsEvent = new GetPlayerStatisticsEvent(new EventDelegate(this, "GotStatistics"));
		NetMgr.GetPlayerStatistics(mStatisticsEvent);
	}

	public void GotStatistics(){
		for (int i = 0; i<ALL.Count; i++) {
			ALL[i].Clear();
		}
		ALL.Clear ();
		for (int i = 0; i<7; i++) {
			AVG.Add (new List<string>());
			ERA.Add (new List<string>());
			HR.Add (new List<string>());
			WIN.Add (new List<string>());
		};
		ALL.Add (AVG);
		ALL.Add (ERA);
		ALL.Add (HR);
		ALL.Add (WIN);
		for (int i =0; i<mStatisticsEvent.Response.data.AVG.Count; i++) {
			AVG[0].Add(mStatisticsEvent.Response.data.AVG[i].ranking.ToString());
			ERA[0].Add(mStatisticsEvent.Response.data.ERA[i].ranking.ToString());
			HR[0].Add(mStatisticsEvent.Response.data.HR[i].ranking.ToString());
			WIN[0].Add(mStatisticsEvent.Response.data.WIN[i].ranking.ToString());

			AVG[1].Add(mStatisticsEvent.Response.data.AVG[i].playerName);
			ERA[1].Add(mStatisticsEvent.Response.data.ERA[i].playerName);
			HR[1].Add(mStatisticsEvent.Response.data.HR[i].playerName);
			WIN[1].Add(mStatisticsEvent.Response.data.WIN[i].playerName);

			AVG[2].Add(mStatisticsEvent.Response.data.AVG[i].teamName);
			ERA[2].Add(mStatisticsEvent.Response.data.ERA[i].teamName);
			HR[2].Add(mStatisticsEvent.Response.data.HR[i].teamName);
			WIN[2].Add(mStatisticsEvent.Response.data.WIN[i].teamName);

			AVG[3].Add(mStatisticsEvent.Response.data.AVG[i].record);
			ERA[3].Add(mStatisticsEvent.Response.data.ERA[i].record);
			HR[3].Add(mStatisticsEvent.Response.data.HR[i].record);
			WIN[3].Add(mStatisticsEvent.Response.data.WIN[i].record);

			AVG[4].Add(mStatisticsEvent.Response.data.AVG[i].teamCode);
			ERA[4].Add(mStatisticsEvent.Response.data.ERA[i].teamCode);
			HR[4].Add(mStatisticsEvent.Response.data.HR[i].teamCode);
			WIN[4].Add(mStatisticsEvent.Response.data.WIN[i].teamCode);

			AVG[5].Add(mStatisticsEvent.Response.data.AVG[i].imageName);
			ERA[5].Add(mStatisticsEvent.Response.data.ERA[i].imageName);
			HR[5].Add(mStatisticsEvent.Response.data.HR[i].imageName);
			WIN[5].Add(mStatisticsEvent.Response.data.WIN[i].imageName);

			AVG[6].Add(mStatisticsEvent.Response.data.AVG[i].imagePath);
			ERA[6].Add(mStatisticsEvent.Response.data.ERA[i].imagePath);
			HR[6].Add(mStatisticsEvent.Response.data.HR[i].imagePath);
			WIN[6].Add(mStatisticsEvent.Response.data.WIN[i].imagePath);
		}


		Debug.Log(mStatisticsEvent.Response.data.AVG[0].playerName
		          +"'s AVG ranking is "+mStatisticsEvent.Response.data.AVG[0].ranking);
		setposition ();
	}


	void setposition(){
		positions = bgs.transform.GetChild(0).transform.localPosition;
		for(int i = 0;i<bgs.transform.childCount;i++){

			bgs.transform.GetChild(i).transform.localPosition = new Vector3(positions.x,positions.y-(gap*i),positions.z);

			bgs.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UILabel>().text = labals[i];
			barposition = bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).transform.localPosition;
			for(int a = 0; a<bgs.transform.GetChild(i).GetChild(0).GetChild(0).childCount-1;a++){

				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).transform.localPosition = new Vector3(
					barposition.x,barposition.y-(a*bargap),barposition.z);
			
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST+ALL[i][6][a]+ALL[i][5][a]);
				Debug.Log(bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(0).GetChild(0).GetChild(0).gameObject);
				StartCoroutine(GetImage (www,bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(0).GetChild(0).GetChild(0).gameObject));

				//bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(0).GetChild(0).GetChild(0).
				//	GetComponent<UISprite>().spriteName = "";
			
			//	Debug.Log(bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).gameObject);
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).GetComponent<UILabel>().text = ALL[i][1][a];
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UILabel>().text = ALL[i][2][a];
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(3).GetComponent<UILabel>().text = ALL[i][3][a];
					
			}
		}
		for (int i = 0; i <ALL.Count; i++) {
			transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<UILabel>().text = labals[i];
		
			for(int a = 0; a<transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).childCount;a++){
				WWW www = new WWW (Constants.IMAGE_SERVER_HOST+ALL[i][6][a]+ALL[i][5][a]);
				StartCoroutine(GetImage (www,transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(0).GetChild(0).GetChild(0).gameObject));
				Debug.Log(Constants.IMAGE_SERVER_HOST+ALL[i][6][a]+ALL[i][5][a]);
				//Debug.Log (Constants.IMAGE_SERVER_HOST+mEvent.Response.data.hitter [index].cardImagePath+image[index]);

				//transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(0).GetComponent<UISprite>().spriteName = "";
				transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(1).GetComponent<UILabel>().text = ALL[i][1][a];
				transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(2).GetComponent<UILabel>().text = ALL[i][2][a];
				transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(3).GetComponent<UILabel>().text = ALL[i][3][a];
			//	Debug.Log(transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(1));
			//	Debug.Log(transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(2));
			//	Debug.Log(transform.GetChild(i+2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(a).GetChild(3));
			}
		}


	}
	void setpositionold(){
		positions = bgs.transform.GetChild(0).transform.localPosition;
		for(int i = 0;i<bgs.transform.childCount;i++){
			
			bgs.transform.GetChild(i).transform.localPosition = new Vector3(positions.x,positions.y-(gap*i),positions.z);
			
			bgs.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UILabel>().text = "set";
			barposition = bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).transform.localPosition;
			for(int a = 0; a<bgs.transform.GetChild(i).GetChild(0).GetChild(0).childCount-1;a++){
				
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).transform.localPosition = new Vector3(
					barposition.x,barposition.y-(a*bargap),barposition.z);
				bgs.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UILabel>().text = labals[i];
				//bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(0).
				//	GetComponent<UISprite>().spriteName = "";
				
				//	Debug.Log(bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).gameObject);
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).GetComponent<UILabel>().text = "name";
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UILabel>().text = "team name";
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(3).GetComponent<UILabel>().text = "0.000";
				
			}
		}
		
		
	}

	public void Allveiw(int i){
		Debug.Log (i);
		transform.GetChild (i + 2).gameObject.SetActive (true);
		transform.GetChild (0).gameObject.SetActive (false);
	
		transform.parent.GetChild (1).GetChild (1).gameObject.SetActive (false);
		transform.parent.GetChild (1).GetChild (0).GetChild(1).gameObject.SetActive (false);
		transform.parent.GetChild (1).GetChild (0).GetChild(2).gameObject.SetActive (false);
		transform.parent.GetChild (1).GetChild (0).GetChild(4).gameObject.SetActive (true);
		transform.parent.GetChild (1).GetChild (0).GetChild(5).gameObject.SetActive (true);
	}

	IEnumerator GetImage(WWW www,GameObject g)
	{
		
		yield return www;
		
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		g.GetComponent<UITexture> ().mainTexture = temp;
	}
	//	public 
}
