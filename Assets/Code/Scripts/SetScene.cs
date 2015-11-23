using UnityEngine;
using System.Collections;
using Pew.Player;

public class SetScene : MonoBehaviour {
	
	public string LevelName;
	
	void Start() {
		
		Debug.Log("Button for " + this.LevelName + " loaded.");
		
	}
	
	public void DoChange() {
		
		StoredPlayerData.PLAYER_DATA.Save();
		Application.LoadLevel(this.LevelName);
		
	}
	
}
