using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreelookCam : MonoBehaviour, ICarObserver
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ragdoll deer = FindObjectOfType<ragdoll>();
        if (deer != null){
            deer.RegisterObserver(this);
        }
    }

    public void onDeerKilled(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
