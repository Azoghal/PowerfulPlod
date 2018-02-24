using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void explodeForce(float xForce,Vector3 xPosition, float xRadius)
    {
        rb.AddExplosionForce(xForce, xPosition, xRadius);
    }
}
