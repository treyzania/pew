using UnityEngine;
using System.collections;

public class GameCountAchievements : MonoBehavior {
	
	public static string[] ACHIEVEMENTS = new string[] {
		GPConstants.achievement_pirate_splayer,
		GPConstants.achievement_space_veteran,
		GPConstants.achievement_starfighter_ace
	};
	
	void Start() {
		
		foreach (string ach in ACHIEVEMENTS) {
			
			PlayGamesPlatform.Instance.IncrementAchievement(ach, 1, (bool success) => {
				// TODO Do something to handle a mess up.
			});
			
		}
		
	}
	
}

