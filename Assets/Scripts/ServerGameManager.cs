using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerGameManager : NetworkBehaviour {

    float WRBrespawnTime;
    public Assets.Scripts.IGamemode currentGamemode;

    // Use this for initialization
    void Start () {
        currentGamemode = gameObject.GetComponent<LMSGameMode>();
        WRBrespawnTime = 4f;
	}

    public void ChangeGamemode(Assets.Scripts.IGamemode newGamemode)
    {
        currentGamemode = newGamemode;
        newGamemode.handleLoading();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void handleWorldRigidBodyRespawn(WorldRigidBody wrb)
    {
        StartCoroutine(respawnWorldRigidbody(wrb));
    }

    public IEnumerator respawnWorldRigidbody(WorldRigidBody wrb)
    {
        yield return new WaitForSeconds(WRBrespawnTime);

        wrb.RpcRespawn();
    }


   
}
