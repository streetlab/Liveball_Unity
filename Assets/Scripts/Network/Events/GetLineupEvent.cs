using UnityEngine;
using System.Collections;

public class GetLineupEvent : BaseEvent {

	public GetLineupEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetLineupResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

	public GetLineupResponse Response
	{
		get{ return response as GetLineupResponse;}
	}

}
