using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Pew.Items;
using Pew.Combat;
using Pew.Google;

namespace Pew.Player {
	
	public class Ship {
		
		public static Ship PlayerInstance;
		
		// Stats and actual gameplay stuff.
		public float Health;
		public GameObject Container; // Null if not in game.
		
		public Ship() {
			
		}
		
		public int GetPlayerAptitude() {
			return StoredPlayerData.PLAYER_DATA.GetAptitude() + 10; // 10 is the base value.
		}
		
	}
	
	[System.Serializable]
	public class StoredPlayerData {
		
		public const string PLAYER_DATA_FILE_NAME = "data.pew";
		
		public static StoredPlayerData PLAYER_DATA = new StoredPlayerData();
		public static bool WasLocalSave = false;
		
		public int Money = 0;
		
		[SerializeField] public Dictionary<ShipPart, SavedUpgradeEntry> Upgrades = new Dictionary<ShipPart, SavedUpgradeEntry>();
		
		public void Save() {
			
			if (!WasLocalSave) {
				
				GoogleSaveFrontend.Save();
				
			} else {
				
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Create(Application.persistentDataPath + "/" + PLAYER_DATA_FILE_NAME);
				
				// Simple enough.
				bf.Serialize(file, this);
				
				file.Close();
				
			}
			
		}
		
		public void SetUpgradeLevel(ShipPart type, SavedUpgradeEntry sue) {
		
			this.Upgrades[type] = sue;
			this.Save();
			
		}
		
		public int GetUpgradeLevel(ShipPart type) {
			
			if (this.Upgrades.ContainsKey(type)) {
				return this.Upgrades[type].Level;
			} else {
				this.SetUpgradeLevel(type, new SavedUpgradeEntry());
				return 0;
			}
			
		}
		
		public int GetAptitude() {
			
			int total = 0; // No dividing by 0.
			
			foreach (SavedUpgradeEntry sue in Upgrades.Values) {
				total += sue.Aptitude;
			}
			
			return total;
			
		}
		
	}
	
	[System.Serializable]
	public class SavedUpgradeEntry {
		
		public int Level;
		public int Aptitude;
		
		public SavedUpgradeEntry(int level, int apt) {
			this.Level = level;
			this.Aptitude = apt;
		}
		
		public SavedUpgradeEntry() : this(0, 0) {
			
		}
		
	}
	
}
