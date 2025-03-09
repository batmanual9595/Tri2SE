using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerWalk : IDeerState
{
    protected DeerStateMachine deer;
    private Rigidbody rb;

    private float speed = 2f;
    private float jumpForce = 5f;
    private float maxSpeed = 2f;
    private float rotationSpeed = 100f;
    
    public DeerWalk(DeerStateMachine deer){
        this.deer = deer;
        rb = deer.rb;
        rb.useGravity = true;
    }

    public void handleGravity(){
        // if (!deer.IsGrounded){
        //     rb.AddForce(deer.transform.up * -5, ForceMode.Acceleration);
        // }
    }
    public void handleForward(){
        rb.AddForce(deer.transform.forward * speed, ForceMode.Impulse);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    public void handleLeft(){
        deer.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }
    public void handleRight(){
        deer.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    public void handleSpace(){
        deer.setState(new DeerJump(deer, speed));
    }
    public void handleShift(){
        deer.setState(new DeerSprint(deer));
    }
    public void advanceState(){

    }
}
