﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorldRigidBody : NetworkBehaviour {

    [SerializeField]
    int powerModifier;
    public GameObject spawner;
	// Use this for initialization
	void Start () {
        powerModifier = 4;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ClientRpc]

    public void RpcExecuteKickForce(float explodePow, Vector3 explodePosit, float explodeRad)
    {
        transform.GetComponent<Rigidbody>().AddExplosionForce(explodePow * powerModifier, explodePosit, explodeRad);
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Transform _spawnPoint = spawner.transform;
        transform.position = _spawnPoint.position;

    }


}
