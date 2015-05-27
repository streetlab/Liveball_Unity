using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseNanooRequest{
	protected string mParam;

	public string ToRequestString()
	{
		string str = mParam + "key=" + Constants.NANOO_API_KEY;

		return str;
	}

}
