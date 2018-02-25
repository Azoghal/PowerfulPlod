using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathBoxHandler : NetworkBehaviour {
    public ServerGameManager serverGameManager;

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();

            if (playerManager != null)
            {
                //serverGameManager.handlePlayerDeath(playerManager); // inform serverGameManager the player has died.
                serverGameManager.SendMessage("handlePlayerDeath",playerManager);
            }
        }
        else if (other.gameObject.CompareTag("World Rigidbody"))
        {
            WorldRigidBody wrb = other.gameObject.GetComponent<WorldRigidBody>();

            if (wrb != null)
            {
                serverGameManager.handleWorldRigidBodyRespawn(wrb);
            }
        }
    }
}
