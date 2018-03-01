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

    Transform lookatondeath;

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
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;
    }

    [ClientRpc]

    void RpcSpawn()
    {
        
        isDead = false;
        ToggleComponents();

    }

    // write ienumerator to spawn

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown("l"))
            {
                RpcSpawn();
            }
            if (Input.GetKeyDown("k"))
            {
                RpcDie();
            }
        }
        
    }

    // set random respawn position when dies
    // round robin
    // move spawnpoints up high
    // when enabled they drop !
}
