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
			
			if (this.Map.ContainsKey(k)) {
				this.Map[k] = v;
			} else {
				this.Map.Add(k, v);
			}
			
		}
		
		public string GetValue(string k) {
			
			if (!this.Map.ContainsKey(k)) this.Map.Add(k, "0"); // Might work properly for numbers.
			return this.Map[k];
			
		}
		
	}
	
}
