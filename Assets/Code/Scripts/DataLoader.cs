using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Pew.Player;

public class DataLoader : MonoBehaviour {
	
	void Start () {
		
		Debug.Log("PDP: " + Application.persistentDataPath);
		
		if (File.Exists(Application.persistentDataPath + "/" + StoredPlayerData.PLAYER_DATA_FILE_NAME)) {
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + StoredPlayerData.PLAYER_DATA_FILE_NAME, FileMode.Open);
			
			StoredPlayerData.PLAYER_DATA = (StoredPlayerData) bf.Deserialize(file);
			StoredPlayerData.WasLoaded = true;
			
			file.Close();
			
		} // Otherwise it handles itself.  It creates an new object, as in the declaration.
		
	}
	
}
