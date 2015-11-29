using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Pew.Player;
using Pew.Google;

public class DataLoader : MonoBehaviour {
	
	public string NextScene = "Menu";
	public string ErrorScene = "NoGPG";
	
	private bool CanSwitch = false;
	
	void Start () {
		
		// This can only ever be run once, as the Init scene can only be accessed on startup.
		
		// If this somehow fails, then it can handle itself normally by creating a new save.
		GoogleFrontend.Init((bool success) => {
			
			if (success) {
				
				GoogleFrontend.LoadGame((bool hadSelection) => {
					
					if (hadSelection) {
						
						OnScreenLog.Log("Beginning menu switch...");
						
						// Let's move into the actual game if it succeeds.
						Application.LoadLevel(this.NextScene);
						Debug.Log("Next scene loaded");
						
					} else {
						
						// No scene selected, quit the game.
						Application.Quit();
						
					}
					
				});
				
			} else {
				
				OnScreenLog.Log("Authentication failed!!!\n<color=red><b>Cannot load game!</b></color>");
				Application.LoadLevelAdditive(this.ErrorScene);
				
			}
			
			//CanSwitch = success;
			//CanSwitch = true; // Should switch if the init is successful.
			
		});
		
		Debug.Log("Google inited.");
		
	}
	
	void Update() {
		
		//Debug.Log("blah");
		
		// This should occur only after the scene fully loads.
		if (this.CanSwitch) {
			
			
			
		}
		
	}
	
}
