using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreelookCam : MonoBehaviour
{
    // Start is called before the first frame update
    GameManagerScript gameManager;
    private Quaternion camRot;
    private Transform cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager = new GameManagerScript();
        cam = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        camRot = cam.transform.rotation;
        gameManager.camRotation = camRot;
    }
}
