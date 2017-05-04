using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinetics : MonoBehaviour {
	private Renderer ren;

	public Vector3 velocity { get; set; }
	public float friction { get; set; }
	public bool kineticsActive { get; set; }

	public void Start() {
		ren = gameObject.GetComponent<Renderer> ();
		friction = 0;
		makeStationary ();
		kineticsActive = true;
	}

	void Update () {
		if (kineticsActive) {
			mop.TranslateObjectPosition(ren, velocity * Time.deltaTime);
			velocity = velocity * (1 - friction);
		}
	}

	public void makeStationary() {
		velocity = new Vector3 (0, 0, 0);
	}
}
