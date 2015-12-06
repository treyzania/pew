using UnityEngine;
using System.Collections;
using Pew.Player;

public class LeaderboardButton : MonoBehaviour {

	void Start () {
		
#if !UNITY_EDITOR		
		
		// Report values.
		if (GameTracker.Active != null) {
			
			// Should these just parse straight to longs?
			long money = (long) float.Parse(GameTracker.Active.GetValue("money_awarded"));
			long score = (long) float.Parse(GameTracker.Active.GetValue("score"));
			
			Social.ReportScore(score, GPConstants.leaderboard_single_game_score, (bool success) => {
				// Nothing!
			});
			
			Social.ReportScore(money, GPConstants.leaderboard_single_game_winnings, (bool success) => {
				// Nothing!
			});
			
			// TODO Make these callbacks to something useful.
						
		}
		
#endif
		
	}
	
	public void LaunchLeaderboards() {
		
#if !UNITY_EDITOR
		Social.ShowLeaderboardUI();
#endif
		
	}
	
}
