using UnityEngine;
using System.Collections.Generic;

namespace Pew.Enemies {
	
	[System.Serializable]
	public class EnemyEntry {
		
		public GameObject Prefab;
		public float Difficulty;
		
	}
	
	public class Enemies {
		
		public Dictionary<GameObject, float> enemies = new Dictionary<GameObject, float>();
		
	}
	
}
