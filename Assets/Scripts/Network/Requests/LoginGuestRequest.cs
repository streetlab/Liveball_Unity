using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class LoginGuestRequest : BaseRequest {

	public LoginGuestRequest(LoginInfo loginInfo)
	{
		Add ("memberEmail", "");
		Add ("favoBB", loginInfo.teamCode);
		Add ("memUID", "");
		Add ("deviceID", loginInfo.DeviceID);

//		mParams = JsonFx.Json.JsonWriter.Serialize (this);
		mDic = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override string GetType ()
	{
		return "apps";
	}

	public override string GetQueryId()
	{
		return "tubyLoginDeviceID";
	}

}
