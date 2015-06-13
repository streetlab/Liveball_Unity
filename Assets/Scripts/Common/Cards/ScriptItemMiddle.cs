using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptItemMiddle : MonoBehaviour {
	//public string grade,maxlv,posi,team,num,name,nowlv,add;
	public GameObject mainItem;
	GetInvenItemEvent mEvent;
	void Start(){
		mEvent = new GetInvenItemEvent (new EventDelegate (this, "GotItemsInven"));
		NetMgr.GetInvenItem (mEvent);
	}
	void GotItemsInven(){
	
		int invencount = mEvent.Response.data.Count;
		List<int> UseItems = new List<int> ();
		for (int i = 0; i <invencount; i++) {
			if(mEvent.Response.data[i].itemType>=6){
				UseItems.Add(i);
			}

		}

		transform.FindChild("ListCards"). GetComponent<UIDraggablePanel2>().Init(UseItems.Count, 
		                                                              delegate(UIListItem item, int index) {
			
	
			item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("name").GetComponent<UILabel>().text = mEvent.Response.data[UseItems[index]].itemName;
			if(mEvent.Response.data[UseItems[index]].itemType==6){
				item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image").GetComponent<UISprite>().spriteName = "gift_c";
			}else{
				item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image").GetComponent<UISprite>().spriteName = "gift_p";
			}
			item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("itemid").GetComponent<UILabel>().text = mEvent.Response.data[UseItems[index]].itemId.ToString();
			item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("itemNo").GetComponent<UILabel>().text = mEvent.Response.data[UseItems[index]].itemNo.ToString();
			item.Target.gameObject.SetActive(true);
		});
	}
}