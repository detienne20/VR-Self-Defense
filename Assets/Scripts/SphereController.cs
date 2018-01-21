using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {

	private byte[] haptics;
	public byte vibrationIntensity;
	public float vibrationDuration;

	// Use this for initialization
	void Start () {
		haptics = new byte [(int)(320 * vibrationDuration)];
		for (int i = 0; i < haptics.Length; ++i) {
			haptics [i] = vibrationIntensity;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		if (col.collider.tag == "Player") {
			Simulation_Game_Manager.spheresOnScene -= 1;
			vibrate ();
			Destroy (gameObject);
		}
	}

	private void vibrate () {
		OVRHaptics.LeftChannel.Mix (new OVRHapticsClip (haptics, haptics.Length));
		OVRHaptics.RightChannel.Mix (new OVRHapticsClip (haptics, haptics.Length));
	}
}
