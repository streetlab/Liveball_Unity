using UnityEngine;
using System.Collections;

public class MainMenuProfileButton : MonoBehaviour {

	public void onProfile(){
		transform.parent.parent.GetComponent<ScriptMainMenuLeft> ().BtnClicked (this.name);
	}
}
