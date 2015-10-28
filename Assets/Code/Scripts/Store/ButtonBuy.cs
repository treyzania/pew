﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;

public class ButtonBuy : MonoBehaviour {

	public UpgradeTrack UpgradeTrack;
	private int UpgradeIndex; // 0 is default.
	
	private Text title;
	private Text desc;
	private Text cost;
	
	void Start() {
		
		StoredPlayerData.PLAYER_DATA.Money = 1000000000;
		StoredPlayerData.PLAYER_DATA.Save();
		
		this.UpgradeIndex = StoredPlayerData.PLAYER_DATA.GetUpgradeLevel(UpgradeTrack.Name);
		
		// Get references to the various fields in the UI.
		Transform content = this.transform.FindChild("ShopContent");
		this.title = content.FindChild("Title").GetComponent<Text>();
		this.desc = content.FindChild("Description").GetComponent<Text>();
		this.cost = content.FindChild("Price").GetComponent<Text>();
		
		this.UpdateDisplay();
		
	}
	
	private UpgradeEntry GetNextUpgrade() {
		
		if (this.UpgradeIndex + 1 < this.UpgradeTrack.Entries.Length) {
			return this.UpgradeTrack.Entries[this.UpgradeIndex + 1];
		} else {
			return null;
		}
		
	}
	
	public void OnButton() {
		
		UpgradeEntry ue = this.GetNextUpgrade();
		
		if (ue.Price <= StoredPlayerData.PLAYER_DATA.Money) {
			
			// Take the money out.
			StoredPlayerData.PLAYER_DATA.Money -= ue.Price;
			StoredPlayerData.PLAYER_DATA.SetUpgradeLevel(this.UpgradeTrack.Name, ++this.UpgradeIndex); // Incs the index everywhere.
			
			StoredPlayerData.PLAYER_DATA.Save(); // Not entirely necessary.
			
			this.UpdateDisplay();
			
		}
		
	}
	
	public void UpdateDisplay() {
		
		UpgradeEntry ue = this.GetNextUpgrade();
		
		if (ue != null) {
			
			this.title.text = ue.Name;
			this.desc.text = ue.Description;
			this.cost.text = ue.Price.ToString();
			
		} else {
			GameObject.Destroy(this.gameObject);
		}
		
	}
	
}
