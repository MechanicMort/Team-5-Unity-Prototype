using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemTest : MonoBehaviour
{
    public Rigidbody rb;
    private PlayerInput playerInput;
    public float speed = 10f;
    private PlayerInputActions playerInputActions;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        //playerInputActions.Enable();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Movement.performed += Movement_performed;
    }

    private void Update()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }
    private void Movement_performed(InputAction.CallbackContext context)
    {
     //   Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }    

    private void Jump(InputAction.CallbackContext context)
    {
      //  Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Jump" + context.phase);
            rb.AddForce(Vector3.up * 50f, ForceMode.Impulse);
        }
        
    }
}

