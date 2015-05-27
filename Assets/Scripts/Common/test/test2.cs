using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour {
	public GameObject Me,Quiz;
	// Use this for initialization
	void Start(){
		O ();
	//	T ();
		//Quiz.GetComponent<ScriptQuizResult> ().InitParticle ();
	}
	void O () {
		
		
		GameObject prefab = Resources.Load ("CoinBlastGold") as GameObject;
		GameObject firework;
		for (int i = 0; i < 10; i++) {
			firework = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			firework.transform.parent = Me.transform;
			firework.transform.localScale = new Vector3 (1f, 1f, 1f);
			
			float posX = UnityEngine.Random.Range(-150f, 150f);
			float posY = UnityEngine.Random.Range(0f, 18f);
			firework.transform.localPosition = new Vector3 (posX, posY, 0);
			
			float delay = UnityEngine.Random.Range(0f, 2f);
			firework.GetComponent<ParticleSystem>().startDelay = delay;
			StartCoroutine(rigntball(delay,firework.transform.localPosition));
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
			//mListParticles.Add (firework);
			
			firework.GetComponent<ParticleSystem> ().Play();
		}
		
		
		
		
		
		
		Quiz.GetComponent<ScriptQuizResult> ().InitParticle ();
		
		
		
	}
	IEnumerator rigntball(float f,Vector3 V){
		yield return new WaitForSeconds (f);
		GameObject prefab = Resources.Load ("spark 08s") as GameObject;
		GameObject firework;
		firework = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
		firework.transform.parent = Me.transform;
		firework.transform.localPosition = V;
	}
	void T () {
		
		
		GameObject prefab = Resources.Load ("spark 08s") as GameObject;
		GameObject firework;
		for (int i = 0; i < 10; i++) {
			firework = Instantiate (prefab, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			firework.transform.parent = Me.transform;
			//firework.transform.localScale = new Vector3 (1f, 1f, 1f);
			
			float posX = UnityEngine.Random.Range(-150f, 150f);
			float posY = UnityEngine.Random.Range(0f, 18f);
			firework.transform.localPosition = new Vector3 (posX, posY, 0);
			
			float delay = UnityEngine.Random.Range(0f, 2f);
		//	firework.GetComponent<NcParticleSystem>().m_fstart = delay;
			//			firework.transform.FindChild("Ring").GetComponent<ParticleSystem>().startDelay = delay;
			
			//			int color = UnityEngine.Random.Range(0, COLORS_FIREWORK.Length-1);
			//			firework.GetComponent<ParticleSystem>().startColor = COLORS_FIREWORK[color];
			//			firework.transform.FindChild("Ring").GetComponent<ParticleSystem>().startColor = COLORS_FIREWORK[color];
			
			//			firework.GetComponent<ScriptParticleResizer> ().ResizeRatio (0.5f);

			//mListParticles.Add (firework);
			
			firework.GetComponent<ParticleSystem> ().Play();
		}
		
		
		
		
		
		
		
		
		
		
	}

}
