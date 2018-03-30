using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class V2PlayerManager : NetworkBehaviour {


    // meshrenderer, box collider in
    // also set gravity
    ServerGameManager servergamemanager;

    Rigidbody rb;
    BoxCollider bc;
    SkinnedMeshRenderer smr;
    PlayerController pc;
    GameObject cam;
    Transform lookatondeath;
    public MainMenuManager mmm;

    [SyncVar] [SerializeField]
    private string _username;
    public string username
    {
        get { return _username; }
        set { _username = value; }
    }

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
        mmm = GameObject.FindGameObjectWithTag("mainMenu").GetComponent<MainMenuManager>();
        servergamemanager = (ServerGameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent<ServerGameManager>();
        
        if (!isLocalPlayer)
        {
            cam.SetActive(false);
            if (isDead == true)
            {
                smr.enabled = false;
            }
            
        }

        else
        {
            CmdPlayerJoined();
            
        }




        if (isDead == false)
        {

            ComponentsLive();
            smr.enabled = true;
        }
        
        
    }

    private void OnConnectedToServer()
    {
        username = mmm.currentName;
    }

    [Command]
    void CmdPlayerJoined()
    {
        servergamemanager.currentGamemode.handlePlayerJoined(this.gameObject);
    }
    

    void ComponentsDie() // run this when switching from dead to alive or vice versa
    {
        
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        bc.enabled = false;
        smr.enabled = false;
        if (isLocalPlayer)
        {
            pc.enabled = false;
        }

    }

    void ComponentsLive() // run this when switching from dead to alive or vice versa
    {

        rb.useGravity = true;
        bc.enabled = true;
        smr.enabled = true;
        if (isLocalPlayer)
        {
            pc.enabled = true;
        }

    }

    [ClientRpc]

    public void RpcDie()
    {
        if (isDead == false)
        {
            isDead = true;
            Debug.Log("killed plater");
            ComponentsDie();
            Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
            transform.position = _spawnPoint.position;
            transform.rotation = _spawnPoint.rotation;
        }
        
    }

    [ClientRpc]

    public void RpcSpawn()
    {
        Debug.Log("spanwed");
        if (isDead == true)
        {
            isDead = false;
            ComponentsLive();
        }
        

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
