using UnityEngine;
using System.Collections;
using System.Text;

public class DeleteInvenItemInfoRequest : BaseRequest {

	public DeleteInvenItemInfoRequest(int itemNo,int itemid)
	{		
		Add ("memSeq", UserMgr.UserInfo.memSeq);
		Add ("itemNo", itemNo);
		Add ("itemId", itemid);
		//		mParams = JsonFx.Json.JsonWriter.Serialize (this);
		mDic = this;
	}
	
	public override string GetType ()
	{
		return "apps";
	}
	
	public override string GetQueryId()
	{
		return "tubyDeleteInvenItem";
	}


}
