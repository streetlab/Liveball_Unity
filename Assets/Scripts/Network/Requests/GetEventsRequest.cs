using UnityEngine;
using System.Collections;
using System.Text;

public class GetEventsRequest : BaseNanooRequest {

	public GetEventsRequest()
	{
		mParam = Constants.QUERY_SERVER_NANOO + "/v1/news/event?";
	}
}
