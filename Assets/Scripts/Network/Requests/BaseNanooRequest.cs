using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseNanooRequest{
	protected string mParam;

	public string ToRequestString()
	{
//		string str = mParam + "key=" + Constants.NANOO_API_KEY;
//		string str = "key=" + Constants.NANOO_API_KEY;
		string str = "http://partner.liveball.kr/comu/nanoo.php";
		return str;
	}

	public string GetParam(){
		return mParam;
	}
}
