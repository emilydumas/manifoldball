using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Following https://www.youtube.com/watch?v=blO039OzUZc

public class SimpleFPSController : MonoBehaviour {
    public float speed = 10.0f;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    private Vector2 mouseLook;
    private Vector2 smoothV;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;		
	}
	
	// Update is called once per frame
	void Update () {
        // Keyboard update
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        var xRotation = Quaternion.AngleAxis(mouseLook.x, transform.InverseTransformDirection(Vector3.up));
        //var yRotation = Quaternion.AngleAxis(-mouseLook.y, transform.InverseTransformDirection(Vector3.right));
        transform.localRotation = xRotation;  //*yRotation;
    }
}
