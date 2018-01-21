using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Simulation_Game_Manager : MonoBehaviour {

	public static int spheresOnScene;
	public int tempSphereAmount;
	public int nextScene;
	public int currScene;

	public GameObject[] spheresArray;
	public GameObject panel;
	public GameObject enemy;
	public GameObject spheres;
	public GameObject nextSceneUI;
	public Text endingPanel;
	public DummyLighting.Target sceneDummyTarget;
	public DummyLighting dummyLightingScript;

	public float timeThroughMoves = 0.5f;
	private float timing;

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
		spheresOnScene = spheresArray.Length;
		start = false;
		StartCoroutine ("simulationStartNumerator");
		previousSphereIndex = spheresOnScene;
		removedSphereIndex = 0;
		timing = timeThroughMoves;
		resetWait = false; //Waits for function to test index
		failed = false;
		tempSphereAmount = spheresOnScene;
		dummyLightingScript.SetLighting (sceneDummyTarget, DummyLighting.LightingMode.Highlight);
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
					SceneManager.LoadScene (nextScene);
				else if ( OVRInput.GetDown(OVRInput.Button.Three))
					SceneManager.LoadScene (0);
				else if ( OVRInput.GetDown(OVRInput.Button.Two))
					SceneManager.LoadScene (currScene);
			}
			//If the simulation is not finished
			else if (!finished && !failed) {
				
				if (removedSphereIndex > 0) {
					timing -= Time.deltaTime;
					Debug.Log (timing);
				}

				tempSphereAmount = spheresOnScene;

				//All spheres have been destroyed, game was finished correctly
				if (spheresOnScene == 0 && finished == false) {
					Debug.Log ("Done");
					//enemy.SetActive (false);
					nextSceneUI.SetActive (true);
					finished = true;
					dummyLightingScript.SetLighting (sceneDummyTarget, DummyLighting.LightingMode.Damage);
				} 
				//There are still spheres on the scene
				else {

					//If a sphere was deleted
					if ((spheresOnScene < previousSphereIndex) && resetWait == false) {
						resetWait = true;
						if (!checkIndex ()) {
							//Wrong Order, Reset Level 
							//TODO:: Show UI saying failed
							endingPanel.text = "You have failed the simulation, you did not select the spheres in the " +
								"correct order.";
							endingPanel.color = Color.red;
							nextSceneUI.SetActive (true);
							//enemy.SetActive (false);
							spheres.SetActive (false);
							failed = true;
						}
					}
				}
				//If the user waited too long
				if (timing <= 0f) {
					endingPanel.text = "You have failed the simulation, you took too long.";
					endingPanel.color = Color.red;
					nextSceneUI.SetActive (true);
					//enemy.SetActive (false);
					spheres.SetActive (false);
					failed = true;
				}

			} 
			else {
				//The game has ended
				if (OVRInput.GetDown (OVRInput.Button.One))
					SceneManager.LoadScene (nextScene);
				else if ( OVRInput.GetDown(OVRInput.Button.Three))
					SceneManager.LoadScene (0);
				else if ( OVRInput.GetDown(OVRInput.Button.Two))
					SceneManager.LoadScene (currScene);
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
		resetTime ();
		resetWait = false;

		//Debug.Log ("Removed Sphere Index: " + removedSphereIndex + " Previous Sphere Index: " + previousSphereIndex
			//+ " Spheres On Scene: " + spheresOnScene );

		return true;
	}

	private void resetTime () {
		timing = timeThroughMoves;
	}

}
