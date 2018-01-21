using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mkPoseData : MonoBehaviour {

	public GameObject prefab;
	public GameObject head;
	public GameObject lhand;
	public GameObject rhand;
	public int framesSkipped = 10;
	
	private float startTime = 0.0f;
	private float mytime = 0.0f;
	private int frameskipper = 0;
	private bool recording = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") || OVRInput.GetUp (OVRInput.Button.One)) {
			recording = !recording;
			if (recording) {
				print ("recording started");
				startTime = Time.time;
			} else {
				print ("recording finished");
			}
		}
		if (recording) {
			frameskipper++;
		}
		if (recording && frameskipper == framesSkipped) {
			frameskipper = 0;
			mytime = Time.time - startTime;
			mkRiftData.Frame data = gameObject.GetComponent<mkRiftData> ().getFrame ();
			GameObject g = Instantiate(prefab, data.h, data.hr, head.transform) as GameObject;
			GameObject h = Instantiate(prefab, data.lh, data.lhr, lhand.transform) as GameObject;
			GameObject f = Instantiate(prefab, data.rh, data.rhr, rhand.transform) as GameObject;
			print ("frame recorded!");
		}
	}
}
