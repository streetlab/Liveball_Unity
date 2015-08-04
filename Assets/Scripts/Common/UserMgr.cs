using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UserMgr : MonoBehaviour {

	
	static UserMgr _instance;
	
	UserInfo _userInfo;
	CardInvenInfo _cardInvenInfo;
	ScheduleInfo _schedule;
	LineupInfo _awayLineup;
	LineupInfo _homeLineup;

	List<ContestListInfo> _contestListInfo;
	List<PresetListInfo> _presetListInfo;

	public static int CurrentContestSeq;
	
	static UserMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UserMgr)) as UserMgr;
				Debug.Log("UserMgr is null");
				if (_instance == null)
				{
					GameObject container = new GameObject();  
					container.name = "UserMgr";  
					_instance = container.AddComponent(typeof(UserMgr)) as UserMgr;
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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static UserInfo UserInfo
	{
		get{ return Instance._userInfo;}
		set{Instance._userInfo = value;}
	}
	
	public static CardInvenInfo CardInvenInfo
	{
		get{ return Instance._cardInvenInfo;}
		set{Instance._cardInvenInfo = value;}
	}
	
	public static ScheduleInfo Schedule
	{
		get{return Instance._schedule;}
		set{Instance._schedule = value;}
	}
	
	public static LineupInfo AwayLineup
	{
		get{return Instance._awayLineup;}
		set{Instance._awayLineup = value;}
	}
	public static List<ContestListInfo> ContestList
	{
		get{return Instance._contestListInfo;}
		set{Instance._contestListInfo = value;}
	}
	public static List<PresetListInfo> PresetList
	{
		get{return Instance._presetListInfo;}
		set{Instance._presetListInfo = value;}
	}
	int userMailCount;
	public static int UserMailCount{
		get {
			return Instance.userMailCount;
		}
		set {
			Instance.userMailCount = value;
		}
	}

	
	public static LineupInfo HomeLineup {
		get {
			return Instance._homeLineup;
		}
		set {
			Instance._homeLineup = value;
		}
	}
}