using UnityEngine;
using System.Collections;

public class ScriptBtnEmotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CloseMenu ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenMenu()
	{
		transform.FindChild("BtnLove").gameObject.SetActive(true);
		transform.FindChild("BtnHappy").gameObject.SetActive(true);
		transform.FindChild("BtnAngry").gameObject.SetActive(true);
		transform.FindChild("BtnIdle").gameObject.SetActive(true);
		transform.FindChild("BtnDrool").gameObject.SetActive(true);
		transform.FindChild("BtnSad").gameObject.SetActive(true);
	}

	public void CloseMenu()
	{
		transform.FindChild("BtnLove").gameObject.SetActive(false);
		transform.FindChild("BtnHappy").gameObject.SetActive(false);
		transform.FindChild("BtnAngry").gameObject.SetActive(false);
		transform.FindChild("BtnIdle").gameObject.SetActive(false);
		transform.FindChild("BtnDrool").gameObject.SetActive(false);
		transform.FindChild("BtnSad").gameObject.SetActive(false);
	}

	public void BtnClicked(string name)
	{
		switch(name)
		{		
		case "Btn":
			break;
		default :
			Debug.Log(name);
			break;
		}
		CloseMenu ();
		ActiveSprEmotion ();
	}

	public void ActiveSprEmotion()
	{
		UISprite spr = transform.FindChild ("SprEmotion").gameObject.GetComponent<UISprite> ();
		spr.spriteName = "ic_face_color";
	}

	public void DeactiveSprEmotion()
	{

	}
}
