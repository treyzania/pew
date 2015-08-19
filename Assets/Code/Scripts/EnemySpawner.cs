using UnityEngine;
using System.Collections.Generic;
using Pew.Enemies;

public class EnemySpawner : ScriptableObject {

	public float BaseDifficulty = 1F;
	public float DifficultyIncreaseFactor = 1.05F;
	public long BaseWavePeriod = 30000; // Millis, 30s.
	public float WavePeriodFactor = 0.90F;
	
	private int WaveNumber = 0; // Starts at 0.
	private long MillisToWave = 5000; // Gives the player time to get ready, 5s.
	
	public EnemyEntry[] EnemyList;
	
	void Start () {
		
		Debug.Log("Loaded " + EnemyList.Count + " enemies for automatic spawning.");
		
	}
	
	void Update () {
		
		if (MillisToWave <= 0) {
			
			this.StartWave();
			this.MillisToWave = (long) (BaseWavePeriod * Mathf.Pow(WavePeriodFactor, WaveNumber));
			
			WaveNumber++;
			
		}
		
	}
	
	public void StartWave() {
		
		float effectiveDifficulty = BaseDifficulty * Mathf.Pow(DifficultyIncreaseFactor, (float) WaveNumber);
		
	}
	
}
