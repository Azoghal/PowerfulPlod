using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {


    float speed;
    public Animator anim;
    bool attacking;
    float t;
    float mouseX;
    float mouseY;
    float cd;
    public bool playing;
    float stompCD;
    float explosionRadius;
    float explosionPower;
    Camera cam;
    bool blownUp;
    CursorLockMode wantedMode;
    GameObject exMenu;

    // Use this for initialization
    void Start () {
        playing = true;
        explosionRadius = 4;
        explosionPower = 1000;
        cd = 2;
        stompCD = 2;
        wantedMode = CursorLockMode.Locked;
        Cursor.lockState = wantedMode;
        PlayerManager Pm = transform.GetComponent<PlayerManager>();
        //Pm.Setup();
	}
	
	// Update is called once per frame
	void Update () {
        speed = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        checkAttack();
        transform.Rotate(new Vector3(0, mouseX, 0));
        if (attacking == false)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        else if (t > 0.8f && blownUp == false)
        {
            blownUp = true;
            // add explosionscript here
            Vector3 explosionPos = transform.GetChild(2).position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    if (rb.gameObject != transform.gameObject)
                    {
                        //rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius);
                        // send force to be recieved
                        CmdSubmitForce(rb.gameObject, explosionPower, explosionPos, explosionRadius);
                    }
                }
            }
        }
        if (Input.GetKeyDown("escape"))
        {
            Instantiate<GameObject>(exMenu);
        }

        anim.SetBool("Attacking", attacking);
        anim.SetFloat("Speed", speed);
        cd += Time.deltaTime;

        

    }

    [Command]
    void CmdSubmitForce(GameObject hit,float explodePower,Vector3 explodePosition, float explodeRadius)
    {
        if (hit.CompareTag("Player"))
            hit.GetComponent<PlayerController>().RpcExecuteKickForce(explodePower, explodePosition, explodeRadius);
        else if (hit.CompareTag("World Rigidbody"))
            hit.GetComponent<WorldRigidBody>().RpcExecuteKickForce(explodePower, explodePosition, explodeRadius);
    }

    [ClientRpc]

    void RpcExecuteKickForce(float explodePow, Vector3 explodePosit, float explodeRad)
    {
        transform.GetComponent<Rigidbody>().AddExplosionForce(explodePow, explodePosit, explodeRad);
    }


    void checkAttack()
    {
        if (Input.GetKeyDown("space") && cd > stompCD)
        {
            attacking = true;
            t = 0;
            cd = 0;
            blownUp = false;
        }

        t+= Time.deltaTime;

        if (t > 1.1f)
        {
            attacking = false;
        }

    }
}
