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
	
}
