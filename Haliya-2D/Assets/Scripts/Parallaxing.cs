using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds; //Array list of all the back and foregrounds to be parallaxed
    private float[] parallaxScales;	//proportion of camera movement to move the backgrounds
    public float smoothing = 1f;	//How smooth the parallax is going to be 

    private Transform cam;			//reference to the main cam's transform
    private Vector3 prevCamPos;		//pos of cam in prev frame

    //called before start
    void Awake(){
    	//set up camera the reference
    	cam = Camera.main.transform;
    }

    //Use this for initialization
    void Start()
    {	
    	//the previous frame had the current frames camera position
    	prevCamPos = cam.position;

    	//assigning corresponding parallaxScales
    	parallaxScales = new float[backgrounds.Length];
    	for (int i=0; i< backgrounds.Length; i++){
    		parallaxScales[i] = backgrounds[i].position.z*-1;
    	} 
    }

    // Update is called once per frame
    void Update()
    {
        //for each background
        for(int i = 0; i < backgrounds.Length; i++){
        	//parallax is the opposite of the camera movement because the previous frame multiplied by the scale
        	float parallax = (prevCamPos.x = cam.position.x) * parallaxScales[i];

        	//set a target x position which is the current position plus the parallax
        	float backgroundTargetPosX = backgrounds[i].position.x + parallax;

        	//create a target position
        	Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
        	

        	backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);


        }
    
    	prevCamPos = cam.position;
    }

}
