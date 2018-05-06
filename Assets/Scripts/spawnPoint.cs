using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class spawnPoint : NetworkBehaviour {

    [SyncVar]
    public bool open = true;
    

    // Use this for initialization
    void Start () {
        open = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
