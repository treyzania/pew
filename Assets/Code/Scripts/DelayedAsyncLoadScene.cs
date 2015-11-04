using UnityEngine;
using System.Collections;

public class DelayedAsyncLoadScene : MonoBehaviour {
	
	public string NextLevel;
	public float TimeToLoad;
	
	private bool loadStarted;
	private AsyncOperation op;
	
	void Start () {
		
		this.StartCoroutine(AsyncLoadNextWorld());
		
	}
	
	void Update () {
		
		TimeToLoad -= Time.deltaTime;
		
		if (TimeToLoad <= 0 && !loadStarted) {
			
			this.loadStarted = true;
			op.allowSceneActivation = true;
			
		}
		
	}
	
	private IEnumerator AsyncLoadNextWorld() {
		
		// Get the operation ready.
		AsyncOperation ao = Application.LoadLevelAsync(this.NextLevel);
		op = ao;
		ao.allowSceneActivation = false;
		yield return 0;
		
		Debug.Log("Loading death scene...");
		
	}
	
}
