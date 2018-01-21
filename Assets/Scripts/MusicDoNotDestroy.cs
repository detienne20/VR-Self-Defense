using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDoNotDestroy : MonoBehaviour {

	private static MusicDoNotDestroy current = null;

	public static MusicDoNotDestroy Instance {
		get {
			return current;
		}
	}

	void Start () {
		if (current != null && current != this) {
			Destroy (this.gameObject);
			return;
		}
		else {
			current = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}
}
