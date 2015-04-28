using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptQuizResult : MonoBehaviour {
	public GameObject mLblLeft;
	public GameObject mSprLeft;
	public GameObject mLblTopRight;
	public GameObject mLblBtmRight;
	public GameObject mSprBtmRight;
	public GameObject mLblDia;
	public GameObject mCurtainLeft;
	public GameObject mCurtainRight;

	public AudioClip mAudioSuccess;
	public AudioClip mAudioFail;

	List<GameObject> mListParticles = new List<GameObject>();

	static Color COLOR_CORRECT = new Color (102f / 255f, 165f / 255f, 242f / 255f);
	static Color COLOR_WRONG = new Color (142f / 255f, 123f / 255f, 103f / 255f);
//	static Color[] COLORS_FIREWORK = {
//		new Color (255f, 255f, 0f),
//		new Color (255f, 0f, 0f),
//		new Color (0f, 0f, 255f),
//		new Color (0f, 255f, 0f),
//		new Color (255f, 0f, 255f),
//		new Color (0f, 255f, 255f)};

	public bool Init(List<SimpleResultInfo> listResult){
		double rewardPoint = 0;
		int betPoint = 0;
		foreach (SimpleResultInfo resultInfo in listResult) {
			rewardPoint += double.Parse(resultInfo.rewardPoint);
			betPoint += int.Parse(resultInfo.betPoint);
		}

		QuizInfo quizInfo = null;
		foreach (QuizInfo quiz in QuizMgr.QuizList) {
			if(listResult[0].quizListSeq == quiz.quizListSeq){
				quizInfo = quiz;
				break;
			}
		}

		string info = quizInfo.playerName + "  ";
		int quizValue = int.Parse (listResult [0].quizValue);
		info += quizInfo.order [quizValue - 1].description + "!!!";
		mLblTopRight.GetComponent<UILabel>().text = info;

		int rand = UnityEngine.Random.Range (1, 3);

		if (rewardPoint > 0) {
//			mSprLeft.GetComponent<UISprite>().color = COLOR_CORRECT;
//			mSprBtmRight.GetComponent<UISprite>().color = COLOR_CORRECT;
			mSprLeft.GetComponent<UISprite>().spriteName = "bg_result 1";
			mSprBtmRight.GetComponent<UISprite>().spriteName = "bg_result 1";
			mCurtainLeft.GetComponent<UISprite>().spriteName = "bg_result_03";
			mCurtainRight.GetComponent<UISprite>().spriteName = "bg_result_03";

			mLblLeft.GetComponent<UILabel>().text =
				transform.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("Correct"+rand).Value;
			mLblBtmRight.GetComponent<UILabel>().text = "+"+rewardPoint;
			mLblBtmRight.GetComponent<UILabel>().color = new Color(1f, 1f, 0);
			mLblDia.GetComponent<UILabel>().text = "+"+(int)(((float)betPoint) * 0.005f);

			double userGoldenBall = double.Parse (UserMgr.UserInfo.userGoldenBall) + rewardPoint;
			UserMgr.UserInfo.userGoldenBall = ""+userGoldenBall;

			transform.root.GetComponent<AudioSource>().PlayOneShot(mAudioSuccess);

			return true;
		} else{
//			mSprLeft.GetComponent<UISprite>().color = COLOR_WRONG;
//			mSprBtmRight.GetComponent<UISprite>().color = COLOR_WRONG;
			mSprLeft.GetComponent<UISprite>().spriteName = "bg_result_04";
			mSprBtmRight.GetComponent<UISprite>().spriteName = "bg_result_04";
			mCurtainLeft.GetComponent<UISprite>().spriteName = "bg_result_02_02";
			mCurtainRight.GetComponent<UISprite>().spriteName = "bg_result_02_02";

			mLblLeft.GetComponent<UILabel>().text =
				transform.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("Wrong"+rand).Value;
			mLblBtmRight.GetComponent<UILabel>().text = "-"+betPoint;
			mLblBtmRight.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
			mLblDia.GetComponent<UILabel>().text = "+"+(int)(((float)betPoint) * 0.005f);

			transform.root.GetComponent<AudioSource>().PlayOneShot(mAudioFail);

			return false;
		}
	}

	public void InitParticle(){
		foreach (GameObject go in mListParticles)
			DestroyImmediate (go);
		mListParticles.Clear ();

		GameObject prefab = Resources.Load ("CoinBlastGold") as GameObject;
		for (int i = 0; i < 10; i++) {
			GameObject firework = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			firework.transform.parent = transform.FindChild ("ResultWindow").transform;
			firework.transform.localScale = new Vector3 (1f, 1f, 1f);

			float posX = UnityEngine.Random.Range(-150f, 150f);
			float posY = UnityEngine.Random.Range(0f, 18f);
			firework.transform.localPosition = new Vector3 (posX, posY, 0);

			float delay = UnityEngine.Random.Range(0f, 2f);
			firework.GetComponent<ParticleSystem>().startDelay = delay;
//			firework.transform.FindChild("Ring").GetComponent<ParticleSystem>().startDelay = delay;

//			int color = UnityEngine.Random.Range(0, COLORS_FIREWORK.Length-1);
//			firework.GetComponent<ParticleSystem>().startColor = COLORS_FIREWORK[color];
//			firework.transform.FindChild("Ring").GetComponent<ParticleSystem>().startColor = COLORS_FIREWORK[color];

//			firework.GetComponent<ScriptParticleResizer> ().ResizeRatio (0.5f);
			firework.GetComponent<ParticleSystem> ().GetComponent<Renderer>().material.renderQueue = 3100;
			firework.transform.FindChild("CoinShine").GetComponent<ParticleSystem>()
				.GetComponent<Renderer>().material.renderQueue = 3100;
			firework.transform.FindChild("CoinTrailSparkly").GetComponent<ParticleSystem>()
				.GetComponent<Renderer>().material.renderQueue = 3100;
			mListParticles.Add (firework);

			firework.GetComponent<ParticleSystem> ().Play();
		}
	}
}
