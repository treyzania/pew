using UnityEngine;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Pew.Items;
using Pew.Combat;

namespace Pew.Player {
	
	public class Ship {
		
		public static Ship PlayerInstance;
		
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
		
		public int GetPlayerAptitude() {
			// TODO Aptitude algorithm.
			return 10;
		}
		
	}
	
	[System.Serializable]
	public class StoredPlayerData {
		
		public const string PLAYER_DATA_FILE_NAME = "data.pew";
		
		public static StoredPlayerData PLAYER_DATA = new StoredPlayerData();
		public static bool WasLoaded = false;
		
		public int Money = 0;
		
		[SerializeField] private Dictionary<string, int> Upgrades = new Dictionary<string, int>();
		
		public void Save() {
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/" + PLAYER_DATA_FILE_NAME);
			
			// Simple enough.
			bf.Serialize(file, this);
			
			file.Close();
			
		}
		
		public void SetUpgradeLevel(string type, int level) {
		
			this.Upgrades[type] = level;
			
			// Really make sure it's saved.
			for (int i = 0; i < 10; i++) {
				this.Save();
			}
			
			
		}
		
		public int GetUpgradeLevel(string type) {
			
			if (this.Upgrades.ContainsKey(type)) {
				return this.Upgrades[type];
			} else {
				this.SetUpgradeLevel(type, 0);
				return 0;
			}
			
		}
		
	}
	
}
