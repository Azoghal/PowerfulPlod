using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LMSGameMode : NetworkBehaviour, Assets.Scripts.IGamemode {

    int ConnectedPlayerCount;
    V2PlayerManager[] Players;
    GameObject[] temp;
    float respawnTime;
    bool matchable;

	// Use this for initialization
	void Start () {
        if (isServer)
        {
            matchable = false;
            ConnectedPlayerCount = 0;
            respawnTime = 3;
        }
	}

    public void handleLoading()
    {

    }

    public void handlePlayerJoined(GameObject player)
    {
        Debug.Log("Player has joined");

        Players = grabPlayers();
        ConnectedPlayerCount = Players.Length;
        if (matchable == false)
        {
            if (ConnectedPlayerCount == 2)
            {
                //spawn all and start game
                spawnAll();
                matchable = true;
            }
        }
        //else if (matchable == true)
        //{
        //    // continue match, new player already dead
        //}

    }

    private V2PlayerManager[] grabPlayers()
    {
        temp = GameObject.FindGameObjectsWithTag("Player");
        Players = new V2PlayerManager[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            Players[i] = temp[i].GetComponent<V2PlayerManager>();
        }
        return Players;
    }

    public void handlePlayerLeft()
    {
        ConnectedPlayerCount--;
        Debug.Log("Left");
        Players = grabPlayers();
        Debug.Log(Players.Length);
        if (matchable == true)
        {
            if (ConnectedPlayerCount < 2)
            {
                Debug.Log("Absolutely");
                killAllAlive();
                matchable = false;
            }
        }
    }

    public void handleStopHost()
    {
        matchable = false;
        ConnectedPlayerCount = 0;
        Players = new V2PlayerManager[1];

    }

    public void playerLeft(V2PlayerManager playerManager)
    {
        temp = GameObject.FindGameObjectsWithTag("Player");
        Players = new V2PlayerManager[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            Players[i] = temp[i].GetComponent<V2PlayerManager>();
        }
        if (matchable == true)
        {
            if (Players.Length == 1)
            {
                matchable = false;
                killAllAlive();
            }
        }

    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        playerLeft(null);
    }


    void spawnAll()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].RpcSpawn();
        }
    }

    void killAllAlive()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].isDead == false)
            {
                Players[i].RpcDie();
            }
        }
    }


    public void handlePlayerDeath(V2PlayerManager playerManager)
    {
        int count = 0;
        playerManager.RpcDie();
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].isDead == false)
            {
                count++;
            }
        }
        Debug.Log(count);
        if (count == 1)
        {
            killAllAlive();
            spawnAll();
        }
        
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
