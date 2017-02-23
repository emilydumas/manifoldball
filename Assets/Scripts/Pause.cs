using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	[SerializeField] private GameObject pausePanel;
	public OVRInput.Controller controller;

	// Use this for initialization
	void Start () {
		pausePanel = GameObject.Find ("Pause Panel");
		pausePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		bool pauseSignal = OVRInput.GetDown (OVRInput.Button.Start);
		if (pauseSignal) {
			if (!pausePanel.activeInHierarchy) {
				PauseGame ();
			} 
			else if (pausePanel.activeInHierarchy) {
				ResumeGame ();
			}
		}		
	}

	private void PauseGame() {
		Time.timeScale = 0;

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

		// Display pause menu
		pausePanel.SetActive (true);
	}

	private void ResumeGame() {
		Time.timeScale = 1;

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

		// Hide pause menu
		pausePanel.SetActive (false);
	}
}
