using UnityEngine;
using System.Collections;
using Pew.Player;

public class HullUpgradeImpl : UpgradeImplBase {
	
	public int[] HealthValues;	
	
	void Start () {
		
		this.DoMaterialChange();
		
		// Update the health.
		HealthManager hm = this.GetComponent<HealthManager>();
		
		hm.MaxHealth = this.HealthValues[this.TrackIndex];
		hm.Health = hm.MaxHealth; // Make sure it's okay.
		
		// Disable it for later.
		this.enabled = false;
		
	}
	
}
