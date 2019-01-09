using System.Collections;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
	private Movement player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<Movement>();
    }

    void OnTriggerEnter2D(Collider2D col){
    	player.grounded = true;
    }

    void OnTriggerStay2D(Collider2D col){
    	player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D col){
    	player.grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
