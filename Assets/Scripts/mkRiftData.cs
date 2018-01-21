using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mkRiftData : MonoBehaviour {

	public GameObject head;
	public GameObject lhand;
	public GameObject rhand;

	public struct Frame
	{
		public float t;

		public Vector3 hp;// = head.transform.position;
		public Vector3 lhp;// = lhand.transform.position;
		public Vector3 rhp;// = rhand.transform.position;

		public Quaternion hr;// = head.transform.position;
		public Quaternion lhr;// = lhand.transform.position;
		public Quaternion rhr;// = rhand.transform.position;
	};

	public Frame getFrame(float startTime){
		Frame F = new Frame();
		F.t = Time.time - startTime;
		F.hp = head.transform.position;
		F.lhp = lhand.transform.position;
		F.rhp = rhand.transform.position;

		F.hr = head.transform.rotation;
		F.lhr = lhand.transform.rotation;
		F.rhr = rhand.transform.rotation;
		return F;
	}
}
