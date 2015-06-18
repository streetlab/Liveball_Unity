﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueMgr : MonoBehaviour {

	public enum DIALOGUE_TYPE
	{
		Alert,
		YesNo,
		Choose
	}

	public enum BTNS
	{
		Btn1,
		Btn2,
		Cancel
	}

	bool mIsExit;
	static DialogueMgr _instance;
	GameObject mDialogueBox;
//	EventDelegate mEvent;
	public delegate void DialogClickHandler(BTNS BtnType);
	public event DialogClickHandler OnClickHandler;

	public void SetHandler(DialogClickHandler handler){
		Instance.OnClickHandler = handler;// as OnClicked;
	}

	public static bool IsShown;

	public static DialogueMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(DialogueMgr)) as DialogueMgr;
				Debug.Log("DialogueMgr is null");
				if (_instance == null)
				{
					GameObject container = new GameObject();  
					container.name = "DialogueMgr";  
					_instance = container.AddComponent(typeof(DialogueMgr)) as DialogueMgr;
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

	public static void ShowExitDialogue(DialogClickHandler handler){
//		Debug.Log("ShowExitDialogue");
		if (Instance.mDialogueBox == null) {
			GameObject prefab = Resources.Load ("CommonDialogue") as GameObject;
			Instance.mDialogueBox = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
		}

		string strTitle = Instance.mDialogueBox.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("exitTitle").Value;
		string strBody = Instance.mDialogueBox.GetComponent<PlayMakerFSM> ().FsmVariables.FindFsmString ("exitBody").Value;

		ShowDialogue (strTitle, strBody, DIALOGUE_TYPE.YesNo, null, null, null, handler);

//		Instance.mIsExit = true;
	}

	public static void ShowDialogue(string strTitle, string strBody, DIALOGUE_TYPE type,
	                                DialogClickHandler handler){
//		Debug.Log("ShowDialogue1");
		ShowDialogue(strTitle, strBody, type, null, null, null, handler);
	}

	public static void ShowDialogue(string strTitle, string strBody, DIALOGUE_TYPE type,
	                                string strBtn1, string strBtn2, string strCancel, DialogClickHandler handler)
	{
		Instance.SetHandler(handler);
//		Debug.Log("ShowDialogue2");
		if (Instance.mDialogueBox == null) {
			GameObject prefab = Resources.Load ("CommonDialogue") as GameObject;
			Instance.mDialogueBox = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
		}

		if (IsShown) {
			DialogueMgr.DismissDialogue();		
		}

		Instance.mDialogueBox.transform.parent = GameObject.Find ("UI Root").transform;
		Instance.mDialogueBox.transform.localScale = new Vector3(1f, 1f, 1f);
		Instance.mDialogueBox.transform.localPosition = new Vector3(0, 0, 0);
		Instance.mDialogueBox.SetActive (true);

		Instance.mDialogueBox.transform.FindChild("Panel").FindChild("LblTitle")
			.GetComponent<UILabel> ().text = strTitle;
		Instance.mDialogueBox.transform.FindChild("Panel").FindChild("LblBody")
			.GetComponent<UILabel> ().text = strBody;

		Instance.SetTypeDialogue (type, strBtn1, strBtn2, strCancel);
//		Instance.mIsExit = false;
//		UtilMgr.SetBackEvent(new EventDelegate(Instance.GetComponent<DialogueMgr>(), "DismissDialogue"));
     	IsShown = true;
	}

	void SetTypeDialogue(DIALOGUE_TYPE type, string strBtn1, string strBtn2, string strCancel)
	{
		GameObject btn1 = Instance.mDialogueBox.transform.FindChild("Panel").FindChild("Btn1").gameObject;
		GameObject btn2 = Instance.mDialogueBox.transform.FindChild("Panel").FindChild("Btn2").gameObject;
		GameObject btnCancel = Instance.mDialogueBox.transform.FindChild("Panel").FindChild("BtnCancel").gameObject;
		HutongGames.PlayMaker.FsmVariables fsmVariables = Instance.mDialogueBox.GetComponent<PlayMakerFSM> ().FsmVariables;

		if (strBtn1 == null
						|| strBtn1.Length < 1)
			strBtn1 = fsmVariables.FindFsmString ("strBtn1").Value;

		if (strBtn2 == null
						|| strBtn2.Length < 1)
						strBtn2 = "";

		if (strCancel == null
		    || strCancel.Length < 1)
			strCancel = fsmVariables.FindFsmString ("strCancel").Value;

		if (type == DIALOGUE_TYPE.Alert) {
			btn1.SetActive(false);
			btn2.SetActive(false);
			btnCancel.SetActive(true);

			strCancel = fsmVariables.FindFsmString ("strAlert").Value;

			btnCancel.transform.FindChild("Label").GetComponent<UILabel>().text = strCancel;
			btnCancel.transform.localPosition = new Vector3(0, -100f, 0);
		} else if(type == DIALOGUE_TYPE.YesNo){
			btn1.SetActive(true);
			btn2.SetActive(false);
			btnCancel.SetActive(true);

			btn1.transform.FindChild("Label").GetComponent<UILabel>().text = strBtn1;
			btnCancel.transform.FindChild("Label").GetComponent<UILabel>().text = strCancel;

			btn1.transform.localPosition = new Vector3(-110f, -100f, 0);
			btnCancel.transform.localPosition = new Vector3(110f, -100f, 0);
		} else if(type == DIALOGUE_TYPE.Choose){
			btn1.SetActive(true);
			btn2.SetActive(true);
			btnCancel.SetActive(true);

			btn1.transform.FindChild("Label").GetComponent<UILabel>().text = strBtn1;
			btn2.transform.FindChild("Label").GetComponent<UILabel>().text = strBtn2;
			btnCancel.transform.FindChild("Label").GetComponent<UILabel>().text = strCancel;

			btn2.transform.localPosition = new Vector3(0, -100f, 0);
			btn1.transform.localPosition = new Vector3(-190f, -100f, 0);
			btnCancel.transform.localPosition = new Vector3(190f, -100f, 0);
		}
	}

	public static void DismissDialogue()
	{
		Instance.mDialogueBox.SetActive (false);
		IsShown = false;
	}

	public void Btn1Clicked()
	{
		DialogueMgr.DismissDialogue ();
		if(Instance.OnClickHandler != null)
			Instance.OnClickHandler(BTNS.Btn1);
	}

	public void Btn2Clicked()
	{
		DialogueMgr.DismissDialogue ();
		if(Instance.OnClickHandler != null)
			Instance.OnClickHandler(BTNS.Btn2);
	}

	public void BtnCancelClicked()
	{
		DialogueMgr.DismissDialogue ();
		if(Instance.OnClickHandler != null)
			Instance.OnClickHandler(BTNS.Cancel);
	}

	public static void ShowGuestDialogue()
	{
		GameObject prefab = Resources.Load ("GuestDialogue") as GameObject;
		GameObject dialogueBox = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
				
		dialogueBox.transform.parent = GameObject.Find ("UI Root").transform;
		dialogueBox.transform.localScale = new Vector3(1f, 1f, 1f);
		dialogueBox.transform.localPosition = new Vector3(0, 0, 0);
		dialogueBox.SetActive (true);
	}

	public void BtnJoinClicked()
	{
		if(Instance.OnClickHandler != null)
			Instance.OnClickHandler(BTNS.Cancel);
	}

	public void BtnContinueClicked()
	{
		if(Instance.OnClickHandler != null)
			Instance.OnClickHandler(BTNS.Cancel);
	}

}
