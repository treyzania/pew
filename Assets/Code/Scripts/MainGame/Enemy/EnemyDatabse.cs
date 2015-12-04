using UnityEngine;
using System.Collections.Generic;

namespace Pew.Enemies {
	
	[System.Serializable]
	public class EnemyEntry {
		
		public GameObject Prefab;
		public float Difficulty;
		[Range(0, 50)] public uint MaxSpawnedInGroup;
		
	}
	
}
