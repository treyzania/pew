using UnityEngine;
using System.Collections;

public class PointValue : MonoBehaviour {

	public int PointsAwarded;
	
	public void AwardPoints() {
		
		GameObject sdObject = GameObject.Find("ScoreDisplay");
		if (sdObject == null) return;
		
		ScoreDisplay sd = sdObject.GetComponent<ScoreDisplay>();
		if (sd == null) return;
		
		sd.AddPoints(PointsAwarded);
		
	}
	
}
