using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("VAR" + moveInput.x  + moveInput.y);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("interactua");   
        }
        
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        rb.MovePosition(rb.position + movement * (speed * Time.deltaTime));
    }
}
