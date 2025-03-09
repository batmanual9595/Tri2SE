using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerStateMachine : MonoBehaviour
{
    private IDeerState deerState;

    public Rigidbody rb;

    public Transform t;

    public bool IsGrounded {get; set;} = true;

    private Animator animator;
    private GameManagerScript gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        deerState = new DeerWalk(this);
        animator = transform.Find("Deer_001").GetComponent<Animator>();
        gameManager = new GameManagerScript();
    }
    public void setState(IDeerState d){
        deerState = d;
    }

    void Update()
    {
        Debug.Log(rb.velocity.magnitude);
        animator.SetFloat("speed", rb.velocity.magnitude);
        if (Input.GetKey(KeyCode.W)) deerState.handleForward();
        if (Input.GetKey(KeyCode.A)) deerState.handleLeft();
        if (Input.GetKey(KeyCode.D)) deerState.handleRight();
        if (Input.GetKey(KeyCode.S)) deerState.handleBack();
        if (Input.GetKey(KeyCode.Space)) deerState.handleSpace();
        if (Input.GetKey(KeyCode.LeftShift)){
           deerState.handleShift(); 
           animator.SetBool("shift", true);
        } 
        else{
            animator.SetBool("shift", false);
        }
        // this.transform.rotation = gameManager.camRotation;
        deerState.handleGravity();
        deerState.advanceState();

    }

    void OnCollisionEnter(Collision c){
        IsGrounded = true;
    }
}
