using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;

public class DeathStatsDisplay : MonoBehaviour {

	public Text BaseScoreText, TimeText, DifficultyText, ScoreText;
	
	void Start () {
		
		if (GameTracker.Active != null) {
			
			this.BaseScoreText.text = GameTracker.Active.GetValue("score");
			this.TimeText.text = Mathf.Round(float.Parse(GameTracker.Active.GetValue("time"))).ToString();
			this.DifficultyText.text = Mathf.Round(float.Parse(GameTracker.Active.GetValue("difficulty"))).ToString();
			this.ScoreText.text = GameTracker.Active.GetValue("score_final");
			
		} else {
			Debug.Log("GameTracker is null.  Was this scene loaded directly?");
		}
		
	}
	
}
