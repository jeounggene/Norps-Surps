using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
	public float size = 5;
	public float zoomFactor = 1;
	public float currentSize = 5;

	public Camera cam;

	public Transform lookAt;

	private bool smooth = true;

	public Vector3 offset  = new Vector3(0f, 0f, -2f);

	[Range(0, 1)]
	public float smoothspeed = 0.125f, scalingSpeed = 0.125f;

	void Start(){
		size = cam.orthographicSize;
		currentSize = size;
	}

	private void LateUpdate () {
		Vector3 desiredPOS = lookAt.transform.position + offset;

		if (smooth) {
			transform.position = Vector3.Lerp (transform.position, desiredPOS, smoothspeed);
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, currentSize, scalingSpeed);
		} else {
			transform.position = desiredPOS;
			cam.orthographicSize = currentSize;
		}
	}

	public void moveTo(Transform pos, bool zoom){
		lookAt = pos;
		if (zoom) {
			currentSize = size * zoomFactor;
		} else {
			currentSize = size;
		}
	}
}
