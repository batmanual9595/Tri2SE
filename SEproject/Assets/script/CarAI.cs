using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour, ICarObserver
{
    public Transform deerTarget;
    private float speed = 15f;
    private float turnSpeed = 5f;

    private MeshRenderer[] meshes;

    public ParticleSystem explosion;

    private Rigidbody rb;
    private bool dying;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshes = GetComponentsInChildren<MeshRenderer>();
        ragdoll deer = FindObjectOfType<ragdoll>();
        if (deer != null){
            deer.RegisterObserver(this);
        }
        explosion = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (deerTarget == null) return;
        if (!dying){
            Vector3 directionToDeer = deerTarget.position - transform.position;
            directionToDeer.y = 0;


            Quaternion targetRotation = Quaternion.LookRotation(directionToDeer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);


            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
            rb.AddForce(new Vector3(0, transform.forward.y * -0.01f, 0), ForceMode.Impulse);
        }
        
    }

    public void onDeerKilled(){
        if (!dying) die();
    }

    public void SetTarget(Transform deerTransform)
    {
        deerTarget = deerTransform;
    }

    private void die(){
        explosion.Play();
        dying = true;
        foreach (MeshRenderer meshRenderer in meshes)
        {
            if (meshRenderer != null)
            {
                foreach (Material mat in meshRenderer.materials)
                {
                    mat.color = Color.black;
                }
            }
        }
        Destroy(this.gameObject, explosion.main.duration);
    }

    public void OnCollisionEnter(Collision c){
        if (c.gameObject.CompareTag("car")){
            if (rb.velocity.magnitude > 10f) die();
        }
    }
}