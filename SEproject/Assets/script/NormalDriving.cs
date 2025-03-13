using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDriving : MonoBehaviour, IDrivingStrat
{
    public virtual void swerve()
    {
        // dont swerve
    }
    public virtual void drive(Rigidbody rb, float speed, Vector3 forward)
    {
        // rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        // rb.AddForce(new Vector3(0, transform.forward.y * -0.01f, 0), ForceMode.Impulse);
    }
    public virtual void brake() { }
}
