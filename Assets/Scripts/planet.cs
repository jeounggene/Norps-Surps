using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet : MonoBehaviour
{
	public float rotateSpeed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
		transform.rotation *= Quaternion.Euler (0, 0, rotateSpeed);
    }
}
