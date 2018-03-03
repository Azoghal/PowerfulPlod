using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LMSGameMode : NetworkBehaviour {

    int ConnectedPlayerCount;
    V2PlayerManager[] Players;
    GameObject[] temp;
    int nextDeathIndex;
    float respawnTime;
    bool matchable;

	// Use this for initialization
	void Start () {

        matchable = false;
        ConnectedPlayerCount = 0;
        respawnTime = 3;
	}

    
    public void playerJoined(V2PlayerManager playerManager)
    {

        temp = GameObject.FindGameObjectsWithTag("Player");
        Players = new V2PlayerManager[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            Players[i] = temp[i].GetComponent<V2PlayerManager>();
        }
        if (matchable == false)
        {
            if (Players.Length == 2)
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
