using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using Pew.Player;

public class MoneyDoubler : MonoBehaviour {
	
	private bool AlreadyDone = false;
	
	public float MultiplyFactor = 2F;
	
	void Start () {
		// Initialize ads?
	}
	
	public void DoAdAndMultiply() {
		
		if (AlreadyDone) return; // No cheating here!
		
		Advertisement.Show("rewardedVideo");
		AdDisplayer.ShowAds = false;
		
		// Actually multiply (double, usually) it.
		GameTracker gt = GameTracker.Active;
		if (gt != null) {
			gt.PutValue("money_awarded", (float.Parse(gt.GetValue("money_awarded")) * this.MultiplyFactor).ToString());
		} else {
			Debug.LogWarning("There doesn't appear to be an active game tracker.  Check into that, m'kay?");
		}
		
		// Sooo long of a name, eh?
		PostGamePlayGamesIntegration.PgpgiActive.DoChecks();
		
		// ;)
		AlreadyDone = true;
		
	}
	
}
