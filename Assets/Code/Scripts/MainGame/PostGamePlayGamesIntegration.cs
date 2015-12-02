using UnityEngine;
using System.Collections;
using Pew.Player;
using GooglePlayGames;

public class PostGamePlayGamesIntegration : MonoBehaviour {

	public static PostGamePlayGamesIntegration PgpgiActive;
	
	void Start() {
		
		PgpgiActive = this;
		this.DoChecks();
		
	}
	
	public void DoChecks() {
		
		if (GameTracker.Active != null) {
			
			float totalMoney = float.Parse(GameTracker.Active.GetValue("money_awarded")) + (float) StoredPlayerData.PLAYER_DATA.Money;
			
			Social.ReportProgress(GPConstants.achievement_the_gates_to_the_universe, totalMoney / 1000000F, (bool success) => {
				// Something.
			});
			
			Social.ReportProgress(GPConstants.achievement_phat_stax, totalMoney / 100000F, (bool success) => {
				// Something.
			});
			
		}
		
	}
	
}
