using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

	private EnemySpawner spawner;
	private GyroMove playerMotion;
	
	void Start () {
		
		this.spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		this.playerMotion = GameObject.Find("Player").GetComponent<GyroMove>();
		
		if (PlayerPrefs.GetInt("done_tutorial") != 1) {
			
			spawner.enabled = false;
			playerMotion.enabled = false;
			
		} else {
			this.Die();
		}
		
	}
	
	public void Die() {
		
		Debug.Log("Tutorial closed.");
		
		PlayerPrefs.SetInt("done_tutorial", 1);
		PlayerPrefs.Save();
		
		spawner.enabled = true;
		playerMotion.enabled = true;
		
		GameObject.Destroy(this.gameObject);
		
		Time.timeScale = 1F;
		
	}
	
}
