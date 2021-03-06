﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FFAGameMode : NetworkBehaviour, Assets.Scripts.IGamemode {

    float respawnTime;

    // Use this for initialization
    void Start()
    {

        respawnTime = 3f;

    }

    public void handleLoading()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Handles player death notification from the death mesh and passes the message forward to clients via RPC, logs the death onto the leaderboard.
    /// </summary>
    /// <param name="playerManager">The playerManager of the dieing player</param>
    /// 
    public void handlePlayerDeath(V2PlayerManager playerManager)
    {
        playerManager.RpcDie();

        // TODO: Handle leaderboard.
        StartCoroutine(handlePlayerSpawn(playerManager));

        // maybe havea coroutine for handle player LMS death, which only respawns once all are dead?

    }

    public void handlePlayerJoined(GameObject player)
    {
        StartCoroutine(handlePlayerSpawn(player.GetComponent<V2PlayerManager>()));
    }

    public void handlePlayerLeft()
    {
        Debug.Log("Left");
    }

    public void handleStopHost()
    {
        
    }

    public IEnumerator handlePlayerSpawn(V2PlayerManager PM)
    {
        yield return new WaitForSeconds(respawnTime);

        PM.RpcSpawn();
    }

    
}
