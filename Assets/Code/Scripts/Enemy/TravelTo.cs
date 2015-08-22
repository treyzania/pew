using UnityEngine;
using System.Collections;

public class TravelTo : MonoBehaviour {

	public GameObject Target;
	
	public float MaxForce, BaseForce;
	public float ForcePercent = 1F;
	public float CutoffDistance;
	public float IdleVelocity, IdleSlowdownFactor;
	public float RotationSpeed;
	
	private Rigidbody rb;
	private TurnTowardsTravel ttt;
	
	void Start() {
		this.rb = this.GetComponent<Rigidbody>();
		this.ttt = this.GetComponent<TurnTowardsTravel>();
	}
	
	void Update() {
		
		Vector3 diff = Target.transform.position - this.transform.position;
		Vector3 dir = diff.normalized;
		float dist = diff.magnitude;
		
		if (dist > this.CutoffDistance) {
			
			rb.AddForce(dir * Mathf.Min(MaxForce, BaseForce * Mathf.Pow(ForcePercent, dist)));
			if (ttt) ttt.enabled = true;
			
		} else {
			
			if (this.rb.velocity.sqrMagnitude > Mathf.Pow(IdleVelocity, 2)) {
				rb.AddForce(-1 * this.rb.velocity * this.rb.mass * IdleSlowdownFactor);
				if (ttt) ttt.enabled = false;
			}
			
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation,
				Quaternion.LookRotation(diff, Vector3.up),
				RotationSpeed * Time.deltaTime
			);
		}
		
	}
	
}
