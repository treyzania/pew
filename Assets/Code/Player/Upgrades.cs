using UnityEngine;
using System;

namespace Pew.Player {
	
	[System.Serializable]
	public class UpgradeEntry {
		
		public string Name = "";
		public string Description = "";
		public int Price = 0;
		public int AptitudeBonus = 0;
		
		public Material PartMaterial = null;
		
	}
	
	[System.Serializable]
	public enum ShipPart {
		
		Hull,
		Laser,
		LaserAmmo,
		Cannons,
		CannonAmmo
		
	}
	
	[System.Serializable]
	public class UpgradeImplBase : MonoBehaviour {
		
		private bool doneInit;
		
		public UpgradeTrack UpgradeTrack;
		public GameObject[] RelevantObjects;
		
		protected int TrackIndex;
		
		private void Init() {
			
			if (!doneInit) {
				this.TrackIndex = StoredPlayerData.PLAYER_DATA.GetUpgradeLevel(UpgradeTrack.Part);
				this.doneInit = true;
			}
			
		}
		
		public void DoMaterialChange() {
			
			this.Init();
			
			foreach (GameObject go in this.RelevantObjects) {
				
				Renderer r = go.GetComponent<Renderer>();
				r.material = this.UpgradeTrack.Entries[this.TrackIndex].PartMaterial;
				
			}
			
		}
		
	}
	
}
