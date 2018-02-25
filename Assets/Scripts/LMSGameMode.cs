using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LMSGameMode : NetworkBehaviour {

    int ConnectedPlayerCount;
    PlayerManager[] deadPlayers; // set size to connectedPlayercount -1. if full, credit last alive and respawn all
    int nextDeathIndex;
    float respawnTime;

	// Use this for initialization
	void Start () {
        ConnectedPlayerCount = Network.connections.Length;
        deadPlayers = new PlayerManager[ConnectedPlayerCount];
        nextDeathIndex = 0;
        respawnTime = 5;
	}

    public void handlePlayerDeath(PlayerManager playerManager)
    {
        playerManager.RpcDie();

        // TODO: Handle leaderboard.

        deadPlayers[nextDeathIndex] = playerManager;
        nextDeathIndex++;

        // maybe havea coroutine for handle player LMS death, which only respawns once all are dead?

        // check that more than one person lives
            
        if (nextDeathIndex == ConnectedPlayerCount-1) // if one person left
        {

            //credit the person still alive

            for (int i = 0; i < ConnectedPlayerCount; i++)
            {
                StartCoroutine(handlePlayerSpawn(deadPlayers[i]));
            }
        }

    }

    public IEnumerator handlePlayerSpawn(PlayerManager PM)
    {
        yield return new WaitForSeconds(respawnTime);

        PM.RpcRespawn();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
