
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 _moveInput;
    private Rigidbody _rb;
    private ColletiveChargeUI _colletiveChargeUI;
    private ChargeMoney _money;
    private bool _canMove = true;


    private void Awake()
    {
        _colletiveChargeUI = GameObject.Find("ColletiveChargeUI").GetComponent<ColletiveChargeUI>();
        _rb = GetComponent<Rigidbody>();
        _canMove = true;
    }

    private void Start()
    {
        _money = GetComponent<ChargeMoney>();
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        _moveInput = context.ReadValue<Vector2>();
        // Debug.Log("VAR" + _moveInput.x  + _moveInput.y);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && _money.canInteract)
        {
            Debug.Log("interactua");
            _colletiveChargeUI.ActiveUI(true);
        }
        
    }

    private void Update()
    {
        
        if (!_canMove)  return;
        
        Vector3 movement = new Vector3(_moveInput.y, 0, -_moveInput.x);
        _rb.MovePosition(_rb.position + movement * (speed * Time.deltaTime));
    }
}
