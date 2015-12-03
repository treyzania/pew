using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pew.Player;
using Pew.Google;
using GooglePlayGames;

public class ButtonBuy : MonoBehaviour {

	public UpgradeTrack UpgradeTrack;
	private int UpgradeIndex; // 0 is default.
	
	private Text title;
	private Text desc;
	private Text cost;
	private Image icon;
	
	public bool ImmediatePurchase = true;
	
	void Start() {
		
		this.UpgradeIndex = StoredPlayerData.PLAYER_DATA.GetUpgradeLevel(UpgradeTrack.Part);
		
		// Get references to the various fields in the UI.
		Transform content = this.transform.FindChild("ShopContent");
		this.title = content.FindChild("Title").GetComponent<Text>();
		this.desc = content.FindChild("Description").GetComponent<Text>();
		this.cost = content.FindChild("Price").GetComponent<Text>();
		this.icon = content.FindChild("Icon").GetComponent<Image>();
		this.icon.sprite = this.UpgradeTrack.Icon;
		
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
			
			if (!this.ImmediatePurchase) {
				ItemPurchaseConfirmation.Active.BeginPurchase(this, PurchaseCallback);
			} else {
				this.DoPurchase();
			}
			
		}
		
	}
	
	private void PurchaseCallback(bool result) {
		
		if (result) this.DoPurchase();
		
	}
	
	private void DoPurchase() {
		
		PlayGamesPlatform.Instance.IncrementAchievement(GPConstants.achievement_bling, 1, (bool success) => {
			if (!success) return; // TODO Make the user aware that something got messed up.
		});
		
		// Take the money out.
		StoredPlayerData.PLAYER_DATA.Money -= this.GetNextUpgrade().Price;
		
		// Increment things.
		this.UpgradeIndex++;
		StoredPlayerData.PLAYER_DATA.SetUpgradeLevel(
			this.UpgradeTrack.Part,
			new SavedUpgradeEntry(
				this.UpgradeIndex,
				this.UpgradeTrack.Entries[this.UpgradeIndex].AptitudeBonus
			)
		);
		
		this.UpdateDisplay();
		
		// This should be done last to ensure that it gets done, eventually.
		GoogleFrontend.Save();
		
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
