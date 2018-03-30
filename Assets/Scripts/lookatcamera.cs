using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Transform temp = Camera.main.transform;
        transform.LookAt(temp);
	}
}
