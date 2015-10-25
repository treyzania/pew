﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;

public class DeathStatsDisplay : MonoBehaviour {

	public Text TimeText, DifficultyText, ScoreText;
	
	void Start () {
		
		if (GameTracker.Active != null) {
			
			this.TimeText.text = GameTracker.Active.GetValue("time") + " s";
			this.DifficultyText.text = GameTracker.Active.GetValue("difficulty");
			this.ScoreText.text = GameTracker.Active.GetValue("score");
			
		} else {
			Debug.Log("GameTracker is null.  Was this scene loaded directly?");
		}
		
	}
	
}