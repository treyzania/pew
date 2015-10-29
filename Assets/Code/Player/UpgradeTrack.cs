using UnityEngine;
using System;

namespace Pew.Player {
	
	[System.Serializable]
	public class UpgradeTrack : ScriptableObject {
		
		public string Name;
		public ShipPart Part;
		public UpgradeEntry[] Entries;
		
	}
	
}
