using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreelookCam : MonoBehaviour
{
    // Start is called before the first frame updat

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
