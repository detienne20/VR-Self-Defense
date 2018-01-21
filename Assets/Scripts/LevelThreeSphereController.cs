using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeSphereController : MonoBehaviour {

	public GameObject rightHand;

	public bool lastSphere;
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
		if (col.collider.tag == "Player" && (OVRInput.Get (OVRInput.RawButton.RIndexTrigger)) && (OVRInput.Get (OVRInput.RawButton.RHandTrigger))
			&& OVRInput.Get (OVRInput.RawTouch.B) && OVRInput.Get (OVRInput.RawTouch.A)) {
			Debug.Log (col.collider.transform.right);
			//if (lastSphere)
				//Debug.Log(Vector3.Angle (-(col.collider.transform.right), Vector3.left));
			if (lastSphere && (rightHand.transform.rotation.z > 75 || rightHand.transform.rotation.z < 0)) { //TODO
				Debug.Log ("RETURNED");
				return;
			}
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
