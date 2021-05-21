using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public LayerMask ground;

    public float moveSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public float groundDistance = 0.25f;

    private new Rigidbody rigidbody;
    private new Camera camera;

    private Vector3 moveDirection;
    private bool sprint;
    private float horizontalRotation;
    private float verticalRotation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = FindObjectOfType<Camera>();

        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }


        if (Input.GetButtonDown("Fire1"))
        {
            LockCursor();
        }

        Vector3 forward = Quaternion.AngleAxis(horizontalRotation, Vector3.up) * Vector3.forward;
        Vector3 right = Quaternion.AngleAxis(horizontalRotation, Vector3.up) * Vector3.right;
        moveDirection = Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right;

        sprint = Input.GetButton("Sprint");

        if (Input.GetButtonDown("Jump") && Physics.CheckSphere(rigidbody.position, groundDistance, ground))
        {
            rigidbody.AddForce(Vector3.up * Mathf.Sqrt(-2.0f * jumpHeight * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            horizontalRotation += Input.GetAxis("Mouse X");
            verticalRotation -= Input.GetAxis("Mouse Y");
            verticalRotation = Mathf.Clamp(verticalRotation, -89.5f, 89.5f);

            camera.transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);
        }
    }

    private void FixedUpdate()
    {
        if (moveDirection.sqrMagnitude > 0)
        {
            float speed = sprint ? 2 * moveSpeed : moveSpeed;
            rigidbody.MovePosition(rigidbody.position + speed * Time.fixedDeltaTime * moveDirection.normalized);
        }
    }
}
