using UnityEngine;
using System.Collections.Generic;

namespace Pew.Player {
	
	public class GameTracker {
		
		public static GameTracker Active;
		
		private Dictionary<string, string> Map;
		
		public GameTracker() {
			this.Map = new Dictionary<string, string>();
		}
		
		public void PutValue(string k, string v) {
			this.Map[k] = v;
		}
		
		public string GetValue(string k) {
			return this.Map[k];
		}
		
	}
	
}
