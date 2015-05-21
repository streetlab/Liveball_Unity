using UnityEngine;
using System.Collections;

public class ScriptNoticeTop : MonoBehaviour {

	void Start(){
		PlayerPrefs.SetString (Constants.PrefNotice, "1");
	}

	public void CloseClicked(){
		AutoFade.LoadLevel("SceneGame");
	}
}
