using UnityEngine;
using System.Collections;

public class GetRankResponse : BaseResponse {
	Rankinfo _data;

	public Rankinfo data
	{
		get{ return _data;}
		set{ _data = value;}
	}
}
