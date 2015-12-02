using UnityEngine;
using System.Collections;

public class RedMatterAchievement : MonoBehaviour {
	
	void Start() {
		
		Social.ReportProgress(GPConstants.achievement_dont_get_sucked_in, 100F, (bool success) => {
			// Something.
		});
		
	}
	
}
