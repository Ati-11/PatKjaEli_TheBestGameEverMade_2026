using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Alteruna;



public class playerController : MonoBehaviour
{
    public float horizontalInput = 1f;
    public float verticalInput = 1f;
    public float speed = 4f;
    public float jumpForce = 10f;
    public float runSpeed = 6f;
    private bool touchingGround;
    private Vector3 MoveDirection;

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public Transform playerCamera;

    private Rigidbody Rigid;
    private Animator GnomePoly_Animator;
    public Alteruna.Avatar Avatar; //SERVER SHENANIGANS



    void Start()
    {
        // SERVER SHENANIGANS 
        Avatar = GetComponent<Alteruna.Avatar>();

        if (!Avatar.IsMe)
        {
            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(false);
            }

            Rigid.isKinematic = true; 
            return;
        }
        // SERVER SHENANIGANS 



        Rigid = GetComponent<Rigidbody>();
        GnomePoly_Animator = GetComponent<Animator>();
        Rigid.interpolation = RigidbodyInterpolation.Interpolate; //Camera movement smoothing 
        Rigid.freezeRotation = true; //Stops player rotation from physics 
        Cursor.lockState = CursorLockMode.Locked;
    }



    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = true;
        }
    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = false;
        }
    }



    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        MoveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetButtonDown("Jump") && (touchingGround))
        {
            Rigid.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }

        else
        {
            speed = 5f;
        }

        GnomePoly_Animator.SetFloat("Speed", MoveDirection.magnitude * speed);
    }

    void LateUpdate()

    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(0f, mouseX, 0f));
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }



    void FixedUpdate()

    {
        Vector3 move = new Vector3(horizontalInput, 0f, verticalInput);
        move = transform.TransformDirection(move);

        Vector3 velocity = move * speed;
        Rigid.linearVelocity = new Vector3(velocity.x, Rigid.linearVelocity.y, velocity.z);
    }

}

