using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {
    private bool changed;
	private Renderer rend;

	// Use this for initialization
	void Start () {
        changed = false;
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!changed)
        {
            changed = true;
            Material mat = new Material(rend.material);
            mat.name = "Highlight";
            mat.color = new Color(1f, 0f, 1f);
            rend.material = mat;
        }
	}
}
