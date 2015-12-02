using UnityEngine;
using System.Collections;
using Pew.Player;
using GooglePlayGames;

public class PostGamePlayGamesIntegration : MonoBehaviour {

	public static PostGamePlayGamesIntegration PgpgiActive;
	
	void Start() {
		
		Debug.Log("PGPGI init begun...");
		
		PgpgiActive = this;
		this.DoChecks();
		
	}
	
	public void DoChecks() {
		
		Debug.Log("Trying PGPGI...");
		
		if (GameTracker.Active != null) {
			
			float maybeOut = 0F;
			float.TryParse(GameTracker.Active.GetValue("money_awarded"), out maybeOut);
			int totalMoney = (int) maybeOut + StoredPlayerData.PLAYER_DATA.Money;
			
			Debug.Log("PGPGI apparent money: " + totalMoney);
			
			if (totalMoney >= 1e6) { // 1 million
				
				Social.ReportProgress(GPConstants.achievement_the_gates_to_the_universe, 100F, (bool success) => {
					Debug.Log("gates: " + success);
				});
				
			}
			
			if (totalMoney >= 1e5) { // 100k
				
				Social.ReportProgress(GPConstants.achievement_phat_stax, 100F, (bool success) => {
					Debug.Log("stax: " + success);
				});
				
			}
			
		}
		
	}
	
}
