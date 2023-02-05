using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {
	public static RespawnPoint instance { get; private set; }

	void Awake() {
		if (!instance) {
			instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update() {

	}
}
