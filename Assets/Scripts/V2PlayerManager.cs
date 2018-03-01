using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class V2PlayerManager : NetworkBehaviour {

    
    // meshrenderer, box collider in
    // also set gravity

    Rigidbody rb;
    BoxCollider bc;
    SkinnedMeshRenderer smr;
    PlayerController pc;

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    void Start () {
        rb = transform.GetComponent<Rigidbody>();
        bc = transform.GetComponent<BoxCollider>();
        smr = transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>();
        pc = transform.GetComponent<PlayerController>();
    }

    void ToggleComponents() // run this when switching from dead to alive or vice versa
    {
        
        rb.velocity = Vector3.zero;
        rb.useGravity = !rb.useGravity;
        bc.enabled = !bc.enabled;
        smr.enabled = !smr.enabled;
        if (isLocalPlayer)
        {
            pc.enabled = !pc.enabled;
        }

    }

    [ClientRpc]

    void RpcDie()
    {
        isDead = true;
        ToggleComponents();
    }

    [ClientRpc]

    void RpcSpawn()
    {
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        isDead = false;
        ToggleComponents();

    } 
}
