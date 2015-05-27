using UnityEngine;
using System.Collections;

public class GetEventsEvent : BaseEvent {
	GetEventsResponse mResponse;

	public GetEventsEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		mResponse = JsonFx.Json.JsonReader.Deserialize<GetEventsResponse>(data);

		eventDelegate.Execute ();
	}

	public GetEventsResponse Response
	{
		get{ return mResponse;}
	}

}
