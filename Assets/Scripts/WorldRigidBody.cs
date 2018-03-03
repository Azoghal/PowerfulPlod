using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorldRigidBody : NetworkBehaviour {

    [SerializeField]
    int powerModifier;
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
        transform.GetComponent<Rigidbody>().AddExplosionForce(explodePow * powerModifier, explodePosit, explodeRad,0.1f);
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position + new Vector3(0,4,0);

    }


}
