﻿using UnityEngine;
using System.Collections;

public class GetProfileEvent : BaseEvent {

	public GetProfileEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = Newtonsoft.Json.JsonConvert.DeserializeObject<GetProfileResponse>(data);

//		if (checkError ())
//			return;

		eventDelegate.Execute ();
	}

	public GetProfileResponse Response
	{
		get{ return response as GetProfileResponse;}
	}

}
