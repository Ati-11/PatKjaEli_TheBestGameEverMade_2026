using Alteruna;
using UnityEngine;
using UnityEngine.EventSystems;

public class MUPlayerController : AttributesSync
{

    [Header("player settings")]
    

    private RigidbodySynchronizable body;
    private Rigidbody unitybody;

    public Alteruna.Avatar avatar;

    //public GameObject connectScreen;

    public Material[] mats = new Material[3];

    public Animator GnomePoly_Animator;   //set from child geom

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


    public bool isInDoorVolume = false;
    public Alteruna.AnimationSynchronizable whichDoor;

    private void Awake()
    {
        body = GetComponent<RigidbodySynchronizable>();
        avatar = GetComponent<Alteruna.Avatar>();
        unitybody = GetComponent<Rigidbody>();

        //put the animator on the thing animated, this could have been the issue
        GnomePoly_Animator = transform.GetChild(0).GetComponent<Animator>();

        /*
        connectScreen = GameObject.FindGameObjectWithTag("start");
        Debug.Log(connectScreen.name);
        */
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!avatar.IsMe)
        {

            
            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(false);
            }

            unitybody.isKinematic = true;
            return;
           
        }

        unitybody.interpolation = RigidbodyInterpolation.Interpolate; //Camera movement smoothing 
        unitybody.freezeRotation = true; //Stops player rotation from physics 
        Cursor.lockState = CursorLockMode.None;

        if (!avatar.IsMe) return;
        {
            //pick a random number for a color FIX THIS WITH: Renderer r = GetComponentInChildren<Renderer>();
            Renderer r = GetComponent<Renderer>();
            int c = Random.Range(0, 3);
            r.material = mats[c];
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!avatar.IsMe)
        {           
            return;
        }

        if(isInDoorVolume)
        {
            Debug.Log("IS IN DOOR VOLUME");
            
            if(Input.GetKey(KeyCode.E))
            {
                Debug.Log("pRESSED E");
                whichDoor.SetBool("Open Right", true);
                whichDoor.SetBool("Open Left", true);
            }

        }


        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        MoveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetButtonDown("Jump") && (touchingGround))
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

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



    void LateUpdate()

    {
        if (!avatar.IsMe) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        body.MoveRotation(body.rotation * Quaternion.Euler(0f, mouseX, 0f));
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }



    void FixedUpdate()

    {
        if (!avatar.IsMe) return;

        Vector3 move = new Vector3(horizontalInput, 0f, verticalInput);
        move = transform.TransformDirection(move);

        Vector3 velocity = move * speed;
        body.velocity = new Vector3(velocity.x, body.velocity.y, velocity.z);

        
    }




}
