using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript
{
    FreelookCam cam;

    public Quaternion camRotation{ get; set;}

    // Start is called before the first frame update
    void Start()
    {
        cam = new FreelookCam();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(camRotation);
    }
}
