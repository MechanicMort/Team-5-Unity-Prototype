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
    public float jumpForce = 5f;
    public float jetPack = 15f;
    public float sens = 0.0005f;
    private float mX;
    private float mY;

    Vector3 velo;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Movement.performed += Movement_performed;
        playerInputActions.Player.Look.performed += Look_performed;
        playerInputActions.Player.JetPack.performed += JetPack_performed;
    }

    private void JetPack_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("JET PACK ZOOOOM");
        if (obj.performed)
        {
            rb.AddForce(new Vector3(0, jetPack, 0), ForceMode.Impulse);
        }
       

    }

    private void Look_performed(InputAction.CallbackContext context)
    {
       
    }

    private void Update()
    {
        //Movement
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movePos = transform.right * inputVector.x + transform.forward * inputVector.y;
        Vector3 newMovePos = new Vector3(movePos.x,0, movePos.z) * speed;
        rb.velocity = newMovePos;             

        //Camera
        Vector2 camInputVector = playerInputActions.Player.Look.ReadValue<Vector2>();
        mX += camInputVector.x;
        mY -= camInputVector.y;
        mY = Mathf.Clamp(mY, -80, 80);
        camera.transform.localRotation = Quaternion.Euler(mY, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, mX, 0);

    }
    private void Movement_performed(InputAction.CallbackContext context)
    {
        
    }    

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Jump" + context.phase);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        
    }
}

