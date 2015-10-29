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
		cf.InitialMissileVelocity = this.MissileValues[this.TrackIndex].Velocity;
		
		// Disable it to speed things up.
		this.enabled = false;
		
	}
	
	[System.Serializable]
	public class CannonUpgradeEntry {
		
		public GameObject Missile;
		[Range(0, 20)] public float Velocity;
		
	}
	
	
}
