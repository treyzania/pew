using UnityEngine;
using System.Collections;
using Pew.Player;

public class LeaderboardButton : MonoBehaviour {

	public static float TIME_DIV_EXPONENT = 1.5F;
	public static float TIME_SUM_FACTOR = 100F;
	public static float DIFF_SUM_EXPONENT = 1.075F;
	
	void Start () {
				
		// Report values.
		if (GameTracker.Active != null) {
			
			long money = (long) float.Parse(GameTracker.Active.GetValue("money_awarded"));
			
			float baseScore = float.Parse(GameTracker.Active.GetValue("score"));
			float timeAlive = float.Parse(GameTracker.Active.GetValue("time"));
			float difficulty = float.Parse(GameTracker.Active.GetValue("difficulty"));
			
			float finalScore =  
				(
					(baseScore / Mathf.Pow(timeAlive, TIME_DIV_EXPONENT))
					* (TIME_SUM_FACTOR * Mathf.Sqrt(timeAlive) + Mathf.Pow(difficulty, DIFF_SUM_EXPONENT))
				)
			;
			
			long longFinalScore = (long) Mathf.Floor(finalScore);
			
			Debug.Log("Calculated score: " + finalScore);
			GameTracker.Active.PutValue("score_final", longFinalScore.ToString());
			
#if !UNITY_EDITOR
			Social.ReportScore(longFinalScore, GPConstants.leaderboard_single_game_score, (bool success) => {
				// Nothing!
			});
			
			Social.ReportScore(money, GPConstants.leaderboard_single_game_winnings, (bool success) => {
				// Nothing!
			});
			
			// TODO Make these callbacks to something useful.
#endif
			
		}
		
	}
	
	public void LaunchLeaderboards() {
		
#if !UNITY_EDITOR
		Social.ShowLeaderboardUI();
#endif
		
	}
	
}
