using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuManager : MonoBehaviour {

    public string currentName;
    public InputField nameIn;
    public InputField IpIn;
    public NewNetworkManager net;


	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void tryToHost()
    {
        currentName = nameIn.text;
        if (currentName != "")
        {
            net.networkPort = 7777;
            net.StartHost();
            transform.position = new Vector3(10000,0,0);
        }
        
    }

    public void tryToJoin()
    {
        currentName = nameIn.text;
        net.networkAddress = IpIn.text;
        if (currentName != "" && IpIn.text != "")
        {
            net.networkPort = 7777;
            net.StartClient();
            transform.position = new Vector3(10000, 0, 0);
        }
    }
}
