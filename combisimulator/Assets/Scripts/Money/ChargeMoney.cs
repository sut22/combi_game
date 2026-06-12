using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ChargeMoney : MonoBehaviour
{
    
    [SerializeField] private SphereCollider collider;
    [SerializeField] private float detectionRadius;
    public bool canInteract = false;
    private float _waitedtime;
 

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = detectionRadius;
        canInteract = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Passenger"))
        {
            Debug.Log(other.name);
            Passanger passanger = other.GameObject().GetComponent<Passanger>();
            Debug.Log($"VAR2 " + passanger.name + " " + passanger.ChargeState());
            canInteract = passanger.ChargeState();
            _waitedtime = passanger.timePassenger();
        }
    }

    private void Update()
    {
        if (_waitedtime >= 0)
        {
            canInteract = false;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
}
