using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LMSGameMode : NetworkBehaviour {

    int ConnectedPlayerCount;
    V2PlayerManager[] Players; 
    int nextDeathIndex;
    float respawnTime;
    bool matchable;

	// Use this for initialization
	void Start () {
        Players = new V2PlayerManager[20];
        matchable = false;
        ConnectedPlayerCount = 0;

	}

    
    public void playerJoined(V2PlayerManager PlayMan)
    {
        
    }

    public void playerLeft(bool isDead)
    {
        
            
            
        
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
            V2PlayerManager v2pm = Players[i].GetComponent<V2PlayerManager>();
            if (v2pm.isDead == false)
            {
                v2pm.RpcDie();
                Players[nextDeathIndex] = v2pm;
                nextDeathIndex++;
            }
        }
    }


    public void handlePlayerDeath(V2PlayerManager playerManager)
    {
        playerManager.RpcDie();
        
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
