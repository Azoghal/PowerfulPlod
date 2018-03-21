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
        NetworkServer.DestroyPlayersForConnection(conn);

        if (conn.lastError != NetworkError.Ok)
        {
            if (LogFilter.logError)
            {
                Debug.LogError("ServerDisconnected due to error: " + conn.lastError);
            }
        }

        currentGamemode = sgm.currentGamemode;
        Debug.Log("Yes");
        currentGamemode.handlePlayerLeft();
        base.OnServerDisconnect(conn);
        
    }

    public override void OnStopHost()
    {
        currentGamemode = sgm.currentGamemode;
        currentGamemode.handleStopHost();
        base.OnStopHost();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
