using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputSystemMovement : MonoBehaviour
{
   public Rigidbody rb;
   public float jumpForce = 25f;
    private PlayerInput playerInput;

   private void Awake()
   {
       rb = GetComponent<Rigidbody>();
       playerInput = GetComponent<PlayerInput>();

       
   }

   public void Jump(InputAction.CallbackContext context)
   {
        if (context.performed)
        {
            Debug.Log("Jump" + context.phase);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }     
   }

   
}
