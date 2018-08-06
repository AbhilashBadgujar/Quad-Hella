using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {
    public PlayerWeapon playerWeapon;

    [SerializeField]
    Camera cam;

    [SerializeField]
    LayerMask layer;
    private string player_tag = "Player" ;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (cam == null)
        {
            print("cam ref error");
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, playerWeapon.range, layer))
        {
            print("hit: " + hit.collider.name);
            if (hit.collider.tag == player_tag)
            {
                CmdPlayerShot(hit.collider.name, playerWeapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string playerID, int damage)
    {
        print(playerID + "Has been Shot");

        Player player = GM.GetPlayerID(playerID);
        player.RpcTakeDamage(damage);
        
    }

 }

