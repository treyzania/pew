using UnityEngine;
using System.Collections;
using Pew.Player;

public class AptitudeCalculator : MonoBehaviour {

	public UpgradeTrack[] TracksChecked;
	
	void Start () {
		
		this.DoCalc();
		
	}
	
	private void DoCalc() {
		
		StoredPlayerData spd = StoredPlayerData.PLAYER_DATA;
		
		for (int i = 0; i < this.TracksChecked.Length; i++) {
			
			// Extract the data.
			UpgradeTrack ut = this.TracksChecked[i];
			ShipPart sp = ut.Part;
			
			try {
				
				// Apply the recalculation.
				SavedUpgradeEntry sue = spd.Upgrades[sp];
				sue.Aptitude = ut.Entries[sue.Level].AptitudeBonus;
				
			} catch {
				
				Debug.LogWarning("Something bad happened when calculating the aptitude values.");
				
			}
			
		}
		
	}
	
}
