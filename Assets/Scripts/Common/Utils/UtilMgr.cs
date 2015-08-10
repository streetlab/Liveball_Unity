﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilMgr : MonoBehaviour {

	static UtilMgr _instance;

	static List<EventDelegate> mListBackEvent = new List<EventDelegate>();
	GameObject mProgressCircle;

	public static bool IsUntouchable;
	public static bool IsShowLoading;
	public static WWW mWWW;
	public static bool IsFirstLanding = true;

	public static int gameround=0;
	public static string SelectTeam = "";
	public static string SelectTeamSeq = "";
	public static bool OnPause;
	public static bool OnFocus;
	public static string PreLoadedLevelName;

	static UtilMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UtilMgr)) as UtilMgr;
				Debug.Log("UtilMgr is null");
				if (_instance == null)
				{
					GameObject container = new GameObject();  
					container.name = "UtilMgr";  
					_instance = container.AddComponent(typeof(UtilMgr)) as UtilMgr;
					Debug.Log("and makes new one");
					
				}
			}
			
			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad (this);
	}

	void Update(){
		if(mWWW != null && IsShowLoading){
			UILabel label = Instance.mProgressCircle.transform.FindChild("Panel").FindChild("SprBG").FindChild("Label").GetComponent<UILabel>();
			int per = (int)(mWWW.uploadProgress * 100f);
			label.text = per + "%";
		}
	}

	public static void AddBackEvent(EventDelegate eventDel)
	{
		Debug.Log("AddBackEvent");
		mListBackEvent.Add (eventDel);
	}

	public static void RemoveAllBackEvents()
	{
		mListBackEvent.Clear ();
	}

	public static void RemoveBackEvent()
	{
		mListBackEvent.RemoveAt(mListBackEvent.Count-1);
	}

	public static bool HasBackEvent()
	{
		if (mListBackEvent.Count > 0)
						return true;
		return false;
	}

	public static void RunAllBackEvents()
	{
		if (mListBackEvent.Count < 1)
						return;

		for(int i = mListBackEvent.Count-1; i > -1; i = mListBackEvent.Count-1){
			EventDelegate eventDel = mListBackEvent[i];
			RemoveBackEvent();
			eventDel.Execute();
		}
	}

	public static bool OnBackPressed()
	{
		Debug.Log("OnBackPressed");
		if (UtilMgr.IsUntouchable)
			return false;

		if(mListBackEvent.Count > 0)
		{
			EventDelegate eventDel = mListBackEvent[mListBackEvent.Count-1];
			RemoveBackEvent();
			eventDel.Execute();
			return true;
		}
		else
		{
//			if(Application.loadedLevelName.Equals("SceneMain")){
////				AutoFade.LoadLevel(PreLoadedLevelName);
//				NetMgr.ExitGame(null);
//				AutoFade.LoadLevel("SceneGame");
//			} else{
//				Instance.ShowExitDialog();
//			}
			Instance.ShowExitDialog();
			return false;
		}
	}

	public static void Quit(){
		Debug.Log("Quit");
//		Instance.QuitGame();
		Application.Quit();
	}

	public void ShowExitDialog(){
		DialogueMgr.ShowExitDialogue(DialogClickHandler);
	}

	public void DialogClickHandler(DialogueMgr.BTNS btn){
//		Debug.Log("Clicked : "+btn);
		if(btn == DialogueMgr.BTNS.Btn1){
			UtilMgr.Quit();
		}
	}

	public static void ResizeList(GameObject go)
	{
		try{
		Vector3 offset3 = go.transform.localPosition;
		offset3.y += UtilMgr.GetScaledPositionY () ;
		go.transform.localPosition = new Vector3 (offset3.x, offset3.y, offset3.z);
		Vector4 offset4 = go.GetComponent<UIPanel> ().baseClipRegion;
		offset4.w -= UtilMgr.GetScaledPositionY () * 2;
		go.GetComponent<UIPanel> ().baseClipRegion = new Vector4 (offset4.x, offset4.y, offset4.z, offset4.w);
		}catch{
		}
	}

	public static float GetScaledPositionY()
	{
		float height = (float)Screen.height;
		float width = (float)Screen.width;
		float ratio = height / width;
		float diff = Constants.DEFAULT_SCR_RATIO - ratio;
//		Debug.Log ("ScaledPositionY is "+360f * diff);

		return 360f * diff;
	}

	public static string RemoveThousandSeperator(string number){
		return number.Replace (",", "");
	}

	public static string AddsThousandsSeparator(string number)
	{
		return AddsThousandsSeparator (double.Parse (number));
	}

	public static string AddsThousandsSeparator(int number)
	{
		return string.Format ("{0:n0}", number);
	}

	public static string AddsThousandsSeparator(double number)
	{
		return string.Format ("{0:n0}", number);
	}

	/** "yyyy-MM-dd HH:mm:ss" */
	public static string GetDateTime(string expression)
	{
		return System.DateTime.Now.ToString (expression);
	}
	/** "20150225182000"  */
	public static string ConvertToDate(string timeStr)
	{
		string year = timeStr.Substring (0, 4);
		string month = timeStr.Substring (4, 2);
		string day = timeStr.Substring (6, 2);
		int nTime = int.Parse(timeStr.Substring (8, 2));
		string minute = timeStr.Substring (10, 2);
		string time;
		if(nTime > 11)
		{
			time = "오후 ";
			if(nTime > 12)
			{
				nTime -= 12;
			}
			time += nTime+":";
		}
		else
		{
			time = "오전 ";
			time += nTime+":";
		}
		string final = year + ". " + month + ". " + day + " " + time + minute;
		return final;
	}

	public static string GetTeamEmblem(string imgName)
	{
		switch(imgName)
		{
		case "sports_team_baseball_lg.png":
		case "LG":
			return "ic_lg";
		case "sports_team_baseball_lt.png":
		case "LT":
			return "ic_lotte";
		case "sports_team_baseball_hh.png":
		case "HH":
			return "ic_hanwha";
		case "sports_team_baseball_ob.png":
		case "OB":
			return "ic_doosan";
		case "sports_team_baseball_ht.png":
		case "HT":
			return "ic_kia";
		case "sports_team_baseball_ss.png":
		case "SS":
			return "ic_samsung";
		case "sports_team_baseball_wo.png":
		case "WO":
			return "ic_nexen";
		case "sports_team_baseball_sk.png":
		case "SK":
			return "ic_sk";
		case "sports_team_baseball_nc.png":
		case "NC":
			return "ic_nc";
		case "sports_team_baseball_kt.png":
		case "kt":
			return "ic_kt";
		}
		return "ic_liveball";
	}
	public static string GetTeamCode(string imgName)
	{
		switch(imgName)
		{
		case "LG":
			return "LG";
		case "롯데":
			return "LT";
		case "한화":
			return "HH";    
		case "두산":
			return "OB";
		case "기아":
			return "HT";
		case "삼성":
			return "SS";
		case "넥센":
			return "WO";        
		case "SK":
			return "SK";        
		case "NC":
			return "NC";
		case "KT":
			return "kt";
		}
		return "ic_liveball";
	}

