using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NewNetworkManager : NetworkManager {

    Assets.Scripts.IGamemode currentGamemode;
    public ServerGameManager sgm; 

	// Use this for initialization
	void Start () {
        currentGamemode = sgm.currentGamemode;
	}

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("Yes");
        currentGamemode.handlePlayerLeft();
        base.OnServerDisconnect(conn);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
