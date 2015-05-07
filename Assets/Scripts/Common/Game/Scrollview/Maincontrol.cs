using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maincontrol : MonoBehaviour {
	
	public float gap = 920;
	float addsum,addsumint;
	public float bargap = 162;
	float topmenu;
	public bool R = false;
	string imgName;
	List<List<List<string>>> ALL = new List<List<List<string>>> ();
	public GameObject bg_g_origin;
	GameObject bar_origin,temp,Childtemp;
	List<GameObject> bg_g_list = new List<GameObject> ();
	List<Vector3> bg_g_vectors = new List<Vector3> ();
	List<string> day = new List<string>();
	List<string> date = new List<string>();
	List<int> dayandday = new List<int>();
	List<string> ch = new List<string>();
	List<int>  daycount = new List<int>();
	List<int>  when = new List<int>();
	Vector3 position,positions;
	float ChuldNum;
	string result ;
	string today;
	char [] array;
	float num =0;
	GetScheduleEvent mScheduleEvent;
	public void editng(){
	}
	void Start(){
		bg_g_origin.SetActive (false);
		topmenu = gap - (bargap*5);
		mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "setarrray"));
		NetMgr.GetScheduleAll (mScheduleEvent);
	}
	void chacktoday(){
		dayandday.Clear ();
		when.Clear ();
		for (int i = 0; i<mScheduleEvent.Response.data.Count; i+=1) {
			array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
			for (int z = 6; z<array.Length; z++) {
				ch.Add (array [z].ToString ());
			}
			result = string.Join ("", ch.ToArray ());
			dayandday.Add(int.Parse(result));
			if(i>0){
				if(
					dayandday[i]!=dayandday[i-1]){
					when.Add(i);
				}
			}else{
				when.Add(i);
			}
			ch.Clear ();
		}
		daycount.Clear ();
		for (int i =0; i<when.Count; i++) {
			daycount.Add(0);
		}
		for (int i =0; i<dayandday.Count; i++) {
			for(int z = 0; z < when.Count;z++){
				if(dayandday[i] == dayandday[when[z]] ){
					daycount[z]+=1;
				}
			}	
		}
		for (int i =0; i<when.Count; i++) {
			ALL.Add(new List<List<string>>());
			for(int a = 0; a<daycount[i];a++){
				ALL[i].Add(new List<string>());
				for(int w = 0; w < 11 ; w ++){
					ALL[i][a].Add("");
				}
			}
		}
	}
	void setarrray(){
		date.Clear ();
		day.Clear ();
		chacktoday ();
		int num = 0;
		for (int i = 0; i < when.Count; i++) {
			for (int s = 0; s < daycount[i]; s++) {
				Debug.Log ("max : " + mScheduleEvent.Response.data.Count + " now : " + num);
				ALL [i] [s] [0] = mScheduleEvent.Response.data [num].extend [0].teamName;
				imgName = UtilMgr.GetTeamEmblem (mScheduleEvent.Response.data [num].extend [0].imageName);
				ALL [i] [s] [1] = imgName;
				ALL [i] [s] [2] = mScheduleEvent.Response.data [num].extend [0].score.ToString ();
				ALL [i] [s] [3] = mScheduleEvent.Response.data [num].extend [1].teamName;
				imgName = UtilMgr.GetTeamEmblem (mScheduleEvent.Response.data [num].extend [1].imageName);
				ALL [i] [s] [4] = imgName;
				ALL [i] [s] [5] = mScheduleEvent.Response.data [num].extend [1].score.ToString ();
				ALL [i] [s] [6] = gettime (num);
				ALL [i] [s] [7] = getarea (num);
				ALL [i] [s] [8] = mScheduleEvent.Response.data [num].bcastChannel;
				ALL [i] [s] [9] = mScheduleEvent.Response.data [num].interActive;
				ALL [i] [s] [10] = mScheduleEvent.Response.data [num].gameStatus.ToString ();
				//                0 : 왼쪽 팀 이름
				//                1 : 왼쪽 팀 이미지이름
				//                2 : 왼쪽 팀 점수
				//                3 : 오른쪽 팀 이름
				//                4 : 오른쪽 팀 이미지이름
				//                5 : 오른쪽 팀 점수
				//                6 : 경기시간
				//                7 : 경기장소
				//                8 : 방송
				//                9 : 경기진행정보(string)
				//                10 : 경기진행정보(int)	
				day.Add (mScheduleEvent.Response.data [num].onairDay + "요일");
				date.Add (getdate (num));
				num++;
			}
		}
		for (int i =0; i<when.Count; i++) {
			temp = (GameObject)Instantiate (bg_g_origin, new Vector3 (0, 0, 0), bg_g_origin.transform.localRotation);
			temp.transform.parent = bg_g_origin.transform.parent;
			temp.transform.localScale = new Vector3 (1, 1, 1);
			if (i > 0) {
				float allsum = 0;
				float n = 0;
				for (int b = 0; b <i; b++) {
					allsum += daycount [b];
					n += 1;
				}
				temp.transform.localPosition = new Vector3 (bg_g_origin.transform.localPosition.x, bg_g_origin.transform.localPosition.y - ((allsum * bargap) + (n * topmenu)), bg_g_origin.transform.localPosition.z);
			} else {
				temp.transform.localPosition = new Vector3 (bg_g_origin.transform.localPosition.x, bg_g_origin.transform.localPosition.y, bg_g_origin.transform.localPosition.z);
			}
			temp.name = "Bg_g " + (i + 1).ToString ();
			//Debug.Log ("day[when[i]] : " + day [when [i]] + " i : " + i + " [when[i]] : " + when [i]);
			temp.transform.FindChild ("bg_add").FindChild ("top_string").GetComponent<UILabel> ().text = day [when [i]];
			temp.transform.FindChild ("bg_add").FindChild ("top_string").FindChild ("date").GetComponent<UILabel> ().text = date [when [i]];
			inputdata (i, temp);
			temp.SetActive (true);
			bg_g_list.Add (temp);
		}
		bg_g_origin.transform.parent.gameObject.SetActive (true);
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
		if (R) {
			reversalTodayPosition ();
		} else {
			TodayPosition ();
		}
	}
	void inputdata (int q,GameObject g){
		bar_origin = g.transform.FindChild ("bg_add").FindChild ("vars").FindChild ("barorigin").gameObject;
		Debug.Log ("bar_origin : " + bar_origin);
		Debug.Log ("daycount[q] : " + daycount [q]);
		for (int i = 0; i<daycount[q]; i++) {
			Childtemp = (GameObject)Instantiate (bar_origin, new Vector3 (0, 0, 0), bar_origin.transform.localRotation);
			Childtemp.transform.parent = bar_origin.transform.parent;
			Childtemp.transform.localScale = new Vector3 (1, 1, 1);
			Childtemp.transform.localPosition = new Vector3 (bar_origin.transform.localPosition.x, bar_origin.transform.localPosition.y - (bargap * i), bar_origin.transform.localPosition.z);
			Childtemp.name = "bar " + (q + 1).ToString () + "-" + (i + 1).ToString ();
			Childtemp.transform.FindChild ("Left_Team").GetComponent<UILabel> ().text = ALL [q] [i] [0];
			Childtemp.transform.FindChild ("Left_Image").GetComponent<UISprite> ().spriteName = ALL [q] [i] [1];
			Childtemp.transform.FindChild ("Score_L").GetComponent<UILabel> ().text = "";
			Childtemp.transform.FindChild ("Right_Team").GetComponent<UILabel> ().text = ALL [q] [i] [3];
			Childtemp.transform.FindChild ("Right_Image").GetComponent<UISprite> ().spriteName = ALL [q] [i] [4];
			Childtemp.transform.FindChild ("Score_R").GetComponent<UILabel> ().text = "";
			Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().text = ALL [q] [i] [6];
			Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().color = new Color (0, 0, 0);
			switch (int.Parse (ALL [q] [i] [10])) {
			case 1:
				Childtemp.transform.FindChild ("Score_L").GetComponent<UILabel> ().text = ALL [q] [i] [2];
				Childtemp.transform.FindChild ("Score_R").GetComponent<UILabel> ().text = ALL [q] [i] [5];
				Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().text = ALL [q] [i] [9];
				Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().color = new Color (0, 0, 0);
				break;
			case 2:
				Childtemp.transform.FindChild ("Score_L").GetComponent<UILabel> ().text = ALL [q] [i] [2];
				Childtemp.transform.FindChild ("Score_R").GetComponent<UILabel> ().text = ALL [q] [i] [5];
				Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().text = "경기종료";
				Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().color = new Color (71f / 255f, 200f / 255f, 62f / 255f);
				break;
			case 3:
				Childtemp.transform.FindChild ("Score_L").GetComponent<UILabel> ().text = ALL [q] [i] [2];
				Childtemp.transform.FindChild ("Score_R").GetComponent<UILabel> ().text = ALL [q] [i] [5];
				Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().text = "우천취소";
				Childtemp.transform.FindChild ("Time").GetComponent<UILabel> ().color = new Color (217f / 255f, 65f / 255f, 140f / 255f);
				break;
				
			}
			Childtemp.transform.FindChild ("district").GetComponent<UILabel> ().text = ALL [q] [i] [7];
			Childtemp.transform.FindChild ("broad").GetComponent<UILabel> ().text = ALL [q] [i] [8];
			Childtemp.SetActive (true);
		}
	}
	void TodayPosition(){
		
		for (int v = 0; v<7; v++) {
			today = (System.DateTime.Now.Day+v).ToString();
			addtoday ();
			for (int i = 0; i< when.Count; i++) {
				if(getday(when[i]) == today){
					float allsum=0;
					float n=0;
					for(int b = 0 ; b <i;b++){
						allsum+=daycount[b];
						n +=1;
					}
					transform.FindChild("Scroll View").FindChild("Bgs").transform.localPosition += new Vector3(0,((allsum*bargap)+(n*topmenu)),0);
					return;
				}		
			}	
		}		
	}
	void reversalTodayPosition(){
		bg_g_vectors.Clear ();
		for (int i = 0; i < bg_g_list.Count; i++) {
			bg_g_vectors.Add(bg_g_list[i].transform.localPosition);
		}
		
		for (int i = bg_g_vectors.Count-1; i>=0; i--) {
			bg_g_list[(bg_g_vectors.Count-1)-i].transform.localPosition = bg_g_vectors[i];
		}
		for (int v = 0; v<7; v++) {
			today = (System.DateTime.Now.Day+v).ToString();
			addtoday ();
			for (int i = 0; i< when.Count; i++) {
				if(getday(when[(when.Count-1)-i]) == today){
					float allsum=0;
					float n=0;
					for(int b = 0 ; b <i;b++){
						allsum+=daycount[(when.Count-1)-b];
						n +=1;
					}
					transform.FindChild("Scroll View").FindChild("Bgs").transform.localPosition += new Vector3(0,((allsum*bargap)+(n*topmenu)),0);
					return;
				}	
			}	
		}
	}
	public void buttening(int pn,int bn){
		int a = 0;
		for (int i = 0; i<pn-1; i++) {
			a += daycount [i];
		}
		a += bn - 1;
		//Debug.Log (i+" and "+a);
		UserMgr.Schedule = mScheduleEvent.Response.data [a];
		AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);	
	}
	string getarea(int i){
		ch.Clear ();
		array = mScheduleEvent.Response.data [i].subTitle.ToCharArray ();
		
		for(int z = 0; z<array.Length;z++){
			if(num==3){
				ch.Add (array[z].ToString());
			}
			if(num==2){
				num+=1;	
			}
			if(array[z]==','){
				num+=1;
			}
		}
		num=0;
		result = string.Join("", ch.ToArray());
		return result;
	}
	string gettime(int i){
		ch.Clear();
		array = mScheduleEvent.Response.data [i].startTime.ToCharArray ();
		for(int z = 8; z<12;z++){
			ch.Add (array[z].ToString());
			if(z==9){
				ch.Add (":");
			}
		}
		result = string.Join("", ch.ToArray());
		return result;
	}
	string getdate(int i){
		ch.Clear();
		array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
		for(int z = 0; z<array.Length;z++){
			ch.Add (array[z].ToString());
			if(z==3||z==5){
				ch.Add (".");
			}
		}
		result = string.Join("", ch.ToArray());
		return result;
	}
	string getday(int i){
		ch.Clear();
		array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
		for (int z = 6; z<array.Length; z++) {
			ch.Add (array [z].ToString ());
		}
		result = string.Join("", ch.ToArray());
		return result;
	}
	void addtoday(){
		if(today.Length<2){
			today = "0"+today;
		}
	}
}