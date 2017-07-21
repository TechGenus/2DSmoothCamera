using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float[] speed = new float[2];

    private Rigidbody rb;

	private float hAxis;
	private float jumpAxis;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        lockZAxis();

        hAxis = Input.GetAxis("Horizontal"); // this takes keyboard input from left/right arrows as well as the a and d keys
        jumpAxis = Input.GetAxis("Jump"); // this takes keyboard input from the space bar

        Vector3 offset = new Vector3(hAxis * speed[0], 0, 0);
        // rb.MovePosition(transform.position + offset); // this moves rigidbody so it is less smooth
		transform.position += offset * Time.deltaTime; // moves character to new position that is old position + offset

        if (isGrounded() && jumpAxis > 0)
            jump();
	}

    bool isGrounded()
    {
        // casting a ray downwards from the center of the character, if the ray hits something it means the character is grounded
        return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 1.5f);
    }

    void jump()
    {
		rb.velocity += new Vector3 (0, jumpAxis * speed [1], 0) * Time.deltaTime * 10; // adds jump speed to current y axis velocity of object
    }

    void lockZAxis()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}