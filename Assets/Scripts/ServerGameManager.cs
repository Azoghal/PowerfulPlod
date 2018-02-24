using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerGameManager : NetworkBehaviour {

    float respawnTime;

	// Use this for initialization
	void Start () {
        respawnTime = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Handles player death notification from the death mesh and passes the message forward to clients via RPC, logs the death onto the leaderboard.
    /// </summary>
    /// <param name="playerManager">The playerManager of the dieing player</param>
    public void handlePlayerDeath (PlayerManager playerManager)
    {
        playerManager.RpcDie();

        // TODO: Handle leaderboard.
        StartCoroutine(handlePlayerSpawn(playerManager));
    }

    public IEnumerator handlePlayerSpawn(PlayerManager PM)
    {
        yield return new WaitForSeconds(respawnTime);

        PM.RpcRespawn();
    }

}
