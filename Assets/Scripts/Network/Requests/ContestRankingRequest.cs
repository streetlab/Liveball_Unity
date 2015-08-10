using UnityEngine;
using System.Collections;
using System.Text;

public class ContestRankingeRequest : BaseRequest {

	public ContestRankingeRequest()
	{		
		Add("memSeq", UserMgr.UserInfo.memSeq);
		Add("gameSeq", UserMgr.Schedule.gameSeq);
		Add("contestSeq", UserMgr.CurrentContestSeq);

		mDic = this;
	}

	public override string GetType ()
	{
		return "spos";
	}

	public override string GetQueryId()
	{
		return "getContestRanking";
	}

}
