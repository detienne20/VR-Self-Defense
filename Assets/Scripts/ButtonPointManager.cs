using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPointManager : MonoBehaviour {
	public Image button;
	public Color normal, highlight;
	public int loadSceneNum;

	private int pointerCount = 0;

	public void AddPointer(){
		if (pointerCount == 0) {
			button.color = highlight;
		}
		pointerCount++;
	}

	public void RemovePointer(){
		pointerCount--;
		if (pointerCount == 0) {
			button.color = normal;
		}
	}

	public void Click(){
		if (loadSceneNum >= 0) {
			SceneManager.LoadScene (loadSceneNum);
		}
	}
}
