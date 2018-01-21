using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour {

	public void level_1 () {
		SceneManager.LoadScene (1);
	}

	public void level_2 () {
		SceneManager.LoadScene (2);
	}

	public void level_3 () {
		SceneManager.LoadScene (3);
	}

	public void level_4 () {
		SceneManager.LoadScene (4);
	}

	//Change later
	public void level_ragdoll () {
		SceneManager.LoadScene (5);
	}

	public void menu () {
		SceneManager.LoadScene (0);
	}
}
