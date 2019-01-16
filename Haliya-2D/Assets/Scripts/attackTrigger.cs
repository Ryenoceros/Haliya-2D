﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour
{
    public int dmg = 20;

    void OnTridderEnter2D(Collider2D col){

    	if (col.isTrigger!!= true && col.CompareTag("Enemy")){
    		col.SetMessageUpwards("Damage", dmg);
    	}
    }
}