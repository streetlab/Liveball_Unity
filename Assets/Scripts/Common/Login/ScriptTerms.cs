using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class ScriptTerms : MonoBehaviour {
	bool IsGuest;

	public GameObject mLblPrivacy;
	public GameObject mLblTerms;
	public GameObject mScrollPrivacy;
	public GameObject mScrollTerms;
	public GameObject mTglPrivacy;
	public GameObject mTglTerms;
	public GameObject mTglAll;
	public GameObject mBtnNext;

	class Terms{
		string _service;

		public string service {
			get {
				return _service;
			}
			set {
				_service = value;
			}
		}

		string _personal;

		public string personal {
			get {
				return _personal;
			}
			set {
				_personal = value;
			}
		}
	}

	public void Init(bool isGuest){
		IsGuest = isGuest;
		GetTerms();
		SetNextBtn(false);
	}

	void GetTerms(){
		WWW www = new WWW("http://liveball.friize.com/terms");
		StartCoroutine(TermsAPI(www));
	}

	IEnumerator TermsAPI(WWW www){

		yield return www;

		Terms terms = Newtonsoft.Json.JsonConvert.DeserializeObject<Terms>(www.text);

		mLblPrivacy.GetComponent<UILabel>().text = terms.personal;
		mLblPrivacy.GetComponent<BoxCollider2D>().size = 
			new Vector2(580, mLblPrivacy.GetComponent<UILabel>().height);
		mScrollPrivacy.GetComponent<UIScrollView>().ResetPosition();

		mLblTerms.GetComponent<UILabel>().text = terms.service;
		mLblTerms.GetComponent<BoxCollider2D>().size = 
			new Vector2(580, mLblTerms.GetComponent<UILabel>().height);
		mScrollTerms.GetComponent<UIScrollView>().ResetPosition();
//		www.text;
	}

	public void BackClicked()
	{
		UtilMgr.OnBackPressed ();
	}

	public void TglPrivacyChanged(){
		if(mTglPrivacy.GetComponent<UIToggle>().value
		   && mTglTerms.GetComponent<UIToggle>().value){
			SetNextBtn(true);
		} else{
			SetNextBtn(false);
		}
	}

	public void TglTermsChanged(){
		if(mTglPrivacy.GetComponent<UIToggle>().value
		   && mTglTerms.GetComponent<UIToggle>().value){
			SetNextBtn(true);
		} else{
			SetNextBtn(false);
		}
	}

	public void TglAllChanged(){
		if(mTglAll.GetComponent<UIToggle>().value){
			mTglPrivacy.GetComponent<UIToggle>().value = true;
			mTglTerms.GetComponent<UIToggle>().value = true;
		} else{
			mTglPrivacy.GetComponent<UIToggle>().value = false;
			mTglTerms.GetComponent<UIToggle>().value = false;
		}

		if(mTglPrivacy.GetComponent<UIToggle>().value
		   && mTglTerms.GetComponent<UIToggle>().value){
			SetNextBtn(true);
		} else{
			SetNextBtn(false);
		}
	}

	public void SetNextBtn(bool isEnable){
		if(isEnable){
			mBtnNext.GetComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
			mBtnNext.transform.FindChild("Label").GetComponent<UILabel>().text = "다음";
			mTglAll.GetComponent<UIToggle>().value = true;

			NextClicked();
		} else{
			mBtnNext.GetComponent<UIButton>().SetState(UIButtonColor.State.Disabled, true);
			mBtnNext.transform.FindChild("Label").GetComponent<UILabel>().text = "모든 약관에 동의해주세요";
			mTglAll.GetComponent<UIToggle>().value = false;
		}
		mBtnNext.GetComponent<UIButton>().isEnabled = isEnable;
	}

	public void NextClicked(){
		if(mTglPrivacy.GetComponent<UIToggle>().value
		   && mTglTerms.GetComponent<UIToggle>().value){
			if(IsGuest){
				transform.parent.GetComponent<ScriptTitle>().OpenGuest();
				transform.parent.FindChild("SelectTeam").GetComponent<ScriptSelectTeam>().SetTeam("");
				transform.parent.FindChild("SelectTeam").GetComponent<ScriptSelectTeam>().GoNext();
			} else{
				transform.parent.FindChild ("FormJoin2").gameObject.SetActive (true);
				transform.parent.FindChild ("Terms").gameObject.SetActive (false);
			}
		}
	}
}
