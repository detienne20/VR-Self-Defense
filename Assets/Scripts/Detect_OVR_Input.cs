using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Detect_OVR_Input : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetDown(OVRInput.Button.One))
			SceneManager.LoadScene (1);
		else if (OVRInput.GetDown(OVRInput.Button.Two))
			SceneManager.LoadScene (2);
		else if (OVRInput.GetDown(OVRInput.Button.Three))
			SceneManager.LoadScene (3);
		else if (OVRInput.GetDown(OVRInput.Button.Four))
			SceneManager.LoadScene (4);
		else if (OVRInput.GetDown(OVRInput.Button.Start))
			SceneManager.LoadScene (5);
	}
}
