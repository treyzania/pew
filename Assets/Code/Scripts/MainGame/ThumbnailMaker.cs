using UnityEngine;
using System.Collections;
using Pew.Google;

public class ThumbnailMaker : MonoBehaviour {
	
	private bool done = false;
	private Camera cam;
	
	void Start() {
		
		cam = this.GetComponent<Camera>();
		this.MakeAndSetThumbnail();
		
	}
	
	void MakeAndSetThumbnail() {
		
		if (!done) {
			
			this.cam.Render();
			
			RenderTexture rt = this.cam.targetTexture;
			RenderTexture.active = rt;
			
			// Actually copy over the data.
			Texture2D t2d = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
			t2d.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
			t2d.Apply();
			RenderTexture.active = null;
			
			// Define it.
			AndroidSaveSystem.SetBannerTexture(t2d);
			
			done = true;
			
		}
		
	}
	
}
