using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    //public float moveSpeed;
    private Vector2 curMoveInput;
    [SerializeField] private PlayerCondition speed;
    //private Condition speed;


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
        speed = GetComponentInParent<PlayerCondition>();
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //moveSpeed = speed.curValue;    
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
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;    // 방향
        dir *= speed.uiCondition.speed.curValue;
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

    private bool IsGrounded()
    {
        // boxCast 이용해서 리팩토링 해보기

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayer))
            {
                return true;
            }
        }

        return false;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //}


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
