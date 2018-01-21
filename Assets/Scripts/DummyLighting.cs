using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLighting : MonoBehaviour {
	
	public TargetLightGroup[] targetLightGroups;
	public Target[] targets;
	public Color highlightColor, damageColor;

	public void SetLighting(Target target, LightingMode lightingMode){
		bool targetFound = false;
		if (target != Target.None) {
			for (int i = 0; i < targets.Length && !targetFound; i++) {
				if (targets [i] == target) {
					targetFound = true;
					if (lightingMode == LightingMode.Off) {
						targetLightGroups [i].SetGroupEnable (false);
					} else {
						targetLightGroups [i].SetGroupEnable (true);
						if (lightingMode == LightingMode.Highlight) {
							targetLightGroups [i].SetGroupColor (highlightColor);
						} else if (lightingMode == LightingMode.Damage) {
							targetLightGroups [i].SetGroupColor (damageColor);
						}
					}
				}
			}
		}

	}


	public enum Target{
		None,
		LeftEar,
		RightEar,
		Nose,
		Neck,
		SolarPlexus,
		LeftTemple
	}

	public enum LightingMode{
		Off, Highlight, Damage
	}
}
