using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    

    [SerializeField]
    Behaviour[] componentsToDisable; // for disabling playerController etc

    Camera sceneCamera;

	void Start () {
        if (isLocalPlayer)
        {
            sceneCamera = GameObject.FindGameObjectWithTag("SceneCamera").GetComponent<Camera>() ;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
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
