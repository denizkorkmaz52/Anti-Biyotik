using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    float turnSmoothVelocity;
    private Vector3 jumpVelocity;

    // Update is called once per frame
    void Update()
    {
        //Take inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Movement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            charController.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        //Jumping and falling
        jumpVelocity.y += gravity * Time.deltaTime;
        if (charController.isGrounded && jumpVelocity.y < 0)
        {
            jumpVelocity.y = -2;
        }

        if (Input.GetButtonDown("Jump") && charController.isGrounded)
        {
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        charController.Move(jumpVelocity * Time.deltaTime);
    }
}
