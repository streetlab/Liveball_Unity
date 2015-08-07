using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContestPresetChangeResponse : BaseResponse {
	List<ContestPresetChangeInfo> _data;

	public List<ContestPresetChangeInfo> data {
		get {
			return _data;
		}
		set {
			_data = value;
		}
	}
}
