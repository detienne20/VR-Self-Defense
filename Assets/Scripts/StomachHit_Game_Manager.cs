using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class StomachHit_Game_Manager : MonoBehaviour {

//Attemp1: 
//	public Transform startPoint; 
//	public Transform endPoint;
//	public float speed= 1.0F; 
//	private float startTime; 
//	private float journeyLength; 

	public Vector3 endPoint; 
	public Rigidbody rb; 
	 
	public static int spheresOnScene;
	public int tempSphereAmount;
	public int nextScene;

	public GameObject enemy; 
	public GameObject[] spheresArray;
	public GameObject panel;
	public GameObject spheres;
	public GameObject nextSceneUI;
	public Text endingPanel;

	private bool start;
	private bool end;
	private bool finished;
	private bool resetWait;
	private bool failed;

	private int previousSphereIndex;
	//private int totalNumberOfSpheres;
	private int removedSphereIndex;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> (); 
		spheresOnScene = spheresArray.Length;
		start = false;
		StartCoroutine ("simulationStartNumerator");
		previousSphereIndex = spheresOnScene;
		removedSphereIndex = 0;
		resetWait = false; //Waits for function to test index
		failed = false;
		tempSphereAmount = spheresOnScene;
	}
		

	// Update is called once per frame
	void Update () {
		//Go to the main menu
		if (OVRInput.GetDown (OVRInput.Button.Start)) {
			SceneManager.LoadScene (0);
		}
		//Make the initial UI & Panel Disappear 
		if (start == false && (Input.GetKeyDown("space") || OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Three)
			|| OVRInput.GetDown(OVRInput.Button.Four) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))) {
			start = true;	
		}
		//Start the simulation
		else {
			//If the simulation was failed
			if (failed) {
				//The game has ended
				if (OVRInput.GetDown (OVRInput.Button.One))
					SceneManager.LoadScene (1);
				else if ( OVRInput.GetDown(OVRInput.Button.Three))
					SceneManager.LoadScene (0);
			}
			//If the simulation is not finished
			else if (!finished && !failed) {

				tempSphereAmount = spheresOnScene;

				//All spheres have been destroyed, game was finished correctly
				if (spheresOnScene == 0 && finished == false) {

					//Attempt1: 
//					startTime = Time.time; 
//					journeyLength = Vector3.Distance (startPoint.position, endPoint.position); 
//					float distance = (Time.time - startTime) * speed; 
//					float journey = distance / journeyLength; 
//					transform.position = Vector3.Lerp (startPoint.position, endPoint.position, journey); 
//
					rb.MovePosition(transform.position+transform.forward*Time.deltaTime); 
					Debug.Log ("Done");
					enemy.SetActive (false);
					nextSceneUI.SetActive (true);
					finished = true;
				} 
				//There are still spheres on the scene
				else {
					//If a sphere was deleted
					if ((spheresOnScene < previousSphereIndex) && resetWait == false) {
						resetWait = true;
						if (!checkIndex ()) {
							//Wrong Order, Reset Level 
							//TODO:: Show UI saying failed
							endingPanel.text = "You have failed the simulation.";
							nextSceneUI.SetActive (true);
							enemy.SetActive (false);
							spheres.SetActive (false);
							failed = true;
						}
					}
				}
			} 
			else {
				//The game has ended
				if (OVRInput.GetDown (OVRInput.Button.One))
					SceneManager.LoadScene (nextScene);
				else if ( OVRInput.GetDown(OVRInput.Button.Three))
					SceneManager.LoadScene (1);
				else if ( OVRInput.GetDown(OVRInput.Button.Two))
					SceneManager.LoadScene (0);
			}
		}
	}

	//Wait until user presses button for panel to disappear
	IEnumerator simulationStartNumerator () {
		yield return new WaitUntil (() => start);
		panel.SetActive (false);
		enemy.SetActive (true);
		spheres.SetActive (true);
	}

	//Check if the correct sphere was destroyed 
	private bool checkIndex () {
		Debug.Log ("Sphere at " + removedSphereIndex + ": " + spheresArray[removedSphereIndex]);
		try {	
			if (spheresArray [removedSphereIndex] != null) {
				//Wrong Order of Colliding with spheres
				//Reset the level
				Debug.Log ("Wrong Order ");
				return false;
			}
		}
		catch (IndexOutOfRangeException e) {
			Debug.Log ("Right Order ");
		}

		++removedSphereIndex;
		--previousSphereIndex;
		resetWait = false;

		//Debug.Log ("Removed Sphere Index: " + removedSphereIndex + " Previous Sphere Index: " + previousSphereIndex
		//+ " Spheres On Scene: " + spheresOnScene );

		return true;
	}




}
