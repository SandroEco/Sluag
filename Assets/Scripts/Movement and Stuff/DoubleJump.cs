using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public Movement movementScript;

    public bool doubleJumping;

    void Start()
    {
        movementScript = GetComponent<Movement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(movementScript.isGrounded || doubleJumping)
            {
                movementScript.Jump();

                doubleJumping = !doubleJumping;
            }
        }

        if(movementScript.isGrounded && !Input.GetButton("Jump"))
        {
            doubleJumping = false;
        }

    }
}
