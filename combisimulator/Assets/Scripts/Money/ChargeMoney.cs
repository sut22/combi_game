using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ChargeMoney : MonoBehaviour
{
    
    [SerializeField] private SphereCollider collider;
    [SerializeField] private float detectionRadius;
    
    public bool CanCharge;
 

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = detectionRadius;
        CanCharge = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Passenger"))
        {
            CanCharge = true;
            Debug.Log($"VAR2 {CanCharge}");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
