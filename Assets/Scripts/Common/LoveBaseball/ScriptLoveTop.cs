using UnityEngine;
using System.Collections;

public class ScriptLoveTop : MonoBehaviour {
	
	public GameObject mLove;
	
	GetScheduleEvent mScheduleEvent;
	
	void Start(){
		OpenNanoo ();
	}
	
	void OpenNanoo(){
		mLove.SetActive (true);
	}

}
