using UnityEngine;
using System.Collections;

public class ScriptItemDetailGraph : MonoBehaviour {

	public GameObject mLblName;
	public GameObject mLblLeft;
	public GameObject mLblRight;
	public GameObject mSprRight;
	public GameObject mSprLeft;

	static Color GRAY = new Color(136f/255f, 141f/255f, 147f/255f);
	static Color SKY = new Color(66f/255f, 134f/255f, 187f/255f);

	public void Init(QuizResultGlobal global, QuizResultGlobal friends, bool isMyChoice)
	{
		mLblName.GetComponent<UILabel> ().text = global.orderTitle;
		mLblLeft.GetComponent<UILabel> ().text = global.selectPercentage + "%";
		mLblLeft.transform.localPosition = new Vector3 (-(global.selectPercentage * 2 + 100), 0f, 0f);
		mLblRight.GetComponent<UILabel> ().text = friends.selectPercentage + "%";
		mLblRight.transform.localPosition = new Vector3 ((friends.selectPercentage * 2 + 100), 0f, 0f);
		mSprLeft.GetComponent<UISprite> ().width = global.selectPercentage * 2;
		mSprRight.GetComponent<UISprite> ().width = friends.selectPercentage * 2;

		if(isMyChoice){
			mSprLeft.GetComponent<UISprite> ().color = SKY;
			mSprRight.GetComponent<UISprite> ().color = SKY;
		} else{
			mSprLeft.GetComponent<UISprite> ().color = GRAY;
			mSprRight.GetComponent<UISprite> ().color = GRAY;
		}
	}
}
