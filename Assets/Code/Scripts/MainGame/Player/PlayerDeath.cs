using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Pew.Player;

public class PlayerDeath : MonoBehaviour {

	public ScoreDisplay ScoreDisplayObject;
	public Animator gameOverAnimator;
	public GameObject[] SpawnAfterDeath;
	
	private Image meter;
	
	void Start () {
		
		GameTracker.Active = new GameTracker();
		this.meter = this.GetComponent<HealthMeter>().ImageComponent;
		
	}
	
	public void HandleDeath() {
		
		StoredPlayerData.PLAYER_DATA.Save();
		
		GameTracker.Active.PutValue("score", Convert.ToString(ScoreDisplayObject.Score));
		GameTracker.Active.PutValue("time", Convert.ToString(Time.timeSinceLevelLoad));
		
		gameOverAnimator.SetBool("PlayerDead", true);
		
		GameObject.Destroy(this.meter.gameObject);
		
		// Post death thing.
		foreach (GameObject go in this.SpawnAfterDeath) GameObject.Instantiate(go);
		
		Social.ReportProgress(GPConstants.achievement_it_takes_practice, 100, (bool success) => {
			// Nothing!
		});
		
	}
	
	
	
}
