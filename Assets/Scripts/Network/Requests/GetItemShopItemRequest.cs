﻿using UnityEngine;
using System.Collections;
using System.Text;

public class GetItemShopItemRequest : BaseRequest {

	public GetItemShopItemRequest()
	{
		Add ("memSeq", UserMgr.UserInfo.memSeq);

		mParams = JsonFx.Json.JsonWriter.Serialize (this);

	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "tubyGetRubyProductListIT";
	}

}
