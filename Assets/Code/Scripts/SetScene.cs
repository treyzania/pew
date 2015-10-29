using UnityEngine;
using System.Collections;
using Pew.Player;

public class SetScene : MonoBehaviour {
	
	public string LevelName;
	
	public void DoChange() {
		
		StoredPlayerData.PLAYER_DATA.Save();
		Application.LoadLevel(this.LevelName);
		
	}
	
}
