using UnityEngine;
using System.Collections;
using Pew.Player;

public class ThrusterUpgradeImpl : UpgradeImplBase {

	public ThrusterUpgradeEntry[] ThrusterValues;
	
	void Start () {
		
		this.DoMaterialChange();
		
		// Update the motion values.
		GyroMove gm = this.GetComponent<GyroMove>();
		
		gm.VelocityFactor = this.ThrusterValues[this.TrackIndex].VelocityFactor;
		gm.MaxVelocity = this.ThrusterValues[this.TrackIndex].MaxVelocity;
		
		// Disable it to speed things up.
		this.enabled = false;
		
	}
	
	[System.Serializable]
	public class ThrusterUpgradeEntry {
		
		[Range(0, 5)] public float VelocityFactor;
		[Range(0, 50)] public float MaxVelocity;
		
	}
	
}
