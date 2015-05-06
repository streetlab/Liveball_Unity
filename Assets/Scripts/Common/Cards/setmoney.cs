using UnityEngine;
using System.Collections;

public class setmoney : MonoBehaviour {
	public GameObject mLblDia,mLblRuby;
	
	// Update is called once per frame
	void Update(){
		SetTopInfo ();
	}
	
	void SetTopInfo()
	{
		mLblDia.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userDiamond);
		//mLblGold.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userGoldenBall);
		mLblRuby.GetComponent<UILabel> ().text = UtilMgr.AddsThousandsSeparator(UserMgr.UserInfo.userRuby);
	}
}
