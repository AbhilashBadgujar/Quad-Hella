using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health;
	void Update () {
        if (health < 0f)
        {
            AI_Movement.IsPlayerAlive = false;
           
        }
	}
}
