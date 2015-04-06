using UnityEngine;

namespace Pew.Items {
	
	public class Item {
		
		public Vector4 ColorMultiplier;
		
		public Item(Vector4 color) {
			this.ColorMultiplier = color;
		}
		
	}
	
	public class Hull : Item, IBuyable {
		
		public int Mass, Armor, MaxHealth;
		public int Cost;
		
		public Hull(Vector4 color, int mass, int armor, int maxHealth, int cost) : base(color){
			
			this.Mass = mass;
			this.Armor = armor;
			this.MaxHealth = maxHealth;
			
			this.Cost = cost;
			
		}
		
		Sprite IBuyable.GetSprite() {
			return null; // TODO Fix this.
		}
		
		int IBuyable.GetCost() {
			return this.Cost;
		}
		
	}
	
	public class Cannon : Item, IBuyable {
		
		/*
			In this situation, the damagae is constant,
			as long as the shell hits a target.
		*/
		
		public float Speed, ReloadTime, BaseDamage;
		public Vector4 ammoColor;
		public int Cost;
		
		public Cannon(Vector4 color, float speed, float reload, float damage, Vector4 ammoColor, int cost) : base(color) {
			
			this.Speed = speed;
			this.ReloadTime = reload;
			this.BaseDamage = damage;
			
			this.ammoColor = ammoColor;
			this.Cost = cost;
			
		}
		
		Sprite IBuyable.GetSprite() {
			return null; // TODO Fix this.
		}
		
		int IBuyable.GetCost() {
			return this.Cost;
		}
		
	}
	
	public class Laser : Item, IBuyable, IPower {
		
		/*
			The damage is a function of distance, d,
			with respect to base damagage, b, and 
			maximum range, r, defined as
			`f(d) = b * sqrt(1 - (d / r)^3)`.
			
			When d is grater than r, the value of f(d)
			should be ignored and 0 should be assumed.
		*/
		
		public float Range, PowerUsage, BaseDamage;
		public Vector4 laserColor;
		public int Cost;
		
		public Laser(Vector4 color, float range, float powerUsage, float damage, Vector4 laserColor, int cost) : base(color) {
			
			this.Range = range;
			this.PowerUsage = powerUsage;
			this.BaseDamage = damage;
			
			this.laserColor = laserColor;
			this.Cost = cost;
			
		}
		
		Sprite IBuyable.GetSprite() {
			return null; // TODO Fix this.
		}
		
		int IBuyable.GetCost() {
			return this.Cost;
		}
		
		float IPower.GetPowerEffect() {
			return this.PowerUsage * -1; // Negative values mean it uses power.
		}
		
	}
	
	public class Reactor : Item, IBuyable, IPower {
		
		public float GenerationRate, PowerStorage;
		public int Cost;
		
		public Reactor(Vector4 color, float rate, float storage, int cost) : base(color) {
			
			this.GenerationRate = rate;
			this.PowerStorage = storage;
			
			this.Cost = cost;
			
		}
		
		Sprite IBuyable.GetSprite() {
			return null; // TODO Fix this.
		}
		
		int IBuyable.GetCost() {
			return this.Cost;
		}
		
		float IPower.GetPowerEffect() {
			return this.GenerationRate;
		}
		
	}
	
	public class ThrusterSet : Item, IBuyable, IPower {
		
		public float Force, PowerUsage;
		public int Cost;
		
		public ThrusterSet(Vector4 color, float force, float power, int cost) : base(color) {
			
			this.Force = force;
			this.PowerUsage = power;
			
			this.Cost = cost;
			
		}
		
		Sprite IBuyable.GetSprite() {
			return null; // TODO Fix this.
		}
		
		int IBuyable.GetCost() {
			return this.Cost;
		}
		
		float IPower.GetPowerEffect() {
			return this.PowerUsage * -1; // Negative values mean it uses power.
		}
		
	}
	
	public class Shield : Item, IBuyable, IPower {
		
		public float ProtectionFactor, PowerUsage;
		public int Cost;
		
		public Shield(Vector4 color, float effect, float power, int cost) : base(color) {
			
			this.ProtectionFactor = effect;
			this.PowerUsage = power;
			
			this.Cost = cost;
			
		}
		
		Sprite IBuyable.GetSprite() {
			return null; // TODO Fix this.
		}
		
		int IBuyable.GetCost() {
			return this.Cost;
		}
		
		float IPower.GetPowerEffect() {
			return this.PowerUsage * -1; // Negative values mean it uses power.
		}
		
	}
	
	public interface IBuyable {
		
		Sprite GetSprite();
		int GetCost();
		
	}
	
	public interface IPower {
		
		float GetPowerEffect();
		
	}
	
}
