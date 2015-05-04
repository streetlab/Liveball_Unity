using UnityEngine;
using System.Collections;

public class ScriptItemChat : MonoBehaviour {

	public GameObject mLblName;
	public GameObject mLblBody;

	public void Init(JiverModel.Message message){
		mLblName.GetComponent<UILabel>().text = message.GetSenderName();
		mLblBody.GetComponent<UILabel>().text = message.GetMessage();
		transform.parent.GetComponent<UIGrid>().Reposition();
		transform.parent.parent.GetComponent<UIScrollView>().ResetPosition();

	}
}
