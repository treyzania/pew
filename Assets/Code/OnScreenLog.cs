using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class OnScreenLog : MonoBehaviour {

	public static OnScreenLog osl;
	
	private Text Text;
	
	// Must load before Start gets called.
	void Awake() {
		this.Text = this.GetComponent<Text>();
		osl = this;
	}
	
	public void AddLine(string line) {
		this.Text.text += "\n" + line;
	}
	
	public static void Log(string line) {
		osl.AddLine(line);
	}
	
}
