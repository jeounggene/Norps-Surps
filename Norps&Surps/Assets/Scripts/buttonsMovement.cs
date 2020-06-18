using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsMovement : MonoBehaviour
{
	private bool smooth = true;

	public Transform a,b;
	private Transform lookAt;

	[Range(0, 1)]
	public float smoothspeed = 0.125f;

	void Start(){
		lookAt = b;
	}

	private void LateUpdate () {
		Vector3 desiredPOS = lookAt.position;

		if (smooth) {
			transform.position = Vector3.Lerp (transform.position, desiredPOS, smoothspeed);
		} else {
			transform.position = desiredPOS;
		}
	}

	public void moveTo(bool outside){
		if (outside) {
			lookAt = b;
		} else {
			lookAt = a;
		}
	}
}
