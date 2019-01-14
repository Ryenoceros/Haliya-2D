using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Sprite[] HeartSprites;

    public Image HeartUI;

    private Movement player;

    void Start(){
    	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();


    }

    void Update(){
    	HeartUI.sprite = HeartSprites[player.curHealth];
    }
}
