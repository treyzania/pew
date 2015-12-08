using UnityEngine;
using System.Collections;
using Pew.Google;
using GooglePlayGames;

public class GameCountAchievements : MonoBehaviour {
	
	public static bool IncrementInProgress = false;
	
	public static string[] ACHIEVEMENTS = new string[] {
		GPConstants.achievement_pirate_slayer,
		GPConstants.achievement_space_veteran,
		GPConstants.achievement_starfighter_ace
	};
	
	void Start() {
		
		Debug.Log("Updating game count achievements...");
		if (IncrementInProgress) return;
		IncrementInProgress = true;
		
#if !UNITY_EDITOR
		foreach (string ach in ACHIEVEMENTS) {
			
			PlayGamesPlatform.Instance.IncrementAchievement(ach, 1, (bool success) => {
				// TODO Do something to handle a mess up.
			});
			
		}
#endif
				
	}
	
	void Update() {
		IncrementInProgress = false;
	}
	
}

