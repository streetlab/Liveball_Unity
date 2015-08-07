using UnityEngine;
using System.Collections;

public class ScriptPincode : MonoBehaviour {

	public GameObject mPopCheckPin;
	public GameObject mBtnConfirm;
	public GameObject mBtnCancel;
	public GameObject mLblBody;
	public GameObject mInput1;
	public GameObject mInput2;
	public GameObject mInput3;

	public GameObject mPopMerge;
	public GameObject mPhoto;
	public GameObject mLblNick;
	public GameObject mLblRuby;
	public GameObject mLblMileage;

	CheckMemberPincodeEvent mPincodeEvent;
	LoginEvent mMergeEvent;


	// Use this for initialization
	void Start () {
		mPopCheckPin.SetActive(false);
		mPopMerge.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool CheckValidation(){
		if(mInput1.GetComponent<UIInput>().value.Length < 3)
			return false;
		if(mInput2.GetComponent<UIInput>().value.Length < 3)
			return false;
		if(mInput3.GetComponent<UIInput>().value.Length < 3)
			return false;

		return true;
	}

	public void CheckPincode(){
		if(CheckValidation()){
			mPincodeEvent = new CheckMemberPincodeEvent(new EventDelegate(this, "CompleteCheckPincode"));
			string pincode = mInput1.GetComponent<UIInput>().value.ToUpper();
			pincode += mInput2.GetComponent<UIInput>().value.ToUpper();
			pincode += mInput3.GetComponent<UIInput>().value.ToUpper();
			NetMgr.CheckMemberPincode(pincode, mPincodeEvent);
		} else{
			DialogueMgr.ShowDialogue("오류", "핀코드를 정확하게 입력해주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}
	}

	void CompleteCheckPincode(){
		if(mPincodeEvent.Response.message != null
		   && mPincodeEvent.Response.message.Length > 0){
			DialogueMgr.ShowDialogue("오류", "일치하는 계정 정보가 없습니다.\n핀코드를 다시 확인해주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else{
//			Debug.Log("img Name : "+mPincodeEvent.Response.data.imageName
//			          +", Path : "+mPincodeEvent.Response.data.imagePath);
			mPopCheckPin.SetActive(false);
			mPopMerge.SetActive(true);
			InitPopMerge(mPincodeEvent.Response.data);
		}
	}

	public void CancelPincode(){
		mPopCheckPin.SetActive(false);
		mPopMerge.SetActive(false);
	}

	public void OpenToCheckPincode(){
		mPopCheckPin.SetActive(true);
	}

	public void InitPopMerge(UserInfo userInfo){
		mLblNick.GetComponent<UILabel>().text = "닉네임 : "+userInfo.memberName;
		mLblRuby.GetComponent<UILabel>().text = "보유 루비 : "+userInfo.totalRuby;
		mLblMileage.GetComponent<UILabel>().text = "보유 마일리지 : "+userInfo.totalMileage;
		WWW www = new WWW(Constants.IMAGE_SERVER_HOST + userInfo.imagePath + userInfo.imageName);
		StartCoroutine(GetImage(www));
	}

	IEnumerator GetImage(WWW www){
		yield return www;
		if(www.error == null){
			Texture2D tmpTex = new Texture2D (0, 0);
			www.LoadImageIntoTexture (tmpTex);
			mPhoto.GetComponent<UITexture>().mainTexture = tmpTex;
		}
	}

	public void ConfirmMerge(){
		mMergeEvent = new LoginEvent(new EventDelegate(this, "CompleteMerge"));
		string pincode = mInput1.GetComponent<UIInput>().value.ToUpper();
		pincode += mInput2.GetComponent<UIInput>().value.ToUpper();
		pincode += mInput3.GetComponent<UIInput>().value.ToUpper();
		NetMgr.MergeMembership(pincode, mMergeEvent);
	}

	void CompleteMerge(){
		if(mMergeEvent.Response.message != null
		   && mMergeEvent.Response.message.Length > 0){
			DialogueMgr.ShowDialogue("오류", mMergeEvent.Response.message, DialogueMgr.DIALOGUE_TYPE.Alert, null);
			return;
		}
		//reload App
		PlayerPrefs.SetString(Constants.PrefNick, mMergeEvent.Response.data.memberName);
		AutoFade.LoadLevel("SceneLogin");
	}

	public void Input1Changed(){
		if(mInput1.GetComponent<UIInput>().value.Length > 2)
			mInput2.GetComponent<UIInput>().isSelected = true;
	}

	public void Input2Changed(){
		if(mInput2.GetComponent<UIInput>().value.Length > 2)
			mInput3.GetComponent<UIInput>().isSelected = true;
	}

	public void Input3Commit(){
		if(mInput2.GetComponent<UIInput>().value.Length > 2)
			CheckPincode();
	}
}
