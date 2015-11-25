﻿using UnityEngine;
using System.Collections;
using Pew.Player;

public class PointValue : MonoBehaviour {

	public int PointsAwarded;
	public int MoneyAwarded;
	
	public void AwardPoints() {
		
		GameObject sdObject = GameObject.Find("ScoreDisplay");
		if (sdObject == null) return;
		
		ScoreDisplay sd = sdObject.GetComponent<ScoreDisplay>();
		if (sd == null) return;
		
		sd.AddPoints(PointsAwarded);
		GameTracker gt = GameTracker.Active;
		gt.PutValue("money_awarded", GameTracker.Active.GetValue("money_awarded") + this.MoneyAwarded);
		
	}
	
}
