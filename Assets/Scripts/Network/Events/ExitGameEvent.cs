﻿using UnityEngine;
using System.Collections;

public class ExitGameEvent : BaseEvent {

	public ExitGameEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<BaseResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

}
