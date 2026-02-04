using UnityEngine;

public class playerController : MonoBehaviour
{
    public float horizontalInput = 1f;
    public float verticalInput = 1f;
    public float speed = 5f;

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public Transform playerCamera;

    private Rigidbody Rigid;

    // -------------------------------------------------- 
    private Alteruna.Avatar Avatar; 
    // -------------------------------------------------- 

    void Start()
    {
        // -------------------------------------------------- 
        Avatar = GetComponent<Alteruna.Avatar>();
        if (!Avatar.IsMe)
        {
            Destroy(this);
            return;
        }
        // -------------------------------------------------- 

        Rigid = GetComponent<Rigidbody>(); 
        Rigid.freezeRotation = true; 
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
      
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX); // player rotate (yaw) 

        // camera (pitch) 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

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
