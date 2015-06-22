using UnityEngine;
using System.Collections;

public class OnWeb : MonoBehaviour {

	public void OpenWeb(){
		Debug.Log (transform.parent.parent.FindChild("BigLogo").FindChild("Team Name").GetComponent<UILabel>().text);
		switch (transform.parent.parent.FindChild("BigLogo").FindChild("Team Name").GetComponent<UILabel>().text) {
		case "삼성 라이온즈":
			Application.OpenURL("http://www.samsunglions.com/"); 
			break;

		case "두산 베어스":
			Application.OpenURL("http://www.doosanbears.com/"); 
			break;

		case "SK 와이번스":
			Application.OpenURL("http://www.skwyverns.com/"); 
			break;

		case "넥센 히어로즈":
			Application.OpenURL("http://www.heroes-baseball.co.kr/"); 
			break;

		case "NC 다이노스":
			Application.OpenURL("http://www.ncdinos.com/"); 
			break;

		case "한화 이글스":
			Application.OpenURL("http://www.hanwhaeagles.co.kr/"); 
			break;

		case "기아 타이거즈":
			Application.OpenURL("http://www.tigers.co.kr/"); 
			break;

		case "롯데 자이언츠":
			Application.OpenURL("http://www.giantsclub.com/"); 
			break;

		case "LG 트윈스":
			Application.OpenURL("http://www.lgtwins.com/"); 
			break;
		case "KT 위즈":
		case "kt wiz":
			Application.OpenURL("http://www.ktwiz.co.kr/"); 
			break;
		};


	}
}
