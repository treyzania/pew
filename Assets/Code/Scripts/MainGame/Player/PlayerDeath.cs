using UnityEngine;
using System;
using System.Collections;
using Pew.Player;

public class PlayerDeath : MonoBehaviour {

	public ScoreDisplay ScoreDisplayObject;
	
	private long startTime;
	
	void Start () {
		
		GameTracker.Active = new GameTracker();
		
	}
	
	public void HandleDeath() {
		
		StoredPlayerData.PLAYER_DATA.Save();
		
		GameTracker.Active.PutValue("score", Convert.ToString(ScoreDisplayObject.Score));
		GameTracker.Active.PutValue("time", Convert.ToString(Time.timeSinceLevelLoad));
		
		Application.LoadLevel("DeathScene");
		
	}
	
}
