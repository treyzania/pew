using UnityEngine;
using System.Collections;
using Pew.Player;

public class LaserUpgradeImpl : UpgradeImplBase {

	public LaserUpgradeEntry[] LaserValues;
	
	void Start () {
		
		this.DoMaterialChange();
		
		// Configure the damage and firerate stuff.
		LaserFiring lf = this.GetComponent<LaserFiring>();
		
		lf.FireThreshold = this.LaserValues[this.TrackIndex].FireRate;
		lf.DamageDealt = this.LaserValues[this.TrackIndex].Damage;
		
		// Disable it to speed things up.
		this.enabled = false;
		
	}
	
	[System.Serializable]
	public class LaserUpgradeEntry {
		
		[Range(0, 1)] public float FireRate; // Per second.
		public float Damage;
		
	}
	
}
