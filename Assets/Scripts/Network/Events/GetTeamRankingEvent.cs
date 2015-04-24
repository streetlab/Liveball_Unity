using UnityEngine;
using System.Collections;

public class GetTeamRankingEvent : BaseEvent {

	public GetTeamRankingEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetTeamRankingResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

	public GetTeamRankingResponse Response
	{
		get{ return response as GetTeamRankingResponse;}
	}

}
