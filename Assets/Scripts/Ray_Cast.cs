using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ray_Cast : MonoBehaviour {
	 
	RaycastHit hit; 
	public float length; 

	public Collider One; 
	public Collider Two; 
	public Collider Three; 
	public Collider Four; 

	public Button one; 
	public Button two;
	public Button three; 
	public Button four; 

	private bool level1=false; 
	private bool level2=false;
	private bool level3=false;
	private bool level4=false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit, length)) {
			if (hit.collider == One) {
				one.Select (); 
				level1 = true;
			} else if (hit.collider == Two) {
				two.Select ();
				level2 = true;
			} else if (hit.collider == Three) { 
				three.Select ();
				level3 = true;
			} else if (hit.collider == Four) {
				four.Select ();
				level4 = true; 
			}
		}
		/*if ((OVRInput.GetDown((OVRInput.Button.SecondaryIndexTrigger))||OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))&& level1==true )
			SceneManager.LoadScene (1);
		else if ((OVRInput.GetDown((OVRInput.Button.SecondaryIndexTrigger))||OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))&& level2==true )
			SceneManager.LoadScene (2);
		else if ((OVRInput.GetDown((OVRInput.Button.SecondaryIndexTrigger))||OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))&& level3==true )
			SceneManager.LoadScene (3);
		else if ((OVRInput.GetDown((OVRInput.Button.SecondaryIndexTrigger))||OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))&& level4==true )
			SceneManager.LoadScene (4);
	
*/
		
	}
}
