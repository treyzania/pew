using UnityEngine;
using Pew.Player;

public class AddMoney : MonoBehaviour {
	
	public int DollaDollaBills = 0;
	
	public void OnButton() {
		
		StoredPlayerData.PLAYER_DATA.Money += this.DollaDollaBills;
		StoredPlayerData.PLAYER_DATA.Save();
		
	}
	
	public void ResetMoney() {
		
		StoredPlayerData.PLAYER_DATA.Money = 0;
		StoredPlayerData.PLAYER_DATA.Save();
		
	}
	
	public void ResetEverything() {
		
		StoredPlayerData.PLAYER_DATA = new StoredPlayerData();
		StoredPlayerData.PLAYER_DATA.Save();
		
	}
	
}
