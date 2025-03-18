using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    private Vector2 curMoveInput;
    [SerializeField] private PlayerCondition speed;



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

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speed = GetComponentInParent<PlayerCondition>();
    }


    // ACT
    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
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
            SoundManager.instance.PlaySFX(SFX.JUMP);
            AchievementManager.instance.Achieve(1);
        }
    }

    private bool IsGrounded()
    {
        // BoxCast?

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

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void OnQInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SoundManager.instance.PlaySFX(SFX.SLIDE);

        }
        if (context.phase == InputActionPhase.Canceled)
        {
            SoundManager.instance.StopSFX();
        }
    }
}
