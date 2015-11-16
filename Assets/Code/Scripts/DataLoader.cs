using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Pew.Player;
using Pew.Google;

public class DataLoader : MonoBehaviour {
	
	// This will be true except when the game first starts.
	private static bool AlreadyLoaded = false;
	
	void Start () {
		
		if (!AlreadyLoaded) {
			
			GoogleFrontend.Init();
			GoogleFrontend.LoadGame();
			
			AlreadyLoaded = true;
			
		}
		
		// If this somehow fails, then it can handle itself normally by creating a new save.
		
	}
	
}
