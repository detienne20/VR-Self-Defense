using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mkPlayMove : MonoBehaviour {

	public mkArrayData arr;
	public GameObject lh;
	public GameObject rh;

	private float startTime;
	private int i;

	// Use this for initialization
	void Start () {
		Reset ();
		print ("STARTING");
	}

	void Reset(){
		startTime = Time.time;
		i = 0;
	}

	Vector3 QLerp(Vector3 p1, Vector3 p2, Vector3 p3, float t, float u){
		return u * u * p1 + 2 * u * t * p2 + t * t * p3;
	}

	// Update is called once per frame
	void Update () {
//		print ((Time.time - startTime));
//		print (i);
		if ((Time.time - startTime) > arr.t [i + 1]) {
			i+=1;
		}
		if (i + 1 < arr.hp.Length) {
			float t = (Time.time - startTime) / arr.t [i+1];
			float u = 1 - t;
			lh.transform.position = Vector3.Lerp (arr.lhp [i], arr.lhp [i + 1], t);
//			lh.transform.position = QLerp (arr.lhp [i], arr.lhp [i+1], arr.lhp [i+2], t, u); 
//			rh.transform.position = QLerp (arr.lhp [i], arr.lhp [i+1], arr.lhp [i+2], t, u); 
		} else{
			Reset();
		}
	}
}
