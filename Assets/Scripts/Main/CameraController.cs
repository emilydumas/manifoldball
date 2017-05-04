using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	// Materials needing to receive camera pose updates
	private List<Renderer> queue = new List<Renderer> ();

	// Update is called once per frame
	void Update () {
		// Apply to each object in the queue
		Matrix4x4 m = Camera.main.transform.worldToLocalMatrix;

		foreach (Renderer r in queue) {
			mop.SetCameraPose (r, m);
		}

//		if (Input.GetKey (KeyCode.Backspace))
//			UnityEditor.EditorApplication.isPlaying = false;
    }

	// Called once before each rendering pass, so up to three times per frame in VR
	// (Left Eye, Right Eye, Desktop display)
	void OnPreRender()
	{
		SetEye (0);
	}

	public void SetEye(int x)
	{
		foreach (Renderer r in queue) {
			r.sharedMaterial.SetInt ("_eye", x);
		}
	}

	public void AddCameraPoseTargetMaterial(Renderer r)
	{
		if (!queue.Contains (r)) {
			queue.Add (r);
			Debug.Log ("CameraController.queue["+(queue.Count - 1).ToString()+"] = \""+ r.name+ "\"");
		}
	}

	public void RemoveCameraPoseTargetMaterial(Renderer r)
	{
		queue.Remove (r);
	}
				
}
