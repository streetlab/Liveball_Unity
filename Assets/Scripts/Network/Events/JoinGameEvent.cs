﻿using UnityEngine;
using System.Collections;

public class JoinGameEvent : BaseEvent {

	public JoinGameEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

}
