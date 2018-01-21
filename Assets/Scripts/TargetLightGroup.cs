using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLightGroup : MonoBehaviour {
	public Light[] groupLights;

	public void SetGroupEnable(bool groupEnable){
		for (int i = 0; i < groupLights.Length; i++) {
			groupLights [i].enabled = groupEnable;
		}
	}

	public void SetGroupColor(Color color){
		for (int i = 0; i < groupLights.Length; i++) {
			groupLights [i].color = color;
		}
	}
}