//	public static string GetTeamEmblem(string teamCode)
//	{
//		switch(teamCode)
//		{
//		case "SS":
//			return "ic_samsung";
//		case "WO":
//			return "ic_nexen";
//		}
//		return null;
//	}

	public static string GetRoundString(int round)
	{

		if(round == 1)
		{
			return "ST";
		}
		else if(round == 2)
		{
			return "ND";
		}
		else if(round == 3)
		{
			return "RD";
		}
		else
		{
			return "TH";
		}
	}

	/** 객체의 이름을 통하여 자식 요소를 찾아서 리턴하는 함수 */
	public static GameObject GetChildObj( GameObject source, string strName) { 
		Transform[] AllData = source.GetComponentsInChildren< Transform >(); 
		GameObject target = null;
		
		foreach( Transform Obj in AllData ) { 
			if( Obj.name == strName ) { 
				target = Obj.gameObject;
				break;
			} 
		}
		
		return target;
	}

	public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] rpixels = result.GetPixels(0);
		float incX = (1.0f / (float)targetWidth);
		float incY = (1.0f / (float)targetHeight);
		for (int px = 0; px < rpixels.Length; px++)
		{
			rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
		}
		result.SetPixels(rpixels, 0);
		result.Apply();
		Destroy (source);
//		System.GC.Collect ();
		return result;
	}

	public static void ShowLoading(bool unTouchable, WWW www){
		if (Instance.mProgressCircle == null) {
			GameObject prefab = Resources.Load ("ProgressCircle1") as GameObject;
			Instance.mProgressCircle = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			Instance.mProgressCircle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			Instance.mProgressCircle.transform.localPosition = new Vector3(0, 0, 0);
		}
		
		Instance.mProgressCircle.transform.parent = GameObject.Find ("UI Root").transform;
		Instance.mProgressCircle.SetActive (true);
		
		UtilMgr.IsUntouchable = unTouchable;

		if(www != null){
			mWWW = www;
			Instance.mProgressCircle.transform.FindChild("Panel").FindChild("SprBG").FindChild("Label").gameObject.SetActive(true);
			Instance.mProgressCircle.transform.FindChild("Panel").FindChild("SprBG").FindChild("Sprite").gameObject.SetActive(true);
		} else{
			Instance.mProgressCircle.transform.FindChild("Panel").FindChild("SprBG").FindChild("Label").gameObject.SetActive(false);
			Instance.mProgressCircle.transform.FindChild("Panel").FindChild("SprBG").FindChild("Sprite").gameObject.SetActive(false);
		}

		IsShowLoading = true;
	}

	public static void ShowLoading(bool unTouchable)
	{
		ShowLoading(unTouchable, null);
	}

	public static void DismissLoading()
	{
		if(Instance.mProgressCircle != null)
			Instance.mProgressCircle.SetActive (false);

		UtilMgr.IsUntouchable = false;
		IsShowLoading = false;
		mWWW = null;
	}
	
	public static bool IsTestServer(){
		bool isTest = false;
		string strTest = PlayerPrefs.GetString (Constants.PrefServerTest);
		if(strTest != null && strTest.Equals("1"))
			isTest = true;
		
		return isTest;
	}

	public static bool IsGuestAccount(){
		bool value = false;
//		string strTest = PlayerPrefs.GetString (Constants.PrefGuest);
//		if(strTest != null && strTest.Equals("1"))
//			value = true;
		
		return value;
	}
}
