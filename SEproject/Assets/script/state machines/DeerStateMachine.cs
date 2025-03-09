using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerStateMachine : MonoBehaviour
{
    private IDeerState deerState;

    public Rigidbody rb;

    public bool IsGrounded {get; set;} = true;

    private Animator animator;
    private GameManagerScript gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        deerState = new DeerWalk(this);
        animator = transform.Find("Deer_001").GetComponent<Animator>();
        gameManager = new GameManagerScript();
    }
    public void setState(IDeerState d){
        deerState = d;
    }

    void Update()
    {
        
        animator.SetFloat("speed", rb.velocity.magnitude);
        if (Input.GetKey(KeyCode.W)) deerState.handleForward();
        if (Input.GetKey(KeyCode.A)) deerState.handleLeft();
        if (Input.GetKey(KeyCode.D)) deerState.handleRight();
        if (Input.GetKeyDown(KeyCode.Space)) deerState.handleSpace();
        if (Input.GetKeyDown(KeyCode.LeftShift)) deerState.handleShift();
        // this.transform.rotation = gameManager.camRotation;
        deerState.handleGravity();
        deerState.advanceState();

    }

    void OnCollisionEnter(Collision c){
        IsGrounded = true;
    }
}
