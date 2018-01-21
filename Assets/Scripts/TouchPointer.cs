using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPointer : MonoBehaviour {
	public Transform cursor;
	public OVRInput.RawButton button1, button2, button3;

	private ButtonPointManager hitButton;

	void Update () {
		RaycastHit hitInfo;
		if(Physics.Raycast(transform.position, transform.forward, out hitInfo)){
			cursor.position = hitInfo.point + hitInfo.normal * 0.1f;
			cursor.forward = -hitInfo.normal;
			ButtonPointManager bpm = hitInfo.collider.GetComponent<ButtonPointManager> ();
			if (hitButton != bpm) {
				if (hitButton != null) {
					hitButton.RemovePointer ();
				}
				if (bpm != null) {
					bpm.AddPointer ();
				}
				hitButton = bpm;
			}
		}
		if (hitButton != null) {
			if (OVRInput.GetDown (button1) || OVRInput.GetDown (button2) || OVRInput.GetDown (button3)) {
				hitButton.Click ();
			}
		}
	}
}
