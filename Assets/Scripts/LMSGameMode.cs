using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LMSGameMode : NetworkBehaviour {

    int ConnectedPlayerCount;
    V2PlayerManager[] deadPlayers; // set size to connectedPlayercount -1. if full, credit last alive and respawn all
    int nextDeathIndex;
    float respawnTime;
    bool matchable;

	// Use this for initialization
	void Start () {
        deadPlayers = new V2PlayerManager[20];
        matchable = false;
        ConnectedPlayerCount = 0;
        nextDeathIndex = 0;
	}

    public void playerJoined(V2PlayerManager PlayMan)
    {
        ConnectedPlayerCount = Network.connections.Length;
        if (matchable == false)
        {
            // add to deadplayers
            addToDeadPlayers(PlayMan);
            if (ConnectedPlayerCount == 2)
            {
                matchable = true;
                //spawn in all
                spawnAll();
            }
        }
        else if (matchable == true)
        {
            // add to deadPlayers
            addToDeadPlayers(PlayMan);
            if (ConnectedPlayerCount == 1)
            {
                matchable = false;
                // kill off everyone
                killAllAlive();
            }
        }
    }

    

    void addToDeadPlayers(V2PlayerManager v)
    {
        v.RpcDie();
        deadPlayers[nextDeathIndex] = v;
        nextDeathIndex++;
    }

    void spawnAll()
    {
        for (int i = 0; i < deadPlayers.Length; i++)
        {
            deadPlayers[i].RpcSpawn();
        }
    }

    void killAllAlive()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            V2PlayerManager v2pm = players[i].GetComponent<V2PlayerManager>();
            if (v2pm.isDead == false)
            {
                v2pm.RpcDie();
                deadPlayers[nextDeathIndex] = v2pm;
                nextDeathIndex++;
            }
        }
    }


    public void handlePlayerDeath(V2PlayerManager playerManager)
    {
        playerManager.RpcDie();
        deadPlayers[nextDeathIndex] = playerManager;
        nextDeathIndex++;

    }

    public IEnumerator handlePlayerSpawn(V2PlayerManager PM)
    {
        yield return new WaitForSeconds(respawnTime);

        PM.RpcSpawn();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
