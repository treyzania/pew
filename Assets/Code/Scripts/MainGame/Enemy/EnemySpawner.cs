using UnityEngine;
using System;
using System.Collections.Generic;
using Pew.Player;
using Pew.Enemies;

public class EnemySpawner : MonoBehaviour {

	public float BaseDifficulty = 10F;
	public float DifficultyIncreaseFactor = 1.05F;
	public long BaseWavePeriod = 30000; // Millis, 30s.
	public float WavePeriodFactor = 0.90F;
	public float SpawningRadius = 35F;
	public float SpawningGroupRadius = 10F;
	public int MaxEnemiesPerWave = 25;
	
	private int WaveNumber = 0; // Starts at 0.
	private long MillisToWave = 5000; // Gives the player time to get ready, 5s.
	
	public EnemyEntry[] EnemyList;
	
	void Start () {
		
		Debug.Log("Loaded " + EnemyList.Length + " enemies for automatic spawning.");
		
	}
	
	void Update () {
		
		if (MillisToWave <= 0) {
			
			this.StartWave();
			this.MillisToWave = (long) (BaseWavePeriod * Mathf.Pow(WavePeriodFactor, WaveNumber));
			
			WaveNumber++;
			
		}
		
		MillisToWave -= (long) (1F / Time.deltaTime);
		
	}
	
	public void StartWave() {
		
		float playerAptitude = Ship.PlayerInstance.GetPlayerAptitude(); // Move?
		GameObject playerObject = Ship.PlayerInstance.Container;
		float effectiveDifficulty = BaseDifficulty * playerAptitude * Mathf.Pow(DifficultyIncreaseFactor, (float) WaveNumber);
		
		GameTracker.Active.PutValue("difficulty", Convert.ToString(effectiveDifficulty));
		
		if (playerObject == null) return; // Oops.
		
		Debug.Log("Starting wave at difficulty " + effectiveDifficulty);
		
		EnemyEntry enemy = SelectEnemy(effectiveDifficulty);
		
		int enemyCount = Mathf.FloorToInt(effectiveDifficulty / enemy.Difficulty); // Combined difficulty is roughly proportional to player aptitude.
		
		// Calculate the location of the group.
		Vector2 groupOffset = SpawningRadius * UnityEngine.Random.insideUnitCircle.normalized;
		Vector3 groupsLocation = playerObject.transform.position + new Vector3(groupOffset.x, 0, groupOffset.y);
		
		Debug.Log("Spawning " + enemyCount + " enemies at " + groupOffset);
		
		for (int i = 0; i < enemyCount; i++) {
			
			// Calculate the location of each specific enemy.
			Vector2 enemyOffset = SpawningGroupRadius * UnityEngine.Random.insideUnitCircle;
			Vector3 enemyLocation = groupsLocation + new Vector3(enemyOffset.x, 0, enemyOffset.y);
			
			// Actually spawn it.
			GameObject actualEnemy = GameObject.Instantiate(enemy.Prefab);
			actualEnemy.transform.position = enemyLocation;
			
			TravelTo tt = actualEnemy.GetComponent<TravelTo>();
			if (tt) tt.Target = playerObject;
			
		}
		
	}
	
	private EnemyEntry SelectEnemy(float maxDifficulty) {
		
		EnemyEntry ee = null;
		
		while (ee == null) {
			
			EnemyEntry testEntry = EnemyList[Mathf.FloorToInt(UnityEngine.Random.Range(0, EnemyList.Length))];
			
			if (
				testEntry.Difficulty <= maxDifficulty && 
				Mathf.FloorToInt(maxDifficulty / testEntry.Difficulty) <= this.MaxEnemiesPerWave
			) ee = testEntry;
			
		}
		
		return ee;
		
	}
	
}
