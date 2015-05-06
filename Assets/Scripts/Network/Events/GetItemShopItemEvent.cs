﻿using UnityEngine;
using System.Collections;

public class GetItemShopItemEvent : BaseEvent {

	public GetItemShopItemEvent(EventDelegate eventDelegate)
	{
		base.eventDelegate = eventDelegate;

		InitEvent += InitResponse;
	}

	public void InitResponse(string data)
	{
		response = JsonFx.Json.JsonReader.Deserialize<GetItemShopItemResponse>(data);

		if (checkError ())
			return;

		eventDelegate.Execute ();
	}

	public GetItemShopItemResponse Response
	{
		get{ return response as GetItemShopItemResponse;}
	}

}
