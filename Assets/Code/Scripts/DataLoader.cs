using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Pew.Player;
using Pew.Google;

public class DataLoader : MonoBehaviour {
	
	void Start () {
		
		GoogleFrontend.Init();
		GoogleFrontend.LoadGame();
		
		// If this somehow fails, then it can handle itself normally by creating a new save.
		
	}
	
}
