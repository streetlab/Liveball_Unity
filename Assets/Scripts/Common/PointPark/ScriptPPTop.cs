using UnityEngine;
using System.Collections;

public class ScriptPPTop : MonoBehaviour {

	bool closeClicked = false;

	public void CloseClicked(){
		if(closeClicked)
			return;

		closeClicked = true;
		AutoFade.LoadLevel("SceneMain");
	}
}
