using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    [SerializeField] private float velocidad = 5.0f;

    void Awake()
    {
        
    }

    // Start se llama en el primer fotograma antes de que Update se ejecute
    void Start()
    {
        
    }

    private void Movement(InputAction.CallbackContext context)
    {
        // do stuff
    }

    private void Interaction(InputAction.CallbackContext context)
    {

    }
}