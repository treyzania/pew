using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using Pew.Player;
using Pew.Google;
using GooglePlayGames;

using System.Threading;

public class ButtonBuy : MonoBehaviour {

	public UpgradeTrack UpgradeTrack;
	private int UpgradeIndex; // 0 is default.
	
	private Text title;
	private Text desc;
	private Text cost;
	private Image icon;
	private AudioSource audio;
	
	public bool ImmediatePurchase = true;
	public AudioClip errorSound;
	
	public static bool AllowPurchasing;
	
	void Start() {
		
		AllowPurchasing = true;
		
		this.UpgradeIndex = StoredPlayerData.PLAYER_DATA.GetUpgradeLevel(UpgradeTrack.Part);
		
		// Get references to the various fields in the UI.
		Transform content = this.transform.FindChild("ShopContent");
		this.title = content.FindChild("Title").GetComponent<Text>();
		this.desc = content.FindChild("Description").GetComponent<Text>();
		this.cost = content.FindChild("Price").GetComponent<Text>();
		this.icon = content.FindChild("Icon").GetComponent<Image>();
		this.icon.sprite = this.UpgradeTrack.Icon;
		
		// Get the audio source.
		this.audio = this.GetComponent<AudioSource>();
		
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
		
		if (!AllowPurchasing) {
			this.audio.PlayOneShot(this.errorSound);
			return;
		}
		
		UpgradeEntry ue = this.GetNextUpgrade();
		
		if (ue.Price <= StoredPlayerData.PLAYER_DATA.Money) {
			
			if (!this.ImmediatePurchase) {
				ItemPurchaseConfirmation.Active.BeginPurchase(this, PurchaseCallback);
			} else {
				this.DoPurchase();
			}
			
		} else {
			this.audio.PlayOneShot(this.errorSound);
		}
		
	}
	
	private void PurchaseCallback(bool result) {
		
		if (result) this.DoPurchase();
		
	}
	
	private void DoPurchase() {
		
#if !UNITY_EDITOR
		PlayGamesPlatform.Instance.IncrementAchievement(GPConstants.achievement_bling, 1, (bool success) => {
			
			if (success) {
				
			} // TODO Make the user aware that something got messed up.
			
		});
#endif
		
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
		this.audio.Play();
		
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
