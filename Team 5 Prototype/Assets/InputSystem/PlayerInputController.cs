using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInputController : MonoBehaviour
{
    //private CharacterController controller;
    public Camera camera;
    public Transform player;

    private Rigidbody rb;
    public bool isGrounded = true;
    int layerMask = 1 << 8;

    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float jumpForce = 10f;

    private Vector2 movementInput = Vector2.zero;

    private Vector2 cameraInput = Vector2.zero;
    private float mX, mY;
    public float mouseSen = 1f;

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

        

        CheckForGrounded();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            Debug.Log("Jumped");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }

    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();

        mX += cameraInput.x * mouseSen;
        mY -= cameraInput.y * mouseSen;
        mY = Mathf.Clamp(mY, -80, 80);
        camera.transform.rotation = Quaternion.Euler(mY, mX, 0);
        player.transform.rotation = Quaternion.Euler(0, mX, 0);



    }


    private void CheckForGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2.5f, layerMask))
        {
            isGrounded = true;
        }
    }






}
