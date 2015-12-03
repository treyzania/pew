using UnityEngine;
using UnityEngine.UI;
using System;

namespace Pew.Player {
	
	[System.Serializable]
	public class UpgradeTrack : ScriptableObject {
		
		public string Name;
		public ShipPart Part;
		public Sprite Icon;
		public UpgradeEntry[] Entries;
		
	}
	
}
