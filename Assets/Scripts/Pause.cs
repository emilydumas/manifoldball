using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	public OVRInput.Controller controller;
	public string pauseButton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		bool pauseSignal = OVRInput.GetDown (OVRInput.Button.Start);
		if (pauseSignal) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				PauseGame ();
			}

			if (Time.timeScale == 0) {
				Time.timeScale = 1;
				ResumeGame ();
			}

			Debug.LogWarning ("Pause button is pressed. TimeScale: " + Time.timeScale);
		}
		
	}

	private void PauseGame() {
		// Disable all other scripts
		GameObject.Find("Sphere").GetComponent<PositionResetter> ().enabled = false;
		GameObject.Find ("Sphere").GetComponent<Wrapper> ().enabled = false;
		GameObject.Find ("Sphere").GetComponent<Highlighter> ().enabled = false;
		GameObject.Find ("Sphere").GetComponent<AddForce> ().enabled = false;
		GameObject.Find ("Runner").GetComponent<Duplicator> ().enabled = false;
		GameObject.Find ("Runner").GetComponent<SharedParameters> ().enabled = false;
		GameObject.Find ("Runner").GetComponent<SharedParameters> ().enabled = false;
		GameObject.Find ("LeftHand").GetComponent<TouchController> ().enabled = false;
		GameObject.Find ("LeftHand").GetComponent<Grab> ().enabled = false;
		GameObject.Find ("RightHand").GetComponent<TouchController> ().enabled = false;

	}

	private void ResumeGame() {
		// Re-enable all other scripts
		GameObject.Find("Sphere").GetComponent<PositionResetter> ().enabled = true;
		GameObject.Find ("Sphere").GetComponent<Wrapper> ().enabled = true;
		GameObject.Find ("Sphere").GetComponent<Highlighter> ().enabled = true;
		GameObject.Find ("Sphere").GetComponent<AddForce> ().enabled = true;
		GameObject.Find ("Runner").GetComponent<Duplicator> ().enabled = true;
		GameObject.Find ("Runner").GetComponent<SharedParameters> ().enabled = true;
		GameObject.Find ("Runner").GetComponent<SharedParameters> ().enabled = true;
		GameObject.Find ("LeftHand").GetComponent<TouchController> ().enabled = true;
		GameObject.Find ("LeftHand").GetComponent<Grab> ().enabled = true;
		GameObject.Find ("RightHand").GetComponent<TouchController> ().enabled = true;
	}
}
