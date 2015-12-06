using UnityEngine;
using System.Collections;

public class DestroyOutOfEditor : MonoBehaviour {

	void Start () {
		
#if !UNITY_EDITOR
		GameObject.Destroy(this.gameObject);
#else
		Debug.Log("The object " + this.gameObject + " would be destroyed here in a real build.");
#endif
		
	}
	
}
