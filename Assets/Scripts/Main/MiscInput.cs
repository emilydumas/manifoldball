using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiscInput : MonoBehaviour {
	public GameObject glove;
	public GameObject racket;
	private bool Ydown = false;

	private HandTracking htGlove;
	private HandTracking htRacket;

	void Start() {
		htGlove = glove.GetComponent<HandTracking> ();
		htRacket = racket.GetComponent<HandTracking> ();
	}

	void Update () {
		// Quit on both thumbstick press
		if (OVRInput.Get (OVRInput.RawButton.LThumbstick) && OVRInput.Get (OVRInput.RawButton.RThumbstick)) {
			UnityEngine.Application.Quit ();
			Debug.LogError ("Quit selected.");
		}

		// Return to main menu on start button press
		if (OVRInput.GetDown (OVRInput.RawButton.Start))
			SceneManager.LoadScene ("MenuScene");

		// Switch hand roles on Y button press
		if (!Ydown && OVRInput.GetDown (OVRInput.RawButton.Y)) {
			if (htGlove != null && htRacket != null) {
				OVRInput.Controller gloveController = htGlove.controller;
				OVRInput.Controller racketController = htRacket.controller;

				htGlove.controller = racketController;
				htRacket.controller = gloveController;
			}
		}
		Ydown = OVRInput.GetDown (OVRInput.RawButton.Y);
	}
}
