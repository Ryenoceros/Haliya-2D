using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float maxSpeed = 5;
	public float speed = 100f;
	public float jumpPower = 300f;
	float hInput = 0;

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
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        if(Input.GetAxis("Horizontal") < -0.1f){
        	transform.localScale = new Vector3(-1, 1, 1);
        }

        if(Input.GetAxis("Horizontal") > 0.1f){
        	transform.localScale = new Vector3(1, 1, 1);
        }

        if(Input.GetButtonDown("Jump") && grounded){
        	
        }

    }

    void Move(float horizontalInput){
    	if (horizontalInput < 0){
    		transform.localScale = new Vector3(-1, 1, 1);
    	}
    	if (horizontalInput > 0){
    		transform.localScale = new Vector3(1, 1, 1);
    	}
    	rb2d.AddForce((Vector2.right * speed) * horizontalInput);

    	//Limiting the speed of the player
    	if(rb2d.velocity.x > maxSpeed)
    	{
    		rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
    	}

    	if (rb2d.velocity.x < -maxSpeed){
    		rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
    	}

    }

    public void Jump(){
    	if(grounded){
    		rb2d.AddForce(Vector2.up * jumpPower);
    	}
    }

    void FixedUpdate()
    {
    	Vector3 easeVelocity = rb2d.velocity;
    	easeVelocity.y = rb2d.velocity.y;
    	easeVelocity.z = 0.0f;
    	easeVelocity.x *= 0.75f;

    	float h = Input.GetAxis("Horizontal");

    	//fake friction easing the x speed of our player
    	if(grounded){
    		rb2d.velocity = easeVelocity;
    	}

    	//Moving the player
    	Move (hInput);
    }

    public void StartMoving(float horizontalInput){
    	hInput = horizontalInput;
    }
}
