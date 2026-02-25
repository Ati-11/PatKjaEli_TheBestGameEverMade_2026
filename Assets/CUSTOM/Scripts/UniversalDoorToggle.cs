using UnityEngine;

public class UniversalDoorToggle : MonoBehaviour
{
    private Animator animator;
    private string openBool; // Will be set automatically
    private Transform player;

    [Header("Player Settings")]
    public float interactDistance = 3f; // Max distance to interact

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator found on this door!");
            return;
        }

        // Auto-find the first bool parameter
        foreach (var param in animator.parameters)
        {
            if (param.type == UnityEngine.AnimatorControllerParameterType.Bool)
            {
                openBool = param.name;
                break;
            }
        }

        if (string.IsNullOrEmpty(openBool))
        {
            Debug.LogError("No bool parameter found in Animator for door toggle!");
        }

        // Auto-find player by tag
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("Player GameObject with tag 'Player' not found.");
    }

    void Update()
    {
        if (player == null || string.IsNullOrEmpty(openBool))
        {
            Debug.Log("Player = " + player);
            Debug.Log("OpenBool = " + openBool);
            return;
        }
           

        // Only interact if player is close enough
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > interactDistance) return;

        // Press E to toggle door
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool isOpen = animator.GetBool(openBool);
            animator.SetBool(openBool, !isOpen);
        }
    }
}