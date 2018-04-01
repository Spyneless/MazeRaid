using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float baseSpeed = 3.0f;
    private float speed;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        speed = baseSpeed;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        speed = baseSpeed;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 dirFront = z * Camera.main.transform.forward;  // front
        Vector3 dirRight = x * Camera.main.transform.right;  // right;

        Vector3 dir = dirFront + dirRight;
        dir = dir.normalized;

        rb.AddForce(dir * speed, ForceMode.VelocityChange);

        //transform.Translate(dirFront.x, 0, dirFront.z);
        //transform.Translate(dirRight.x, 0, dirRight.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            //rigidbody.velocity = Vector3.zero;
            speed = 0;
        }
    }
}
