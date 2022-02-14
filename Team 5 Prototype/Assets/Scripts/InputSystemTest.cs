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
    public Transform player;
    public Transform camera; 
    public float sens = 1f;
    private float x;
    private float y;



    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Movement.performed += Movement_performed;
        playerInputActions.Player.Look.performed += Look_performed;
    }

    private void Look_performed(InputAction.CallbackContext context)
    {
        //Vector2 inputVector = playerInputActions.Player.Look.ReadValue<Vector2>();

        //x += inputVector.y;
        //y += inputVector.x;
        //x = Mathf.Clamp(x, -90, 90);

        //camera.transform.localRotation = Quaternion.Euler(x, 0, 0);
        //transform.localRotation = Quaternion.Euler(0, y, 0);


    }

    private void Update()
    {
        //Movement
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movePos = transform.right * inputVector.x + transform.forward * inputVector.y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z) * speed;
        rb.velocity = newMovePos;

        //Camera
        Vector2 caminputVector = playerInputActions.Player.Look.ReadValue<Vector2>();
        x += caminputVector.y * sens;
        y += caminputVector.x * sens;
        x = Mathf.Clamp(x, -90, 90);
        camera.transform.localRotation = Quaternion.Euler(x, 0, 0);
        transform.localRotation = Quaternion.Euler(0, y, 0);

        

    }
    private void Movement_performed(InputAction.CallbackContext context)
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movePos = transform.right * inputVector.x + transform.forward * inputVector.y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z) * speed;
        rb.velocity = newMovePos;
    }    

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Jump" + context.phase);
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        
    }
}

