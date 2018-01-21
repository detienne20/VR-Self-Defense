using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;
using Oculus.Avatar;
using System;

public class Record_Move : MonoBehaviour {
	
	public OvrAvatarDriver Driver;

	public struct MoveFrame
	{
		public float t;
		public OvrAvatarDriver.PoseFrame pf;
		public MoveFrame(float mytime, OvrAvatarDriver.PoseFrame p){
			t = mytime;
			pf = p;
		}
	};

	private List<MoveFrame> frames;
	private float startTime = 0.0f;
	private float mytime = 0.0f;
	private int frameskipper = 0;
	public int framesSkipped = 50;

	private bool recording = false;

	// Use this for initialization
	void Start () {
		frames = new List<MoveFrame> ();
	}
	
	// Update is called once per frame
	void Update () {
		if( OVRInput.GetUp(OVRInput.Button.One) ){
			recording = !recording;
			if (recording) {
				print ("recording started");
				startTime = Time.time;
			} else {
				print ("recording finished");
				print ("name recording");
				//https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934
//				BinaryFormatter bf = new BinaryFormatter ();
//				FileStream file = File.Create ("Assets\\Moves\\move001.mx");
////				FileStream file = File.Create (Application.persistentDataPath + "/move001.mx");
//				//bf.Serialize (file, BitStream.Serialize(frames));
////				print (JsonConvert.SerializeObject(frames, Formatting.Indented););
//				file.Close ();
//				foreach (MoveFrame m in frames) {
//					print (m.t);
//				}
			}
		}
//		OvrAvatarDriver.GetCurrentPose ();
		if (recording) {
			frameskipper++;
		}
		if (recording && frameskipper == framesSkipped) {
			frameskipper = 0;
			mytime = Time.time - startTime;
			OvrAvatarDriver.PoseFrame pose = Driver.GetCurrentPose ();
			frames.Add( new MoveFrame( mytime, pose) );
			print ("frame recorded!");
		}
	}
}
