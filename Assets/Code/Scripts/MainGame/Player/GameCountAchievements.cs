using UnityEngine;
using System.Collections;
using Pew.Google;
using GooglePlayGames;

public class GameCountAchievements : MonoBehaviour {
	
	public static string[] ACHIEVEMENTS = new string[] {
		GPConstants.achievement_pirate_slayer,
		GPConstants.achievement_space_veteran,
		GPConstants.achievement_starfighter_ace
	};
	
	void Start() {
		
#if !UNITY_EDITOR
		foreach (string ach in ACHIEVEMENTS) {
			
			PlayGamesPlatform.Instance.IncrementAchievement(ach, 1, (bool success) => {
				// TODO Do something to handle a mess up.
			});
			
		}
#endif
				
	}
	
}

