
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 _moveInput;
    private Rigidbody _rb;
    private ColletiveChargeUI _colletiveChargeUI;
    private ChargeMoney _money;


    private void Awake()
    {
        _colletiveChargeUI = GameObject.Find("ColletiveChargeUI").GetComponent<ColletiveChargeUI>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _money = GetComponent<ChargeMoney>();
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        _moveInput = context.ReadValue<Vector2>();
        //Debug.Log("VAR" + moveInput.x  + moveInput.y);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && _money.CanCharge == true)
        {
            Debug.Log("interactua");
            _colletiveChargeUI.ActiveUI(true);
        }
        
    }

    private void Update()
    {
        Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y);
        _rb.MovePosition(_rb.position + movement * (speed * Time.deltaTime));
    }
}
