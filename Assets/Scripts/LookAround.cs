using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour {

    public float speedH = 2;
    public float speedY = 2;

    private float yaw;
    private float pitch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedY * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
