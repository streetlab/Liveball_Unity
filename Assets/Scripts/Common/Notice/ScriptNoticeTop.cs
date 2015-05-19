using UnityEngine;
using System.Collections;

public class ScriptNoticeTop : MonoBehaviour {

	public void CloseClicked(){
		AutoFade.LoadLevel("SceneGame");
	}
}
