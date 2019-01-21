using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
	//floats
	public float maxSpeed = 5;
	public float speed = 100f;
	public float jumpPower = 300f;
	float hInput = 0;

	//dashvariables
	public float dashDistance;
	float dashTimer;
	public float dashTime;
	bool facingRight;

	//stats	
	public int curHealth;
	public int maxHealth = 100;

	//bools
	public bool grounded;
	public bool canDoubleJump;

    //References
	private Rigidbody2D rb2d;
	private Animator anim;
    private gameMaster gm;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        curHealth = maxHealth;

        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
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
        	rb2d.AddForce(Vector2.up * jumpPower);
        	canDoubleJump = true;
        }
        else if(Input.GetButtonDown("Jump") && !grounded){
    		if(canDoubleJump){
    			canDoubleJump = false;
    			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
    			rb2d.AddForce(Vector2.up * jumpPower);
    		}
    	}

    	if (curHealth > maxHealth){
    		curHealth = maxHealth;
    	}

    	if(curHealth <= 0){
    		Die();
    	}

    	//Dash code
    	if(Input.GetKeyDown(KeyCode.LeftArrow)){
    		anim.SetBool("Dashing", true);
    	}
    	else{
    		anim.SetBool("Dashing", false);
    	}

    	if(transform.localScale.x == 1)
    		facingRight = true;
    	else
    		facingRight = false;
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
    		canDoubleJump = true;
    	}
    	else{
    		if(canDoubleJump){
    			canDoubleJump = false;
    			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
    			rb2d.AddForce(Vector2.up * jumpPower);
    		}
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


    void Die(){

    	//restart stage
    	SceneManager.LoadScene("DieStage11");
    }


    public void Damage (int dmg){

        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir ) {

    	float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

    	while ( knockDur > timer){

    		timer+=Time.deltaTime;

    		rb2d.AddForce(new Vector3 (knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
    	}

    	yield return 0;
    }

    public void Dash(){
    	Vector3 dash;
    	if (facingRight)
    		dash = new Vector3(dashDistance, 0, 0);
    	else
    		dash = new Vector3(-dashDistance, 0, 0);
    	transform.position+= dash;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Coin")){
            Destroy(col.gameObject);
            gm.points += 1;
        }
    }

}
