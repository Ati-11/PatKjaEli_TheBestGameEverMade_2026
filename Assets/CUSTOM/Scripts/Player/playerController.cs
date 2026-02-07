using UnityEngine;

public class playerController : MonoBehaviour
{
    public float horizontalInput = 1f;
    public float verticalInput = 1f;
    public float speed = 5f;
    public float jumpForce = 5f;
    public float runSpeed = 50f;
    private bool touchingGround;

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public Transform playerCamera;

    private Rigidbody Rigid;


    private Alteruna.Avatar Avatar; //SERVER SHENANIGANS

    void Start()
    {
        // SERVER SHENANIGANS
        Avatar = GetComponent<Alteruna.Avatar>();
        if (!Avatar.IsMe)
        {
            Destroy(this);
            return;
        }
        // SERVER SHENANIGANS

        Rigid.interpolation = RigidbodyInterpolation.Interpolate; //Camera movement interpolation 
        Rigid = GetComponent<Rigidbody>(); 
        Rigid.freezeRotation = true; //Stops player rotation from physics 
        Cursor.lockState = CursorLockMode.Locked; 
    }



    private void OnCollisionEnter(Collision collision) 
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
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX); 
        xRotation -= mouseY; 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 



        if (Input.GetButtonDown("Jump") && (touchingGround))
        {
            Rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = 5f;
            Debug.Log("Running");
        }

    }
    void FixedUpdate()
    {
        //
        Vector3 move = new Vector3(horizontalInput, 0f, verticalInput);
        move = transform.TransformDirection(move);

        Vector3 velocity = move * speed;
        Rigid.linearVelocity = new Vector3(velocity.x, Rigid.linearVelocity.y, velocity.z);
        //
    }
}
