using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mkPoseData : MonoBehaviour {

	public GameObject prefab;
	public GameObject head;
	public GameObject lhand;
	public GameObject rhand;
	public int framesSkipped = 10;

	private List<mkRiftData.Frame> frames = new List<mkRiftData.Frame>(); 
	
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
				mkArrayData ard = GameObject.Find ("RawMove").GetComponent<mkArrayData> ();
				int c = frames.Count;
				ard.t = new float[c];
				ard.hp = new Vector3[c];
				ard.lhp = new Vector3[c];
				ard.rhp = new Vector3[c];
				ard.hr = new Quaternion[c];
				ard.lhr = new Quaternion[c];
				ard.rhr = new Quaternion[c];
				int i = 0;
				foreach (mkRiftData.Frame f in frames) {
					ard.t [i] = f.t; 
					ard.hp [i] = f.hp;
					ard.lhp [i] = f.lhp;
					ard.rhp [i] = f.rhp;
					ard.hr [i] = f.hr;
					ard.lhr [i] = f.lhr;
					ard.lhr [i] = f.rhr;
					i++;
				}
			}
		}
		if (recording) {
			frameskipper++;
		}
		if (recording && frameskipper == framesSkipped) {
			frameskipper = 0;
			mytime = (Time.time - startTime)*0.1f;
			mkRiftData.Frame data = gameObject.GetComponent<mkRiftData> ().getFrame (startTime);
			frames.Add (data);
			GameObject g = Instantiate(prefab, data.hp, data.hr, head.transform) as GameObject;
			GameObject h = Instantiate(prefab, data.lhp, data.lhr, lhand.transform) as GameObject;
			GameObject f = Instantiate(prefab, data.rhp, data.rhr, rhand.transform) as GameObject;
			print ("frame recorded!");
		}
	}
}
