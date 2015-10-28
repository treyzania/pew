using UnityEngine;
using System;

namespace Pew.Player {
	
	[System.Serializable]
	public class UpgradeEntry {
		
		public string Name;
		public string Description;
		public int Price;
		
		public Material PartMaterial;
		
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
	public class UpgradeTrack : ScriptableObject {
		
		public string Name;
		public ShipPart Part;
		public UpgradeEntry[] Entries;
		
	}
	
}
