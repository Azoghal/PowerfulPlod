using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxspawnmover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(Mathf.Sin(Time.time)/10,0,Mathf.Cos(Time.time)/10));
	}
}
