using UnityEngine;
using System.Collections.Generic;
using Pew.Items;
using Pew.Combat;

namespace Pew.Player {
	
	public class Ship {
		
		// Structure.
		public Hull ShipHull;
		public Cannon ShipCannons;
		public Laser ShipLasers;
		public Reactor ShipReactor;
		public ThrusterSet ShipThrusters;
		public Shield ShipShield;
		
		// Stats and actual gameplay stuff.
		public float Health, EnergyStored;
		public GameObject Container; // Null if not in game.
		
		public Ship() {
			
		}
		
		public void DoDamage(float damage, DamageType theType) {
			
			float armor = this.ShipHull.Armor;
			float shield = this.ShipShield.ProtectionFactor;
			
			
			
		}
		
	}
	
}
