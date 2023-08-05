using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class playerJump : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private InputActionReference runActionReference;
    [SerializeField] private InputActionReference rgrabActionReference;
    [SerializeField] private InputActionReference lgrabActionReference;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float jumpForce = 500.0f;
    public Vector3 jumpvelocity;
    public float dashCooldown;
    WaitForSeconds dashcoolamt;
    public ActionBasedContinuousMoveProvider moveprovref;



    //private bool IsGrounded => Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 2.0f), Vector3.down, 2.0f);

    public bool grounded;

    private void Awake()
    {
        dashcoolamt = new WaitForSeconds(0.5f);

        if (!characterController)
            characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
       // jumpActionReference.action.performed += OnJump;
    }

    private void Update()
    {
        // grounded = IsGrounded;
        if (jumpActionReference.action.IsPressed() == true)
        {
            //jumpvelocity.y = 10f;
            characterController.Move(jumpvelocity * Time.deltaTime);

            // jetpack mode characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
        }

        if(runActionReference.action.IsPressed() ==true)
        {
            moveprovref.moveSpeed = 25f;
            StartCoroutine(DashTimer());
        }

        if(rgrabActionReference.action.IsPressed()==true || lgrabActionReference.action.IsPressed() == true)
        {
            moveprovref.moveSpeed = 1f;
        }
        else
        {
            moveprovref.moveSpeed = 2.5f;
        }
    }

  /*  private void OnJump(InputAction.CallbackContext obj)
    {
        if (!grounded) return;
        characterController.Move(Vector3.up * jumpForce*Time.deltaTime);
        characterController.Move(Vector3.forward * (jumpForce/2)*Time.deltaTime);



    }
  */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            grounded = false;
        }
    }

    IEnumerator DashTimer()
    {
        yield return dashcoolamt;
        moveprovref.moveSpeed = 2.5f;
    }
}