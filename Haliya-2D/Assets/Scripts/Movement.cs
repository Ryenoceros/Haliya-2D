using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 150f;

	public bool grounded;

	private Rigidbody2D rb2d;
	private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        //transform.position = transform.position + horizontal * Time.deltaTime;

        anim.SetBool("Grounded",grounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    void FixedUpdate()
    {
    	float h = Input.GetAxis("Horizontal");

    	//Moving the player
    	rb2d.AddForce((Vector2.right * speed) * h);

    	//Limiting the speed of the player
    	if(rb2d.velocity.x > maxSpeed)
    	{
    		rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
    	}

    	if (rb2d,velocity.x < -maxSpeed){
    		rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
    	}
    }
}
