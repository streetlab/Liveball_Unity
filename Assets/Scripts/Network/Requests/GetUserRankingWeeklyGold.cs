using UnityEngine;
using System.Collections;
using System.Text;

public class GetUserRankingWeeklyGold : BaseRequest {

	public GetUserRankingWeeklyGold(int memSeq)
	{
		Add ("memSeq", memSeq);

//		mParams = JsonFx.Json.JsonWriter.Serialize (this);
		mDic = this;

	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "getUserRankingWeeklyGold";
	}

}
