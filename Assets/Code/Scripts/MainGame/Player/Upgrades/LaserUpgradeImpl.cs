using UnityEngine;
using System.Collections;
using Pew.Player;

public class LaserUpgradeImpl : UpgradeImplBase {

	public LaserUpgradeEntry[] LaserValues;
	
	void Start () {
		
		this.DoMaterialChange();
		
		// Configure the damage and firerate stuff.
		CannonFiring cf = this.GetComponent<CannonFiring>();
		
		cf.FireThreshold = this.LaserValues[this.TrackIndex].FireRate;
		cf.DamageDealt = this.LaserValues[this.TrackIndex].Damage;
		
		// Disable it to speed things up.
		this.enabled = false;
		
	}
	
	[System.Serializable]
	public class LaserUpgradeEntry {
		
		[Range(0, 1)] public float FireRate; // Per second.
		public float Damage;
		
	}
	
}
