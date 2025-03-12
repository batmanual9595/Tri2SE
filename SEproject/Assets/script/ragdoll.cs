using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll : MonoBehaviour
{
    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    private Rigidbody rb;
    private Animator animator;
    private new Collider collider;

    public GameObject ragdollRootObject;

    private DeerStateMachine deerController;

    private List<ICarObserver> observers = new List<ICarObserver>();
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = transform.Find("Capsule").GetComponent<CapsuleCollider>();
        animator = transform.Find("Deer_001").GetComponent<Animator>();
        colliders = ragdollRootObject.GetComponentsInChildren<Collider>();
        rigidbodies = ragdollRootObject.GetComponentsInChildren<Rigidbody>();
        deerController = GetComponent<DeerStateMachine>();

        setRagdoll(false, null, null);
    }

    public void setRagdoll(bool state, Rigidbody carb, Collision co)
    {
        rb.isKinematic = state;
        collider.enabled = !state;
        animator.enabled = !state;
        deerController.enabled = !state;

        foreach (Collider c in colliders)
        {
            c.enabled = state;
        }

        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = !state;
            if (co != null) r.AddForce(new Vector3(co.gameObject.transform.forward.x, 1f, co.gameObject.transform.forward.z) * carb.velocity.magnitude * 2f, ForceMode.Impulse);
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("car"))
        {
            Rigidbody carb = c.gameObject.GetComponent<Rigidbody>();
            NotifyDeerKilled();
            setRagdoll(true, carb, c);
        }
    }

    public void RegisterObserver(ICarObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(ICarObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }

    public void NotifyDeerKilled()
    {
        foreach (ICarObserver observer in observers)
        {
            observer.onDeerKilled();
        }
    }
}
