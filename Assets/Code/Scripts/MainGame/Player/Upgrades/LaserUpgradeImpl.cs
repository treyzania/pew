using UnityEngine;
using System.Collections;
using Pew.Player;

public class LaserUpgradeImpl : UpgradeImplBase {

	public LaserUpgradeEntry[] LaserValues;
	
	void Start () {
		
		this.DoMaterialChange();
		
		// Configure the data stuff.
		CannonFiring cf = this.GetComponent<CannonFiring>();
		
		cf.DamageDealt = this.LaserValues[this.TrackIndex].Damage;
		cf.FireThreshold = this.LaserValues[this.TrackIndex].FireRate;
		
		this.enabled = false;
		
	}
	
	[System.Serializable]
	public class LaserUpgradeEntry {
		
		public float FireRate;
		public float Damage;
		
	}
	
}
