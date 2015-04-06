using UnityEngine;
using System.Collections;
using Pew.Items;
using Pew.Player;

namespace Pew.Combat {
	
	public class DamageCalculator {
		
		public static float LaserDamageCurve(float distance, float max) {
			
			/*
			 Follows `f(d) = sqrt(1 - (d / r)^3)`, where d is the distance,
			 and r is the maximum distance.
			*/
			return Mathf.Max(
				Mathf.Sqrt(1 - Mathf.Pow(distance / max, 3)),
				0F
			);
			
		}
		
		public static float ArmorFactorCurve() {
			return 0F;
		}
		
	}
	
	public enum DamageType {
		
		SHELL,
		LASER,
		UNDEFINED
		
	}
	
}
