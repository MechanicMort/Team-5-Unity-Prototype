using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInputController : MonoBehaviour
{
    //private CharacterController controller;
   
    private Rigidbody rb;
    public bool isGrounded = true;
    int layerMask = 1 << 8;

    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float jumpForce = 10f;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    void Update()
    {
        float x = movementInput.x * moveSpeed;
        float y = movementInput.y * moveSpeed;

        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);
        rb.velocity = newMovePos;

        if (jumped && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        CheckForGrounded();


    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.ReadValue<bool>();
        jumped = context.action.triggered;
    }

    void CheckForGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2.5f, layerMask))
        {
            isGrounded = true;
        }
    }






}
