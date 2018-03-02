using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class V2PlayerManager : NetworkBehaviour {


    // meshrenderer, box collider in
    // also set gravity
    GameObject servergamemanager;

    Rigidbody rb;
    BoxCollider bc;
    SkinnedMeshRenderer smr;
    PlayerController pc;
    GameObject cam;
    Transform lookatondeath;

    [SyncVar]
    private bool _isDead = true;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    void Start () {
        //isDead = false;
        rb = transform.GetComponent<Rigidbody>();
        bc = transform.GetComponent<BoxCollider>();
        smr = transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>();
        pc = transform.GetComponent<PlayerController>();
        cam = transform.GetChild(1).gameObject;
        if (!isLocalPlayer)
        {
            cam.SetActive(false);
        }
        if (isDead == false)
        {
            ToggleComponents();
        }
        servergamemanager = GameObject.FindGameObjectWithTag("GameController");
        servergamemanager.SendMessage("playerJoined", this);
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

    public void RpcDie()
    {
        isDead = true;
        ToggleComponents();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;
    }

    [ClientRpc]

    public void RpcSpawn()
    {
        
        isDead = false;
        ToggleComponents();

    }

    // write ienumerator to spawn

    private void Update()
    {
        
        
    }

    // set random respawn position when dies
    // round robin
    // move spawnpoints up high
    // when enabled they drop !
}
