using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Pew.Items;
using Pew.Combat;
using Pew.Google;
using Pew.Util;

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
		public const string SAVES_TIMES_VALUES_FILE_NAME = "times.properties";
		
		public const string TIME_CLOUD_KEY = "cloud";
		public const string TIME_LOCAL_KEY = "local";
		
		public static StoredPlayerData PLAYER_DATA = new StoredPlayerData();
		public static bool WasLocalSave = false;
		
		public int Money = 0;
		
		[SerializeField] public Dictionary<ShipPart, SavedUpgradeEntry> Upgrades = new Dictionary<ShipPart, SavedUpgradeEntry>();
		
		public void Save() {
			
			Debug.Log("Saving game...");
			GoogleFrontend.Save();
			
		}
		
		public void LocalSave() {
			
			Debug.Log("Caching local save...");
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/" + PLAYER_DATA_FILE_NAME);
			
			// Simple enough.
			bf.Serialize(file, this);
			file.Close();
			
			// Update the time values.
			StoredPlayerData.UpdateTimeValue(StoredPlayerData.TIME_LOCAL_KEY);
			
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
			
			int total = 0; // No dividing by 0, so this is okay.
			
			foreach (SavedUpgradeEntry sue in Upgrades.Values) total += sue.Aptitude;
			
			return total;
			
		}
		
		public static void UpdateTimeValue(string name) {
			
			Properties times = new Properties(Application.persistentDataPath + "/" + SAVES_TIMES_VALUES_FILE_NAME);
			DateTime dt = DateTime.UtcNow;
			
			Debug.Log("Updating time for \"" + name + "\" as " + dt.ToString());
			times.Set(name, dt);
			times.Save();
			
		}
		
		public static DateTime GetTimeValue(string name) {
			
			Properties times = new Properties(Application.persistentDataPath + "/" + SAVES_TIMES_VALUES_FILE_NAME);
			
			DateTime dt = new DateTime(); // Should be null?
			DateTime.TryParse(times.Get(name, DateTime.MinValue.ToString()), out dt);
			
			Debug.Log("Loading time for \"" + name + "\" as " + dt.ToString());
			
			return dt;
			
		}
		
		public static StoredPlayerData LocalLoad() {
			
			string path = Application.persistentDataPath + "/" + StoredPlayerData.PLAYER_DATA_FILE_NAME;
			
			Debug.Log("Loading cached game save...");
			
			if (File.Exists(path)) {
				
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(path, FileMode.Open);
				StoredPlayerData cache = null;
				
				cache = (StoredPlayerData) bf.Deserialize(file);
				StoredPlayerData.WasLocalSave = true;
				
				file.Close();
				
				return cache;
				
			} else {
				
				Debug.LogWarning("None found!  Creating new object...");
				return new StoredPlayerData();
				
			}
			
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
