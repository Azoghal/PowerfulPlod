using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    

    [SerializeField]
    Behaviour[] componentsToDisable; // for disabling playerController etc

    Camera sceneCamera;

	void Start () {
        if (isLocalPlayer)
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

        }
        else
        {
            GetComponent<Camera>().enabled = false;
        }
	}

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
