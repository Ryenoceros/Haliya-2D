using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour{

	private Movement player;

	void Start(){

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

	}

	void OnTriggerEnter2D(Collider2D ool) {

		if (ool.CompareTag("Player")){

			player.Damage(1);

			StartCoroutine (player.Knockback(0.02f, 350, player.transform.position ));
		}

	}
}
