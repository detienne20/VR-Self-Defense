using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mkRiftData : MonoBehaviour {

	public GameObject head;
	public GameObject lhand;
	public GameObject rhand;

	public struct Frame
	{
		public Vector3 h;// = head.transform.position;
		public Vector3 lh;// = lhand.transform.position;
		public Vector3 rh;// = rhand.transform.position;

		public Quaternion hr;// = head.transform.position;
		public Quaternion lhr;// = lhand.transform.position;
		public Quaternion rhr;// = rhand.transform.position;
	};

	public Frame getFrame(){
		Frame F = new Frame(); 
		F.h = head.transform.position;
		F.lh = lhand.transform.position;
		F.rh = rhand.transform.position;

		F.hr = head.transform.rotation;
		F.lhr = lhand.transform.rotation;
		F.rhr = rhand.transform.rotation;
		return F;
	}
}
