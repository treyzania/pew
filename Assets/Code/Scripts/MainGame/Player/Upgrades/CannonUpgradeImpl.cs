using UnityEngine;
using System.Collections;
using Pew.Player;

public class CannonUpgradeImpl : UpgradeImplBase {
	
	public CannonUpgradeEntry[] MissileValues;
	
	void Start () {
		
		this.DoMaterialChange();
		
		// Set up the cannon stuff.
		CannonFiring cf = this.GetComponent<CannonFiring>();
		
		cf.Missile = this.MissileValues[this.TrackIndex].Missile;
		cf.FiringCost = this.MissileValues[this.TrackIndex].Cost;
		cf.InitialMissileVelocity = this.MissileValues[this.TrackIndex].Velocity;
		cf.FireThreshold = this.MissileValues[this.TrackIndex].ReloadTime;
		
		// Disable it to speed things up.
		this.enabled = false;
		
	}
	
	[System.Serializable]
	public class CannonUpgradeEntry {
		
		public GameObject Missile;
		public int Cost;
		[Range(0, 20)] public float Velocity;
		[Range(0, 30)] public float ReloadTime;
		
	}
	
	
}
