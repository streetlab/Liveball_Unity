using UnityEngine;
using System.Collections;

public class CheckVersionEvent : BaseEvent {

	public CheckVersionEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<CheckVersionResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

	public CheckVersionResponse Response
	{
		get{ return response as CheckVersionResponse;}
	}

}
