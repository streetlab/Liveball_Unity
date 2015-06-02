﻿using UnityEngine;
using System.Collections;

public class GetRankEvent : BaseEvent {

	public GetRankEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetRankResponse>(data);

//		if (checkError ())
//			return;

		eventDelegate.Execute ();
	}

	public GetRankResponse Response
	{
		get{ return response as GetRankResponse;}
	}

}
