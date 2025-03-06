using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour
{

    [Header("Move")]
    public float moveSpeed;
    private Vector2 curMoveInput;


    [Header("Jump")]
    public float jumpPower;
    public LayerMask groundLayer;

    [Header("Look")]
    private Vector2 mouseDelta;
    public Transform cameraContainer;
    public float minX;
    public float maxX;
    private float curXRot;
    public float lookSensitivity;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ACT
    private void FixedUpdate()
    {
        Move();   
    }

    private void LateUpdate()
    {
        Look();
    }

    // MOVE
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;    // πÊ«‚
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    // JUMP
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    private bool IsGrounded ()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 0.1f, groundLayer))
            return true;

        return false;
    }


    // LOOK
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void Look()
    {
        curXRot += mouseDelta.y * lookSensitivity;
        curXRot = Mathf.Clamp(curXRot, minX, maxX);
        cameraContainer.localEulerAngles = new Vector3(-curXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
}
