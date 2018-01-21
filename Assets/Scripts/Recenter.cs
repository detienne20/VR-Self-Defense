using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Recenter : MonoBehaviour {
	
	void Update () {
		if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstick) || OVRInput.GetDown (OVRInput.Button.SecondaryThumbstick)) {
			InputTracking.Recenter ();
		}

	}
}
