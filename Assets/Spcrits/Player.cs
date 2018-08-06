﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SyncVar]
    bool _isDead = false;

    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField] int maxHealth = 100;

    [SyncVar]
    int currentHealth;

    [SerializeField]
    Behaviour[] disableOnDeath;
    bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefults();
    }

    private void SetDefults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.K))
        {
            RpcTakeDamage(999);
        }
    }

    [ClientRpc]
    public void RpcTakeDamage(int ammount)
    {
        if (isDead)
            return;
        currentHealth -= ammount;
        print(currentHealth);
        print(transform.name + "now has " + currentHealth + "health.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        StartCoroutine(Respwan());
    }

    IEnumerator Respwan()
    {
        yield return new WaitForSeconds(GM.gm.matchSettings.respwanTime);

        SetDefults();

        Transform _startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;
    }
}
