using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 12f;
    public float jumpForce = 10f;
    public bool playerOnGround = true;
    public GameObject origin;

    int layerMask = 1 << 8;

    private void Start()
    {
        Debug.Log(LayerMask.LayerToName(layerMask));
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        Crouching();
        CheckForGrounded();
        OnDrawGizmos();

    }

    private void FixedUpdate()
    {       
        Jumping();
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) { moveSpeed = 24f;}
        else{ moveSpeed = 12f;}

        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed;

        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);
        rb.velocity = newMovePos;
    }

    void Jumping()
    {
        if (Input.GetKey(KeyCode.Space) && playerOnGround)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            playerOnGround = false;
        }
    }

    void Crouching()
    {

    }

    void CheckForGrounded()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(origin.transform.position, 1f);
    }
}
