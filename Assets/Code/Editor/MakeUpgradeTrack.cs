using UnityEngine;
using UnityEditor;
using System;
using Pew.Player;

public class MakeUpgradeTrack {
	
	[MenuItem("Assets/Create/Upgrade Track")]
	public static void CreateTrack() {
		
		UpgradeTrack asset = ScriptableObject.CreateInstance<UpgradeTrack>();
		
		asset.GetType = typeof(UpgradeTrack);
		
		AssetDatabase.CreateAsset(asset, "Assets/NewUpgradeTrack.asset");
		AssetDatabase.SaveAssets();
		
		EditorUtility.FocusProjectWindow();
		
		Selection.activeObject = asset;
		
	}
	
}
