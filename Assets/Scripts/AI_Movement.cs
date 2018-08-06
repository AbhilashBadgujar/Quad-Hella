using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour {


    public Transform Player;
    public float range;
    public float Speed;
    public float moveSpeed;
    public GameObject bullet;
    public float delayTime = 0.5f;
    private float counter = 0;
    public static bool IsPlayerAlive = true;
    
    // Update is called once per frame
    void Update()
    {
        if (IsPlayerAlive)
        {
            range = Vector3.Distance(Player.position, transform.position);

            if (range < 15f)
            {
                look();
            }

            if (range < 12f)
            {
                if (range > 5f)
                {
                    move();
                }
                else if (range < 3f)
                {
                    hit();
                }
            }
        }
    }

    void look()
    {
        Quaternion rotation = Quaternion.LookRotation(Player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Speed);
    }

    void move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void hit()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        counter = 0;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Health>().health -= 5.0f;
            }

        }
    }
}
