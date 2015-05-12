using UnityEngine;
using System.Collections;
using System.Text;

public class UpdateMemberInfoRequest : BaseRequest {

	public UpdateMemberInfoRequest(UserInfo userInfo)
	{		
		Add ("memSeq", UserMgr.UserInfo.memSeq);
		Add ("memName", userInfo.memberName);
		Add ("memEmail", userInfo.memberEmail);
		Add ("memImage", userInfo.imageName);
		Add ("favoBB", userInfo.teamCode);

		mParams = JsonFx.Json.JsonWriter.Serialize (this);

	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "tubyUpdateMemberInfo";
	}

}
