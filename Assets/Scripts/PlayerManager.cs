using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; } 
    }

    [SerializeField]
    private Behaviour[] DisableOnDeath;
    private bool[] WasEnabled;
    
    //void checkDead()
    //{
    //    if (transform.position.y <= -5) // this is moving to server side
    //    {
    //        Die();
    //    }
    //}


    [ClientRpc]
    public void RpcDie()
    {
        if (!isLocalPlayer)
        {
            transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            if (!isDead)
                Debug.Log("player has died");

            isDead = true;

            //disable components

            for (int i = 0; i < DisableOnDeath.Length; i++)
            {
                DisableOnDeath[i].enabled = false;
            }

            Collider _col = GetComponent<Collider>();
            if (_col != null)
                _col.enabled = false;

            Rigidbody _rb = GetComponent<Rigidbody>();
            if (_rb != null)
            {
                _rb.useGravity = false;
                _rb.velocity = Vector3.zero;
            }



            transform.position = new Vector3(0, 3, -5);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;

            // call respawn method

            StartCoroutine(respawn());
        }
    }

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(3f);
        setDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;

    }

	// Use this for initialization
	public void Setup () {

        WasEnabled = new bool[DisableOnDeath.Length];
        for (int i = 0; i < WasEnabled.Length; i++)
        {
            WasEnabled[i] = DisableOnDeath[i].enabled;
        }

        setDefaults();
	}

    void setDefaults()
    {
        isDead = false;

        for (int i = 0; i < DisableOnDeath.Length; i++)
        {
            DisableOnDeath[i].enabled = WasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;

        Rigidbody _rb = GetComponent<Rigidbody>();
        if (_rb != null)
            _rb.useGravity = true;

        transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
        //checkDead();
	}
}
