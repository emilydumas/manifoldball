using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	[SerializeField] private GameObject pausePanel;
	public OVRInput.Controller controller;

	// To avoid calling GetComponent too many times in Update(),
	// it is better to store the components in variables once.
	// Reference: http://wiki.unity3d.com/index.php?title=General_Performance_Tips
	private PositionResetter positionResetter;
	private ManualBouncePhysics ManualBouncePhysics;
	private TouchController touchController;

	// Use this for initialization
	void Start () {
		positionResetter = GameObject.Find("Sphere").GetComponent<PositionResetter> ();
		ManualBouncePhysics = GameObject.Find ("Sphere").GetComponent<ManualBouncePhysics> ();
		touchController = GameObject.Find ("LeftHand").GetComponent<TouchController> ();
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

		// Disable scripts related to movements
		positionResetter.enabled = false;
		ManualBouncePhysics.enabled = false;
		touchController.enabled = false;

		// Display pause menu
		pausePanel.SetActive (true);
	}

	private void ResumeGame() {
		Time.timeScale = 1;

		// Re-enable scripts
		positionResetter.enabled = true;
		ManualBouncePhysics.enabled = true;
		touchController.enabled = true;

		// Hide pause menu
		pausePanel.SetActive (false);
	}
}
