using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipManager))]
[RequireComponent(typeof(CannonFiring))]
public class CannonTargeting : MonoBehaviour {

	public RectTransform Target;
	public Transform ShipRoot;
	public float RotationAdjustFactor = 1F;
	public CannonFiring cf;
	
	void Start () {
		this.cf = this.GetComponent<CannonFiring>();
	}
	
	public void Logg() {
		
		Debug.Log("The lasers are being fired...");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Calculate the vector.
		Vector3 adjPos = this.Target.position - Camera.main.WorldToScreenPoint(this.ShipRoot.position);
		Vector3 rotatedAdj = new Vector3(-adjPos.x, 0, -adjPos.y)
			/*adjPos
			*
			new Quaternion
				()
			*/
			;
		
		// Set the direction.
		this.transform.rotation = Quaternion.Slerp(
			this.transform.rotation,
			Quaternion.LookRotation(rotatedAdj, Vector3.up),
			Time.deltaTime * this.RotationAdjustFactor
		);
		
	}
	
}
